using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace SchoolSchedule.Core
{
    /// <summary>
    /// Запросы к базе в одном месте. Каждое изменение заканчивается
    /// <see cref="Touch"/> — счётчиком, по которому телевизор понимает, что
    /// расписание правили, и перерисовывает себя сам.
    /// </summary>
    public static class Repo
    {
        /// <summary>Ключ клетки: день + номер урока (или класс + номер урока).</summary>
        public static int Key(int a, int b) { return a * 100 + b; }

        // ================= КЛАССЫ =================

        public static List<SchoolClass> Classes()
        {
            return Db.Query(
                "SELECT id, name, sort_order FROM classes ORDER BY sort_order, name",
                row => new SchoolClass
                {
                    Id = Db.Int(row, 0),
                    Name = Db.Str(row, 1),
                    SortOrder = Db.Int(row, 2)
                });
        }

        public static SchoolClass ClassById(IEnumerable<SchoolClass> classes, int id)
        {
            foreach (var item in classes)
            {
                if (item.Id == id) return item;
            }
            return null;
        }

        public static void AddClass(string name)
        {
            name = Trim(name, 32);
            if (string.IsNullOrEmpty(name)) return;

            var next = Convert.ToInt32(Db.Scalar("SELECT COALESCE(MAX(sort_order), 0) + 10 FROM classes") ?? 10);
            Db.Exec("INSERT INTO classes (name, sort_order) VALUES (@p0, @p1)", name, next);
            Touch();
        }

        public static void RenameClass(int id, string name)
        {
            name = Trim(name, 32);
            if (string.IsNullOrEmpty(name)) return;

            Db.Exec("UPDATE classes SET name = @p0 WHERE id = @p1", name, id);
            Touch();
        }

        /// <summary>
        /// Удалить класс вместе с его расписанием — обе сетки сразу.
        ///
        /// Внешнего ключа с ON DELETE CASCADE в таблице нет намеренно (он
        /// требует права REFERENCES, которого у школьного пользователя базы
        /// может не быть), поэтому уроки убираем сами. Одной транзакцией:
        /// класс без расписания и расписание без класса одинаково нехороши.
        /// </summary>
        public static void DeleteClass(int id)
        {
            Db.Batch(new List<Db.Statement>
            {
                Db.S("DELETE FROM schedule WHERE class_id = @p0", id),
                Db.S("DELETE FROM classes WHERE id = @p0", id)
            });
            Touch();
        }

        /// <summary>Переставить класс в списке: direction = -1 вверх, +1 вниз.</summary>
        public static void MoveClass(int id, int direction)
        {
            var classes = Classes();
            int index = classes.FindIndex(c => c.Id == id);
            if (index < 0) return;

            int target = index + direction;
            if (target < 0 || target >= classes.Count) return;

            // Порядок переписываем целиком: если раньше у всех стоял ноль,
            // обмен двух значений ничего бы не изменил.
            var reordered = new List<SchoolClass>(classes);
            var moved = reordered[index];
            reordered.RemoveAt(index);
            reordered.Insert(target, moved);

            var statements = new List<Db.Statement>();
            for (int i = 0; i < reordered.Count; i++)
                statements.Add(Db.S("UPDATE classes SET sort_order = @p0 WHERE id = @p1", (i + 1) * 10, reordered[i].Id));

            Db.Batch(statements);
            Touch();
        }

        // ================= ЗВОНКИ =================

        public static List<LessonTime> LessonTimes()
        {
            return Db.Query(
                "SELECT lesson_no, start_time, end_time FROM lesson_times ORDER BY lesson_no",
                row => new LessonTime
                {
                    No = Db.Int(row, 0),
                    Start = Db.Time(row, 1),
                    End = Db.Time(row, 2)
                });
        }

        public static void SaveLessonTime(int no, TimeSpan start, TimeSpan end)
        {
            Db.Exec(
                "INSERT INTO lesson_times (lesson_no, start_time, end_time) VALUES (@p0, @p1, @p2) " +
                "ON DUPLICATE KEY UPDATE start_time = VALUES(start_time), end_time = VALUES(end_time)",
                no, Sql(start), Sql(end));
            Touch();
        }

        public static void DeleteLessonTime(int no)
        {
            Db.Exec("DELETE FROM lesson_times WHERE lesson_no = @p0", no);
            Touch();
        }

        private static string Sql(TimeSpan time)
        {
            return ((int)time.TotalHours).ToString("00") + ":" + time.Minutes.ToString("00") + ":00";
        }

        // ================= РАСПИСАНИЕ =================

        /// <summary>Неделя одного класса. Ключ — <see cref="Key"/>(день недели, номер урока).</summary>
        public static Dictionary<int, Lesson> WeekOfClass(int variant, int classId)
        {
            var rows = Db.Query(
                "SELECT weekday, lesson_no, subject, teacher, room FROM schedule " +
                "WHERE variant = @p0 AND class_id = @p1",
                row => new Lesson
                {
                    ClassId = classId,
                    Weekday = Db.Int(row, 0),
                    No = Db.Int(row, 1),
                    Subject = Db.Str(row, 2),
                    Teacher = Db.Str(row, 3),
                    Room = Db.Str(row, 4)
                },
                variant, classId);

            var map = new Dictionary<int, Lesson>();
            foreach (var lesson in rows) map[Key(lesson.Weekday, lesson.No)] = lesson;
            return map;
        }

        /// <summary>Один день всех классов. Ключ — <see cref="Key"/>(id класса, номер урока).</summary>
        public static Dictionary<int, Lesson> DayOfAllClasses(int variant, int weekday)
        {
            var rows = Db.Query(
                "SELECT class_id, lesson_no, subject, teacher, room FROM schedule " +
                "WHERE variant = @p0 AND weekday = @p1",
                row => new Lesson
                {
                    ClassId = Db.Int(row, 0),
                    Weekday = weekday,
                    No = Db.Int(row, 1),
                    Subject = Db.Str(row, 2),
                    Teacher = Db.Str(row, 3),
                    Room = Db.Str(row, 4)
                },
                variant, weekday);

            var map = new Dictionary<int, Lesson>();
            foreach (var lesson in rows) map[Key(lesson.ClassId, lesson.No)] = lesson;
            return map;
        }

        public static void SaveCell(int variant, int classId, int weekday, int no,
                                    string subject, string teacher, string room)
        {
            subject = Trim(subject, 120);
            if (string.IsNullOrEmpty(subject))
            {
                ClearCell(variant, classId, weekday, no);
                return;
            }

            Db.Exec(
                "INSERT INTO schedule (variant, class_id, weekday, lesson_no, subject, teacher, room) " +
                "VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6) " +
                "ON DUPLICATE KEY UPDATE subject = VALUES(subject), teacher = VALUES(teacher), room = VALUES(room)",
                variant, classId, weekday, no, subject, Trim(teacher, 120), Trim(room, 32));
            Touch();
        }

        public static void ClearCell(int variant, int classId, int weekday, int no)
        {
            Db.Exec(
                "DELETE FROM schedule WHERE variant = @p0 AND class_id = @p1 AND weekday = @p2 AND lesson_no = @p3",
                variant, classId, weekday, no);
            Touch();
        }

        /// <summary>
        /// Скопировать одну сетку в другую (обычно обычное → изменённое, чтобы
        /// учителю было что править, а не заполнять неделю с нуля).
        /// classId = null — все классы.
        /// </summary>
        public static void CopyVariant(int from, int to, int? classId)
        {
            var where = classId.HasValue ? " AND class_id = @p1" : "";
            var statements = new List<Db.Statement>
            {
                classId.HasValue
                    ? Db.S("DELETE FROM schedule WHERE variant = @p0 AND class_id = @p1", to, classId.Value)
                    : Db.S("DELETE FROM schedule WHERE variant = @p0", to),

                classId.HasValue
                    ? Db.S("INSERT INTO schedule (variant, class_id, weekday, lesson_no, subject, teacher, room) " +
                           "SELECT @p2, class_id, weekday, lesson_no, subject, teacher, room " +
                           "FROM schedule WHERE variant = @p0" + where, from, classId.Value, to)
                    : Db.S("INSERT INTO schedule (variant, class_id, weekday, lesson_no, subject, teacher, room) " +
                           "SELECT @p1, class_id, weekday, lesson_no, subject, teacher, room " +
                           "FROM schedule WHERE variant = @p0", from, to)
            };

            Db.Batch(statements);
            Touch();
        }

        public static void ClearVariant(int variant, int? classId)
        {
            if (classId.HasValue)
                Db.Exec("DELETE FROM schedule WHERE variant = @p0 AND class_id = @p1", variant, classId.Value);
            else
                Db.Exec("DELETE FROM schedule WHERE variant = @p0", variant);
            Touch();
        }

        /// <summary>Скопировать день внутри одной сетки — «во вторник как в понедельник».</summary>
        public static void CopyDay(int variant, int classId, int fromDay, int toDay)
        {
            if (fromDay == toDay) return;

            Db.Batch(new List<Db.Statement>
            {
                Db.S("DELETE FROM schedule WHERE variant = @p0 AND class_id = @p1 AND weekday = @p2",
                     variant, classId, toDay),
                Db.S("INSERT INTO schedule (variant, class_id, weekday, lesson_no, subject, teacher, room) " +
                     "SELECT @p0, class_id, @p3, lesson_no, subject, teacher, room " +
                     "FROM schedule WHERE variant = @p0 AND class_id = @p1 AND weekday = @p2",
                     variant, classId, fromDay, toDay)
            });
            Touch();
        }

        /// <summary>Сколько клеток заполнено — по этому числу видно, пустая сетка или нет.</summary>
        public static int CountCells(int variant)
        {
            return Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM schedule WHERE variant = @p0", variant) ?? 0);
        }

        /// <summary>Подсказки для полей «предмет», «учитель», «кабинет» — из того, что уже вводили.</summary>
        public static List<string> Suggestions(string column)
        {
            if (column != "subject" && column != "teacher" && column != "room") return new List<string>();

            return Db.Query(
                "SELECT DISTINCT " + column + " FROM schedule " +
                "WHERE " + column + " IS NOT NULL AND " + column + " <> '' ORDER BY " + column,
                row => Db.Str(row, 0));
        }

        private static string Trim(string value, int max)
        {
            if (value == null) return null;
            value = value.Trim();
            if (value.Length > max) value = value.Substring(0, max);
            return value;
        }

        // ================= КАЛЕНДАРЬ =================

        public static Dictionary<DateTime, CalendarDay> DaysBetween(DateTime from, DateTime to)
        {
            var rows = Db.Query(
                "SELECT `day`, is_holiday, title, variant, note FROM calendar_days " +
                "WHERE `day` BETWEEN @p0 AND @p1",
                ReadDay, from.Date, to.Date);

            var map = new Dictionary<DateTime, CalendarDay>();
            foreach (var day in rows) map[day.Date] = day;
            return map;
        }

        public static CalendarDay DayMark(DateTime date)
        {
            var rows = Db.Query(
                "SELECT `day`, is_holiday, title, variant, note FROM calendar_days WHERE `day` = @p0",
                ReadDay, date.Date);
            return rows.Count > 0 ? rows[0] : null;
        }

        public static List<CalendarDay> Upcoming(DateTime from, int limit)
        {
            return Db.Query(
                "SELECT `day`, is_holiday, title, variant, note FROM calendar_days " +
                "WHERE `day` >= @p0 ORDER BY `day` LIMIT " + Math.Max(1, limit),
                ReadDay, from.Date);
        }

        private static CalendarDay ReadDay(IDataRecord row)
        {
            return new CalendarDay
            {
                Date = Db.Date(row, 0).Date,
                IsHoliday = Db.Bool(row, 1),
                Title = Db.Str(row, 2),
                Variant = Db.IntOrNull(row, 3),
                Note = Db.Str(row, 4)
            };
        }

        public static void SaveDayMark(CalendarDay day)
        {
            Db.Exec(
                "INSERT INTO calendar_days (`day`, is_holiday, title, variant, note) " +
                "VALUES (@p0, @p1, @p2, @p3, @p4) " +
                "ON DUPLICATE KEY UPDATE is_holiday = VALUES(is_holiday), title = VALUES(title), " +
                "variant = VALUES(variant), note = VALUES(note)",
                day.Date.Date, day.IsHoliday ? 1 : 0, Trim(day.Title, 120),
                day.Variant.HasValue ? (object)day.Variant.Value : null, Trim(day.Note, 255));
            Touch();
        }

        public static void DeleteDayMark(DateTime date)
        {
            Db.Exec("DELETE FROM calendar_days WHERE `day` = @p0", date.Date);
            Touch();
        }

        // ================= НАСТРОЙКИ =================

        public static Dictionary<string, string> Settings()
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var rows = Db.Query("SELECT k, v FROM settings",
                                row => new KeyValuePair<string, string>(Db.Str(row, 0), Db.Str(row, 1)));

            foreach (var pair in rows) map[pair.Key] = pair.Value ?? "";
            return map;
        }

        public static void Set(string key, string value)
        {
            SetMany(new[] { new KeyValuePair<string, string>(key, value) });
        }

        public static void SetMany(IEnumerable<KeyValuePair<string, string>> values)
        {
            // Значение обрезаем под VARCHAR(255): длинное объявление иначе
            // валит весь запрос, и вместе с ним откатываются все остальные
            // настройки, которые учитель только что выставил.
            var statements = new List<Db.Statement>();
            foreach (var pair in values)
            {
                statements.Add(Db.S(
                    "INSERT INTO settings (k, v) VALUES (@p0, @p1) ON DUPLICATE KEY UPDATE v = VALUES(v)",
                    pair.Key, Trim(pair.Value, 255) ?? ""));
            }

            statements.Add(BumpStatement());
            Db.Batch(statements);
        }

        /// <summary>
        /// Счётчик изменений. Телевизор раз в несколько секунд спрашивает только
        /// это число — один крохотный запрос вместо перечитывания всей базы, —
        /// и перерисовывается, лишь когда оно выросло.
        /// </summary>
        public static long Revision()
        {
            var value = Db.Scalar("SELECT v FROM settings WHERE k = @p0", SettingKeys.Revision);
            long parsed;
            if (value != null && long.TryParse(Convert.ToString(value), NumberStyles.Integer,
                                               CultureInfo.InvariantCulture, out parsed))
                return parsed;
            return 0;
        }

        public static void Touch()
        {
            var statement = BumpStatement();
            Db.Exec(statement.Sql, statement.Args);
        }

        private static Db.Statement BumpStatement()
        {
            return Db.S(
                "INSERT INTO settings (k, v) VALUES (@p0, '1') " +
                "ON DUPLICATE KEY UPDATE v = CAST(CAST(v AS UNSIGNED) + 1 AS CHAR)",
                SettingKeys.Revision);
        }
    }
}
