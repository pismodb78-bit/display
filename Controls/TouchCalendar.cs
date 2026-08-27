using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Controls
{
    /// <summary>
    /// Календарь, нарисованный своими руками.
    ///
    /// Системный MonthCalendar на сенсорном экране не годится: он не тянется
    /// вместе с окном и остаётся размером со спичечный коробок — попасть
    /// пальцем в число невозможно. Этот занимает всё отведённое место, так что
    /// клетка получается с ноготь большого пальца и крупнее.
    ///
    /// Заодно видно то, чего в системном не показать: праздники залиты цветом,
    /// а дни с назначенной сеткой помечены точкой.
    /// </summary>
    public partial class TouchCalendar : UserControl
    {
        private DateTime _selected = DateTime.Today;
        private DateTime _month = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        /// <summary>Отметки календаря: праздники и дни со своей сеткой.</summary>
        public Dictionary<DateTime, CalendarDay> Marks { get; set; }

        /// <summary>Выбрали другое число.</summary>
        public event EventHandler DateChanged;

        /// <summary>Пролистали месяц — самое время подгрузить отметки.</summary>
        public event EventHandler MonthChanged;

        public TouchCalendar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Marks = new Dictionary<DateTime, CalendarDay>();
        }

        public DateTime SelectedDate
        {
            get { return _selected; }
            set
            {
                var date = value.Date;
                if (_selected == date) return;

                _selected = date;
                ShowMonthOf(date);
                Invalidate();
            }
        }

        /// <summary>Первое число показываемого месяца.</summary>
        public DateTime VisibleMonth
        {
            get { return _month; }
        }

        private void ShowMonthOf(DateTime date)
        {
            var month = new DateTime(date.Year, date.Month, 1);
            if (month == _month) return;

            _month = month;
            var handler = MonthChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void ShowMonth(int delta)
        {
            _month = _month.AddMonths(delta);

            var handler = MonthChanged;
            if (handler != null) handler(this, EventArgs.Empty);

            Invalidate();
        }

        // --- Разметка -----------------------------------------------------

        private int HeaderHeight { get { return Math.Max(Ui.Px(44), (int)(Height * 0.15)); } }

        private int WeekdayHeight { get { return Math.Max(Ui.Px(26), (int)(Height * 0.09)); } }

        private int CellWidth { get { return Width / 7; } }

        private int CellHeight { get { return Math.Max(1, (Height - HeaderHeight - WeekdayHeight) / 6); } }

        /// <summary>Понедельник недели, с которой начинается сетка месяца.</summary>
        private DateTime GridStart
        {
            get { return _month.AddDays(1 - Ru.Weekday(_month)); }
        }

        private Rectangle ArrowLeft
        {
            get { return new Rectangle(0, 0, Math.Max(Ui.Px(60), Width / 7), HeaderHeight); }
        }

        private Rectangle ArrowRight
        {
            get
            {
                var width = Math.Max(Ui.Px(60), Width / 7);
                return new Rectangle(Width - width, 0, width, HeaderHeight);
            }
        }

        // --- Отрисовка ----------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.Clear(Ui.Card);

            PaintHeader(graphics);
            PaintWeekdays(graphics);
            PaintDays(graphics);
        }

        private void PaintHeader(Graphics graphics)
        {
            var header = new Rectangle(0, 0, Width, HeaderHeight);
            using (var brush = new SolidBrush(Ui.Header))
                graphics.FillRectangle(brush, header);

            Ui.DrawCentered(graphics, Ru.MonthName(_month.Month) + " " + _month.Year,
                            Ui.Fp(HeaderHeight * 0.42f, true), Ui.Accent, header);

            PaintArrow(graphics, ArrowLeft, "◀");
            PaintArrow(graphics, ArrowRight, "▶");
        }

        private void PaintArrow(Graphics graphics, Rectangle bounds, string glyph)
        {
            using (var brush = new SolidBrush(Ui.CardLight))
                graphics.FillRectangle(brush, Rectangle.Inflate(bounds, -Ui.Px(6), -Ui.Px(6)));

            Ui.DrawCentered(graphics, glyph, Ui.Fp(bounds.Height * 0.38f, true), Ui.Text, bounds);
        }

        private void PaintWeekdays(Graphics graphics)
        {
            var top = HeaderHeight;
            var font = Ui.Fp(WeekdayHeight * 0.62f, true);

            for (int day = 1; day <= 7; day++)
            {
                var bounds = new Rectangle((day - 1) * CellWidth, top, CellWidth, WeekdayHeight);
                Ui.DrawCentered(graphics, Ru.DayShort(day), font, day >= 6 ? Ui.Warn : Ui.Muted, bounds);
            }
        }

        private void PaintDays(Graphics graphics)
        {
            var start = GridStart;
            var top = HeaderHeight + WeekdayHeight;
            var cellHeight = CellHeight;
            var cellWidth = CellWidth;

            var numberFont = Ui.Fp(cellHeight * 0.38f, true);
            var titleFont = Ui.Fp(cellHeight * 0.18f, false);

            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    var date = start.AddDays(row * 7 + column);
                    var bounds = new Rectangle(column * cellWidth, top + row * cellHeight, cellWidth, cellHeight);
                    PaintDay(graphics, bounds, date, numberFont, titleFont);
                }
            }
        }

        private void PaintDay(Graphics graphics, Rectangle bounds, DateTime date, Font numberFont, Font titleFont)
        {
            var outside = date.Month != _month.Month;
            var selected = date == _selected;

            CalendarDay mark = null;
            if (Marks != null) Marks.TryGetValue(date, out mark);

            var background = outside ? Ui.Bg : (date.Day % 2 == 0 ? Ui.RowEven : Ui.RowOdd);
            var foreground = outside ? Ui.Line : Ui.Text;

            if (mark != null && mark.IsHoliday && !outside)
            {
                background = Ui.WarnBg;
                foreground = Ui.Warn;
            }

            if (selected)
            {
                background = Ui.AccentDark;
                foreground = Ui.OnAccent;
            }

            var cell = Rectangle.Inflate(bounds, -Ui.Px(3), -Ui.Px(3));
            using (var brush = new SolidBrush(background))
                graphics.FillRectangle(brush, cell);

            // Сегодняшнее число обводим — на стене это самый частый вопрос.
            if (date == DateTime.Today && !selected)
            {
                using (var pen = new Pen(Ui.Accent, Ui.Px(3)))
                    graphics.DrawRectangle(pen, cell.X, cell.Y, cell.Width - 1, cell.Height - 1);
            }

            var hasTitle = mark != null && !string.IsNullOrWhiteSpace(mark.Title) && !outside;
            var numberRect = hasTitle
                ? new Rectangle(cell.X, cell.Y + (int)(cell.Height * 0.06), cell.Width, (int)(cell.Height * 0.56))
                : cell;

            Ui.DrawCentered(graphics, date.Day.ToString(), numberFont, foreground, numberRect);

            if (hasTitle)
            {
                var titleRect = new Rectangle(cell.X + Ui.Px(2), cell.Y + (int)(cell.Height * 0.6),
                                              cell.Width - Ui.Px(4), (int)(cell.Height * 0.34));
                Ui.DrawCentered(graphics, mark.Title, titleFont, selected ? Ui.OnAccent : Ui.Muted, titleRect);
            }

            // Точка — на этот день назначена своя сетка расписания.
            if (mark != null && mark.Variant.HasValue && !outside)
            {
                var size = Ui.Px(10);
                using (var brush = new SolidBrush(selected ? Ui.OnAccent : Ui.Accent))
                    graphics.FillEllipse(brush, cell.Right - size - Ui.Px(6), cell.Y + Ui.Px(6), size, size);
            }
        }

        // --- Нажатия ------------------------------------------------------

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (ArrowLeft.Contains(e.Location)) { ShowMonth(-1); return; }
            if (ArrowRight.Contains(e.Location)) { ShowMonth(1); return; }
            if (e.Y < HeaderHeight + WeekdayHeight) return;

            var column = e.X / Math.Max(1, CellWidth);
            var row = (e.Y - HeaderHeight - WeekdayHeight) / Math.Max(1, CellHeight);
            if (column < 0 || column > 6 || row < 0 || row > 5) return;

            var date = GridStart.AddDays(row * 7 + column);

            // Тычок в число соседнего месяца перелистывает туда — так же, как
            // это делает бумажный календарь под пальцем.
            _selected = date;
            ShowMonthOf(date);
            Invalidate();

            var handler = DateChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
