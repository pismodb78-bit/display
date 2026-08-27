using System;

namespace SchoolSchedule.Core
{
    /// <summary>
    /// Русские подписи дат своими руками, а не через CultureInfo.
    /// Причина простая: на телевизоре может стоять английская Windows, и
    /// «Tuesday, August 26» на школьном расписании смотрелось бы странно.
    /// </summary>
    public static class Ru
    {
        private static readonly string[] Days =
        {
            "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье"
        };

        private static readonly string[] DaysShort = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };

        private static readonly string[] MonthsGenitive =
        {
            "января", "февраля", "марта", "апреля", "мая", "июня",
            "июля", "августа", "сентября", "октября", "ноября", "декабря"
        };

        private static readonly string[] Months =
        {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        /// <summary>Номер дня недели по-человечески: 1 = понедельник ... 7 = воскресенье.</summary>
        public static int Weekday(DateTime date)
        {
            int dow = (int)date.DayOfWeek;      // 0 = воскресенье
            return dow == 0 ? 7 : dow;
        }

        public static string DayName(int weekday)
        {
            return (weekday >= 1 && weekday <= 7) ? Days[weekday - 1] : "";
        }

        public static string DayShort(int weekday)
        {
            return (weekday >= 1 && weekday <= 7) ? DaysShort[weekday - 1] : "";
        }

        /// <summary>«августа» — для подписи «26 августа».</summary>
        public static string MonthGenitive(int month)
        {
            return (month >= 1 && month <= 12) ? MonthsGenitive[month - 1] : "";
        }

        public static string MonthName(int month)
        {
            return (month >= 1 && month <= 12) ? Months[month - 1] : "";
        }

        /// <summary>«26 августа 2026».</summary>
        public static string Date(DateTime date)
        {
            return date.Day + " " + MonthsGenitive[date.Month - 1] + " " + date.Year;
        }

        /// <summary>«Вторник, 26 августа 2026».</summary>
        public static string LongDate(DateTime date)
        {
            return DayName(Weekday(date)) + ", " + Date(date);
        }

        /// <summary>«Вт, 26 августа».</summary>
        public static string ShortDate(DateTime date)
        {
            return DayShort(Weekday(date)) + ", " + date.Day + " " + MonthsGenitive[date.Month - 1];
        }

        public static string Time(TimeSpan time)
        {
            return ((int)time.TotalHours).ToString("00") + ":" + time.Minutes.ToString("00");
        }

        /// <summary>«сегодня» / «завтра» / «в пятницу» — для подписи над таблицей.</summary>
        public static string Relative(DateTime date)
        {
            var days = (date.Date - DateTime.Today).Days;
            if (days == 0) return "сегодня";
            if (days == 1) return "завтра";
            if (days == 2) return "послезавтра";
            if (days == -1) return "вчера";
            return Date(date);
        }

        /// <summary>«2 урока» / «5 уроков» — правильное окончание.</summary>
        public static string Lessons(int count)
        {
            int mod100 = count % 100;
            int mod10 = count % 10;
            if (mod100 >= 11 && mod100 <= 14) return count + " уроков";
            if (mod10 == 1) return count + " урок";
            if (mod10 >= 2 && mod10 <= 4) return count + " урока";
            return count + " уроков";
        }
    }
}
