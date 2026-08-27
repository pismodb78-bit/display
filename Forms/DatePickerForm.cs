using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Календарь для выбора дня или недели.
    ///
    /// Праздники и дни с назначенной сеткой показаны жирным — видно сразу,
    /// какие числа учитель уже отметил, и не нужно тыкать в каждое.
    /// </summary>
    public partial class DatePickerForm : Form
    {
        private readonly int _daysCount;
        private Dictionary<DateTime, CalendarDay> _marks = new Dictionary<DateTime, CalendarDay>();
        private DateTime _marksFrom = DateTime.MaxValue;
        private DateTime _marksTo = DateTime.MinValue;

        public DateTime SelectedDate { get; private set; }

        public DatePickerForm(DateTime current, int daysCount)
        {
            _daysCount = daysCount;
            InitializeComponent();
            ApplyTheme();

            SelectedDate = current.Date;
            calendar.SelectionStart = SelectedDate;
            calendar.SelectionEnd = SelectedDate;

            LoadMarks();
            ShowInfo();
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(90);
            titleLabel.Font = Ui.F(24f, true);
            titleLabel.ForeColor = Ui.Accent;

            quickPanel.BackColor = Ui.Bg;
            quickPanel.Height = Ui.Px(100);
            quickPanel.Padding = new Padding(Ui.Px(20), Ui.Px(15), Ui.Px(20), Ui.Px(15));

            foreach (var button in new[] { todayButton, tomorrowButton, nextSchoolDayButton, prevMonthButton, nextMonthButton })
                Ui.TouchButton(button, Ui.Card, Ui.Text, 14f, false);

            todayButton.Width = Ui.Px(170);
            tomorrowButton.Width = Ui.Px(170);
            nextSchoolDayButton.Width = Ui.Px(280);
            prevMonthButton.Width = Ui.Px(90);
            nextMonthButton.Width = Ui.Px(90);

            // Календарь оставлен светлым нарочно: системный элемент не всегда
            // слушается перекраски, и «наполовину тёмный» выглядел бы поломкой.
            // Белый лист на тёмном фоне читается как лист бумаги.
            calendarCard.BackColor = Color.White;
            calendar.Font = Ui.F(16f);
            calendar.TitleBackColor = Ui.AccentDark;
            calendar.TitleForeColor = Color.White;
            calendar.TrailingForeColor = Color.Silver;

            infoPanel.BackColor = Ui.Bg;
            infoPanel.Height = Ui.Px(70);
            infoLabel.Font = Ui.F(15f, true);
            infoLabel.ForeColor = Ui.Muted;

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            Ui.PrimaryButton(okButton);
            cancelButton.Width = Ui.Px(180);
            okButton.Width = Ui.Px(300);

            ClientSize = new Size(Ui.Px(880), Ui.Px(800));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CenterCalendar();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterCalendar();
        }

        private void CenterCalendar()
        {
            calendar.Left = Math.Max(0, (calendarCard.ClientSize.Width - calendar.Width) / 2);
            calendar.Top = Math.Max(0, (calendarCard.ClientSize.Height - calendar.Height) / 2);
        }

        /// <summary>Пометки на видимый месяц и соседние — жирным на календаре.</summary>
        private void LoadMarks()
        {
            try
            {
                var from = new DateTime(calendar.SelectionStart.Year, calendar.SelectionStart.Month, 1).AddMonths(-1);
                var to = from.AddMonths(3);
                _marks = Repo.DaysBetween(from, to);
                _marksFrom = from;
                _marksTo = to;

                var bolded = new List<DateTime>();
                foreach (var pair in _marks)
                {
                    if (pair.Value.IsHoliday || pair.Value.Variant.HasValue) bolded.Add(pair.Key);
                }
                calendar.BoldedDates = bolded.ToArray();
            }
            catch
            {
                // Календарь без пометок всё ещё работает — дату выбрать можно.
                calendar.BoldedDates = new DateTime[0];
                _marksFrom = DateTime.MaxValue;
                _marksTo = DateTime.MinValue;
            }
        }

        private void CalendarDateChanged(object sender, DateRangeEventArgs e)
        {
            SelectedDate = calendar.SelectionStart.Date;

            // Стрелками самого календаря можно уехать далеко от загруженного
            // куска — тогда праздники там выглядели бы обычными днями.
            if (SelectedDate < _marksFrom || SelectedDate > _marksTo) LoadMarks();

            ShowInfo();
        }

        private void ShowInfo()
        {
            CalendarDay mark;
            _marks.TryGetValue(SelectedDate, out mark);

            var text = Ru.LongDate(SelectedDate);
            var color = Ui.Muted;

            if (mark != null && mark.IsHoliday)
            {
                text += "  ·  " + (string.IsNullOrWhiteSpace(mark.Title) ? "праздник, уроков нет" : mark.Title);
                color = Ui.Warn;
            }
            else if (mark != null && mark.Variant.HasValue)
            {
                text += "  ·  сетка: " + Variant.Name(mark.Variant.Value).ToLowerInvariant();
                color = Ui.Accent;
            }
            else if (Ru.Weekday(SelectedDate) > _daysCount)
            {
                text += "  ·  выходной";
                color = Ui.Warn;
            }

            infoLabel.Text = text;
            infoLabel.ForeColor = color;
        }

        private void Select(DateTime date)
        {
            SelectedDate = date.Date;
            calendar.SelectionStart = SelectedDate;
            calendar.SelectionEnd = SelectedDate;
            LoadMarks();
            ShowInfo();
        }

        private void TodayClicked(object sender, EventArgs e) { Select(DateTime.Today); }

        private void TomorrowClicked(object sender, EventArgs e) { Select(DateTime.Today.AddDays(1)); }

        private void NextSchoolDayClicked(object sender, EventArgs e)
        {
            Select(ScheduleResolver.NextSchoolDay(DateTime.Today.AddDays(1), _marks, _daysCount, 21));
        }

        private void PrevMonthClicked(object sender, EventArgs e) { Select(SelectedDate.AddMonths(-1)); }

        private void NextMonthClicked(object sender, EventArgs e) { Select(SelectedDate.AddMonths(1)); }

        private void OkClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                CancelClicked(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref message, keyData);
        }

        /// <summary>Спросить дату. null — отказались.</summary>
        public static DateTime? Ask(IWin32Window owner, DateTime current, int daysCount)
        {
            using (var form = new DatePickerForm(current, daysCount))
                return form.ShowDialog(owner) == DialogResult.OK ? form.SelectedDate : (DateTime?)null;
        }
    }
}
