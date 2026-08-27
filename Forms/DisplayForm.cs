using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Экран в коридоре. Показывает либо неделю одного класса, либо один день
    /// всех классов, сам следит за временем и сам подхватывает правки учителя.
    ///
    /// Ключевая мысль: программа не хранит состояние «что показывать» у себя.
    /// Всё лежит в базе, а экран раз в несколько секунд спрашивает счётчик
    /// изменений. Учитель нажал «Применить» у себя в кабинете — телевизор
    /// переключился сам. Никто ни к чему не подходит и ничего не перезапускает.
    /// </summary>
    public partial class DisplayForm : Form, IMessageFilter
    {
        /// <summary>Что нарисовать в одной клетке. Кладётся в Tag ячейки.</summary>
        private sealed class CellInfo
        {
            public string Main;
            public string Sub;
            public bool NumberColumn;
            public bool Replacement;   // отличается от обычной сетки
            public bool Cancelled;     // урок был в обычной сетке, а в изменённой его нет
            public bool Holiday;
            public bool Today;
            public bool CurrentLesson;
        }

        private DisplaySettings _settings = DisplaySettings.From(null);
        private List<SchoolClass> _classes = new List<SchoolClass>();
        private List<LessonTime> _times = new List<LessonTime>();
        private Dictionary<DateTime, CalendarDay> _marks = new Dictionary<DateTime, CalendarDay>();

        private Dictionary<int, Lesson> _gridRegular = new Dictionary<int, Lesson>();
        private Dictionary<int, Lesson> _gridModified = new Dictionary<int, Lesson>();

        // Что показано сейчас. Пока экрана не касались, повторяет настройки из
        // базы; после касания живёт своей жизнью и возвращается к настройкам
        // через idle_seconds.
        private string _mode = DisplaySettings.ModeDay;
        private int _classId;
        private DateTime _date = DateTime.Today;
        private int _page;
        private bool _followSettings = true;
        private DateTime _lastTouch = DateTime.MinValue;

        private DayPlan _plan;
        private DayPlan[] _weekPlans = new DayPlan[0];
        private DateTime[] _weekDates = new DateTime[0];
        private List<SchoolClass> _pageClasses = new List<SchoolClass>();
        private int _rowLessons;
        private int _currentLessonNo;
        private bool _currentIsBreak;

        private long _revision = -1;
        private bool _schemaOk;
        private DateTime _shownFor = DateTime.MinValue;
        private string _tickerText = "";
        private float _tickerOffset;
        private int _headerTaps;
        private DateTime _headerTapAt = DateTime.MinValue;
        private bool _allowExit;
        private bool _offline;
        private bool _polling;

        public DisplayForm()
        {
            InitializeComponent();
            ApplyTheme();

            Ui.EnableDoubleBuffer(grid);
            Ui.EnableDoubleBuffer(tickerPanel);

            if (AppConfig.FullScreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                TopMost = false;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                ClientSize = new Size(Math.Min(1600, Screen.PrimaryScreen.WorkingArea.Width - 80),
                                      Math.Min(960, Screen.PrimaryScreen.WorkingArea.Height - 80));
            }

            pollTimer.Interval = AppConfig.RefreshSeconds * 1000;
            Application.AddMessageFilter(this);
        }

        // ===================== ОФОРМЛЕНИЕ =====================

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(110);
            schoolLabel.Font = Ui.F(24f, true);
            schoolLabel.ForeColor = Ui.Text;
            schoolLabel.Width = Ui.Px(460);
            schoolLabel.Padding = new Padding(Ui.Px(24), 0, 0, 0);

            dateLabel.Font = Ui.F(18f, true);
            dateLabel.ForeColor = Ui.Muted;

            variantLabel.Font = Ui.F(16f, true);
            variantLabel.Width = Ui.Px(380);

            statusLabel.Font = Ui.F(12f);
            statusLabel.ForeColor = Ui.Muted;
            statusLabel.Width = Ui.Px(240);

            clockLabel.Font = Ui.F(30f, true);
            clockLabel.ForeColor = Ui.Accent;
            clockLabel.Width = Ui.Px(220);

            subHeaderPanel.BackColor = Ui.Bg;
            subHeaderPanel.Height = Ui.Px(60);
            titleLabel.Font = Ui.F(20f, true);
            titleLabel.ForeColor = Ui.Text;

            Ui.StyleGrid(grid, true);
            grid.BackgroundColor = Ui.Bg;

            messageLabel.Font = Ui.F(30f, true);
            messageLabel.ForeColor = Ui.Muted;
            messageLabel.BackColor = Ui.Bg;

            tickerPanel.BackColor = Ui.Header;
            tickerPanel.Height = Ui.Px(56);

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(104);
            footerPanel.Padding = new Padding(Ui.Px(16), Ui.Px(10), Ui.Px(16), Ui.Px(10));

            navFlow.BackColor = Ui.Header;

            StyleNav(classButton, Ui.Px(240));
            StyleNav(modeButton, Ui.Px(240));
            StyleNav(prevButton, Ui.Px(100));
            StyleNav(todayButton, Ui.Px(180));
            StyleNav(nextButton, Ui.Px(100));
            StyleNav(calendarButton, Ui.Px(220));
            StyleNav(pagePrevButton, Ui.Px(80));
            StyleNav(pageNextButton, Ui.Px(80));

            pageLabel.Font = Ui.F(14f, true);
            pageLabel.ForeColor = Ui.Muted;
            pageLabel.Size = new Size(Ui.Px(120), Ui.Px(78));

            Ui.TouchButton(teacherButton, Ui.AccentDark, Ui.OnAccent, 14f, true);
            teacherButton.Width = Ui.Px(220);
        }

        private void StyleNav(Button button, int width)
        {
            Ui.TouchButton(button, Ui.Card, Ui.Text, 14f, false);
            button.Size = new Size(width, Ui.Px(78));
            button.Margin = new Padding(Ui.Px(3), Ui.Px(3), Ui.Px(3), Ui.Px(3));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Первое обращение к базе — тоже в фоне: окно должно появиться
            // сразу, даже если сервер выключен и подключение отвалится по
            // таймауту. Раньше программа замирала на старте до отказа.
            ShowProblem("Подключаюсь к базе…", Db.SafeDescription());
            BeginInvoke((MethodInvoker)delegate { PollTick(this, EventArgs.Empty); });
        }

        // ===================== ЗАГРУЗКА =====================

        /// <summary>Полное перечитывание: настройки, классы, звонки, сетка.</summary>
        private void ReloadAll()
        {
            try
            {
                if (!_schemaOk)
                {
                    string error;
                    _schemaOk = Schema.Ensure(out error);
                    if (!_schemaOk) { ShowFailure(error); return; }
                }

                _settings = DisplaySettings.From(Repo.Settings());
                ApplyThemeIfChanged();
                _classes = Repo.Classes();
                _times = Repo.LessonTimes();
                _revision = Repo.Revision();
                _offline = false;

                if (_followSettings) ApplySettingsView();

                LoadDay();
                Render();
                ArmRotation();
            }
            catch (Exception ex)
            {
                _schemaOk = false;
                ShowFailure(Db.Explain(ex));
            }
        }

        /// <summary>
        /// Учитель переключил тему — перекрашиваемся, не перезапускаясь.
        /// Тема из ip.txt сильнее: она задана для этого конкретного экрана.
        /// </summary>
        private void ApplyThemeIfChanged()
        {
            var theme = string.IsNullOrWhiteSpace(AppConfig.Theme)
                ? Ui.ParseTheme(_settings.Theme)
                : Ui.ParseTheme(AppConfig.Theme);

            if (theme == Ui.Theme) return;

            Ui.SetTheme(theme);
            ApplyTheme();
            Invalidate(true);
        }

        /// <summary>Вернуться к тому, что назначил учитель на вкладке «Показ».</summary>
        private void ApplySettingsView()
        {
            _mode = _settings.Mode;
            _classId = _settings.DisplayClassId;
            _date = _settings.EffectiveDate(DateTime.Now);
            _page = 0;

            if (_mode == DisplaySettings.ModeWeek && !HasClass(_classId) && _classes.Count > 0)
                _classId = _classes[0].Id;
        }

        private bool HasClass(int id)
        {
            return Repo.ClassById(_classes, id) != null;
        }

        /// <summary>Отметки календаря и обе сетки на показываемый день (или неделю).</summary>
        private void LoadDay()
        {
            var from = ScheduleResolver.MondayOf(_date).AddDays(-7);
            _marks = Repo.DaysBetween(from, from.AddDays(42));

            // «Ближайший учебный день» считается только когда экран живёт по
            // настройкам: если человек листает стрелками — он листает.
            if (_followSettings && _settings.DateMode == "next")
            {
                _date = ScheduleResolver.NextSchoolDay(DateTime.Today, _marks, _settings.DaysCount, 21);
                if (_date < from || _date > from.AddDays(42))
                    _marks = Repo.DaysBetween(ScheduleResolver.MondayOf(_date).AddDays(-7),
                                              ScheduleResolver.MondayOf(_date).AddDays(35));
            }

            _plan = ScheduleResolver.Resolve(_date, Mark(_date), _settings.ActiveVariant, _settings.DaysCount);
            ScheduleResolver.CurrentLesson(_times, DateTime.Now, out _currentLessonNo, out _currentIsBreak);

            if (_mode == DisplaySettings.ModeWeek)
            {
                if (HasClass(_classId))
                {
                    _gridRegular = Repo.WeekOfClass(Variant.Regular, _classId);
                    _gridModified = Repo.WeekOfClass(Variant.Modified, _classId);
                }
                else
                {
                    _gridRegular = new Dictionary<int, Lesson>();
                    _gridModified = new Dictionary<int, Lesson>();
                }
            }
            else
            {
                _gridRegular = Repo.DayOfAllClasses(Variant.Regular, _plan.Weekday);
                _gridModified = Repo.DayOfAllClasses(Variant.Modified, _plan.Weekday);
            }

            _shownFor = _settings.EffectiveDate(DateTime.Now);
        }

        private CalendarDay Mark(DateTime date)
        {
            CalendarDay mark;
            return _marks.TryGetValue(date.Date, out mark) ? mark : null;
        }

        // ===================== ОТРИСОВКА =====================

        private void Render()
        {
            UpdateHeader();
            UpdateTicker();
            UpdateNav();

            if (_classes.Count == 0)
            {
                ShowProblem("Классов пока нет",
                            "Нажмите «Учитель» и добавьте классы на вкладке «Классы»." + Environment.NewLine +
                            "После этого здесь появится расписание.");
                return;
            }

            if (_mode == DisplaySettings.ModeDay && _plan.IsHoliday)
            {
                var title = string.IsNullOrWhiteSpace(_plan.Title) ? "Праздник" : _plan.Title;
                ShowProblem(title, Ru.LongDate(_date) + " — уроков нет");
                return;
            }

            if (_mode == DisplaySettings.ModeDay && !_plan.IsSchoolDay)
            {
                ShowProblem("Выходной", Ru.LongDate(_date) + " — уроков нет");
                return;
            }

            messageLabel.Visible = false;
            grid.Visible = true;

            if (_mode == DisplaySettings.ModeWeek) BuildWeekGrid();
            else BuildDayGrid();

            LayoutGrid();
            grid.ClearSelection();
            UpdateStatus();
        }

        /// <summary>
        /// База не отвечает. Отдельно от обычных сообщений вроде «праздник»:
        /// после сбоя экран обязан перечитать всё, как только связь вернётся,
        /// а в праздник дёргать сервер каждые десять секунд незачем.
        /// </summary>
        private void ShowFailure(string details)
        {
            ShowFailure(details, null);
        }

        /// <summary>
        /// Заголовок подбираем по делу: «нет связи» и «не хватает прав» —
        /// разные беды, и человеку у экрана важно, какая именно.
        /// </summary>
        private void ShowFailure(string details, string title)
        {
            _offline = true;

            if (title == null)
                title = Db.IsOnline || (details ?? "").Contains("прав") ? "База не отвечает как надо" : "Нет связи с базой";

            ShowProblem(title, details);
        }

        private void ShowProblem(string title, string details)
        {
            var text = title + Environment.NewLine + Environment.NewLine + details;

            // Сообщения бывают и в одну строку («Праздник»), и на три строки
            // с ответом сервера. Крупный шрифт для длинного текста означал бы
            // обрезанные по краям слова, поэтому размер выбираем по длине.
            var length = (details ?? "").Length;
            messageLabel.Font = Ui.F(length > 160 ? 15f : length > 80 ? 20f : 28f, true);

            messageLabel.Text = text;
            messageLabel.Bounds = ContentRect();
            messageLabel.Visible = true;
            messageLabel.BringToFront();
            grid.Visible = false;

            UpdateHeader();
            UpdateStatus();
            UpdateNav();
        }

        private void UpdateHeader()
        {
            schoolLabel.Text = _settings.SchoolName;
            clockLabel.Text = DateTime.Now.ToString("HH:mm");

            var relative = Ru.Relative(_date);
            var suffix = relative == "сегодня" || relative == "завтра" || relative == "послезавтра"
                       ? "  ·  " + relative
                       : "";

            dateLabel.Text = _mode == DisplaySettings.ModeWeek
                ? WeekTitle()
                : Ru.LongDate(_date) + suffix;

            var variant = _plan != null ? _plan.Variant : _settings.ActiveVariant;
            if (variant == Variant.Modified)
            {
                variantLabel.Text = "ИЗМЕНЁННОЕ РАСПИСАНИЕ";
                variantLabel.ForeColor = Ui.Warn;
            }
            else
            {
                variantLabel.Text = "";
                variantLabel.ForeColor = Ui.Muted;
            }

            var title = _mode == DisplaySettings.ModeWeek
                ? ClassName(_classId) + "  ·  расписание на неделю"
                : "Все классы  ·  " + Ru.DayName(_plan != null ? _plan.Weekday : Ru.Weekday(_date));

            if (_plan != null && !string.IsNullOrWhiteSpace(_plan.Title) && !_plan.IsHoliday)
                title += "  ·  " + _plan.Title;

            titleLabel.Text = title;
        }

        /// <summary>«24 — 29 августа 2026»: месяц и год не повторяем, если они общие.</summary>
        private string WeekTitle()
        {
            var from = ScheduleResolver.MondayOf(_date);
            var to = from.AddDays(_settings.DaysCount - 1);

            if (from.Month == to.Month && from.Year == to.Year)
                return "Неделя " + from.Day + " — " + to.Day + " " + Ru.MonthGenitive(to.Month) + " " + to.Year;

            if (from.Year == to.Year)
                return "Неделя " + from.Day + " " + Ru.MonthGenitive(from.Month) + " — " + Ru.Date(to);

            return "Неделя " + Ru.Date(from) + " — " + Ru.Date(to);
        }

        private string ClassName(int id)
        {
            var found = Repo.ClassById(_classes, id);
            return found != null ? found.Name : "Класс не выбран";
        }

        private void UpdateStatus()
        {
            if (Db.IsOnline)
            {
                statusLabel.Text = "● база на связи";
                statusLabel.ForeColor = Ui.Ok;
            }
            else
            {
                statusLabel.Text = "● нет связи с базой";
                statusLabel.ForeColor = Ui.Danger;
            }
        }

        private void UpdateNav()
        {
            classButton.Text = _mode == DisplaySettings.ModeWeek ? "Класс: " + ClassName(_classId) : "Выбрать класс";
            modeButton.Text = _mode == DisplaySettings.ModeWeek ? "Показать день" : "Показать неделю";
            todayButton.Text = _date.Date == DateTime.Today ? "Сегодня" : "К сегодня";

            var pages = PageCount();
            var paged = _mode == DisplaySettings.ModeDay && pages > 1;

            pagePrevButton.Visible = paged;
            pageNextButton.Visible = paged;
            pageLabel.Visible = paged;
            if (paged) pageLabel.Text = (_page + 1) + " / " + pages;
        }

        private int PageCount()
        {
            if (_classes.Count == 0) return 1;
            return (_classes.Count + _settings.ClassesPerPage - 1) / _settings.ClassesPerPage;
        }

        private void BuildWeekGrid()
        {
            var days = _settings.DaysCount;
            var monday = ScheduleResolver.MondayOf(_date);

            _weekDates = new DateTime[days];
            _weekPlans = new DayPlan[days];
            for (int i = 0; i < days; i++)
            {
                _weekDates[i] = monday.AddDays(i);
                _weekPlans[i] = ScheduleResolver.Resolve(_weekDates[i], Mark(_weekDates[i]),
                                                         _settings.ActiveVariant, days);
            }

            // Строк ровно столько, сколько стоит в «Показ → уроков». Раньше
            // пустые строки снизу отбрасывались, и в день без уроков сетка
            // сжималась до четырёх строк, хотя в настройках было восемь —
            // человек у экрана видел не то, что задавал.
            _rowLessons = _settings.LessonsCount;

            grid.Columns.Clear();
            AddColumn("Урок", "");

            for (int i = 0; i < days; i++)
                AddColumn(Ru.DayName(i + 1), _weekDates[i].Day + " " + MonthShort(_weekDates[i]));

            grid.Rows.Clear();
            for (int no = 1; no <= _rowLessons; no++)
            {
                var index = grid.Rows.Add();
                grid.Rows[index].Cells[0].Tag = NumberCell(no);

                for (int i = 0; i < days; i++)
                {
                    var plan = _weekPlans[i];
                    var key = Repo.Key(i + 1, no);

                    grid.Rows[index].Cells[i + 1].Tag = plan.IsHoliday
                        ? HolidayCell(plan, no)
                        : LessonCell(plan, LessonAt(plan.Variant, key), LessonAt(Variant.Regular, key),
                                     no, _weekDates[i]);
                }
            }
        }

        private void BuildDayGrid()
        {
            _pageClasses = PageClasses();

            _rowLessons = _settings.LessonsCount;

            grid.Columns.Clear();
            AddColumn("Урок", "");
            foreach (var item in _pageClasses) AddColumn(item.Name, "");

            grid.Rows.Clear();
            for (int no = 1; no <= _rowLessons; no++)
            {
                var index = grid.Rows.Add();
                grid.Rows[index].Cells[0].Tag = NumberCell(no);

                for (int i = 0; i < _pageClasses.Count; i++)
                {
                    var key = Repo.Key(_pageClasses[i].Id, no);
                    grid.Rows[index].Cells[i + 1].Tag =
                        LessonCell(_plan, LessonAt(_plan.Variant, key), LessonAt(Variant.Regular, key), no, _date);
                }
            }
        }

        private List<SchoolClass> PageClasses()
        {
            var perPage = _settings.ClassesPerPage;
            var pages = PageCount();
            if (_page >= pages) _page = 0;

            var result = new List<SchoolClass>();
            for (int i = _page * perPage; i < Math.Min(_classes.Count, (_page + 1) * perPage); i++)
                result.Add(_classes[i]);

            return result;
        }

        private Lesson LessonAt(int variant, int key)
        {
            var source = variant == Variant.Modified ? _gridModified : _gridRegular;
            Lesson lesson;
            return source.TryGetValue(key, out lesson) && !lesson.IsEmpty ? lesson : null;
        }

        private void AddColumn(string main, string sub)
        {
            var column = new DataGridViewTextBoxColumn
            {
                HeaderText = string.IsNullOrEmpty(sub) ? main : main + " " + sub,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Resizable = DataGridViewTriState.False,
                Tag = new[] { main, sub }
            };
            grid.Columns.Add(column);
        }

        private CellInfo NumberCell(int no)
        {
            var info = new CellInfo { Main = no.ToString(), NumberColumn = true, CurrentLesson = IsCurrent(no) };

            foreach (var time in _times)
            {
                if (time.No == no) { info.Sub = Ru.Time(time.Start) + "–" + Ru.Time(time.End); break; }
            }
            return info;
        }

        private bool IsCurrent(int no)
        {
            return _date.Date == DateTime.Today && _currentLessonNo == no && _currentLessonNo > 0;
        }

        private CellInfo HolidayCell(DayPlan plan, int no)
        {
            return new CellInfo
            {
                Holiday = true,
                Main = no == 1 ? (string.IsNullOrWhiteSpace(plan.Title) ? "Праздник" : plan.Title) : "",
                Sub = no == 1 ? "уроков нет" : ""
            };
        }

        private CellInfo LessonCell(DayPlan plan, Lesson lesson, Lesson regular, int no, DateTime date)
        {
            var info = new CellInfo
            {
                Today = date.Date == DateTime.Today,
                CurrentLesson = IsCurrent(no) && date.Date == DateTime.Today
            };

            // Замену показываем, только когда на этот день включена изменённая
            // сетка: в обычные дни оранжевые клетки сбивали бы с толку.
            var comparing = _settings.ShowReplacements && plan.Variant == Variant.Modified;

            if (lesson == null)
            {
                if (comparing && regular != null)
                {
                    info.Cancelled = true;
                    info.Main = "нет урока";
                    info.Sub = "вместо: " + regular.Subject;
                }
                return info;
            }

            info.Main = lesson.Subject;
            info.Sub = Details(lesson);

            if (comparing && !lesson.SameAs(regular))
            {
                info.Replacement = true;
                if (regular != null && !string.IsNullOrWhiteSpace(regular.Subject) &&
                    !string.Equals(regular.Subject, lesson.Subject, StringComparison.OrdinalIgnoreCase))
                {
                    info.Sub = string.IsNullOrEmpty(info.Sub)
                        ? "замена: " + regular.Subject
                        : info.Sub + "   ·   замена: " + regular.Subject;
                }
            }

            return info;
        }

        private static string Details(Lesson lesson)
        {
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(lesson.Teacher)) parts.Add(lesson.Teacher.Trim());
            if (!string.IsNullOrWhiteSpace(lesson.Room)) parts.Add("каб. " + lesson.Room.Trim());
            return string.Join("   ·   ", parts);
        }

        private static string MonthShort(DateTime date)
        {
            return Ru.MonthGenitive(date.Month);
        }

        /// <summary>Растянуть таблицу по экрану: ничего не должно прокручиваться.</summary>
        private void LayoutGrid()
        {
            if (grid.Columns.Count == 0 || grid.Rows.Count == 0) return;

            var height = grid.ClientSize.Height;
            var width = grid.ClientSize.Width;
            if (height <= 0 || width <= 0) return;

            var headerHeight = Math.Max(Ui.Px(56), Math.Min(Ui.Px(110), height / 9));
            grid.ColumnHeadersHeight = headerHeight;

            var rowHeight = Math.Max(Ui.Px(44), (height - headerHeight) / grid.Rows.Count);
            foreach (DataGridViewRow row in grid.Rows) row.Height = rowHeight;

            // Если строки всё-таки не влезли (много уроков на маленьком окне),
            // включаем прокрутку — лучше так, чем обрезать последний урок.
            var overflow = headerHeight + rowHeight * grid.Rows.Count > height;
            grid.ScrollBars = overflow ? ScrollBars.Vertical : ScrollBars.None;
            if (overflow) width -= SystemInformation.VerticalScrollBarWidth;

            var first = Math.Max(Ui.Px(120), Math.Min(Ui.Px(190), width / 9));
            grid.Columns[0].Width = first;

            var rest = grid.Columns.Count - 1;
            if (rest <= 0) return;

            var each = (width - first) / rest;
            for (int i = 1; i < grid.Columns.Count; i++)
                grid.Columns[i].Width = i == grid.Columns.Count - 1 ? width - first - each * (rest - 1) : each;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (grid.Visible) LayoutGrid();
            if (messageLabel.Visible) messageLabel.Bounds = ContentRect();
        }

        /// <summary>
        /// Место между шапкой и нижними кнопками — там, где обычно таблица.
        /// Считаем сами, а не берём границы таблицы: когда она спрятана,
        /// раскладка её не трогает и размеры остаются старыми.
        /// </summary>
        private Rectangle ContentRect()
        {
            var top = subHeaderPanel.Bottom;
            var bottom = tickerPanel.Visible ? tickerPanel.Top : footerPanel.Top;
            if (bottom <= top) bottom = ClientSize.Height;

            return Rectangle.FromLTRB(0, top, ClientSize.Width, bottom);
        }

        // ===================== РИСОВАНИЕ ЯЧЕЕК =====================

        private void GridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            if (e.RowIndex < 0)
            {
                PaintHeader(e.Graphics, e.CellBounds, e.ColumnIndex);
                e.Handled = true;
                return;
            }

            var info = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as CellInfo;
            PaintCell(e.Graphics, e.CellBounds, info, e.RowIndex, e.ColumnIndex);
            e.Handled = true;
        }

        private void PaintHeader(Graphics graphics, Rectangle bounds, int columnIndex)
        {
            var parts = grid.Columns[columnIndex].Tag as string[];
            var main = parts != null ? parts[0] : grid.Columns[columnIndex].HeaderText;
            var sub = parts != null ? parts[1] : "";

            var today = _mode == DisplaySettings.ModeWeek
                     && columnIndex > 0
                     && columnIndex - 1 < _weekDates.Length
                     && _weekDates[columnIndex - 1].Date == DateTime.Today;

            using (var brush = new SolidBrush(today ? Ui.AccentDark : Ui.GridHeader))
                graphics.FillRectangle(brush, bounds);

            using (var pen = new Pen(Ui.Line))
                graphics.DrawLine(pen, bounds.Right - 1, bounds.Top + 6, bounds.Right - 1, bounds.Bottom - 6);

            var hasSub = !string.IsNullOrEmpty(sub);
            var mainFont = Ui.Fp(bounds.Height * (hasSub ? 0.36f : 0.44f), true);
            var mainRect = hasSub
                ? new Rectangle(bounds.X, bounds.Y + (int)(bounds.Height * 0.06), bounds.Width, (int)(bounds.Height * 0.55))
                : bounds;

            Ui.DrawCentered(graphics, main, mainFont, today ? Color.White : Ui.Accent, mainRect);

            if (hasSub)
            {
                var subRect = new Rectangle(bounds.X, bounds.Y + (int)(bounds.Height * 0.58), bounds.Width,
                                            (int)(bounds.Height * 0.36));
                Ui.DrawCentered(graphics, sub, Ui.Fp(bounds.Height * 0.26f, false),
                                today ? Color.White : Ui.Muted, subRect);
            }
        }

        private void PaintCell(Graphics graphics, Rectangle bounds, CellInfo info, int rowIndex, int columnIndex)
        {
            var background = rowIndex % 2 == 0 ? Ui.RowOdd : Ui.RowEven;
            var foreground = Ui.Text;
            var subColor = Ui.Muted;

            if (info != null)
            {
                if (info.NumberColumn) { background = Ui.GridHeader; foreground = Ui.Accent; }
                if (info.Holiday) { background = Ui.WarnBg; foreground = Ui.Warn; }
                if (info.Replacement) { background = Ui.WarnBg; foreground = Ui.Warn; subColor = Ui.Warn; }
                if (info.Cancelled) { background = Blend(Ui.WarnBg, Ui.Bg, 0.55); foreground = Ui.Muted; }
                if (info.CurrentLesson) background = Blend(background, Ui.Accent, 0.22);
            }

            using (var brush = new SolidBrush(background))
                graphics.FillRectangle(brush, bounds);

            using (var pen = new Pen(Ui.Line))
            {
                graphics.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
                graphics.DrawLine(pen, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom);
            }

            if (info == null) return;

            // Оранжевая полоска слева — чтобы замену было видно и боковым зрением,
            // и на плохо откалиброванном телевизоре, где цвета «плывут».
            if (info.Replacement || info.Cancelled)
            {
                using (var brush = new SolidBrush(Ui.Warn))
                    graphics.FillRectangle(brush, new Rectangle(bounds.Left, bounds.Top, Ui.Px(6), bounds.Height));
            }

            if (info.CurrentLesson && info.NumberColumn)
            {
                using (var brush = new SolidBrush(Ui.Accent))
                    graphics.FillRectangle(brush, new Rectangle(bounds.Left, bounds.Top, Ui.Px(6), bounds.Height));
            }

            if (string.IsNullOrEmpty(info.Main) && string.IsNullOrEmpty(info.Sub)) return;

            var hasSub = !string.IsNullOrEmpty(info.Sub);
            var pad = Ui.Px(8);
            var inner = new Rectangle(bounds.X + pad, bounds.Y + Ui.Px(2), bounds.Width - pad * 2, bounds.Height - Ui.Px(4));

            if (info.NumberColumn)
            {
                var numberRect = new Rectangle(inner.X, inner.Y + (int)(inner.Height * 0.05), inner.Width,
                                               (int)(inner.Height * 0.5));
                Ui.DrawCentered(graphics, info.Main, Ui.Fp(inner.Height * 0.42f, true), foreground, numberRect);

                if (hasSub)
                {
                    var timeRect = new Rectangle(inner.X, inner.Y + (int)(inner.Height * 0.55), inner.Width,
                                                 (int)(inner.Height * 0.4));
                    Ui.DrawCentered(graphics, info.Sub, Ui.Fp(inner.Height * 0.2f, false), Ui.Muted, timeRect);
                }
                return;
            }

            if (!hasSub)
            {
                Ui.DrawCentered(graphics, info.Main, Ui.Fp(inner.Height * 0.34f, true), foreground, inner);
                return;
            }

            var mainRect = new Rectangle(inner.X, inner.Y + (int)(inner.Height * 0.06), inner.Width,
                                         (int)(inner.Height * 0.52));
            var detailRect = new Rectangle(inner.X, inner.Y + (int)(inner.Height * 0.58), inner.Width,
                                           (int)(inner.Height * 0.36));

            Ui.DrawCentered(graphics, info.Main, Ui.Fp(inner.Height * 0.32f, true), foreground, mainRect);
            Ui.DrawCentered(graphics, info.Sub, Ui.Fp(inner.Height * 0.21f, false), subColor, detailRect);
        }

        private static Color Blend(Color from, Color to, double amount)
        {
            return Color.FromArgb(
                (int)(from.R + (to.R - from.R) * amount),
                (int)(from.G + (to.G - from.G) * amount),
                (int)(from.B + (to.B - from.B) * amount));
        }

        private void GridSelectionChanged(object sender, EventArgs e)
        {
            // Выделение на витрине не нужно: палец всё равно попадает по ячейке.
            if (grid.SelectedCells.Count > 0) grid.ClearSelection();
        }

        /// <summary>Тычок в колонку переключает вид: день класса ⇄ неделя класса.</summary>
        private void GridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            Touched();
            if (e.ColumnIndex <= 0) return;

            if (_mode == DisplaySettings.ModeDay)
            {
                var index = e.ColumnIndex - 1;
                if (index >= _pageClasses.Count) return;

                _classId = _pageClasses[index].Id;
                _mode = DisplaySettings.ModeWeek;
            }
            else
            {
                var index = e.ColumnIndex - 1;
                if (index >= _weekDates.Length) return;

                _date = _weekDates[index];
                _mode = DisplaySettings.ModeDay;
            }

            Redraw(true);
        }

        // ===================== БЕГУЩАЯ СТРОКА =====================

        private void UpdateTicker()
        {
            var text = (_settings.Ticker ?? "").Trim();
            if (text != _tickerText)
            {
                _tickerText = text;
                _tickerOffset = 0;
            }

            var visible = _tickerText.Length > 0;
            tickerPanel.Visible = visible;
            tickerTimer.Enabled = visible;
            tickerPanel.Invalidate();
        }

        private void TickerTick(object sender, EventArgs e)
        {
            _tickerOffset += Ui.Scale * 2f;
            tickerPanel.Invalidate();
        }

        private void TickerPaint(object sender, PaintEventArgs e)
        {
            if (_tickerText.Length == 0) return;

            var font = Ui.Fp(tickerPanel.Height * 0.5f, true);
            var size = e.Graphics.MeasureString(_tickerText, font);
            var top = (tickerPanel.Height - size.Height) / 2f;

            using (var brush = new SolidBrush(Ui.Warn))
            {
                // Короткое объявление стоит на месте: бегущая строка из трёх
                // слов только раздражает. Длинное — едет справа налево.
                if (size.Width <= tickerPanel.Width - Ui.Px(40))
                {
                    tickerTimer.Enabled = false;
                    e.Graphics.DrawString(_tickerText, font, brush, (tickerPanel.Width - size.Width) / 2f, top);
                    return;
                }

                var span = size.Width + tickerPanel.Width;
                if (_tickerOffset > span) _tickerOffset = 0;

                e.Graphics.DrawString(_tickerText, font, brush, tickerPanel.Width - _tickerOffset, top);
            }
        }

        // ===================== КНОПКИ =====================

        private void Touched()
        {
            _lastTouch = DateTime.Now;
            _followSettings = false;
            rotateTimer.Enabled = false;
        }

        /// <summary>Перерисовать. reloadData — сходить в базу за сеткой заново.</summary>
        private void Redraw(bool reloadData)
        {
            // Пока база молчит, в неё не ходим: каждое нажатие стрелки иначе
            // упирается в таймаут подключения, и экран замирает на секунды.
            // Опрос в фоне сам заметит, когда связь вернётся.
            if (_offline)
            {
                UpdateHeader();
                UpdateNav();
                return;
            }

            try
            {
                if (reloadData) LoadDay();
                Render();
            }
            catch (Exception ex)
            {
                ShowFailure(Db.Explain(ex));
            }
        }

        private void ClassClicked(object sender, EventArgs e)
        {
            Touched();
            using (var picker = new ClassPickerForm(_classes, _classId))
            {
                if (picker.ShowDialog(this) != DialogResult.OK) return;

                if (picker.SelectedClassId == 0)
                {
                    _mode = DisplaySettings.ModeDay;
                }
                else
                {
                    _classId = picker.SelectedClassId;
                    _mode = DisplaySettings.ModeWeek;
                }
            }
            Redraw(true);
        }

        private void ModeClicked(object sender, EventArgs e)
        {
            Touched();
            if (_mode == DisplaySettings.ModeWeek) _mode = DisplaySettings.ModeDay;
            else
            {
                _mode = DisplaySettings.ModeWeek;
                if (!HasClass(_classId) && _classes.Count > 0) _classId = _classes[0].Id;
            }
            Redraw(true);
        }

        private void PrevClicked(object sender, EventArgs e)
        {
            Touched();
            _date = _date.AddDays(_mode == DisplaySettings.ModeWeek ? -7 : -1);
            Redraw(true);
        }

        private void NextClicked(object sender, EventArgs e)
        {
            Touched();
            _date = _date.AddDays(_mode == DisplaySettings.ModeWeek ? 7 : 1);
            Redraw(true);
        }

        private void TodayClicked(object sender, EventArgs e)
        {
            Touched();
            _date = DateTime.Today;
            Redraw(true);
        }

        private void CalendarClicked(object sender, EventArgs e)
        {
            Touched();
            var picked = DatePickerForm.Ask(this, _date, _settings.DaysCount);
            if (!picked.HasValue) return;

            _date = picked.Value;
            Redraw(true);
        }

        private void PagePrevClicked(object sender, EventArgs e)
        {
            Touched();
            _page = (_page - 1 + PageCount()) % PageCount();
            Redraw(false);
        }

        private void PageNextClicked(object sender, EventArgs e)
        {
            Touched();
            _page = (_page + 1) % PageCount();
            Redraw(false);
        }

        /// <summary>Три быстрых касания шапки — тот же вход в режим учителя.</summary>
        private void AnyTouch(object sender, MouseEventArgs e)
        {
            Touched();

            if ((DateTime.Now - _headerTapAt).TotalSeconds > 2) _headerTaps = 0;
            _headerTapAt = DateTime.Now;
            _headerTaps++;

            if (_headerTaps >= 3)
            {
                _headerTaps = 0;
                TeacherClicked(sender, EventArgs.Empty);
            }
        }

        private void TeacherClicked(object sender, EventArgs e)
        {
            Touched();

            if (!PasswordForm.Ask(this, _settings.AdminPasswordHash)) return;

            // Если базы нет, редактор открывать бессмысленно — человеку нужен
            // не он, а окно подключения. Ведём сразу туда.
            if (_offline || !Db.IsOnline)
            {
                using (var connection = new ConnectionForm()) connection.ShowDialog(this);

                _schemaOk = false;
                ReloadAll();

                // Связь наладилась — открываем то, ради чего и нажимали
                // «Учитель», а не оставляем человека перед пустым экраном.
                if (_offline) return;
            }

            bool exit;
            using (var editor = new EditorForm())
            {
                editor.ShowDialog(this);
                exit = editor.ExitRequested;
            }

            _followSettings = true;
            _schemaOk = false;
            ReloadAll();

            if (exit)
            {
                _allowExit = true;
                Close();
            }
        }

        // ===================== ТАЙМЕРЫ =====================

        private void ClockTick(object sender, EventArgs e)
        {
            clockLabel.Text = DateTime.Now.ToString("HH:mm");

            // Вернуться к показу «по умолчанию», когда от экрана отошли.
            if (!_followSettings && (DateTime.Now - _lastTouch).TotalSeconds > _settings.IdleSeconds)
            {
                _followSettings = true;
                ApplySettingsView();
                Redraw(true);
                ArmRotation();
                return;
            }

            // Полночь и порог «после уроков показывать завтра» — оба меняют
            // дату показа без всякого участия человека.
            if (_followSettings && _settings.EffectiveDate(DateTime.Now).Date != _shownFor.Date)
            {
                ApplySettingsView();
                Redraw(true);
                return;
            }

            // Подсветка идущего урока живёт своей жизнью и меняется по звонку.
            int no;
            bool isBreak;
            ScheduleResolver.CurrentLesson(_times, DateTime.Now, out no, out isBreak);
            if (no != _currentLessonNo || isBreak != _currentIsBreak)
            {
                _currentLessonNo = no;
                _currentIsBreak = isBreak;
                if (_date.Date == DateTime.Today && grid.Visible) Redraw(false);
            }
        }

        /// <summary>
        /// Сердце «горячей замены»: спрашиваем у базы одно число — счётчик
        /// изменений. Выросло — значит расписание правили, и экран
        /// перечитывает всё заново. Никаких перезапусков.
        ///
        /// Запрос уходит в фоновый поток нарочно: когда сервер выключен,
        /// подключение отваливается по таймауту несколько секунд, и делать
        /// это в главном потоке — значит останавливать часы и не пускать
        /// пальцы к кнопкам каждые десять секунд.
        /// </summary>
        private void PollTick(object sender, EventArgs e)
        {
            pollTimer.Interval = Math.Max(2000, AppConfig.RefreshSeconds * 1000);
            if (_polling) return;

            if (AppConfig.ReloadIfChanged())
            {
                // Поменяли ip.txt — переподключаемся к новому серверу.
                Db.Reconfigure();
                _schemaOk = false;
            }

            _polling = true;
            var probe = System.Threading.Tasks.Task.Run(new Func<object>(Probe));

            probe.ContinueWith(delegate (System.Threading.Tasks.Task<object> finished)
            {
                if (IsDisposed || !IsHandleCreated) return;

                try
                {
                    BeginInvoke((MethodInvoker)delegate { PollFinished(finished.Result); });
                }
                catch (ObjectDisposedException) { /* окно уже закрыли */ }
            });
        }

        /// <summary>Фоновая часть опроса. Возвращает номер ревизии или исключение.</summary>
        private object Probe()
        {
            try
            {
                if (!_schemaOk)
                {
                    string error;
                    if (!Schema.Ensure(out error)) return new InvalidOperationException(error);
                    _schemaOk = true;
                }
                return Repo.Revision();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private void PollFinished(object result)
        {
            _polling = false;

            var failure = result as Exception;
            if (failure != null)
            {
                _schemaOk = false;
                ShowFailure(Db.Explain(failure));
                return;
            }

            var revision = Convert.ToInt64(result);

            // _offline — база только что вернулась: перечитываем, даже если
            // ревизия не менялась, иначе на экране навсегда останется ошибка.
            if (revision != _revision || _offline)
            {
                _revision = revision;
                ReloadAll();
                ArmRotation();
            }
            else
            {
                UpdateStatus();
            }
        }

        private void ArmRotation()
        {
            var rotate = _settings.AutoRotate && _followSettings;
            rotateTimer.Interval = Math.Max(5000, _settings.RotateSeconds * 1000);
            rotateTimer.Enabled = rotate;
        }

        /// <summary>Автолистание: страницы классов или классы по очереди.</summary>
        private void RotateTick(object sender, EventArgs e)
        {
            if (!_settings.AutoRotate || !_followSettings || _classes.Count == 0) return;

            if (_mode == DisplaySettings.ModeDay)
            {
                if (PageCount() <= 1) return;
                _page = (_page + 1) % PageCount();
                Redraw(false);
            }
            else
            {
                var index = _classes.FindIndex(c => c.Id == _classId);
                _classId = _classes[(index + 1 + _classes.Count) % _classes.Count].Id;
                Redraw(true);
            }
        }

        // ===================== КИОСК =====================

        /// <summary>Любое касание экрана продлевает «ручной» режим.</summary>
        public bool PreFilterMessage(ref Message message)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_TOUCH = 0x0240;
            const int WM_POINTERDOWN = 0x0246;

            if (message.Msg == WM_LBUTTONDOWN || message.Msg == WM_TOUCH || message.Msg == WM_POINTERDOWN)
                _lastTouch = DateTime.Now;

            return false;
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            // Аварийный выход с телевизора, где нет ни мыши, ни диспетчера задач.
            if (keyData == (Keys.Control | Keys.Shift | Keys.Q))
            {
                if (PasswordForm.Ask(this, _settings.AdminPasswordHash))
                {
                    _allowExit = true;
                    Close();
                }
                return true;
            }

            if (keyData == Keys.F5)
            {
                _schemaOk = false;
                ReloadAll();
                return true;
            }

            return base.ProcessCmdKey(ref message, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // На стене окно закрывать нечем и незачем: случайное Alt+F4 не
            // должно оставить коридор с пустым экраном до прихода учителя.
            if (!_allowExit && AppConfig.FullScreen &&
                e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            Application.RemoveMessageFilter(this);
            base.OnFormClosing(e);
        }
    }
}
