using System;
using System.Collections.Generic;

namespace SchoolSchedule.Core
{
    /// <summary>Что показывать за конкретное число.</summary>
    public sealed class DayPlan
    {
        public DateTime Date { get; set; }
        public int Weekday { get; set; }
        public int Variant { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsSchoolDay { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }

        /// <summary>Сетку на этот день назначили отметкой в календаре, а не общим переключателем.</summary>
        public bool VariantForced { get; set; }
    }

    /// <summary>
    /// Правила, по которым дата превращается в расписание.
    ///
    /// Порядок такой: если день помечен праздником — уроков нет; иначе сетка
    /// берётся из отметки на этом дне, а если там ничего не назначено —
    /// из общего переключателя «обычное / изменённое».
    ///
    /// Из-за этого «изменённое» умеет работать двумя способами: включить его
    /// целиком (заболел учитель — переключили на неделю) или назначить только
    /// на завтра, не трогая остальные дни.
    /// </summary>
    public static class ScheduleResolver
    {
        public static DayPlan Resolve(DateTime date, CalendarDay mark, int activeVariant, int daysCount)
        {
            var weekday = Ru.Weekday(date);
            var plan = new DayPlan
            {
                Date = date.Date,
                Weekday = weekday,
                Variant = activeVariant,
                IsSchoolDay = weekday <= daysCount,
                Title = null,
                Note = null
            };

            if (mark != null)
            {
                plan.IsHoliday = mark.IsHoliday;
                plan.Title = mark.Title;
                plan.Note = mark.Note;

                if (mark.Variant.HasValue)
                {
                    plan.Variant = mark.Variant.Value;
                    plan.VariantForced = true;
                }

                // Учебный день можно назначить и на субботу «сверх сетки»:
                // если день отмечен и не как праздник, уроки показываем.
                if (!mark.IsHoliday && mark.Variant.HasValue) plan.IsSchoolDay = true;
            }

            if (plan.IsHoliday) plan.IsSchoolDay = false;
            return plan;
        }

        /// <summary>Понедельник недели, в которую попадает дата.</summary>
        public static DateTime MondayOf(DateTime date)
        {
            return date.Date.AddDays(1 - Ru.Weekday(date));
        }

        /// <summary>
        /// Ближайший учебный день начиная с указанного: пропускает выходные и
        /// праздники. Нужен режиму «следующий учебный день» — в пятницу вечером
        /// на экране должен быть понедельник, а не пустая суббота.
        /// </summary>
        public static DateTime NextSchoolDay(DateTime from, Dictionary<DateTime, CalendarDay> marks,
                                             int daysCount, int searchDays)
        {
            for (int offset = 0; offset <= searchDays; offset++)
            {
                var date = from.Date.AddDays(offset);
                CalendarDay mark = null;
                if (marks != null) marks.TryGetValue(date, out mark);

                var plan = Resolve(date, mark, Variant.Regular, daysCount);
                if (plan.IsSchoolDay) return date;
            }
            return from.Date;
        }

        /// <summary>Идёт ли сейчас урок, и какой. no = 0 — уроков сейчас нет.</summary>
        public static void CurrentLesson(List<LessonTime> times, DateTime now, out int no, out bool isBreak)
        {
            no = 0;
            isBreak = false;
            if (times == null || times.Count == 0) return;

            var moment = now.TimeOfDay;
            for (int i = 0; i < times.Count; i++)
            {
                if (moment >= times[i].Start && moment <= times[i].End)
                {
                    no = times[i].No;
                    return;
                }

                // Между уроками подсвечиваем следующий: так на экране видно,
                // куда идти после звонка.
                if (moment < times[i].Start)
                {
                    if (i > 0 && moment > times[i - 1].End)
                    {
                        no = times[i].No;
                        isBreak = true;
                    }
                    return;
                }
            }
        }
    }
}
