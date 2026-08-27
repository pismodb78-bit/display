using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SchoolSchedule.Core
{
    /// <summary>Одна строка файла: клетка расписания.</summary>
    public sealed class ImportRow
    {
        public string ClassName { get; set; }
        public int Weekday { get; set; }
        public int LessonNo { get; set; }
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Room { get; set; }
    }

    public sealed class ImportResult
    {
        public List<ImportRow> Rows = new List<ImportRow>();
        public List<string> Errors = new List<string>();
        public List<string> ClassNames = new List<string>();
    }

    /// <summary>
    /// Загрузка расписания файлом и выгрузка обратно.
    ///
    /// Расписание на год редко набивают заново: оно уже лежит у завуча в Excel.
    /// «Сохранить как CSV» → «Загрузить из файла» экономит вечер работы, и это
    /// же способ залить расписание с другого компьютера, где базы под рукой нет.
    ///
    /// Формат простой и читаемый глазами:
    ///     Класс;День;Урок;Предмет;Учитель;Кабинет
    ///     5А;Понедельник;1;Математика;Иванова И.И.;204
    /// </summary>
    public static class CsvSchedule
    {
        public const string Header = "Класс;День;Урок;Предмет;Учитель;Кабинет";

        private static readonly string[][] DayNames =
        {
            new[] { "понедельник", "пн", "1" },
            new[] { "вторник", "вт", "2" },
            new[] { "среда", "ср", "3" },
            new[] { "четверг", "чт", "4" },
            new[] { "пятница", "пт", "5" },
            new[] { "суббота", "сб", "6" },
            new[] { "воскресенье", "вс", "7" }
        };

        // ================= ЧТЕНИЕ =================

        public static ImportResult Parse(string path)
        {
            var result = new ImportResult();
            var text = ReadText(path);

            var lines = text.Replace("\r\n", "\n").Split('\n');
            char separator = DetectSeparator(lines);

            var seenClasses = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (line.Length == 0) continue;

                var fields = SplitLine(line, separator);
                if (fields.Count < 4)
                {
                    result.Errors.Add("Строка " + (i + 1) + ": нужно минимум 4 поля (класс, день, урок, предмет).");
                    continue;
                }

                // Шапку пропускаем — её узнаём по тому, что «урок» не число.
                int lessonNo;
                if (!int.TryParse(fields[2].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out lessonNo))
                {
                    if (i == 0) continue;
                    result.Errors.Add("Строка " + (i + 1) + ": номер урока «" + fields[2] + "» — не число.");
                    continue;
                }

                var weekday = ParseWeekday(fields[1]);
                if (weekday == 0)
                {
                    result.Errors.Add("Строка " + (i + 1) + ": непонятный день недели «" + fields[1] + "».");
                    continue;
                }

                var className = fields[0].Trim();
                if (className.Length == 0)
                {
                    result.Errors.Add("Строка " + (i + 1) + ": пустое название класса.");
                    continue;
                }

                if (lessonNo < 1 || lessonNo > 12)
                {
                    result.Errors.Add("Строка " + (i + 1) + ": номер урока " + lessonNo + " вне диапазона 1…12.");
                    continue;
                }

                if (!seenClasses.ContainsKey(className))
                {
                    seenClasses[className] = true;
                    result.ClassNames.Add(className);
                }

                result.Rows.Add(new ImportRow
                {
                    ClassName = className,
                    Weekday = weekday,
                    LessonNo = lessonNo,
                    Subject = fields[3].Trim(),
                    Teacher = fields.Count > 4 ? fields[4].Trim() : "",
                    Room = fields.Count > 5 ? fields[5].Trim() : ""
                });
            }

            return result;
        }

        /// <summary>
        /// Excel в русской Windows сохраняет CSV в кодировке 1251 и без BOM,
        /// а всё остальное — в UTF-8. Определяем по факту, а не по вере.
        /// </summary>
        public static string ReadText(string path)
        {
            var bytes = File.ReadAllBytes(path);

            if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                return new UTF8Encoding(false).GetString(bytes, 3, bytes.Length - 3);

            try
            {
                return new UTF8Encoding(false, true).GetString(bytes);
            }
            catch (DecoderFallbackException)
            {
                try { return Encoding.GetEncoding(1251).GetString(bytes); }
                catch { return Encoding.Default.GetString(bytes); }
            }
        }

        private static char DetectSeparator(string[] lines)
        {
            foreach (var line in lines)
            {
                if (line.Trim().Length == 0) continue;

                int semicolons = Count(line, ';');
                int commas = Count(line, ',');
                int tabs = Count(line, '\t');

                if (tabs > semicolons && tabs > commas) return '\t';
                if (commas > semicolons) return ',';
                return ';';
            }
            return ';';
        }

        private static int Count(string text, char c)
        {
            int count = 0;
            foreach (var item in text)
            {
                if (item == c) count++;
            }
            return count;
        }

        /// <summary>Разбор строки с учётом кавычек: «Иванова, И.И.» — одно поле.</summary>
        public static List<string> SplitLine(string line, char separator)
        {
            var fields = new List<string>();
            var current = new StringBuilder();
            bool quoted = false;

            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];

                if (quoted)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"') { current.Append('"'); i++; }
                        else quoted = false;
                    }
                    else current.Append(c);
                    continue;
                }

                if (c == '"') { quoted = true; continue; }
                if (c == separator) { fields.Add(current.ToString()); current.Clear(); continue; }

                current.Append(c);
            }

            fields.Add(current.ToString());
            return fields;
        }

        public static int ParseWeekday(string value)
        {
            var text = (value ?? "").Trim().ToLowerInvariant().Replace("ё", "е");
            for (int day = 0; day < DayNames.Length; day++)
            {
                foreach (var name in DayNames[day])
                {
                    if (text == name.Replace("ё", "е")) return day + 1;
                }
            }
            return 0;
        }

        // ================= ЗАПИСЬ В БАЗУ =================

        /// <summary>
        /// Залить разобранный файл в выбранную сетку.
        /// Всё одной транзакцией: наполовину залитое расписание хуже незалитого.
        /// </summary>
        public static int Apply(ImportResult data, int variant, bool createMissingClasses, bool replaceAll)
        {
            var classes = Repo.Classes();
            var byName = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in classes) byName[item.Name] = item.Id;

            if (createMissingClasses)
            {
                bool added = false;
                foreach (var name in data.ClassNames)
                {
                    if (byName.ContainsKey(name)) continue;
                    Repo.AddClass(name);
                    added = true;
                }

                if (added)
                {
                    byName.Clear();
                    foreach (var item in Repo.Classes()) byName[item.Name] = item.Id;
                }
            }

            var statements = new List<Db.Statement>();
            if (replaceAll) statements.Add(Db.S("DELETE FROM schedule WHERE variant = @p0", variant));

            int written = 0;
            foreach (var row in data.Rows)
            {
                int classId;
                if (!byName.TryGetValue(row.ClassName, out classId)) continue;

                if (string.IsNullOrWhiteSpace(row.Subject))
                {
                    statements.Add(Db.S(
                        "DELETE FROM schedule WHERE variant = @p0 AND class_id = @p1 AND weekday = @p2 AND lesson_no = @p3",
                        variant, classId, row.Weekday, row.LessonNo));
                    continue;
                }

                statements.Add(Db.S(
                    "INSERT INTO schedule (variant, class_id, weekday, lesson_no, subject, teacher, room) " +
                    "VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6) " +
                    "ON DUPLICATE KEY UPDATE subject = VALUES(subject), teacher = VALUES(teacher), room = VALUES(room)",
                    variant, classId, row.Weekday, row.LessonNo,
                    Cut(row.Subject, 120), Cut(row.Teacher, 120), Cut(row.Room, 32)));
                written++;
            }

            Db.Batch(statements);
            Repo.Touch();
            return written;
        }

        private static string Cut(string value, int max)
        {
            if (value == null) return null;
            value = value.Trim();
            return value.Length > max ? value.Substring(0, max) : value;
        }

        // ================= ВЫГРУЗКА =================

        /// <summary>
        /// Сохранить сетку в файл. UTF-8 с BOM и «;» — в этом виде Excel
        /// открывает файл двойным щелчком и не спрашивает лишнего.
        /// </summary>
        public static int Export(string path, int variant, List<SchoolClass> classes, int daysCount, int lessonsCount)
        {
            var text = new StringBuilder();
            text.AppendLine(Header);

            int written = 0;
            foreach (var schoolClass in classes)
            {
                var week = Repo.WeekOfClass(variant, schoolClass.Id);

                for (int day = 1; day <= daysCount; day++)
                {
                    for (int no = 1; no <= lessonsCount; no++)
                    {
                        Lesson lesson;
                        if (!week.TryGetValue(Repo.Key(day, no), out lesson) || lesson.IsEmpty) continue;

                        text.Append(Escape(schoolClass.Name)).Append(';')
                            .Append(Ru.DayName(day)).Append(';')
                            .Append(no).Append(';')
                            .Append(Escape(lesson.Subject)).Append(';')
                            .Append(Escape(lesson.Teacher)).Append(';')
                            .Append(Escape(lesson.Room))
                            .AppendLine();
                        written++;
                    }
                }
            }

            File.WriteAllText(path, text.ToString(), new UTF8Encoding(true));
            return written;
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.IndexOf(';') < 0 && value.IndexOf('"') < 0 && value.IndexOf('\n') < 0) return value;

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        /// <summary>Пример файла — его кладут рядом с .exe, чтобы было с чего начать.</summary>
        public static string Sample()
        {
            var text = new StringBuilder();
            text.AppendLine(Header);
            text.AppendLine("5А;Понедельник;1;Математика;Иванова И.И.;204");
            text.AppendLine("5А;Понедельник;2;Русский язык;Петрова А.С.;112");
            text.AppendLine("5А;Вторник;1;Физкультура;Сидоров П.П.;спортзал");
            text.AppendLine("5Б;Понедельник;1;Русский язык;Петрова А.С.;112");
            return text.ToString();
        }
    }
}
