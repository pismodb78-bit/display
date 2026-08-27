using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SchoolSchedule.Core
{
    /// <summary>
    /// Настройки из ip.txt, который лежит в папке с .exe.
    ///
    /// Зачем отдельный текстовый файл, а не окно настроек: адрес базы нужно
    /// уметь поправить на месте, за минуту, без установленной студии и без
    /// пересборки — телевизор в коридоре, а базу перевесили на другой IP.
    ///
    /// Файл перечитывается на ходу: <see cref="ReloadIfChanged"/> смотрит на
    /// время записи, и если файл сохранили — настройки подхватываются, а
    /// подключение к базе пересоздаётся. Перезапускать .exe не нужно.
    ///
    /// Понимаются оба формата:
    ///   одна строка  server=192.168.0.20;port=3306;uid=user1;password=scent01;database=school_schedule
    ///   или по строке на параметр (см. шаблон ip.txt).
    /// </summary>
    public static class AppConfig
    {
        private static readonly object Sync = new object();
        private static Dictionary<string, string> _values = NewMap();
        private static DateTime _lastWrite = DateTime.MinValue;
        private static string _path;

        /// <summary>Путь к найденному (или созданному) ip.txt.</summary>
        public static string FilePath
        {
            get { lock (Sync) { return _path ?? DefaultPath; } }
        }

        /// <summary>Путь, по которому файл создаётся, если его нет, — рядом с .exe.</summary>
        public static string DefaultPath
        {
            get { return Path.Combine(AppContext.BaseDirectory, "ip.txt"); }
        }

        private static Dictionary<string, string> NewMap()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Где искать файл: рядом с .exe, а при запуске из Visual Studio —
        /// ещё и в корне проекта, чтобы не копировать его туда руками.
        /// </summary>
        private static IEnumerable<string> Candidates()
        {
            yield return DefaultPath;
            yield return Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "ip.txt");
            yield return Path.Combine(Environment.CurrentDirectory, "ip.txt");
        }

        /// <summary>
        /// Первое чтение. Если файла нет — рядом с .exe кладётся шаблон,
        /// чтобы человеку было что править, а не искать формат в документации.
        /// </summary>
        public static void Load()
        {
            string found = null;
            foreach (var candidate in Candidates())
            {
                if (File.Exists(candidate)) { found = candidate; break; }
            }

            if (found == null)
            {
                found = DefaultPath;
                try { File.WriteAllText(found, Template, new UTF8Encoding(true)); }
                catch { /* каталог только для чтения — работаем на значениях по умолчанию */ }
            }

            lock (Sync) { _path = found; }
            Read(found);
        }

        /// <summary>
        /// Перечитать, если файл изменился с прошлого раза.
        /// Возвращает true, когда настройки действительно обновились.
        /// </summary>
        public static bool ReloadIfChanged()
        {
            string path;
            lock (Sync) { path = _path; }
            if (path == null || !File.Exists(path)) return false;

            DateTime stamp;
            try { stamp = File.GetLastWriteTimeUtc(path); }
            catch { return false; }

            lock (Sync) { if (stamp == _lastWrite) return false; }
            return Read(path);
        }

        private static bool Read(string path)
        {
            string text;
            DateTime stamp;
            try
            {
                text = File.ReadAllText(path);
                stamp = File.GetLastWriteTimeUtc(path);
            }
            catch
            {
                // Блокнот держит файл открытым доли секунды при сохранении —
                // не беда, прочитаем на следующем тике таймера.
                return false;
            }

            var map = Parse(text);
            lock (Sync)
            {
                _values = map;
                _lastWrite = stamp;
            }
            return true;
        }

        /// <summary>
        /// Разбор: комментарии (# и //) отбрасываются, дальше текст режется по
        /// переводам строк и по «;» — так один разбор понимает и построчный
        /// формат, и строку подключения в одну линию.
        /// </summary>
        public static Dictionary<string, string> Parse(string text)
        {
            var map = NewMap();
            if (string.IsNullOrEmpty(text)) return map;

            foreach (var rawLine in text.Replace("\r\n", "\n").Split('\n'))
            {
                var line = rawLine.Trim();
                if (line.Length == 0) continue;
                if (line.StartsWith("#") || line.StartsWith("//")) continue;

                foreach (var pair in line.Split(';'))
                {
                    var eq = pair.IndexOf('=');
                    if (eq <= 0) continue;

                    var key = pair.Substring(0, eq).Trim();
                    var value = pair.Substring(eq + 1).Trim();
                    if (key.Length == 0) continue;

                    map[key] = value;
                }
            }
            return map;
        }

        /// <summary>Значение по первому из перечисленных имён (синонимы ключей).</summary>
        public static string Get(string defaultValue, params string[] keys)
        {
            lock (Sync)
            {
                foreach (var key in keys)
                {
                    string value;
                    if (_values.TryGetValue(key, out value) && !string.IsNullOrWhiteSpace(value))
                        return value;
                }
            }
            return defaultValue;
        }

        public static int GetInt(int defaultValue, params string[] keys)
        {
            var raw = Get(null, keys);
            int parsed;
            if (raw != null && int.TryParse(raw.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed))
                return parsed;
            return defaultValue;
        }

        public static double GetDouble(double defaultValue, params string[] keys)
        {
            var raw = Get(null, keys);
            if (raw == null) return defaultValue;

            // «1,3» и «1.3» пишут одинаково часто — принимаем оба варианта.
            raw = raw.Trim().Replace(',', '.');
            double parsed;
            if (double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed))
                return parsed;
            return defaultValue;
        }

        public static bool GetBool(bool defaultValue, params string[] keys)
        {
            var raw = Get(null, keys);
            if (raw == null) return defaultValue;

            switch (raw.Trim().ToLowerInvariant())
            {
                case "1": case "true": case "yes": case "да": case "on": return true;
                case "0": case "false": case "no": case "нет": case "off": return false;
                default: return defaultValue;
            }
        }

        // --- Параметры, которые спрашивают чаще всего ---------------------

        public static string Server { get { return Get("127.0.0.1", "server", "host", "ip", "адрес"); } }
        public static int Port { get { return GetInt(3306, "port", "порт"); } }
        public static string User { get { return Get("root", "uid", "user", "username", "login", "пользователь"); } }
        public static string Password { get { return Get("", "password", "pwd", "pass", "пароль"); } }
        public static string Database { get { return Get("school_schedule", "database", "db", "base", "база"); } }

        /// <summary>Запасной пароль учителя — пока свой не задан в программе.</summary>
        public static string FallbackAdminPassword { get { return Get("1234", "admin", "adminpassword", "adminpass"); } }

        /// <summary>
        /// Тема для этого компьютера. Пусто — берётся та, что задана в базе
        /// (её выбирает учитель для всех экранов сразу). Строка в ip.txt
        /// перебивает: телевизор может остаться тёмным, а ноутбук — светлым.
        /// </summary>
        public static string Theme { get { return Get("", "theme", "тема"); } }

        public static bool FullScreen { get { return GetBool(true, "fullscreen", "полныйэкран"); } }
        public static double Scale { get { return Clamp(GetDouble(1.0, "scale", "масштаб"), 0.5, 3.0); } }

        /// <summary>Период опроса базы, секунд. Меньше 2 не берём — незачем дёргать сервер.</summary>
        public static int RefreshSeconds { get { return (int)Clamp(GetInt(10, "refresh", "обновление"), 2, 600); } }

        private static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// Записать новые значения подключения, сохранив комментарии и порядок
        /// строк. Нужно окну «Подключение»: на телевизоре Блокнота под рукой нет.
        /// </summary>
        public static void Save(Dictionary<string, string> updates)
        {
            var path = FilePath;
            string text;
            try { text = File.Exists(path) ? File.ReadAllText(path) : Template; }
            catch { text = Template; }

            var lines = new List<string>(text.Replace("\r\n", "\n").Split('\n'));
            var written = NewMap();

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var trimmed = line.Trim();
                if (trimmed.Length == 0 || trimmed.StartsWith("#") || trimmed.StartsWith("//")) continue;

                var eq = trimmed.IndexOf('=');
                if (eq <= 0 || trimmed.Contains(";")) continue;   // строку подключения целиком не трогаем

                var key = trimmed.Substring(0, eq).Trim();
                string value;
                if (!updates.TryGetValue(key, out value)) continue;

                // Выравнивание «key      = value» сохраняем — файл читают глазами.
                var pad = trimmed.Substring(0, eq).Length - key.Length;
                lines[i] = key + new string(' ', Math.Max(pad, 1)) + "= " + value;
                written[key] = value;
            }

            foreach (var pair in updates)
            {
                if (written.ContainsKey(pair.Key)) continue;
                lines.Add(pair.Key + " = " + pair.Value);
            }

            File.WriteAllText(path, string.Join(Environment.NewLine, lines).TrimEnd() + Environment.NewLine,
                              new UTF8Encoding(true));
            Read(path);
        }

        /// <summary>Шаблон файла — он же подсказка по формату.</summary>
        public const string Template =
            "# ----------------------------------------------------------------------\r\n" +
            "#  Школьное расписание — настройки подключения.\r\n" +
            "#  Файл правится Блокнотом; программа подхватывает изменения на ходу.\r\n" +
            "# ----------------------------------------------------------------------\r\n" +
            "\r\n" +
            "# --- База данных (MAMP / phpMyAdmin) ---\r\n" +
            "server   = 192.168.0.20\r\n" +
            "port     = 3306\r\n" +
            "uid      = user1\r\n" +
            "password = scent01\r\n" +
            "database = school_schedule\r\n" +
            "\r\n" +
            "# --- Пароль для входа в режим учителя ---\r\n" +
            "admin = 1234\r\n" +
            "\r\n" +
            "# --- Экран ---\r\n" +
            "fullscreen = 1\r\n" +
            "scale      = 1.0\r\n" +
            "refresh    = 10\r\n";
    }
}
