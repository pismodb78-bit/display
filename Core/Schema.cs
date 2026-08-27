using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SchoolSchedule.Core
{
    /// <summary>
    /// Таблицы создаются самой программой при первом запуске. Тот же набор
    /// лежит в sql/school_schedule.sql — если у пользователя базы нет прав на
    /// CREATE, файл загружают в phpMyAdmin руками, и программа находит всё
    /// готовым.
    /// </summary>
    public static class Schema
    {
        /// <summary>Таблицы, без которых программа работать не может.</summary>
        public static readonly string[] Tables =
        {
            "classes", "lesson_times", "schedule", "calendar_days", "settings"
        };

        public static bool Ensure(out string error)
        {
            error = null;
            try
            {
                Db.Batch(Statements());
                return true;
            }
            catch (MySqlException ex) when (ex.Number == 1049)
            {
                // Базы ещё нет — создаём и повторяем. Раньше попытка создания
                // шла всегда, и на выключенном сервере программа ждала два
                // таймаута подряд вместо одного: экран заметно подвисал.
                if (!TryCreateDatabase())
                {
                    error = Db.Explain(ex);
                    return false;
                }

                try
                {
                    Db.Batch(Statements());
                    return true;
                }
                catch (Exception retry)
                {
                    error = Db.Explain(retry);
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = Db.Explain(ex);
                return false;
            }
        }

        /// <summary>
        /// Создать саму базу, если её ещё нет. Прав может не хватить — это не
        /// беда: следующий шаг всё равно скажет человеку, что делать.
        /// </summary>
        private static bool TryCreateDatabase()
        {
            var builder = new MySqlConnectionStringBuilder(Db.ConnectionString);
            var name = builder.Database;

            // Имя базы приходит из текстового файла, поэтому в обратные кавычки
            // его не подставляем без проверки: разрешаем только буквы, цифры и «_».
            if (string.IsNullOrWhiteSpace(name) || !IsSimpleName(name)) return false;

            builder.Database = "";
            try
            {
                using (var connection = new MySqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    var sql = "CREATE DATABASE IF NOT EXISTS `" + name + "` "
                            + "DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci";
                    using (var command = new MySqlCommand(sql, connection))
                        command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                // Нет прав на CREATE DATABASE — базу заведут в phpMyAdmin.
                return false;
            }
        }

        private static bool IsSimpleName(string name)
        {
            foreach (var c in name)
            {
                if (!char.IsLetterOrDigit(c) && c != '_') return false;
            }
            return true;
        }

        private static IEnumerable<Db.Statement> Statements()
        {
            yield return Db.S(@"
CREATE TABLE IF NOT EXISTS classes (
  id         INT         NOT NULL AUTO_INCREMENT,
  name       VARCHAR(32) NOT NULL,
  sort_order INT         NOT NULL DEFAULT 0,
  PRIMARY KEY (id),
  UNIQUE KEY uq_classes_name (name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

            yield return Db.S(@"
CREATE TABLE IF NOT EXISTS lesson_times (
  lesson_no  TINYINT NOT NULL,
  start_time TIME    NOT NULL,
  end_time   TIME    NOT NULL,
  PRIMARY KEY (lesson_no)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

            // variant: 0 — обычное расписание, 1 — изменённое.
            // Обе сетки живут в одной таблице: запросы одинаковые, а переключение
            // показа — это смена одного числа, а не подмена таблицы.
            //
            // Внешнего ключа на classes здесь нарочно нет. Для его создания MySQL
            // требует право REFERENCES, а школьному пользователю базы его выдают
            // далеко не всегда: на такой учётной записи вся установка падала бы
            // с «REFERENCES command denied» и программа не заводилась вовсе.
            // Уроки удалённого класса убирает Repo.DeleteClass — одной
            // транзакцией, то есть ровно то же, что делал бы ON DELETE CASCADE.
            yield return Db.S(@"
CREATE TABLE IF NOT EXISTS schedule (
  id        INT          NOT NULL AUTO_INCREMENT,
  variant   TINYINT      NOT NULL,
  class_id  INT          NOT NULL,
  weekday   TINYINT      NOT NULL,
  lesson_no TINYINT      NOT NULL,
  subject   VARCHAR(120) NOT NULL,
  teacher   VARCHAR(120) NULL,
  room      VARCHAR(32)  NULL,
  PRIMARY KEY (id),
  UNIQUE KEY uq_cell (variant, class_id, weekday, lesson_no),
  KEY idx_day (variant, weekday, class_id, lesson_no),
  KEY idx_class (class_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

            // Отметки на календаре: праздники, каникулы и дни, которым назначена
            // своя сетка (например, «в эту пятницу — изменённое»).
            yield return Db.S(@"
CREATE TABLE IF NOT EXISTS calendar_days (
  day        DATE         NOT NULL,
  is_holiday TINYINT(1)   NOT NULL DEFAULT 0,
  title      VARCHAR(120) NULL,
  variant    TINYINT      NULL,
  note       VARCHAR(255) NULL,
  PRIMARY KEY (day)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

            yield return Db.S(@"
CREATE TABLE IF NOT EXISTS settings (
  k VARCHAR(64)  NOT NULL,
  v VARCHAR(255) NOT NULL,
  PRIMARY KEY (k)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

            // Значения по умолчанию. INSERT IGNORE — уже выставленное учителем
            // не трогаем ни при одном последующем запуске.
            var defaults = new[]
            {
                Pair(SettingKeys.Revision, "1"),
                Pair(SettingKeys.ActiveVariant, "0"),
                Pair(SettingKeys.DisplayMode, "day"),
                Pair(SettingKeys.DisplayClass, "0"),
                Pair(SettingKeys.DisplayDateMode, "today"),
                Pair(SettingKeys.DisplayDate, ""),
                Pair(SettingKeys.SchoolName, "Расписание уроков"),
                Pair(SettingKeys.Ticker, ""),
                Pair(SettingKeys.LessonsCount, "8"),
                Pair(SettingKeys.DaysCount, "6"),
                Pair(SettingKeys.AutoRotate, "0"),
                Pair(SettingKeys.RotateSeconds, "20"),
                Pair(SettingKeys.IdleSeconds, "120"),
                Pair(SettingKeys.TomorrowAfter, ""),
                Pair(SettingKeys.ClassesPerPage, "8"),
                Pair(SettingKeys.ShowReplacements, "1"),
                Pair(SettingKeys.Theme, "dark"),
            };

            foreach (var pair in defaults)
                yield return Db.S("INSERT IGNORE INTO settings (k, v) VALUES (@p0, @p1)", pair.Key, pair.Value);

            // Звонки по умолчанию — восемь уроков с 8:30. Учитель поправит их
            // на вкладке «Звонки», пустая таблица там выглядела бы поломкой.
            var bells = new[]
            {
                "1 08:30 09:15", "2 09:25 10:10", "3 10:25 11:10", "4 11:25 12:10",
                "5 12:20 13:05", "6 13:15 14:00", "7 14:10 14:55", "8 15:05 15:50"
            };

            foreach (var bell in bells)
            {
                var parts = bell.Split(' ');
                yield return Db.S(
                    "INSERT IGNORE INTO lesson_times (lesson_no, start_time, end_time) VALUES (@p0, @p1, @p2)",
                    int.Parse(parts[0]), parts[1] + ":00", parts[2] + ":00");
            }
        }

        private static KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
