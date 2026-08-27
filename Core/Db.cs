using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace SchoolSchedule.Core
{
    /// <summary>
    /// Всё общение с MySQL. Соединения короткие и берутся из пула: на экране
    /// программа работает сутками, а держать одно соединение открытым сутками —
    /// верный способ однажды упереться в «server has gone away».
    /// </summary>
    public static class Db
    {
        private static readonly object Sync = new object();
        private static string _connectionString;

        /// <summary>Последняя ошибка — её показывает полоска состояния на экране.</summary>
        public static string LastError { get; private set; }

        public static DateTime LastErrorAt { get; private set; }
        public static DateTime LastSuccessAt { get; private set; }
        public static bool IsOnline { get; private set; }

        public static string ConnectionString
        {
            get
            {
                lock (Sync)
                {
                    if (_connectionString == null) _connectionString = Build();
                    return _connectionString;
                }
            }
        }

        /// <summary>
        /// Перечитать ip.txt и собрать строку подключения заново.
        /// Старый пул закрываем — иначе программа продолжит ходить на прежний
        /// адрес, и правка файла останется без последствий.
        /// </summary>
        public static void Reconfigure()
        {
            lock (Sync)
            {
                var fresh = Build();
                if (fresh == _connectionString) return;

                try { MySqlConnection.ClearAllPools(); } catch { }
                _connectionString = fresh;
                IsOnline = false;
            }
        }

        private static string Build()
        {
            var builder = new MySqlConnectionStringBuilder();
            builder.Server = AppConfig.Server;
            builder.Port = (uint)Math.Max(1, AppConfig.Port);
            builder.UserID = AppConfig.User;
            builder.Password = AppConfig.Password;
            builder.Database = AppConfig.Database;
            builder.CharacterSet = "utf8mb4";
            builder.ConnectionTimeout = 6;          // экран не должен «висеть», если сервер выключен
            builder.DefaultCommandTimeout = 20;
            builder.AllowPublicKeyRetrieval = true; // MySQL 8 с caching_sha2_password
            builder.Pooling = true;
            builder.MinimumPoolSize = 0;
            builder.MaximumPoolSize = 10;

            var ssl = AppConfig.Get(null, "sslmode", "ssl");
            if (ssl != null)
            {
                switch (ssl.Trim().ToLowerInvariant())
                {
                    case "none": case "disabled": case "0": builder.SslMode = MySqlSslMode.Disabled; break;
                    case "required": case "1": builder.SslMode = MySqlSslMode.Required; break;
                }
            }

            return builder.ConnectionString;
        }

        /// <summary>Строка подключения без пароля — её не стыдно показать в окне ошибки.</summary>
        public static string SafeDescription()
        {
            return AppConfig.User + "@" + AppConfig.Server + ":" + AppConfig.Port + "/" + AppConfig.Database;
        }

        private static MySqlConnection Open()
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        private static void Ok()
        {
            IsOnline = true;
            LastSuccessAt = DateTime.Now;
            LastError = null;
        }

        private static void Fail(Exception ex)
        {
            IsOnline = false;
            LastError = Explain(ex);
            LastErrorAt = DateTime.Now;
        }

        /// <summary>
        /// Человеческий текст вместо стека исключения: на телевизоре это увидят
        /// не программисты, и подсказка должна говорить, что делать.
        /// </summary>
        public static string Explain(Exception ex)
        {
            return Explain(ex, AppConfig.Server, AppConfig.Port, AppConfig.User, AppConfig.Database);
        }

        private static string Explain(Exception ex, string server, int port, string user, string database)
        {
            var mysql = ex as MySqlException;
            if (mysql != null)
            {
                switch (mysql.Number)
                {
                    case 0:
                    case 1042:
                        return "Сервер " + server + ":" + port + " не отвечает. "
                             + "Проверьте, что MAMP запущен и компьютер в той же сети.";
                    case 1045:
                        return "Сервер не принял логин или пароль (" + user + "). Проверьте ip.txt.";
                    case 1049:
                        return "На сервере нет базы «" + database + "». "
                             + "Создайте её в phpMyAdmin и загрузите sql/school_schedule.sql.";
                    case 1044:
                        return "Пользователю " + user + " закрыт доступ к базе «" + database + "».";
                    case 1062:
                        return "Такая запись уже есть — например, класс с этим названием.";
                    case 1406:
                        return "Слишком длинный текст для этого поля.";
                    case 1130:
                        return "Сервер не разрешает подключаться с этого компьютера. "
                             + "В MAMP нужно разрешить доступ по сети пользователю " + user + ".";
                }
            }
            return ex.Message;
        }

        // --- Запросы ------------------------------------------------------
        //
        // Значения всегда идут параметрами @p0, @p1 … — конкатенации строк в
        // SQL здесь нет ни одной. В расписании попадаются фамилии с кавычками,
        // да и подставлять пользовательский текст в запрос просто нельзя.

        private static void Bind(MySqlCommand command, object[] values)
        {
            if (values == null) return;
            for (int i = 0; i < values.Length; i++)
                command.Parameters.AddWithValue("@p" + i, values[i] ?? DBNull.Value);
        }

        public static int Exec(string sql, params object[] values)
        {
            try
            {
                using (var connection = Open())
                using (var command = new MySqlCommand(sql, connection))
                {
                    Bind(command, values);
                    var affected = command.ExecuteNonQuery();
                    Ok();
                    return affected;
                }
            }
            catch (Exception ex) { Fail(ex); throw; }
        }

        /// <summary>Запрос вместе со значениями — из таких собирается транзакция.</summary>
        public sealed class Statement
        {
            public string Sql;
            public object[] Args;
        }

        public static Statement S(string sql, params object[] values)
        {
            return new Statement { Sql = sql, Args = values };
        }

        /// <summary>
        /// Несколько запросов одной транзакцией — например, копирование сетки.
        /// Либо переносится всё, либо ничего: расписание, скопированное
        /// наполовину, хуже нескопированного.
        /// </summary>
        public static void Batch(IEnumerable<Statement> statements)
        {
            try
            {
                using (var connection = Open())
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var statement in statements)
                    {
                        if (statement == null || string.IsNullOrWhiteSpace(statement.Sql)) continue;
                        using (var command = new MySqlCommand(statement.Sql, connection, transaction))
                        {
                            Bind(command, statement.Args);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    Ok();
                }
            }
            catch (Exception ex) { Fail(ex); throw; }
        }

        public static object Scalar(string sql, params object[] values)
        {
            try
            {
                using (var connection = Open())
                using (var command = new MySqlCommand(sql, connection))
                {
                    Bind(command, values);
                    var result = command.ExecuteScalar();
                    Ok();
                    return result == DBNull.Value ? null : result;
                }
            }
            catch (Exception ex) { Fail(ex); throw; }
        }

        public static List<T> Query<T>(string sql, Func<IDataRecord, T> read, params object[] values)
        {
            var rows = new List<T>();
            try
            {
                using (var connection = Open())
                using (var command = new MySqlCommand(sql, connection))
                {
                    Bind(command, values);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) rows.Add(read(reader));
                    }
                    Ok();
                    return rows;
                }
            }
            catch (Exception ex) { Fail(ex); throw; }
        }

        // --- Чтение полей без сюрпризов -----------------------------------

        public static string Str(IDataRecord row, int index)
        {
            return row.IsDBNull(index) ? null : row.GetString(index);
        }

        public static int Int(IDataRecord row, int index)
        {
            return row.IsDBNull(index) ? 0 : Convert.ToInt32(row.GetValue(index));
        }

        public static int? IntOrNull(IDataRecord row, int index)
        {
            return row.IsDBNull(index) ? (int?)null : Convert.ToInt32(row.GetValue(index));
        }

        public static bool Bool(IDataRecord row, int index)
        {
            return !row.IsDBNull(index) && Convert.ToInt32(row.GetValue(index)) != 0;
        }

        public static DateTime Date(IDataRecord row, int index)
        {
            return row.IsDBNull(index) ? DateTime.MinValue : Convert.ToDateTime(row.GetValue(index));
        }

        public static TimeSpan Time(IDataRecord row, int index)
        {
            if (row.IsDBNull(index)) return TimeSpan.Zero;

            var value = row.GetValue(index);
            if (value is TimeSpan) return (TimeSpan)value;
            if (value is DateTime) return ((DateTime)value).TimeOfDay;

            TimeSpan parsed;
            return TimeSpan.TryParse(Convert.ToString(value), out parsed) ? parsed : TimeSpan.Zero;
        }

        /// <summary>
        /// Проверить произвольный адрес, не трогая ip.txt и текущее подключение.
        /// Так кнопка «Проверить» отвечает ровно на тот вопрос, который задан
        /// в полях, и ничего за собой не оставляет, если человек передумает.
        /// </summary>
        public static bool TestConnection(string server, int port, string user, string password,
                                          string database, out string message)
        {
            try
            {
                var builder = new MySqlConnectionStringBuilder();
                builder.Server = server;
                builder.Port = (uint)Math.Max(1, port);
                builder.UserID = user;
                builder.Password = password;
                builder.Database = database;
                builder.CharacterSet = "utf8mb4";
                builder.ConnectionTimeout = 6;
                builder.AllowPublicKeyRetrieval = true;
                builder.Pooling = false;   // разовая проверка, пул тут ни к чему

                using (var connection = new MySqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SELECT VERSION()", connection))
                    {
                        var version = Convert.ToString(command.ExecuteScalar());
                        message = "Подключение есть. MySQL " + version +
                                  " (" + user + "@" + server + ":" + port + "/" + database + ")";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = Explain(ex, server, port, user, database);
                return false;
            }
        }

        /// <summary>Проверка текущего подключения: версия сервера или текст ошибки.</summary>
        public static bool Test(out string message)
        {
            try
            {
                using (var connection = Open())
                using (var command = new MySqlCommand("SELECT VERSION()", connection))
                {
                    var version = Convert.ToString(command.ExecuteScalar());
                    Ok();
                    message = "Подключение есть. MySQL " + version + " (" + SafeDescription() + ")";
                    return true;
                }
            }
            catch (Exception ex)
            {
                Fail(ex);
                message = Explain(ex);
                return false;
            }
        }
    }
}
