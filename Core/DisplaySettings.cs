using System;
using System.Collections.Generic;
using System.Globalization;

namespace SchoolSchedule.Core
{
    /// <summary>
    /// Настройки показа — то, что учитель задаёт на вкладке «Показ», а
    /// телевизор читает из базы. Здесь же решается, за какое число показывать
    /// расписание: сегодня, завтра или выбранную дату.
    /// </summary>
    public sealed class DisplaySettings
    {
        public int ActiveVariant { get; set; }
        public string Mode { get; set; }              // week | day
        public int DisplayClassId { get; set; }
        public string DateMode { get; set; }          // today | tomorrow | next | fixed
        public DateTime? FixedDate { get; set; }
        public string SchoolName { get; set; }
        public string Ticker { get; set; }
        public int LessonsCount { get; set; }
        public int DaysCount { get; set; }
        public bool AutoRotate { get; set; }
        public int RotateSeconds { get; set; }
        public int IdleSeconds { get; set; }
        public TimeSpan? TomorrowAfter { get; set; }
        public int ClassesPerPage { get; set; }
        public bool ShowReplacements { get; set; }
        public string AdminPasswordHash { get; set; }

        /// <summary>«dark» или «light» — что показывать на экране.</summary>
        public string Theme { get; set; }

        public const string ModeWeek = "week";
        public const string ModeDay = "day";

        public static DisplaySettings From(Dictionary<string, string> raw)
        {
            var settings = new DisplaySettings
            {
                ActiveVariant = Int(raw, SettingKeys.ActiveVariant, 0) == Variant.Modified ? Variant.Modified : Variant.Regular,
                Mode = Str(raw, SettingKeys.DisplayMode, ModeDay) == ModeWeek ? ModeWeek : ModeDay,
                DisplayClassId = Int(raw, SettingKeys.DisplayClass, 0),
                DateMode = Str(raw, SettingKeys.DisplayDateMode, "today"),
                SchoolName = Str(raw, SettingKeys.SchoolName, "Расписание уроков"),
                Ticker = Str(raw, SettingKeys.Ticker, ""),
                LessonsCount = Clamp(Int(raw, SettingKeys.LessonsCount, 8), 1, 12),
                DaysCount = Clamp(Int(raw, SettingKeys.DaysCount, 6), 1, 7),
                AutoRotate = Int(raw, SettingKeys.AutoRotate, 0) != 0,
                RotateSeconds = Clamp(Int(raw, SettingKeys.RotateSeconds, 20), 5, 600),
                IdleSeconds = Clamp(Int(raw, SettingKeys.IdleSeconds, 120), 15, 3600),
                ClassesPerPage = Clamp(Int(raw, SettingKeys.ClassesPerPage, 8), 2, 20),
                ShowReplacements = Int(raw, SettingKeys.ShowReplacements, 1) != 0,
                AdminPasswordHash = Str(raw, SettingKeys.AdminPassword, ""),
                Theme = Str(raw, SettingKeys.Theme, "dark")
            };

            settings.FixedDate = ParseDate(Str(raw, SettingKeys.DisplayDate, ""));
            settings.TomorrowAfter = ParseTime(Str(raw, SettingKeys.TomorrowAfter, ""));
            return settings;
        }

        /// <summary>
        /// Дата, за которую показывается расписание.
        /// Режим «today» с заполненным «после …» сам переключается на завтра,
        /// когда уроки уже закончились: в три часа дня расписание на сегодня
        /// на стене никому не нужно.
        /// </summary>
        public DateTime EffectiveDate(DateTime now)
        {
            switch (DateMode)
            {
                case "tomorrow":
                    return now.Date.AddDays(1);
                case "fixed":
                    return FixedDate.HasValue ? FixedDate.Value.Date : now.Date;
                case "next":
                    return now.Date;    // ближайший учебный день ищется отдельно, по календарю
                default:
                    if (TomorrowAfter.HasValue && now.TimeOfDay >= TomorrowAfter.Value)
                        return now.Date.AddDays(1);
                    return now.Date;
            }
        }

        public static DateTime? ParseDate(string value)
        {
            DateTime parsed;
            if (!string.IsNullOrWhiteSpace(value) &&
                DateTime.TryParseExact(value.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out parsed))
                return parsed;
            return null;
        }

        public static string FormatDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static TimeSpan? ParseTime(string value)
        {
            TimeSpan parsed;
            if (!string.IsNullOrWhiteSpace(value) && TimeSpan.TryParse(value.Trim(), CultureInfo.InvariantCulture, out parsed))
                return parsed;
            return null;
        }

        private static string Str(Dictionary<string, string> raw, string key, string fallback)
        {
            string value;
            if (raw != null && raw.TryGetValue(key, out value) && !string.IsNullOrWhiteSpace(value)) return value.Trim();
            return fallback;
        }

        private static int Int(Dictionary<string, string> raw, string key, int fallback)
        {
            int parsed;
            if (int.TryParse(Str(raw, key, ""), NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed))
                return parsed;
            return fallback;
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
