using System;

namespace SchoolSchedule.Core
{
    /// <summary>Класс школы: «5А», «11Б». Создаётся учителем, заранее их нет.</summary>
    public sealed class SchoolClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public override string ToString() { return Name; }
    }

    /// <summary>Звонки: во сколько начинается и заканчивается урок с таким номером.</summary>
    public sealed class LessonTime
    {
        public int No { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public string Range
        {
            get { return Ru.Time(Start) + " – " + Ru.Time(End); }
        }
    }

    /// <summary>Одна клетка расписания.</summary>
    public sealed class Lesson
    {
        public int ClassId { get; set; }
        public int Weekday { get; set; }     // 1 = понедельник ... 7 = воскресенье
        public int No { get; set; }          // номер урока
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Room { get; set; }

        public bool IsEmpty
        {
            get { return string.IsNullOrWhiteSpace(Subject); }
        }

        /// <summary>Сравнение по содержимому — им подсвечиваются замены.</summary>
        public bool SameAs(Lesson other)
        {
            if (other == null) return IsEmpty;
            return Norm(Subject) == Norm(other.Subject)
                && Norm(Teacher) == Norm(other.Teacher)
                && Norm(Room) == Norm(other.Room);
        }

        private static string Norm(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? "" : s.Trim().ToLowerInvariant();
        }
    }

    /// <summary>Отметка на календаре: праздник, каникулы или день с особой сеткой.</summary>
    public sealed class CalendarDay
    {
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public string Title { get; set; }

        /// <summary>null — какая сетка включена в настройках; иначе принудительно на этот день.</summary>
        public int? Variant { get; set; }

        public string Note { get; set; }
    }

    /// <summary>Два расписания: обычное и изменённое.</summary>
    public static class Variant
    {
        public const int Regular = 0;
        public const int Modified = 1;

        public static string Name(int variant)
        {
            return variant == Modified ? "Изменённое" : "Обычное";
        }

        public static int Other(int variant)
        {
            return variant == Modified ? Regular : Modified;
        }
    }

    /// <summary>Имена строк в таблице settings — чтобы не разъезжались опечатки.</summary>
    public static class SettingKeys
    {
        public const string Revision = "revision";                 // счётчик изменений, по нему обновляется экран
        public const string ActiveVariant = "active_variant";      // какая сетка показывается: 0 / 1
        public const string DisplayMode = "display_mode";          // week | day
        public const string DisplayClass = "display_class_id";
        public const string DisplayDateMode = "display_date_mode"; // today | tomorrow | next | fixed
        public const string DisplayDate = "display_date";          // для fixed, ГГГГ-ММ-ДД
        public const string SchoolName = "school_name";
        public const string Ticker = "ticker";                     // строка объявления внизу экрана
        public const string LessonsCount = "lessons_count";
        public const string DaysCount = "days_count";              // 5 или 6 учебных дней
        public const string AutoRotate = "auto_rotate";
        public const string RotateSeconds = "rotate_seconds";
        public const string IdleSeconds = "idle_seconds";          // возврат к показу по умолчанию
        public const string TomorrowAfter = "tomorrow_after";      // ЧЧ:ММ — после этого часа показывать завтра
        public const string ClassesPerPage = "classes_per_page";
        public const string AdminPassword = "admin_password";      // pbkdf2$...
        public const string ShowReplacements = "show_replacements";// подсвечивать отличия от обычной сетки
        public const string Theme = "theme";                       // dark | light
    }
}
