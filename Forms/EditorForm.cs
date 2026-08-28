using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Окно учителя: классы, обе сетки расписания, звонки, календарь и
    /// управление тем, что висит на экране.
    ///
    /// Сюда пускают по паролю, а всё, что здесь меняется, сразу уходит в базу.
    /// Телевизор увидит правки сам — заходить в коридор и перезапускать
    /// программу не нужно.
    /// </summary>
    public partial class EditorForm : Form
    {
        private int _variant = Variant.Regular;
        private int _classId;
        private List<SchoolClass> _classes = new List<SchoolClass>();
        private List<LessonTime> _times = new List<LessonTime>();
        private DisplaySettings _settings = DisplaySettings.From(null);
        private Dictionary<int, Lesson> _gridCurrent = new Dictionary<int, Lesson>();
        private Dictionary<int, Lesson> _gridOther = new Dictionary<int, Lesson>();
        private bool _loading;
        private bool _displayDirty;

        /// <summary>Учитель нажал «Выйти из программы».</summary>
        public bool ExitRequested { get; private set; }

        public EditorForm()
        {
            InitializeComponent();
            ApplyTheme();

            // Полоску вкладок Windows красит по-своему и BackColor не слушает:
            // на тёмной теме над тёмной страницей висела бы светло-серая
            // гребёнка. Рисуем её сами.
            tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabs.DrawItem += TabsDrawItem;

            // Экранная клавиатура одна на всё окно: всплывает под тем полем,
            // в которое ткнули пальцем, и прячется, когда ввод закончен.
            HookTextBoxes(this);
            HookDisplayChanges();
            editorKeyboard.EnterPressed += delegate { editorKeyboard.Visible = false; };

            // Вкладки перечитывают данные при переходе: класс, добавленный на
            // соседней вкладке, должен появиться в списках сразу.
            tabs.SelectedIndexChanged += TabChanged;

            LoadAll();
        }

        // ===================== ОФОРМЛЕНИЕ =====================

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;
            Font = Ui.F(13f);

            if (AppConfig.FullScreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                ClientSize = new Size(Math.Min(Ui.Px(1500), Screen.PrimaryScreen.WorkingArea.Width - 60),
                                      Math.Min(Ui.Px(960), Screen.PrimaryScreen.WorkingArea.Height - 60));
            }

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(90);
            titleLabel.Font = Ui.F(20f, true);
            titleLabel.ForeColor = Ui.Accent;
            titleLabel.Width = Ui.Px(400);
            dbLabel.Font = Ui.F(11f);
            dbLabel.ForeColor = Ui.Muted;
            Ui.PrimaryButton(closeButton);
            closeButton.Width = Ui.Px(240);

            // Выход из программы — прямо в шапке. На телевизоре окно закрыть
            // больше нечем: рамки нет, Alt+F4 заблокирован, и без этой кнопки
            // остаётся только диспетчер задач.
            Ui.DangerButton(exitButton);
            exitButton.Width = Ui.Px(300);

            tabs.Font = Ui.F(14f, true);
            tabs.ItemSize = new Size(Ui.Px(230), Ui.Px(56));
            tabs.SizeMode = TabSizeMode.Fixed;

            foreach (TabPage page in tabs.TabPages)
            {
                page.BackColor = Ui.Bg;
                page.ForeColor = Ui.Text;
            }

            statusPanel.BackColor = Ui.Header;
            statusPanel.Height = Ui.Px(60);
            statusLabel.Font = Ui.F(13f, true);
            statusLabel.ForeColor = Ui.Muted;

            // --- расписание ---
            scheduleTopPanel.Height = Ui.Px(80);
            scheduleToolsPanel.Height = Ui.Px(80);
            StyleToggle(regularVariantButton, _variant == Variant.Regular);
            StyleToggle(modifiedVariantButton, _variant == Variant.Modified);
            regularVariantButton.Width = Ui.Px(260);
            modifiedVariantButton.Width = Ui.Px(248);

            classCaptionLabel.Font = Ui.F(14f, true);
            classCaptionLabel.ForeColor = Ui.Muted;
            classCaptionLabel.Width = Ui.Px(100);
            StyleCombo(classCombo, Ui.Px(240));

            foreach (var button in new[] { copyVariantButton, clearVariantButton, copyDayButton, importButton, exportButton })
                Ui.TouchButton(button, Ui.Card, Ui.Text, 13f, false);

            copyVariantButton.Width = Ui.Px(360);
            clearVariantButton.Width = Ui.Px(240);
            copyDayButton.Width = Ui.Px(260);
            importButton.Width = Ui.Px(300);
            exportButton.Width = Ui.Px(300);

            scheduleHintLabel.Font = Ui.F(12f);
            scheduleHintLabel.ForeColor = Ui.Muted;

            Ui.StyleGrid(scheduleGrid, true);
            Ui.EnableDoubleBuffer(scheduleGrid);

            // --- классы ---
            StyleList(classesList);
            classesSidePanel.Width = Ui.Px(460);
            classesSidePanel.BackColor = Ui.Bg;
            classesSidePanel.Padding = new Padding(Ui.Px(16));
            StyleBox(classNameBox);
            classNameBox.Font = Ui.F(18f, true);

            Ui.PrimaryButton(classAddButton);
            foreach (var button in new[] { classRenameButton, classUpButton, classDownButton })
                Ui.TouchButton(button, Ui.Card, Ui.Text, 13f, false);
            Ui.DangerButton(classDeleteButton);

            foreach (var button in new[] { classAddButton, classRenameButton, classDeleteButton, classUpButton, classDownButton })
                button.Height = Ui.Px(64);

            classesHintLabel.Font = Ui.F(12f);
            classesHintLabel.ForeColor = Ui.Muted;
            classesHintLabel.Height = Ui.Px(104);
            editorKeyboard.Height = (int)(ClientSize.Height * 0.42);

            // --- звонки ---
            Ui.StyleGrid(bellsGrid, true);
            bellsGrid.ScrollBars = ScrollBars.Vertical;
            bellsSidePanel.Width = Ui.Px(460);
            bellsSidePanel.Padding = new Padding(Ui.Px(16));
            Ui.PrimaryButton(bellsSaveButton);
            Ui.TouchButton(bellAddButton, Ui.Card, Ui.Text, 13f, false);
            Ui.DangerButton(bellDeleteButton);

            foreach (var button in new[] { bellsSaveButton, bellAddButton, bellDeleteButton })
                button.Height = Ui.Px(64);

            bellsHintLabel.Font = Ui.F(12f);
            bellsHintLabel.ForeColor = Ui.Muted;
            bellsHintLabel.Height = Ui.Px(160);

            // --- календарь ---
            calendarCard.Width = Ui.Px(620);
            calendarCard.BackColor = Ui.Card;

            calendarSidePanel.Padding = new Padding(Ui.Px(20), Ui.Px(16), Ui.Px(20), Ui.Px(16));
            dayHeaderLabel.Font = Ui.F(20f, true);
            dayHeaderLabel.ForeColor = Ui.Accent;
            dayHeaderLabel.Height = Ui.Px(108);

            holidayCheck.Font = Ui.F(15f, true);
            holidayCheck.ForeColor = Ui.Warn;
            holidayCheck.Height = Ui.Px(60);

            foreach (var label in new[] { dayTitleCaption, dayVariantLabel, upcomingLabel })
            {
                label.Font = Ui.F(13f);
                label.ForeColor = Ui.Muted;
                label.Height = Ui.Px(40);
            }

            StyleBox(dayTitleBox);
            StyleCombo(dayVariantCombo, 0);
            dayButtonsPanel.Height = Ui.Px(80);
            Ui.PrimaryButton(daySaveButton);
            Ui.DangerButton(dayDeleteButton);
            daySaveButton.Width = Ui.Px(340);
            dayDeleteButton.Width = Ui.Px(300);
            StyleList(upcomingList);

            // --- показ ---
            displayTable.RowStyles.Clear();
            for (int i = 0; i < displayTable.RowCount; i++)
                displayTable.RowStyles.Add(new RowStyle(SizeType.Absolute, Ui.Px(i == 6 ? 86 : 68)));

            displayTable.ColumnStyles[0].Width = Ui.Px(420);
            displayTable.Padding = new Padding(Ui.Px(24), Ui.Px(16), Ui.Px(24), Ui.Px(16));

            foreach (var label in new[] { modeLabel, displayClassLabel, dateLabel, tomorrowLabel, variantCaptionLabel,
                                          schoolLabel, tickerLabel, numbersLabel, rotateLabel, extrasLabel,
                                          themeLabel, lessonsLabel, daysLabel, perPageLabel, idleLabel })
            {
                label.Font = Ui.F(13f, true);
                label.ForeColor = Ui.Muted;
            }

            StyleCombo(modeCombo, Ui.Px(520));
            StyleCombo(displayClassCombo, Ui.Px(520));
            StyleCombo(dateModeCombo, Ui.Px(520));
            StyleCombo(themeCombo, Ui.Px(520));
            StyleBox(tomorrowAfterBox);
            StyleBox(schoolBox);
            StyleBox(tickerBox);
            schoolBox.MaxLength = 255;
            tickerBox.MaxLength = 255;
            dayTitleBox.MaxLength = 120;
            classNameBox.MaxLength = 32;
            tomorrowAfterBox.Width = Ui.Px(200);
            schoolBox.Width = Ui.Px(700);
            tickerBox.Width = Ui.Px(900);

            datePicker.Font = Ui.F(13f);
            datePicker.Width = Ui.Px(420);
            datePicker.Left = Ui.Px(530);

            StyleToggle(showRegularButton, _settings.ActiveVariant == Variant.Regular);
            StyleToggle(showModifiedButton, _settings.ActiveVariant == Variant.Modified);
            showRegularButton.Width = Ui.Px(340);
            showModifiedButton.Width = Ui.Px(340);
            showModifiedButton.Left = Ui.Px(340);

            foreach (var updown in new[] { lessonsUpDown, daysUpDown, perPageUpDown, rotateUpDown, idleUpDown })
            {
                updown.Font = Ui.F(14f, true);
                updown.BackColor = Ui.Card;
                updown.ForeColor = Ui.Text;
                updown.BorderStyle = BorderStyle.FixedSingle;
                updown.Width = Ui.Px(110);
            }

            lessonsUpDown.Minimum = 1; lessonsUpDown.Maximum = 12;
            daysUpDown.Minimum = 1; daysUpDown.Maximum = 7;
            perPageUpDown.Minimum = 2; perPageUpDown.Maximum = 20;
            rotateUpDown.Minimum = 5; rotateUpDown.Maximum = 600;
            idleUpDown.Minimum = 15; idleUpDown.Maximum = 3600;

            lessonsLabel.Width = Ui.Px(200); lessonsUpDown.Left = Ui.Px(200);
            daysLabel.Left = Ui.Px(310); daysLabel.Width = Ui.Px(220); daysUpDown.Left = Ui.Px(530);
            perPageLabel.Left = Ui.Px(640); perPageLabel.Width = Ui.Px(240); perPageUpDown.Left = Ui.Px(880);

            autoRotateCheck.Font = Ui.F(13f);
            autoRotateCheck.ForeColor = Ui.Text;
            autoRotateCheck.Width = Ui.Px(370);
            rotateUpDown.Left = Ui.Px(370);
            idleLabel.Left = Ui.Px(480); idleLabel.Width = Ui.Px(400); idleUpDown.Left = Ui.Px(880);

            replacementsCheck.Font = Ui.F(13f);
            replacementsCheck.ForeColor = Ui.Text;
            replacementsCheck.Width = Ui.Px(900);

            displayApplyPanel.Height = Ui.Px(100);
            Ui.TouchButton(applyDisplayButton, Ui.OkBg, Ui.OnAccent, 15f, true);
            applyDisplayButton.Width = Ui.Px(560);

            // --- доступ ---
            accessTable.RowStyles.Clear();
            for (int i = 0; i < accessTable.RowCount; i++)
                accessTable.RowStyles.Add(new RowStyle(SizeType.Absolute, Ui.Px(76)));

            accessTable.ColumnStyles[0].Width = Ui.Px(420);
            accessTable.Height = Ui.Px(340);
            accessTable.Padding = new Padding(Ui.Px(24), Ui.Px(20), Ui.Px(24), Ui.Px(10));

            foreach (var label in new[] { currentPasswordLabel, newPasswordLabel, confirmPasswordLabel })
            {
                label.Font = Ui.F(13f, true);
                label.ForeColor = Ui.Muted;
            }

            foreach (var box in new[] { currentPasswordBox, newPasswordBox, confirmPasswordBox })
            {
                StyleBox(box);
                box.Width = Ui.Px(520);
            }

            accessInfoLabel.Font = Ui.F(12f);
            accessInfoLabel.ForeColor = Ui.Muted;
            accessButtonsPanel.Height = Ui.Px(100);

            Ui.PrimaryButton(changePasswordButton);
            Ui.TouchButton(connectionButton, Ui.Card, Ui.Text, 13f, false);
            Ui.DangerButton(exitAppButton);
            changePasswordButton.Width = Ui.Px(420);
            connectionButton.Width = Ui.Px(420);
            connectionButton.Left = Ui.Px(444);
            exitAppButton.Width = Ui.Px(320);
        }

        /// <summary>Заголовок одной вкладки: выбранная — цветом, остальные — фоном шапки.</summary>
        private void TabsDrawItem(object sender, DrawItemEventArgs e)
        {
            var page = tabs.TabPages[e.Index];
            var selected = tabs.SelectedIndex == e.Index;

            using (var brush = new SolidBrush(selected ? Ui.AccentDark : Ui.Header))
                e.Graphics.FillRectangle(brush, e.Bounds);

            using (var pen = new Pen(Ui.Line))
                e.Graphics.DrawLine(pen, e.Bounds.Right - 1, e.Bounds.Top + Ui.Px(8),
                                    e.Bounds.Right - 1, e.Bounds.Bottom - Ui.Px(8));

            Ui.DrawCentered(e.Graphics, page.Text, Ui.F(14f, selected),
                            selected ? Ui.OnAccent : Ui.Muted, e.Bounds);
        }

        /// <summary>
        /// Разложить заголовки вкладок по ширине окна.
        ///
        /// Запас в двадцать точек — не придирка: без него шесть вкладок ровно
        /// по ширине не помещались, TabControl включал стрелки прокрутки, и
        /// «Показ» с «Доступом» уезжали за край. Именно там лежат название
        /// школы и выход из программы, и найти их было нельзя.
        /// </summary>
        private void LayoutTabs()
        {
            if (tabs.TabPages.Count == 0 || tabs.ClientSize.Width <= 0) return;

            var width = (tabs.ClientSize.Width - Ui.Px(20)) / tabs.TabPages.Count;
            if (width < Ui.Px(110)) width = Ui.Px(110);

            tabs.ItemSize = new Size(width, Ui.Px(56));
        }

        private void StyleToggle(Button button, bool active)
        {
            Ui.TouchButton(button, active ? Ui.AccentDark : Ui.Card, active ? Ui.OnAccent : Ui.Muted, 14f, active);
        }

        private void StyleCombo(ComboBox combo, int width)
        {
            combo.Font = Ui.F(14f);
            combo.BackColor = Ui.Card;
            combo.ForeColor = Ui.Text;
            combo.IntegralHeight = false;
            combo.DropDownHeight = Ui.Px(300);
            if (width > 0) combo.Width = width;
        }

        private void StyleBox(TextBox box)
        {
            box.Font = Ui.F(14f);
            box.BackColor = Ui.Card;
            box.ForeColor = Ui.Text;
        }

        private void StyleList(ListBox list)
        {
            list.Font = Ui.F(15f);
            list.BackColor = Ui.Bg;
            list.ForeColor = Ui.Text;
            list.BorderStyle = BorderStyle.None;
            list.ItemHeight = Ui.Px(44);
            list.IntegralHeight = false;
        }

        // ===================== ЗАГРУЗКА =====================

        private void LoadAll()
        {
            try
            {
                _settings = DisplaySettings.From(Repo.Settings());
                _classes = Repo.Classes();
                _times = Repo.LessonTimes();

                if (!HasClass(_classId) && _classes.Count > 0) _classId = _classes[0].Id;

                dbLabel.Text = "База: " + Db.SafeDescription();

                FillClassCombo();
                FillClassesList();
                FillBells();
                FillCalendarTab();
                FillDisplayTab();
                FillAccessTab();
                LoadSchedule();

                Say("");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private bool HasClass(int id)
        {
            return Repo.ClassById(_classes, id) != null;
        }

        private void Say(string message)
        {
            Say(message, false);
        }

        private void Say(string message, bool error)
        {
            statusLabel.Text = message;
            statusLabel.ForeColor = error ? Ui.Danger : Ui.Ok;
        }

        /// <summary>Подписаться на все поля ввода окна — включая те, что лежат в панелях.</summary>
        private void HookTextBoxes(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child == editorKeyboard) continue;

                var box = child as TextBoxBase;
                if (box != null)
                {
                    box.Enter += TextBoxEntered;
                    box.Leave += TextBoxLeft;
                }

                if (child.HasChildren) HookTextBoxes(child);
            }
        }

        private void TextBoxEntered(object sender, EventArgs e)
        {
            editorKeyboard.Target = (Control)sender;
            editorKeyboard.Visible = true;
        }

        private void TextBoxLeft(object sender, EventArgs e)
        {
            // Проверяем после того, как фокус устоится: переход из одного поля
            // в другое не должен мигать клавиатурой.
            BeginInvoke((MethodInvoker)delegate
            {
                var active = ActiveControl as TextBoxBase;
                if (active != null) editorKeyboard.Target = active;
                else editorKeyboard.Visible = false;
            });
        }

        /// <summary>
        /// Следим за правками на вкладке «Показ», чтобы набранное название
        /// школы не пропало, если человек закроет окно, не нажав «Применить».
        /// </summary>
        private void HookDisplayChanges()
        {
            foreach (var box in new[] { schoolBox, tickerBox, tomorrowAfterBox })
                box.TextChanged += DisplayChanged;

            foreach (var combo in new[] { modeCombo, displayClassCombo, dateModeCombo, themeCombo })
                combo.SelectedIndexChanged += DisplayChanged;

            foreach (var updown in new[] { lessonsUpDown, daysUpDown, perPageUpDown, rotateUpDown, idleUpDown })
                updown.ValueChanged += DisplayChanged;

            foreach (var check in new[] { autoRotateCheck, replacementsCheck })
                check.CheckedChanged += DisplayChanged;

            datePicker.ValueChanged += DisplayChanged;
        }

        private void DisplayChanged(object sender, EventArgs e)
        {
            if (!_loading) _displayDirty = true;
        }

        private void TabChanged(object sender, EventArgs e)
        {
            editorKeyboard.Visible = false;

            try
            {
                _classes = Repo.Classes();
                _times = Repo.LessonTimes();

                if (tabs.SelectedTab == tabSchedule) { FillClassCombo(); LoadSchedule(); }
                else if (tabs.SelectedTab == tabClasses) FillClassesList();
                else if (tabs.SelectedTab == tabBells) FillBells();
                else if (tabs.SelectedTab == tabCalendar) FillCalendarTab();
                else if (tabs.SelectedTab == tabDisplay) FillDisplayTab();
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        // ===================== ВКЛАДКА «РАСПИСАНИЕ» =====================

        private void FillClassCombo()
        {
            _loading = true;
            classCombo.Items.Clear();
            foreach (var item in _classes) classCombo.Items.Add(item);

            var selected = Repo.ClassById(_classes, _classId);
            if (selected == null && _classes.Count > 0) { selected = _classes[0]; _classId = selected.Id; }
            classCombo.SelectedItem = selected;
            _loading = false;
        }

        private void ClassComboChanged(object sender, EventArgs e)
        {
            if (_loading) return;

            var selected = classCombo.SelectedItem as SchoolClass;
            if (selected == null) return;

            _classId = selected.Id;
            LoadSchedule();
        }

        private void RegularVariantClicked(object sender, EventArgs e) { SwitchVariant(Variant.Regular); }

        private void ModifiedVariantClicked(object sender, EventArgs e) { SwitchVariant(Variant.Modified); }

        private void SwitchVariant(int variant)
        {
            _variant = variant;
            StyleToggle(regularVariantButton, _variant == Variant.Regular);
            StyleToggle(modifiedVariantButton, _variant == Variant.Modified);
            copyVariantButton.Text = _variant == Variant.Modified
                ? "Копировать обычное → изменённое"
                : "Копировать изменённое → обычное";
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            try
            {
                if (!HasClass(_classId))
                {
                    scheduleGrid.Columns.Clear();
                    scheduleGrid.Rows.Clear();
                    scheduleHintLabel.Text = "Сначала добавьте классы на вкладке «Классы»";
                    return;
                }

                _gridCurrent = Repo.WeekOfClass(_variant, _classId);
                _gridOther = Repo.WeekOfClass(Variant.Other(_variant), _classId);

                BuildScheduleGrid();

                scheduleHintLabel.Text = "Нажмите на клетку, чтобы поставить или заменить урок  ·  сетка: "
                                       + Variant.Name(_variant).ToLowerInvariant();
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void BuildScheduleGrid()
        {
            var days = _settings.DaysCount;
            var lessons = _settings.LessonsCount;

            scheduleGrid.Columns.Clear();
            scheduleGrid.Rows.Clear();

            AddScheduleColumn("Урок");
            for (int day = 1; day <= days; day++) AddScheduleColumn(Ru.DayName(day));

            for (int no = 1; no <= lessons; no++)
            {
                var index = scheduleGrid.Rows.Add();
                scheduleGrid.Rows[index].Cells[0].Value = no.ToString();

                for (int day = 1; day <= days; day++)
                {
                    Lesson lesson;
                    _gridCurrent.TryGetValue(Repo.Key(day, no), out lesson);
                    scheduleGrid.Rows[index].Cells[day].Tag = lesson;
                }
            }

            LayoutScheduleGrid();
        }

        private void AddScheduleColumn(string header)
        {
            scheduleGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = header,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Resizable = DataGridViewTriState.False
            });
        }

        private void LayoutScheduleGrid()
        {
            if (scheduleGrid.Columns.Count == 0 || scheduleGrid.Rows.Count == 0) return;

            var height = scheduleGrid.ClientSize.Height;
            var width = scheduleGrid.ClientSize.Width;
            if (height <= 0 || width <= 0) return;

            scheduleGrid.ColumnHeadersHeight = Ui.Px(56);

            var rowHeight = Math.Max(Ui.Px(52), (height - scheduleGrid.ColumnHeadersHeight) / scheduleGrid.Rows.Count);
            foreach (DataGridViewRow row in scheduleGrid.Rows) row.Height = rowHeight;

            var overflow = scheduleGrid.ColumnHeadersHeight + rowHeight * scheduleGrid.Rows.Count > height;
            scheduleGrid.ScrollBars = overflow ? ScrollBars.Vertical : ScrollBars.None;
            if (overflow) width -= SystemInformation.VerticalScrollBarWidth;

            var first = Ui.Px(90);
            scheduleGrid.Columns[0].Width = first;

            var rest = scheduleGrid.Columns.Count - 1;
            if (rest <= 0) return;

            var each = (width - first) / rest;
            for (int i = 1; i < scheduleGrid.Columns.Count; i++)
                scheduleGrid.Columns[i].Width = i == scheduleGrid.Columns.Count - 1
                    ? width - first - each * (rest - 1)
                    : each;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (tabs == null) return;

            LayoutTabs();
            if (tabs.SelectedTab == tabSchedule) LayoutScheduleGrid();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            LayoutTabs();
        }

        private void ScheduleCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            var bounds = e.CellBounds;
            var lesson = scheduleGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as Lesson;

            var background = e.RowIndex % 2 == 0 ? Ui.RowOdd : Ui.RowEven;
            var foreground = Ui.Text;

            if (e.ColumnIndex == 0) { background = Ui.GridHeader; foreground = Ui.Accent; }

            // Когда правим изменённую сетку, отличия от обычной подсвечиваем —
            // иначе не видно, что уже заменено, а что просто скопировано.
            bool differs = false;
            if (e.ColumnIndex > 0 && _variant == Variant.Modified)
            {
                Lesson other;
                _gridOther.TryGetValue(Repo.Key(e.ColumnIndex, e.RowIndex + 1), out other);

                var left = lesson ?? new Lesson();
                differs = !left.SameAs(other);
                if (differs) { background = Ui.WarnBg; foreground = Ui.Warn; }
            }

            using (var brush = new SolidBrush(background))
                e.Graphics.FillRectangle(brush, bounds);

            using (var pen = new Pen(Ui.Line))
            {
                e.Graphics.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
                e.Graphics.DrawLine(pen, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom);
            }

            if (e.ColumnIndex == 0)
            {
                var text = Convert.ToString(scheduleGrid.Rows[e.RowIndex].Cells[0].Value);
                Ui.DrawCentered(e.Graphics, text, Ui.Fp(bounds.Height * 0.4f, true), foreground, bounds);
                e.Handled = true;
                return;
            }

            if (lesson == null || lesson.IsEmpty)
            {
                Ui.DrawCentered(e.Graphics, differs ? "нет урока" : "+",
                                Ui.Fp(bounds.Height * 0.28f, false),
                                differs ? Ui.Warn : Ui.CardLight, bounds);
                e.Handled = true;
                return;
            }

            var pad = Ui.Px(6);
            var inner = new Rectangle(bounds.X + pad, bounds.Y + pad, bounds.Width - pad * 2, bounds.Height - pad * 2);
            var mainRect = new Rectangle(inner.X, inner.Y, inner.Width, (int)(inner.Height * 0.55));
            var subRect = new Rectangle(inner.X, inner.Y + (int)(inner.Height * 0.55), inner.Width, (int)(inner.Height * 0.45));

            Ui.DrawCentered(e.Graphics, lesson.Subject, Ui.Fp(inner.Height * 0.32f, true), foreground, mainRect);

            var details = new List<string>();
            if (!string.IsNullOrWhiteSpace(lesson.Teacher)) details.Add(lesson.Teacher);
            if (!string.IsNullOrWhiteSpace(lesson.Room)) details.Add("каб. " + lesson.Room);

            if (details.Count > 0)
                Ui.DrawCentered(e.Graphics, string.Join(" · ", details), Ui.Fp(inner.Height * 0.22f, false),
                                Ui.Muted, subRect);

            e.Handled = true;
        }

        private void ScheduleSelectionChanged(object sender, EventArgs e)
        {
            if (scheduleGrid.SelectedCells.Count > 0) scheduleGrid.ClearSelection();
        }

        private void ScheduleCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0) return;
            if (!HasClass(_classId)) return;

            var day = e.ColumnIndex;
            var no = e.RowIndex + 1;
            var lesson = scheduleGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as Lesson;

            var title = ClassName() + "  ·  " + Ru.DayName(day) + "  ·  " + no + "-й урок";
            var subtitle = "Сетка: " + Variant.Name(_variant).ToLowerInvariant() + TimeHint(no);

            try
            {
                using (var form = new LessonEditForm(title, subtitle, lesson,
                                                     Repo.Suggestions("subject"),
                                                     Repo.Suggestions("teacher"),
                                                     Repo.Suggestions("room")))
                {
                    if (form.ShowDialog(this) != DialogResult.OK) return;

                    if (form.Cleared) Repo.ClearCell(_variant, _classId, day, no);
                    else Repo.SaveCell(_variant, _classId, day, no, form.Subject, form.Teacher, form.Room);
                }

                LoadSchedule();
                Say("Сохранено. Экран обновится сам в течение " + AppConfig.RefreshSeconds + " с.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private string TimeHint(int no)
        {
            foreach (var time in _times)
            {
                if (time.No == no) return "  ·  " + time.Range;
            }
            return "";
        }

        private string ClassName()
        {
            var found = Repo.ClassById(_classes, _classId);
            return found != null ? found.Name : "";
        }

        private void CopyVariantClicked(object sender, EventArgs e)
        {
            var from = Variant.Other(_variant);
            var question = "Скопировать сетку «" + Variant.Name(from).ToLowerInvariant() + "» в «"
                         + Variant.Name(_variant).ToLowerInvariant() + "»?\n\n"
                         + "Да — для всех классов\n"
                         + "Нет — только для класса " + ClassName() + "\n\n"
                         + "То, что сейчас в сетке «" + Variant.Name(_variant).ToLowerInvariant() + "», будет заменено.";

            var answer = MessageBox.Show(this, question, "Копирование расписания",
                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (answer == DialogResult.Cancel) return;

            try
            {
                Repo.CopyVariant(from, _variant, answer == DialogResult.Yes ? (int?)null : _classId);
                LoadSchedule();
                Say("Скопировано.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void ClearVariantClicked(object sender, EventArgs e)
        {
            var question = "Очистить сетку «" + Variant.Name(_variant).ToLowerInvariant() + "»?\n\n"
                         + "Да — у всех классов\n"
                         + "Нет — только у класса " + ClassName();

            var answer = MessageBox.Show(this, question, "Очистка расписания",
                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (answer == DialogResult.Cancel) return;

            try
            {
                Repo.ClearVariant(_variant, answer == DialogResult.Yes ? (int?)null : _classId);
                LoadSchedule();
                Say("Очищено.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        /// <summary>«Во вторник как в понедельник» — самая частая правка руками.</summary>
        private void CopyDayClicked(object sender, EventArgs e)
        {
            if (!HasClass(_classId)) return;

            using (var form = new CopyDayForm(_settings.DaysCount))
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    Repo.CopyDay(_variant, _classId, form.FromDay, form.ToDay);
                    LoadSchedule();
                    Say(Ru.DayName(form.FromDay) + " скопирован в " + Ru.DayName(form.ToDay).ToLowerInvariant() + ".");
                }
                catch (Exception ex)
                {
                    Say(Db.Explain(ex), true);
                }
            }
        }

        // ===================== ФАЙЛЫ =====================

        private void ImportClicked(object sender, EventArgs e)
        {
            string path;
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Выберите файл с расписанием";
                dialog.Filter = "Расписание (*.csv;*.txt)|*.csv;*.txt|Все файлы (*.*)|*.*";
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                path = dialog.FileName;
            }

            ImportResult data;
            try
            {
                data = CsvSchedule.Parse(path);
            }
            catch (Exception ex)
            {
                Say("Не удалось прочитать файл: " + ex.Message, true);
                return;
            }

            if (data.Rows.Count == 0)
            {
                MessageBox.Show(this,
                    "В файле не нашлось ни одной строки расписания." + Environment.NewLine + Environment.NewLine +
                    "Ожидается такой вид (первая строка — заголовок):" + Environment.NewLine + Environment.NewLine +
                    CsvSchedule.Sample(),
                    "Файл не подошёл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var summary = "Файл: " + System.IO.Path.GetFileName(path) + Environment.NewLine +
                          "Уроков в файле: " + data.Rows.Count + Environment.NewLine +
                          "Классов: " + data.ClassNames.Count + " (" + string.Join(", ", data.ClassNames) + ")" +
                          Environment.NewLine +
                          (data.Errors.Count > 0 ? "Пропущено строк: " + data.Errors.Count + Environment.NewLine : "") +
                          Environment.NewLine +
                          "Залить в сетку «" + Variant.Name(_variant).ToLowerInvariant() + "»?" + Environment.NewLine +
                          Environment.NewLine +
                          "Да — заменить сетку целиком" + Environment.NewLine +
                          "Нет — добавить к тому, что уже есть";

            var answer = MessageBox.Show(this, summary, "Загрузка расписания",
                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (answer == DialogResult.Cancel) return;

            try
            {
                var written = CsvSchedule.Apply(data, _variant, true, answer == DialogResult.Yes);

                _classes = Repo.Classes();
                FillClassCombo();
                LoadSchedule();

                var message = "Загружено уроков: " + written + ".";
                if (data.Errors.Count > 0) message += " Пропущено строк: " + data.Errors.Count + ".";
                Say(message);

                if (data.Errors.Count > 0)
                    MessageBox.Show(this, string.Join(Environment.NewLine, data.Errors.ToArray()),
                                    "Строки, которые не удалось разобрать", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void ExportClicked(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Куда сохранить расписание";
                dialog.Filter = "Расписание (*.csv)|*.csv|Все файлы (*.*)|*.*";
                dialog.FileName = "Расписание_" + (_variant == Variant.Modified ? "изменённое" : "обычное") + "_" +
                                  DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".csv";

                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    var written = CsvSchedule.Export(dialog.FileName, _variant, _classes,
                                                     _settings.DaysCount, _settings.LessonsCount);
                    Say("Сохранено уроков: " + written + " → " + dialog.FileName);
                }
                catch (Exception ex)
                {
                    Say("Не удалось записать файл: " + ex.Message, true);
                }
            }
        }

        // ===================== ВКЛАДКА «КЛАССЫ» =====================

        private void FillClassesList()
        {
            classesList.Items.Clear();
            foreach (var item in _classes) classesList.Items.Add(item);

            classesHintLabel.Text = _classes.Count == 0
                ? "Классов пока нет. Впишите название и нажмите «Добавить класс». Можно сразу несколько через запятую: 5А, 5Б, 6А"
                : "Всего классов: " + _classes.Count + ". Порядок кнопками «Выше»/«Ниже» — в нём классы идут и на экране.";
        }

        private SchoolClass SelectedClass()
        {
            return classesList.SelectedItem as SchoolClass;
        }

        private void ClassAddClicked(object sender, EventArgs e)
        {
            var text = (classNameBox.Text ?? "").Trim();
            if (text.Length == 0) { Say("Впишите название класса.", true); return; }

            int added = 0;
            try
            {
                foreach (var part in text.Split(',', ';'))
                {
                    var name = part.Trim();
                    if (name.Length == 0) continue;

                    Repo.AddClass(name);
                    added++;
                }

                classNameBox.Text = "";
                Say(added == 1 ? "Класс добавлен." : "Добавлено классов: " + added + ".");
            }
            catch (Exception ex)
            {
                // Одно имя не прошло (например, повтор) — остальные всё равно
                // созданы, и список обязан их показать.
                Say(Db.Explain(ex) + (added > 0 ? " Добавлено до ошибки: " + added + "." : ""), true);
            }
            finally
            {
                _classes = Repo.Classes();
                FillClassesList();
                FillClassCombo();
            }
        }

        private void ClassRenameClicked(object sender, EventArgs e)
        {
            var selected = SelectedClass();
            if (selected == null) { Say("Выберите класс в списке.", true); return; }

            var name = (classNameBox.Text ?? "").Trim();
            if (name.Length == 0) { Say("Впишите новое название в поле сверху.", true); return; }

            try
            {
                Repo.RenameClass(selected.Id, name);
                classNameBox.Text = "";
                _classes = Repo.Classes();
                FillClassesList();
                FillClassCombo();
                Say("Переименовано.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void ClassDeleteClicked(object sender, EventArgs e)
        {
            var selected = SelectedClass();
            if (selected == null) { Say("Выберите класс в списке.", true); return; }

            var answer = MessageBox.Show(this,
                "Удалить класс " + selected.Name + "?" + Environment.NewLine + Environment.NewLine +
                "Вместе с ним удалится и его расписание — в обеих сетках.",
                "Удаление класса", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes) return;

            try
            {
                Repo.DeleteClass(selected.Id);
                _classes = Repo.Classes();
                if (!HasClass(_classId)) _classId = _classes.Count > 0 ? _classes[0].Id : 0;

                FillClassesList();
                FillClassCombo();
                Say("Класс удалён.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void ClassUpClicked(object sender, EventArgs e) { MoveClass(-1); }

        private void ClassDownClicked(object sender, EventArgs e) { MoveClass(1); }

        private void MoveClass(int direction)
        {
            var selected = SelectedClass();
            if (selected == null) { Say("Выберите класс в списке.", true); return; }

            try
            {
                Repo.MoveClass(selected.Id, direction);
                _classes = Repo.Classes();
                FillClassesList();

                for (int i = 0; i < classesList.Items.Count; i++)
                {
                    var item = classesList.Items[i] as SchoolClass;
                    if (item != null && item.Id == selected.Id) { classesList.SelectedIndex = i; break; }
                }

                FillClassCombo();
                Say("Порядок изменён.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        // ===================== ВКЛАДКА «ЗВОНКИ» =====================

        private void FillBells()
        {
            bellsGrid.Columns.Clear();
            bellsGrid.Rows.Clear();

            bellsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Урок",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = Ui.Px(140)
            });
            bellsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Начало",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = Ui.Px(240)
            });
            bellsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Конец",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = Ui.Px(240)
            });

            foreach (var time in _times)
            {
                var index = bellsGrid.Rows.Add();
                bellsGrid.Rows[index].Cells[0].Value = time.No;
                bellsGrid.Rows[index].Cells[1].Value = Ru.Time(time.Start);
                bellsGrid.Rows[index].Cells[2].Value = Ru.Time(time.End);
                bellsGrid.Rows[index].Height = Ui.Px(56);
            }

            bellsGrid.ColumnHeadersHeight = Ui.Px(56);
        }

        private void BellsCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            EditBell(e.RowIndex);
        }

        private void BellEditClicked(object sender, EventArgs e)
        {
            var row = bellsGrid.CurrentRow != null ? bellsGrid.CurrentRow.Index : -1;
            if (row < 0) { Say("Выберите урок в таблице.", true); return; }

            EditBell(row);
        }

        /// <summary>
        /// Время правится в отдельном окне кнопками. В самой таблице это
        /// делать нечем: у экрана на стене нет клавиатуры, а экранную в клетку
        /// таблицы не подставить — звонки оказывались нередактируемыми.
        /// </summary>
        private void EditBell(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _times.Count) return;

            var time = _times[rowIndex];

            using (var form = new BellEditForm(time.No, time.Start, time.End))
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    if (form.Removed) Repo.DeleteLessonTime(time.No);
                    else Repo.SaveLessonTime(time.No, form.Start, form.End);

                    _times = Repo.LessonTimes();
                    FillBells();

                    Say(form.Removed
                        ? "Урок " + time.No + " убран из звонков."
                        : "Урок " + time.No + ": " + Ru.Time(form.Start) + " – " + Ru.Time(form.End) + ".");
                }
                catch (Exception ex)
                {
                    Say(Db.Explain(ex), true);
                }
            }
        }

        private void BellAddClicked(object sender, EventArgs e)
        {
            try
            {
                var next = 1;
                var start = new TimeSpan(8, 30, 0);

                if (_times.Count > 0)
                {
                    var last = _times[_times.Count - 1];
                    next = last.No + 1;
                    start = last.End.Add(TimeSpan.FromMinutes(10));
                }

                if (next > 12) { Say("Больше двенадцати уроков не бывает.", true); return; }

                Repo.SaveLessonTime(next, start, start.Add(TimeSpan.FromMinutes(45)));
                _times = Repo.LessonTimes();
                FillBells();
                Say("Урок " + next + " добавлен.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void BellDeleteClicked(object sender, EventArgs e)
        {
            if (_times.Count == 0) return;

            try
            {
                var last = _times[_times.Count - 1];
                Repo.DeleteLessonTime(last.No);
                _times = Repo.LessonTimes();
                FillBells();
                Say("Урок " + last.No + " убран из звонков.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        // ===================== ВКЛАДКА «КАЛЕНДАРЬ» =====================

        private void FillCalendarTab()
        {
            LoadDayMark(calendar.SelectedDate);
            FillUpcoming();
        }

        private void CalendarDateChanged(object sender, EventArgs e)
        {
            LoadDayMark(calendar.SelectedDate);
        }

        private void LoadDayMark(DateTime date)
        {
            _loading = true;
            try
            {
                dayHeaderLabel.Text = Ru.LongDate(date);

                if (dayVariantCombo.Items.Count == 0)
                {
                    dayVariantCombo.Items.Add("как в настройках показа");
                    dayVariantCombo.Items.Add("обычное");
                    dayVariantCombo.Items.Add("изменённое");
                }

                var mark = Repo.DayMark(date);
                holidayCheck.Checked = mark != null && mark.IsHoliday;
                dayTitleBox.Text = mark != null ? (mark.Title ?? "") : "";
                dayVariantCombo.SelectedIndex = mark != null && mark.Variant.HasValue ? mark.Variant.Value + 1 : 0;

                var from = calendar.VisibleMonth.AddMonths(-1);
                calendar.Marks = Repo.DaysBetween(from, from.AddMonths(3));
                calendar.Invalidate();
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
            finally
            {
                _loading = false;
            }
        }

        private void FillUpcoming()
        {
            try
            {
                upcomingList.Items.Clear();
                foreach (var day in Repo.Upcoming(DateTime.Today, 40))
                {
                    var text = Ru.ShortDate(day.Date) + "  —  ";
                    if (day.IsHoliday) text += "праздник" + (string.IsNullOrWhiteSpace(day.Title) ? "" : ": " + day.Title);
                    else if (day.Variant.HasValue) text += "сетка: " + Variant.Name(day.Variant.Value).ToLowerInvariant();
                    else text += string.IsNullOrWhiteSpace(day.Title) ? "отметка" : day.Title;

                    upcomingList.Items.Add(new UpcomingItem { Date = day.Date, Text = text });
                }
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private sealed class UpcomingItem
        {
            public DateTime Date;
            public string Text;
            public override string ToString() { return Text; }
        }

        private void UpcomingSelected(object sender, EventArgs e)
        {
            var item = upcomingList.SelectedItem as UpcomingItem;
            if (item == null) return;

            calendar.SelectedDate = item.Date;
            LoadDayMark(item.Date);
        }

        private void DaySaveClicked(object sender, EventArgs e)
        {
            var date = calendar.SelectedDate;

            try
            {
                var mark = new CalendarDay
                {
                    Date = date,
                    IsHoliday = holidayCheck.Checked,
                    Title = (dayTitleBox.Text ?? "").Trim(),
                    Variant = dayVariantCombo.SelectedIndex > 0 ? (int?)(dayVariantCombo.SelectedIndex - 1) : null
                };

                Repo.SaveDayMark(mark);
                FillUpcoming();
                LoadDayMark(date);
                Say("Отметка на " + Ru.Date(date) + " сохранена.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void DayDeleteClicked(object sender, EventArgs e)
        {
            var date = calendar.SelectedDate;

            try
            {
                Repo.DeleteDayMark(date);
                FillUpcoming();
                LoadDayMark(date);
                Say("Отметка на " + Ru.Date(date) + " убрана.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        // ===================== ВКЛАДКА «ПОКАЗ» =====================

        private void FillDisplayTab()
        {
            _loading = true;
            try
            {
                if (modeCombo.Items.Count == 0)
                {
                    modeCombo.Items.Add("Все классы на один день");
                    modeCombo.Items.Add("Неделя одного класса");

                    dateModeCombo.Items.Add("Сегодня");
                    dateModeCombo.Items.Add("Завтра");
                    dateModeCombo.Items.Add("Ближайший учебный день");
                    dateModeCombo.Items.Add("Выбранная дата");

                    themeCombo.Items.Add("Тёмные (для телевизора)");
                    themeCombo.Items.Add("Светлые");
                }

                themeCombo.SelectedIndex = Ui.ParseTheme(_settings.Theme) == AppTheme.Light ? 1 : 0;

                modeCombo.SelectedIndex = _settings.Mode == DisplaySettings.ModeWeek ? 1 : 0;

                displayClassCombo.Items.Clear();
                foreach (var item in _classes) displayClassCombo.Items.Add(item);
                displayClassCombo.SelectedItem = Repo.ClassById(_classes, _settings.DisplayClassId);

                switch (_settings.DateMode)
                {
                    case "tomorrow": dateModeCombo.SelectedIndex = 1; break;
                    case "next": dateModeCombo.SelectedIndex = 2; break;
                    case "fixed": dateModeCombo.SelectedIndex = 3; break;
                    default: dateModeCombo.SelectedIndex = 0; break;
                }

                datePicker.Value = _settings.FixedDate.HasValue ? _settings.FixedDate.Value : DateTime.Today;
                tomorrowAfterBox.Text = _settings.TomorrowAfter.HasValue ? Ru.Time(_settings.TomorrowAfter.Value) : "";

                StyleToggle(showRegularButton, _settings.ActiveVariant == Variant.Regular);
                StyleToggle(showModifiedButton, _settings.ActiveVariant == Variant.Modified);

                schoolBox.Text = _settings.SchoolName;
                tickerBox.Text = _settings.Ticker;
                lessonsUpDown.Value = _settings.LessonsCount;
                daysUpDown.Value = _settings.DaysCount;
                perPageUpDown.Value = _settings.ClassesPerPage;
                autoRotateCheck.Checked = _settings.AutoRotate;
                rotateUpDown.Value = _settings.RotateSeconds;
                idleUpDown.Value = _settings.IdleSeconds;
                replacementsCheck.Checked = _settings.ShowReplacements;
                _displayDirty = false;
            }
            finally
            {
                _loading = false;
            }
        }

        private void ShowRegularClicked(object sender, EventArgs e) { SetActiveVariant(Variant.Regular); }

        private void ShowModifiedClicked(object sender, EventArgs e) { SetActiveVariant(Variant.Modified); }

        /// <summary>
        /// Главный переключатель: обычное расписание или изменённое.
        /// Он применяется сразу, не дожидаясь кнопки «Применить», — ради него
        /// к программе чаще всего и подходят.
        /// </summary>
        private void SetActiveVariant(int variant)
        {
            _settings.ActiveVariant = variant;
            StyleToggle(showRegularButton, variant == Variant.Regular);
            StyleToggle(showModifiedButton, variant == Variant.Modified);

            try
            {
                Repo.Set(SettingKeys.ActiveVariant, variant.ToString());
                Say("На экране теперь " + Variant.Name(variant).ToLowerInvariant() + " расписание.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void ApplyDisplayClicked(object sender, EventArgs e)
        {
            ApplyDisplay();
        }

        private bool ApplyDisplay()
        {
            var time = (tomorrowAfterBox.Text ?? "").Trim();
            if (time.Length > 0 && !DisplaySettings.ParseTime(time).HasValue)
            {
                Say("Время пишется как 14:30 — или оставьте поле пустым.", true);
                return false;
            }

            var selectedClass = displayClassCombo.SelectedItem as SchoolClass;
            if (modeCombo.SelectedIndex == 1 && selectedClass == null)
            {
                Say("Для показа недели нужно выбрать класс.", true);
                return false;
            }

            var dateMode = dateModeCombo.SelectedIndex == 1 ? "tomorrow"
                         : dateModeCombo.SelectedIndex == 2 ? "next"
                         : dateModeCombo.SelectedIndex == 3 ? "fixed"
                         : "today";

            var values = new List<KeyValuePair<string, string>>
            {
                Pair(SettingKeys.DisplayMode, modeCombo.SelectedIndex == 1 ? DisplaySettings.ModeWeek : DisplaySettings.ModeDay),
                Pair(SettingKeys.DisplayClass, selectedClass != null ? selectedClass.Id.ToString() : "0"),
                Pair(SettingKeys.DisplayDateMode, dateMode),
                Pair(SettingKeys.DisplayDate, DisplaySettings.FormatDate(datePicker.Value)),
                Pair(SettingKeys.TomorrowAfter, time),
                Pair(SettingKeys.ActiveVariant, _settings.ActiveVariant.ToString()),
                Pair(SettingKeys.SchoolName, (schoolBox.Text ?? "").Trim()),
                Pair(SettingKeys.Ticker, (tickerBox.Text ?? "").Trim()),
                Pair(SettingKeys.LessonsCount, ((int)lessonsUpDown.Value).ToString()),
                Pair(SettingKeys.DaysCount, ((int)daysUpDown.Value).ToString()),
                Pair(SettingKeys.ClassesPerPage, ((int)perPageUpDown.Value).ToString()),
                Pair(SettingKeys.AutoRotate, autoRotateCheck.Checked ? "1" : "0"),
                Pair(SettingKeys.RotateSeconds, ((int)rotateUpDown.Value).ToString()),
                Pair(SettingKeys.IdleSeconds, ((int)idleUpDown.Value).ToString()),
                Pair(SettingKeys.ShowReplacements, replacementsCheck.Checked ? "1" : "0"),
                Pair(SettingKeys.Theme, themeCombo.SelectedIndex == 1 ? "light" : "dark")
            };

            try
            {
                Repo.SetMany(values);
                _settings = DisplaySettings.From(Repo.Settings());
                _displayDirty = false;
                RepaintTheme();
                LoadSchedule();
                Say("Готово. Экран переключится сам в течение " + AppConfig.RefreshSeconds + " с.");
                return true;
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
                return false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_displayDirty)
            {
                var answer = MessageBox.Show(this,
                    "Настройки показа изменены, но не применены." + Environment.NewLine + Environment.NewLine +
                    "Применить их сейчас?",
                    "Показ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel || (answer == DialogResult.Yes && !ApplyDisplay()))
                {
                    // Остаёмся в окне — и снимаем намерение выйти, иначе
                    // следующее «Готово» неожиданно закрыло бы программу.
                    ExitRequested = false;
                    tabs.SelectedTab = tabDisplay;
                    e.Cancel = true;
                    return;
                }

                _displayDirty = false;
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Перекрасить окно, если тему переключили. Витрина на телевизоре
        /// сделает то же самое сама, когда увидит новый счётчик правок.
        /// </summary>
        private void RepaintTheme()
        {
            var theme = string.IsNullOrWhiteSpace(AppConfig.Theme)
                ? Ui.ParseTheme(_settings.Theme)
                : Ui.ParseTheme(AppConfig.Theme);

            if (theme == Ui.Theme) return;

            Ui.SetTheme(theme);
            ApplyTheme();
            editorKeyboard.Restyle();
            Invalidate(true);
        }

        private static KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        // ===================== ВКЛАДКА «ДОСТУП» =====================

        private void FillAccessTab()
        {
            var stored = _settings.AdminPasswordHash;
            accessInfoLabel.Text =
                (string.IsNullOrWhiteSpace(stored)
                    ? "Сейчас действует пароль из файла ip.txt (строка admin). Смените его здесь — новый будет храниться в базе, в зашифрованном виде."
                    : "Пароль задан в программе и хранится в базе зашифрованным.") +
                Environment.NewLine + Environment.NewLine +
                "Файл настроек: " + AppConfig.FilePath + Environment.NewLine +
                "База: " + Db.SafeDescription() + Environment.NewLine + Environment.NewLine +
                "Если пароль забыт: откройте в phpMyAdmin таблицу settings и удалите строку admin_password — " +
                "снова заработает пароль из ip.txt." + Environment.NewLine + Environment.NewLine +
                "Название школы и бегущая строка меняются на вкладке «Показ» — там же, где всё, " +
                "что видно на экране в коридоре." + Environment.NewLine + Environment.NewLine +
                "Расписание можно править и с другого компьютера: поставьте там эту же программу, " +
                "укажите в ip.txt тот же сервер — и добавьте строку mode = editor, чтобы она открывалась " +
                "сразу в режиме учителя, без полноэкранного показа.";
        }

        private void ChangePasswordClicked(object sender, EventArgs e)
        {
            var current = currentPasswordBox.Text ?? "";
            var fresh = newPasswordBox.Text ?? "";
            var confirm = confirmPasswordBox.Text ?? "";

            var stored = _settings.AdminPasswordHash;
            var ok = string.IsNullOrWhiteSpace(stored)
                ? current == AppConfig.FallbackAdminPassword
                : PasswordHasher.Verify(current, stored);

            if (!ok) { Say("Текущий пароль не подошёл.", true); return; }
            if (fresh.Length < 4) { Say("Новый пароль — не короче четырёх знаков.", true); return; }
            if (fresh != confirm) { Say("Новый пароль и повтор не совпали.", true); return; }

            try
            {
                Repo.Set(SettingKeys.AdminPassword, PasswordHasher.Hash(fresh));
                _settings = DisplaySettings.From(Repo.Settings());

                currentPasswordBox.Text = "";
                newPasswordBox.Text = "";
                confirmPasswordBox.Text = "";

                FillAccessTab();
                Say("Пароль изменён.");
            }
            catch (Exception ex)
            {
                Say(Db.Explain(ex), true);
            }
        }

        private void ConnectionClicked(object sender, EventArgs e)
        {
            using (var form = new ConnectionForm())
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;
            }

            string error;
            if (!Schema.Ensure(out error))
            {
                Say(error, true);
                return;
            }

            LoadAll();
            Say("Настройки подключения сохранены.");
        }

        private void ExitAppClicked(object sender, EventArgs e)
        {
            var answer = MessageBox.Show(this,
                "Закрыть программу?" + Environment.NewLine + Environment.NewLine +
                "Экран в коридоре погаснет до следующего запуска.",
                "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes) return;

            ExitRequested = true;
            Close();
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref message, keyData);
        }
    }
}
