namespace SchoolSchedule.Forms
{
    partial class EditorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.scheduleGrid = new System.Windows.Forms.DataGridView();
            this.scheduleToolsPanel = new System.Windows.Forms.Panel();
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.copyDayButton = new System.Windows.Forms.Button();
            this.scheduleHintLabel = new System.Windows.Forms.Label();
            this.scheduleTopPanel = new System.Windows.Forms.Panel();
            this.clearVariantButton = new System.Windows.Forms.Button();
            this.copyVariantButton = new System.Windows.Forms.Button();
            this.classCombo = new System.Windows.Forms.ComboBox();
            this.classCaptionLabel = new System.Windows.Forms.Label();
            this.modifiedVariantButton = new System.Windows.Forms.Button();
            this.regularVariantButton = new System.Windows.Forms.Button();
            this.tabClasses = new System.Windows.Forms.TabPage();
            this.classesList = new System.Windows.Forms.ListBox();
            this.classesSidePanel = new System.Windows.Forms.Panel();
            this.classesHintLabel = new System.Windows.Forms.Label();
            this.classDownButton = new System.Windows.Forms.Button();
            this.classUpButton = new System.Windows.Forms.Button();
            this.classDeleteButton = new System.Windows.Forms.Button();
            this.classRenameButton = new System.Windows.Forms.Button();
            this.classAddButton = new System.Windows.Forms.Button();
            this.classNameBox = new System.Windows.Forms.TextBox();
            this.editorKeyboard = new SchoolSchedule.Controls.OnScreenKeyboard();
            this.tabBells = new System.Windows.Forms.TabPage();
            this.bellsGrid = new System.Windows.Forms.DataGridView();
            this.bellsSidePanel = new System.Windows.Forms.Panel();
            this.bellsHintLabel = new System.Windows.Forms.Label();
            this.bellDeleteButton = new System.Windows.Forms.Button();
            this.bellAddButton = new System.Windows.Forms.Button();
            this.bellsSaveButton = new System.Windows.Forms.Button();
            this.tabCalendar = new System.Windows.Forms.TabPage();
            this.calendarSidePanel = new System.Windows.Forms.Panel();
            this.upcomingList = new System.Windows.Forms.ListBox();
            this.upcomingLabel = new System.Windows.Forms.Label();
            this.dayButtonsPanel = new System.Windows.Forms.Panel();
            this.dayDeleteButton = new System.Windows.Forms.Button();
            this.daySaveButton = new System.Windows.Forms.Button();
            this.dayVariantCombo = new System.Windows.Forms.ComboBox();
            this.dayVariantLabel = new System.Windows.Forms.Label();
            this.dayTitleBox = new System.Windows.Forms.TextBox();
            this.dayTitleCaption = new System.Windows.Forms.Label();
            this.holidayCheck = new System.Windows.Forms.CheckBox();
            this.dayHeaderLabel = new System.Windows.Forms.Label();
            this.calendarCard = new System.Windows.Forms.Panel();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.tabDisplay = new System.Windows.Forms.TabPage();
            this.displayTable = new System.Windows.Forms.TableLayoutPanel();
            this.modeLabel = new System.Windows.Forms.Label();
            this.modeCombo = new System.Windows.Forms.ComboBox();
            this.displayClassLabel = new System.Windows.Forms.Label();
            this.displayClassCombo = new System.Windows.Forms.ComboBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.datePanel = new System.Windows.Forms.Panel();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.dateModeCombo = new System.Windows.Forms.ComboBox();
            this.tomorrowLabel = new System.Windows.Forms.Label();
            this.tomorrowAfterBox = new System.Windows.Forms.TextBox();
            this.variantCaptionLabel = new System.Windows.Forms.Label();
            this.variantPanel = new System.Windows.Forms.Panel();
            this.showModifiedButton = new System.Windows.Forms.Button();
            this.showRegularButton = new System.Windows.Forms.Button();
            this.schoolLabel = new System.Windows.Forms.Label();
            this.schoolBox = new System.Windows.Forms.TextBox();
            this.tickerLabel = new System.Windows.Forms.Label();
            this.tickerBox = new System.Windows.Forms.TextBox();
            this.numbersLabel = new System.Windows.Forms.Label();
            this.numbersPanel = new System.Windows.Forms.Panel();
            this.perPageUpDown = new System.Windows.Forms.NumericUpDown();
            this.perPageLabel = new System.Windows.Forms.Label();
            this.daysUpDown = new System.Windows.Forms.NumericUpDown();
            this.daysLabel = new System.Windows.Forms.Label();
            this.lessonsUpDown = new System.Windows.Forms.NumericUpDown();
            this.lessonsLabel = new System.Windows.Forms.Label();
            this.rotateLabel = new System.Windows.Forms.Label();
            this.rotatePanel = new System.Windows.Forms.Panel();
            this.idleUpDown = new System.Windows.Forms.NumericUpDown();
            this.idleLabel = new System.Windows.Forms.Label();
            this.rotateUpDown = new System.Windows.Forms.NumericUpDown();
            this.autoRotateCheck = new System.Windows.Forms.CheckBox();
            this.extrasLabel = new System.Windows.Forms.Label();
            this.themeLabel = new System.Windows.Forms.Label();
            this.themeCombo = new System.Windows.Forms.ComboBox();
            this.replacementsCheck = new System.Windows.Forms.CheckBox();
            this.displayApplyPanel = new System.Windows.Forms.Panel();
            this.applyDisplayButton = new System.Windows.Forms.Button();
            this.tabAccess = new System.Windows.Forms.TabPage();
            this.accessInfoLabel = new System.Windows.Forms.Label();
            this.accessButtonsPanel = new System.Windows.Forms.Panel();
            this.exitAppButton = new System.Windows.Forms.Button();
            this.connectionButton = new System.Windows.Forms.Button();
            this.changePasswordButton = new System.Windows.Forms.Button();
            this.accessTable = new System.Windows.Forms.TableLayoutPanel();
            this.currentPasswordLabel = new System.Windows.Forms.Label();
            this.currentPasswordBox = new System.Windows.Forms.TextBox();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.newPasswordBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.confirmPasswordBox = new System.Windows.Forms.TextBox();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.dbLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.tabSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleGrid)).BeginInit();
            this.scheduleToolsPanel.SuspendLayout();
            this.scheduleTopPanel.SuspendLayout();
            this.tabClasses.SuspendLayout();
            this.classesSidePanel.SuspendLayout();
            this.tabBells.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bellsGrid)).BeginInit();
            this.bellsSidePanel.SuspendLayout();
            this.tabCalendar.SuspendLayout();
            this.calendarSidePanel.SuspendLayout();
            this.dayButtonsPanel.SuspendLayout();
            this.calendarCard.SuspendLayout();
            this.tabDisplay.SuspendLayout();
            this.displayTable.SuspendLayout();
            this.datePanel.SuspendLayout();
            this.variantPanel.SuspendLayout();
            this.numbersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.perPageUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.daysUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessonsUpDown)).BeginInit();
            this.rotatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateUpDown)).BeginInit();
            this.displayApplyPanel.SuspendLayout();
            this.tabAccess.SuspendLayout();
            this.accessButtonsPanel.SuspendLayout();
            this.accessTable.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSchedule);
            this.tabs.Controls.Add(this.tabClasses);
            this.tabs.Controls.Add(this.tabBells);
            this.tabs.Controls.Add(this.tabCalendar);
            this.tabs.Controls.Add(this.tabDisplay);
            this.tabs.Controls.Add(this.tabAccess);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.ItemSize = new System.Drawing.Size(220, 56);
            this.tabs.Location = new System.Drawing.Point(0, 90);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1500, 810);
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabs.TabIndex = 1;
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.scheduleGrid);
            this.tabSchedule.Controls.Add(this.scheduleToolsPanel);
            this.tabSchedule.Controls.Add(this.scheduleTopPanel);
            this.tabSchedule.Location = new System.Drawing.Point(4, 60);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Size = new System.Drawing.Size(1492, 746);
            this.tabSchedule.TabIndex = 0;
            this.tabSchedule.Text = "Расписание";
            // 
            // scheduleGrid
            // 
            this.scheduleGrid.AllowUserToAddRows = false;
            this.scheduleGrid.AllowUserToDeleteRows = false;
            this.scheduleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.scheduleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleGrid.Location = new System.Drawing.Point(0, 160);
            this.scheduleGrid.Name = "scheduleGrid";
            this.scheduleGrid.ReadOnly = true;
            this.scheduleGrid.RowHeadersVisible = false;
            this.scheduleGrid.Size = new System.Drawing.Size(1492, 586);
            this.scheduleGrid.TabIndex = 2;
            this.scheduleGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.ScheduleCellPainting);
            this.scheduleGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScheduleCellClick);
            this.scheduleGrid.SelectionChanged += new System.EventHandler(this.ScheduleSelectionChanged);
            // 
            // scheduleToolsPanel
            // 
            this.scheduleToolsPanel.Controls.Add(this.scheduleHintLabel);
            this.scheduleToolsPanel.Controls.Add(this.exportButton);
            this.scheduleToolsPanel.Controls.Add(this.importButton);
            this.scheduleToolsPanel.Controls.Add(this.copyDayButton);
            this.scheduleToolsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.scheduleToolsPanel.Location = new System.Drawing.Point(0, 80);
            this.scheduleToolsPanel.Name = "scheduleToolsPanel";
            this.scheduleToolsPanel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.scheduleToolsPanel.Size = new System.Drawing.Size(1492, 80);
            this.scheduleToolsPanel.TabIndex = 1;
            // 
            // scheduleHintLabel
            // 
            this.scheduleHintLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleHintLabel.Location = new System.Drawing.Point(12, 6);
            this.scheduleHintLabel.Name = "scheduleHintLabel";
            this.scheduleHintLabel.Size = new System.Drawing.Size(608, 68);
            this.scheduleHintLabel.TabIndex = 3;
            this.scheduleHintLabel.Text = "Нажмите на клетку, чтобы поставить или заменить урок";
            this.scheduleHintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exportButton
            // 
            this.exportButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.exportButton.Location = new System.Drawing.Point(1180, 6);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(300, 68);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "Сохранить в файл…";
            this.exportButton.Click += new System.EventHandler(this.ExportClicked);
            // 
            // importButton
            // 
            this.importButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.importButton.Location = new System.Drawing.Point(880, 6);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(300, 68);
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Загрузить из файла…";
            this.importButton.Click += new System.EventHandler(this.ImportClicked);
            // 
            // copyDayButton
            // 
            this.copyDayButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.copyDayButton.Location = new System.Drawing.Point(620, 6);
            this.copyDayButton.Name = "copyDayButton";
            this.copyDayButton.Size = new System.Drawing.Size(260, 68);
            this.copyDayButton.TabIndex = 0;
            this.copyDayButton.Text = "Скопировать день…";
            this.copyDayButton.Click += new System.EventHandler(this.CopyDayClicked);
            // 
            // scheduleTopPanel
            // 
            this.scheduleTopPanel.Controls.Add(this.clearVariantButton);
            this.scheduleTopPanel.Controls.Add(this.copyVariantButton);
            this.scheduleTopPanel.Controls.Add(this.classCombo);
            this.scheduleTopPanel.Controls.Add(this.classCaptionLabel);
            this.scheduleTopPanel.Controls.Add(this.modifiedVariantButton);
            this.scheduleTopPanel.Controls.Add(this.regularVariantButton);
            this.scheduleTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.scheduleTopPanel.Location = new System.Drawing.Point(0, 0);
            this.scheduleTopPanel.Name = "scheduleTopPanel";
            this.scheduleTopPanel.Padding = new System.Windows.Forms.Padding(12, 8, 12, 4);
            this.scheduleTopPanel.Size = new System.Drawing.Size(1492, 80);
            this.scheduleTopPanel.TabIndex = 0;
            // 
            // clearVariantButton
            // 
            this.clearVariantButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.clearVariantButton.Location = new System.Drawing.Point(1240, 8);
            this.clearVariantButton.Name = "clearVariantButton";
            this.clearVariantButton.Size = new System.Drawing.Size(240, 68);
            this.clearVariantButton.TabIndex = 5;
            this.clearVariantButton.Text = "Очистить сетку";
            this.clearVariantButton.Click += new System.EventHandler(this.ClearVariantClicked);
            // 
            // copyVariantButton
            // 
            this.copyVariantButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.copyVariantButton.Location = new System.Drawing.Point(880, 8);
            this.copyVariantButton.Name = "copyVariantButton";
            this.copyVariantButton.Size = new System.Drawing.Size(360, 68);
            this.copyVariantButton.TabIndex = 4;
            this.copyVariantButton.Text = "Копировать обычное → изменённое";
            this.copyVariantButton.Click += new System.EventHandler(this.CopyVariantClicked);
            // 
            // classCombo
            // 
            this.classCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.classCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.classCombo.Location = new System.Drawing.Point(620, 8);
            this.classCombo.Name = "classCombo";
            this.classCombo.Size = new System.Drawing.Size(240, 40);
            this.classCombo.TabIndex = 3;
            this.classCombo.SelectedIndexChanged += new System.EventHandler(this.ClassComboChanged);
            // 
            // classCaptionLabel
            // 
            this.classCaptionLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.classCaptionLabel.Location = new System.Drawing.Point(520, 8);
            this.classCaptionLabel.Name = "classCaptionLabel";
            this.classCaptionLabel.Size = new System.Drawing.Size(100, 68);
            this.classCaptionLabel.TabIndex = 2;
            this.classCaptionLabel.Text = "Класс";
            this.classCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // modifiedVariantButton
            // 
            this.modifiedVariantButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.modifiedVariantButton.Location = new System.Drawing.Point(272, 8);
            this.modifiedVariantButton.Name = "modifiedVariantButton";
            this.modifiedVariantButton.Size = new System.Drawing.Size(248, 68);
            this.modifiedVariantButton.TabIndex = 1;
            this.modifiedVariantButton.Text = "Изменённое";
            this.modifiedVariantButton.Click += new System.EventHandler(this.ModifiedVariantClicked);
            // 
            // regularVariantButton
            // 
            this.regularVariantButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.regularVariantButton.Location = new System.Drawing.Point(12, 8);
            this.regularVariantButton.Name = "regularVariantButton";
            this.regularVariantButton.Size = new System.Drawing.Size(260, 68);
            this.regularVariantButton.TabIndex = 0;
            this.regularVariantButton.Text = "Обычное";
            this.regularVariantButton.Click += new System.EventHandler(this.RegularVariantClicked);
            // 
            // tabClasses
            // 
            this.tabClasses.Controls.Add(this.classesList);
            this.tabClasses.Controls.Add(this.classesSidePanel);
            this.tabClasses.Location = new System.Drawing.Point(4, 60);
            this.tabClasses.Name = "tabClasses";
            this.tabClasses.Size = new System.Drawing.Size(1492, 746);
            this.tabClasses.TabIndex = 1;
            this.tabClasses.Text = "Классы";
            // 
            // classesList
            // 
            this.classesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classesList.IntegralHeight = false;
            this.classesList.ItemHeight = 44;
            this.classesList.Location = new System.Drawing.Point(0, 0);
            this.classesList.Name = "classesList";
            this.classesList.Size = new System.Drawing.Size(1032, 496);
            this.classesList.TabIndex = 0;
            // 
            // classesSidePanel
            // 
            this.classesSidePanel.Controls.Add(this.classesHintLabel);
            this.classesSidePanel.Controls.Add(this.classDownButton);
            this.classesSidePanel.Controls.Add(this.classUpButton);
            this.classesSidePanel.Controls.Add(this.classDeleteButton);
            this.classesSidePanel.Controls.Add(this.classRenameButton);
            this.classesSidePanel.Controls.Add(this.classAddButton);
            this.classesSidePanel.Controls.Add(this.classNameBox);
            this.classesSidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.classesSidePanel.Location = new System.Drawing.Point(1032, 0);
            this.classesSidePanel.Name = "classesSidePanel";
            this.classesSidePanel.Padding = new System.Windows.Forms.Padding(16);
            this.classesSidePanel.Size = new System.Drawing.Size(460, 496);
            this.classesSidePanel.TabIndex = 1;
            // 
            // classesHintLabel
            // 
            this.classesHintLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.classesHintLabel.Location = new System.Drawing.Point(16, 376);
            this.classesHintLabel.Name = "classesHintLabel";
            this.classesHintLabel.Size = new System.Drawing.Size(428, 104);
            this.classesHintLabel.TabIndex = 6;
            this.classesHintLabel.Text = "Можно вписать сразу несколько через запятую: 5А, 5Б, 6А";
            // 
            // classDownButton
            // 
            this.classDownButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.classDownButton.Location = new System.Drawing.Point(16, 336);
            this.classDownButton.Name = "classDownButton";
            this.classDownButton.Size = new System.Drawing.Size(428, 64);
            this.classDownButton.TabIndex = 5;
            this.classDownButton.Text = "Ниже ▼";
            this.classDownButton.Click += new System.EventHandler(this.ClassDownClicked);
            // 
            // classUpButton
            // 
            this.classUpButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.classUpButton.Location = new System.Drawing.Point(16, 272);
            this.classUpButton.Name = "classUpButton";
            this.classUpButton.Size = new System.Drawing.Size(428, 64);
            this.classUpButton.TabIndex = 4;
            this.classUpButton.Text = "Выше ▲";
            this.classUpButton.Click += new System.EventHandler(this.ClassUpClicked);
            // 
            // classDeleteButton
            // 
            this.classDeleteButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.classDeleteButton.Location = new System.Drawing.Point(16, 208);
            this.classDeleteButton.Name = "classDeleteButton";
            this.classDeleteButton.Size = new System.Drawing.Size(428, 64);
            this.classDeleteButton.TabIndex = 3;
            this.classDeleteButton.Text = "Удалить класс";
            this.classDeleteButton.Click += new System.EventHandler(this.ClassDeleteClicked);
            // 
            // classRenameButton
            // 
            this.classRenameButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.classRenameButton.Location = new System.Drawing.Point(16, 144);
            this.classRenameButton.Name = "classRenameButton";
            this.classRenameButton.Size = new System.Drawing.Size(428, 64);
            this.classRenameButton.TabIndex = 2;
            this.classRenameButton.Text = "Переименовать";
            this.classRenameButton.Click += new System.EventHandler(this.ClassRenameClicked);
            // 
            // classAddButton
            // 
            this.classAddButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.classAddButton.Location = new System.Drawing.Point(16, 80);
            this.classAddButton.Name = "classAddButton";
            this.classAddButton.Size = new System.Drawing.Size(428, 64);
            this.classAddButton.TabIndex = 1;
            this.classAddButton.Text = "Добавить класс";
            this.classAddButton.Click += new System.EventHandler(this.ClassAddClicked);
            // 
            // classNameBox
            // 
            this.classNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.classNameBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.classNameBox.Location = new System.Drawing.Point(16, 16);
            this.classNameBox.Name = "classNameBox";
            this.classNameBox.Size = new System.Drawing.Size(428, 40);
            this.classNameBox.TabIndex = 0;
            // 
            // editorKeyboard
            // 
            this.editorKeyboard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.editorKeyboard.Location = new System.Drawing.Point(0, 640);
            this.editorKeyboard.Name = "editorKeyboard";
            this.editorKeyboard.ShowEnterKey = true;
            this.editorKeyboard.Size = new System.Drawing.Size(1500, 260);
            this.editorKeyboard.TabIndex = 3;
            this.editorKeyboard.Visible = false;
            // 
            // tabBells
            // 
            this.tabBells.Controls.Add(this.bellsGrid);
            this.tabBells.Controls.Add(this.bellsSidePanel);
            this.tabBells.Location = new System.Drawing.Point(4, 60);
            this.tabBells.Name = "tabBells";
            this.tabBells.Size = new System.Drawing.Size(1492, 746);
            this.tabBells.TabIndex = 2;
            this.tabBells.Text = "Звонки";
            // 
            // bellsGrid
            // 
            this.bellsGrid.AllowUserToAddRows = false;
            this.bellsGrid.AllowUserToDeleteRows = false;
            this.bellsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.bellsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bellsGrid.Location = new System.Drawing.Point(0, 0);
            this.bellsGrid.Name = "bellsGrid";
            this.bellsGrid.RowHeadersVisible = false;
            this.bellsGrid.Size = new System.Drawing.Size(1032, 746);
            this.bellsGrid.TabIndex = 0;
            // 
            // bellsSidePanel
            // 
            this.bellsSidePanel.Controls.Add(this.bellsHintLabel);
            this.bellsSidePanel.Controls.Add(this.bellDeleteButton);
            this.bellsSidePanel.Controls.Add(this.bellAddButton);
            this.bellsSidePanel.Controls.Add(this.bellsSaveButton);
            this.bellsSidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.bellsSidePanel.Location = new System.Drawing.Point(1032, 0);
            this.bellsSidePanel.Name = "bellsSidePanel";
            this.bellsSidePanel.Padding = new System.Windows.Forms.Padding(16);
            this.bellsSidePanel.Size = new System.Drawing.Size(460, 746);
            this.bellsSidePanel.TabIndex = 1;
            // 
            // bellsHintLabel
            // 
            this.bellsHintLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bellsHintLabel.Location = new System.Drawing.Point(16, 570);
            this.bellsHintLabel.Name = "bellsHintLabel";
            this.bellsHintLabel.Size = new System.Drawing.Size(428, 160);
            this.bellsHintLabel.TabIndex = 3;
            this.bellsHintLabel.Text = "Время пишется как 08:30. По звонкам на экране подсвечивается идущий урок.";
            // 
            // bellDeleteButton
            // 
            this.bellDeleteButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.bellDeleteButton.Location = new System.Drawing.Point(16, 144);
            this.bellDeleteButton.Name = "bellDeleteButton";
            this.bellDeleteButton.Size = new System.Drawing.Size(428, 64);
            this.bellDeleteButton.TabIndex = 2;
            this.bellDeleteButton.Text = "Убрать последний урок";
            this.bellDeleteButton.Click += new System.EventHandler(this.BellDeleteClicked);
            // 
            // bellAddButton
            // 
            this.bellAddButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.bellAddButton.Location = new System.Drawing.Point(16, 80);
            this.bellAddButton.Name = "bellAddButton";
            this.bellAddButton.Size = new System.Drawing.Size(428, 64);
            this.bellAddButton.TabIndex = 1;
            this.bellAddButton.Text = "Добавить урок";
            this.bellAddButton.Click += new System.EventHandler(this.BellAddClicked);
            // 
            // bellsSaveButton
            // 
            this.bellsSaveButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.bellsSaveButton.Location = new System.Drawing.Point(16, 16);
            this.bellsSaveButton.Name = "bellsSaveButton";
            this.bellsSaveButton.Size = new System.Drawing.Size(428, 64);
            this.bellsSaveButton.TabIndex = 0;
            this.bellsSaveButton.Text = "Сохранить звонки";
            this.bellsSaveButton.Click += new System.EventHandler(this.BellsSaveClicked);
            // 
            // tabCalendar
            // 
            this.tabCalendar.Controls.Add(this.calendarSidePanel);
            this.tabCalendar.Controls.Add(this.calendarCard);
            this.tabCalendar.Location = new System.Drawing.Point(4, 60);
            this.tabCalendar.Name = "tabCalendar";
            this.tabCalendar.Size = new System.Drawing.Size(1492, 746);
            this.tabCalendar.TabIndex = 3;
            this.tabCalendar.Text = "Календарь";
            // 
            // calendarSidePanel
            // 
            this.calendarSidePanel.Controls.Add(this.upcomingList);
            this.calendarSidePanel.Controls.Add(this.upcomingLabel);
            this.calendarSidePanel.Controls.Add(this.dayButtonsPanel);
            this.calendarSidePanel.Controls.Add(this.dayVariantCombo);
            this.calendarSidePanel.Controls.Add(this.dayVariantLabel);
            this.calendarSidePanel.Controls.Add(this.dayTitleBox);
            this.calendarSidePanel.Controls.Add(this.dayTitleCaption);
            this.calendarSidePanel.Controls.Add(this.holidayCheck);
            this.calendarSidePanel.Controls.Add(this.dayHeaderLabel);
            this.calendarSidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarSidePanel.Location = new System.Drawing.Point(560, 0);
            this.calendarSidePanel.Name = "calendarSidePanel";
            this.calendarSidePanel.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.calendarSidePanel.Size = new System.Drawing.Size(932, 746);
            this.calendarSidePanel.TabIndex = 1;
            // 
            // upcomingList
            // 
            this.upcomingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upcomingList.IntegralHeight = false;
            this.upcomingList.ItemHeight = 36;
            this.upcomingList.Location = new System.Drawing.Point(20, 480);
            this.upcomingList.Name = "upcomingList";
            this.upcomingList.Size = new System.Drawing.Size(892, 250);
            this.upcomingList.TabIndex = 8;
            this.upcomingList.SelectedIndexChanged += new System.EventHandler(this.UpcomingSelected);
            // 
            // upcomingLabel
            // 
            this.upcomingLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.upcomingLabel.Location = new System.Drawing.Point(20, 440);
            this.upcomingLabel.Name = "upcomingLabel";
            this.upcomingLabel.Size = new System.Drawing.Size(892, 40);
            this.upcomingLabel.TabIndex = 7;
            this.upcomingLabel.Text = "Отмеченные дни";
            // 
            // dayButtonsPanel
            // 
            this.dayButtonsPanel.Controls.Add(this.dayDeleteButton);
            this.dayButtonsPanel.Controls.Add(this.daySaveButton);
            this.dayButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dayButtonsPanel.Location = new System.Drawing.Point(20, 360);
            this.dayButtonsPanel.Name = "dayButtonsPanel";
            this.dayButtonsPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.dayButtonsPanel.Size = new System.Drawing.Size(892, 80);
            this.dayButtonsPanel.TabIndex = 6;
            // 
            // dayDeleteButton
            // 
            this.dayDeleteButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.dayDeleteButton.Location = new System.Drawing.Point(340, 8);
            this.dayDeleteButton.Name = "dayDeleteButton";
            this.dayDeleteButton.Size = new System.Drawing.Size(300, 64);
            this.dayDeleteButton.TabIndex = 1;
            this.dayDeleteButton.Text = "Убрать отметку";
            this.dayDeleteButton.Click += new System.EventHandler(this.DayDeleteClicked);
            // 
            // daySaveButton
            // 
            this.daySaveButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.daySaveButton.Location = new System.Drawing.Point(0, 8);
            this.daySaveButton.Name = "daySaveButton";
            this.daySaveButton.Size = new System.Drawing.Size(340, 64);
            this.daySaveButton.TabIndex = 0;
            this.daySaveButton.Text = "Сохранить отметку";
            this.daySaveButton.Click += new System.EventHandler(this.DaySaveClicked);
            // 
            // dayVariantCombo
            // 
            this.dayVariantCombo.Dock = System.Windows.Forms.DockStyle.Top;
            this.dayVariantCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dayVariantCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dayVariantCombo.Location = new System.Drawing.Point(20, 312);
            this.dayVariantCombo.Name = "dayVariantCombo";
            this.dayVariantCombo.Size = new System.Drawing.Size(892, 40);
            this.dayVariantCombo.TabIndex = 5;
            // 
            // dayVariantLabel
            // 
            this.dayVariantLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dayVariantLabel.Location = new System.Drawing.Point(20, 272);
            this.dayVariantLabel.Name = "dayVariantLabel";
            this.dayVariantLabel.Size = new System.Drawing.Size(892, 40);
            this.dayVariantLabel.TabIndex = 4;
            this.dayVariantLabel.Text = "Какое расписание показывать в этот день";
            // 
            // dayTitleBox
            // 
            this.dayTitleBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dayTitleBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.dayTitleBox.Location = new System.Drawing.Point(20, 224);
            this.dayTitleBox.Name = "dayTitleBox";
            this.dayTitleBox.Size = new System.Drawing.Size(892, 40);
            this.dayTitleBox.TabIndex = 3;
            // 
            // dayTitleCaption
            // 
            this.dayTitleCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.dayTitleCaption.Location = new System.Drawing.Point(20, 184);
            this.dayTitleCaption.Name = "dayTitleCaption";
            this.dayTitleCaption.Size = new System.Drawing.Size(892, 40);
            this.dayTitleCaption.TabIndex = 2;
            this.dayTitleCaption.Text = "Название (например: День знаний, Каникулы)";
            // 
            // holidayCheck
            // 
            this.holidayCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.holidayCheck.Location = new System.Drawing.Point(20, 124);
            this.holidayCheck.Name = "holidayCheck";
            this.holidayCheck.Size = new System.Drawing.Size(892, 60);
            this.holidayCheck.TabIndex = 1;
            this.holidayCheck.Text = "Праздник или каникулы — уроков нет";
            // 
            // dayHeaderLabel
            // 
            this.dayHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dayHeaderLabel.Location = new System.Drawing.Point(20, 16);
            this.dayHeaderLabel.Name = "dayHeaderLabel";
            this.dayHeaderLabel.Size = new System.Drawing.Size(892, 108);
            this.dayHeaderLabel.TabIndex = 0;
            this.dayHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // calendarCard
            // 
            this.calendarCard.BackColor = System.Drawing.Color.White;
            this.calendarCard.Controls.Add(this.calendar);
            this.calendarCard.Dock = System.Windows.Forms.DockStyle.Left;
            this.calendarCard.Location = new System.Drawing.Point(0, 0);
            this.calendarCard.Name = "calendarCard";
            this.calendarCard.Size = new System.Drawing.Size(560, 746);
            this.calendarCard.TabIndex = 0;
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(24, 24);
            this.calendar.MaxSelectionCount = 1;
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 0;
            this.calendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.CalendarDateChanged);
            // 
            // tabDisplay
            // 
            this.tabDisplay.AutoScroll = true;
            this.tabDisplay.Controls.Add(this.displayTable);
            this.tabDisplay.Controls.Add(this.displayApplyPanel);
            this.tabDisplay.Location = new System.Drawing.Point(4, 60);
            this.tabDisplay.Name = "tabDisplay";
            this.tabDisplay.Size = new System.Drawing.Size(1492, 746);
            this.tabDisplay.TabIndex = 4;
            this.tabDisplay.Text = "Показ";
            // 
            // displayTable
            // 
            this.displayTable.ColumnCount = 2;
            this.displayTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.displayTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.displayTable.Controls.Add(this.modeLabel, 0, 0);
            this.displayTable.Controls.Add(this.modeCombo, 1, 0);
            this.displayTable.Controls.Add(this.displayClassLabel, 0, 1);
            this.displayTable.Controls.Add(this.displayClassCombo, 1, 1);
            this.displayTable.Controls.Add(this.dateLabel, 0, 2);
            this.displayTable.Controls.Add(this.datePanel, 1, 2);
            this.displayTable.Controls.Add(this.tomorrowLabel, 0, 3);
            this.displayTable.Controls.Add(this.tomorrowAfterBox, 1, 3);
            this.displayTable.Controls.Add(this.variantCaptionLabel, 0, 4);
            this.displayTable.Controls.Add(this.variantPanel, 1, 4);
            this.displayTable.Controls.Add(this.schoolLabel, 0, 5);
            this.displayTable.Controls.Add(this.schoolBox, 1, 5);
            this.displayTable.Controls.Add(this.tickerLabel, 0, 6);
            this.displayTable.Controls.Add(this.tickerBox, 1, 6);
            this.displayTable.Controls.Add(this.numbersLabel, 0, 7);
            this.displayTable.Controls.Add(this.numbersPanel, 1, 7);
            this.displayTable.Controls.Add(this.rotateLabel, 0, 8);
            this.displayTable.Controls.Add(this.rotatePanel, 1, 8);
            this.displayTable.Controls.Add(this.extrasLabel, 0, 9);
            this.displayTable.Controls.Add(this.replacementsCheck, 1, 9);
            this.displayTable.Controls.Add(this.themeLabel, 0, 10);
            this.displayTable.Controls.Add(this.themeCombo, 1, 10);
            this.displayTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayTable.Location = new System.Drawing.Point(0, 0);
            this.displayTable.Name = "displayTable";
            this.displayTable.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.displayTable.RowCount = 11;
            this.displayTable.Size = new System.Drawing.Size(1492, 646);
            this.displayTable.TabIndex = 0;
            // 
            // modeLabel
            // 
            this.modeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(414, 60);
            this.modeLabel.TabIndex = 0;
            this.modeLabel.Text = "Что показывать";
            this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modeCombo
            // 
            this.modeCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.modeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modeCombo.Name = "modeCombo";
            this.modeCombo.Size = new System.Drawing.Size(520, 40);
            this.modeCombo.TabIndex = 1;
            // 
            // displayClassLabel
            // 
            this.displayClassLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayClassLabel.Name = "displayClassLabel";
            this.displayClassLabel.Size = new System.Drawing.Size(414, 60);
            this.displayClassLabel.TabIndex = 2;
            this.displayClassLabel.Text = "Класс (для показа недели)";
            this.displayClassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // displayClassCombo
            // 
            this.displayClassCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.displayClassCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.displayClassCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.displayClassCombo.Name = "displayClassCombo";
            this.displayClassCombo.Size = new System.Drawing.Size(520, 40);
            this.displayClassCombo.TabIndex = 3;
            // 
            // dateLabel
            // 
            this.dateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(414, 60);
            this.dateLabel.TabIndex = 4;
            this.dateLabel.Text = "За какой день";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // datePanel
            // 
            this.datePanel.Controls.Add(this.datePicker);
            this.datePanel.Controls.Add(this.dateModeCombo);
            this.datePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datePanel.Name = "datePanel";
            this.datePanel.Size = new System.Drawing.Size(1000, 60);
            this.datePanel.TabIndex = 5;
            // 
            // datePicker
            // 
            this.datePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.datePicker.Location = new System.Drawing.Point(530, 0);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(420, 40);
            this.datePicker.TabIndex = 1;
            // 
            // dateModeCombo
            // 
            this.dateModeCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateModeCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dateModeCombo.Location = new System.Drawing.Point(0, 0);
            this.dateModeCombo.Name = "dateModeCombo";
            this.dateModeCombo.Size = new System.Drawing.Size(520, 40);
            this.dateModeCombo.TabIndex = 0;
            // 
            // tomorrowLabel
            // 
            this.tomorrowLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowLabel.Name = "tomorrowLabel";
            this.tomorrowLabel.Size = new System.Drawing.Size(414, 60);
            this.tomorrowLabel.TabIndex = 6;
            this.tomorrowLabel.Text = "После этого времени — завтрашний день";
            this.tomorrowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tomorrowAfterBox
            // 
            this.tomorrowAfterBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tomorrowAfterBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.tomorrowAfterBox.Name = "tomorrowAfterBox";
            this.tomorrowAfterBox.Size = new System.Drawing.Size(200, 40);
            this.tomorrowAfterBox.TabIndex = 7;
            // 
            // variantCaptionLabel
            // 
            this.variantCaptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variantCaptionLabel.Name = "variantCaptionLabel";
            this.variantCaptionLabel.Size = new System.Drawing.Size(414, 80);
            this.variantCaptionLabel.TabIndex = 8;
            this.variantCaptionLabel.Text = "Какое расписание сейчас на экране";
            this.variantCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // variantPanel
            // 
            this.variantPanel.Controls.Add(this.showModifiedButton);
            this.variantPanel.Controls.Add(this.showRegularButton);
            this.variantPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variantPanel.Name = "variantPanel";
            this.variantPanel.Size = new System.Drawing.Size(1000, 80);
            this.variantPanel.TabIndex = 9;
            // 
            // showModifiedButton
            // 
            this.showModifiedButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.showModifiedButton.Location = new System.Drawing.Point(340, 0);
            this.showModifiedButton.Name = "showModifiedButton";
            this.showModifiedButton.Size = new System.Drawing.Size(340, 68);
            this.showModifiedButton.TabIndex = 1;
            this.showModifiedButton.Text = "Изменённое";
            this.showModifiedButton.Click += new System.EventHandler(this.ShowModifiedClicked);
            // 
            // showRegularButton
            // 
            this.showRegularButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.showRegularButton.Location = new System.Drawing.Point(0, 0);
            this.showRegularButton.Name = "showRegularButton";
            this.showRegularButton.Size = new System.Drawing.Size(340, 68);
            this.showRegularButton.TabIndex = 0;
            this.showRegularButton.Text = "Обычное";
            this.showRegularButton.Click += new System.EventHandler(this.ShowRegularClicked);
            // 
            // schoolLabel
            // 
            this.schoolLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schoolLabel.Name = "schoolLabel";
            this.schoolLabel.Size = new System.Drawing.Size(414, 60);
            this.schoolLabel.TabIndex = 10;
            this.schoolLabel.Text = "Заголовок на экране";
            this.schoolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // schoolBox
            // 
            this.schoolBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.schoolBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.schoolBox.Name = "schoolBox";
            this.schoolBox.Size = new System.Drawing.Size(700, 40);
            this.schoolBox.TabIndex = 11;
            // 
            // tickerLabel
            // 
            this.tickerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tickerLabel.Name = "tickerLabel";
            this.tickerLabel.Size = new System.Drawing.Size(414, 60);
            this.tickerLabel.TabIndex = 12;
            this.tickerLabel.Text = "Объявление внизу экрана";
            this.tickerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tickerBox
            // 
            this.tickerBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tickerBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.tickerBox.Name = "tickerBox";
            this.tickerBox.Size = new System.Drawing.Size(900, 40);
            this.tickerBox.TabIndex = 13;
            // 
            // numbersLabel
            // 
            this.numbersLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numbersLabel.Name = "numbersLabel";
            this.numbersLabel.Size = new System.Drawing.Size(414, 70);
            this.numbersLabel.TabIndex = 14;
            this.numbersLabel.Text = "Размер сетки";
            this.numbersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numbersPanel
            // 
            this.numbersPanel.Controls.Add(this.perPageUpDown);
            this.numbersPanel.Controls.Add(this.perPageLabel);
            this.numbersPanel.Controls.Add(this.daysUpDown);
            this.numbersPanel.Controls.Add(this.daysLabel);
            this.numbersPanel.Controls.Add(this.lessonsUpDown);
            this.numbersPanel.Controls.Add(this.lessonsLabel);
            this.numbersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numbersPanel.Name = "numbersPanel";
            this.numbersPanel.Size = new System.Drawing.Size(1000, 70);
            this.numbersPanel.TabIndex = 15;
            // 
            // perPageUpDown
            // 
            this.perPageUpDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.perPageUpDown.Location = new System.Drawing.Point(880, 0);
            this.perPageUpDown.Name = "perPageUpDown";
            this.perPageUpDown.Size = new System.Drawing.Size(110, 40);
            this.perPageUpDown.TabIndex = 5;
            // 
            // perPageLabel
            // 
            this.perPageLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.perPageLabel.Location = new System.Drawing.Point(640, 0);
            this.perPageLabel.Name = "perPageLabel";
            this.perPageLabel.Size = new System.Drawing.Size(240, 60);
            this.perPageLabel.TabIndex = 4;
            this.perPageLabel.Text = "классов на экране";
            this.perPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // daysUpDown
            // 
            this.daysUpDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.daysUpDown.Location = new System.Drawing.Point(530, 0);
            this.daysUpDown.Name = "daysUpDown";
            this.daysUpDown.Size = new System.Drawing.Size(110, 40);
            this.daysUpDown.TabIndex = 3;
            // 
            // daysLabel
            // 
            this.daysLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.daysLabel.Location = new System.Drawing.Point(310, 0);
            this.daysLabel.Name = "daysLabel";
            this.daysLabel.Size = new System.Drawing.Size(220, 60);
            this.daysLabel.TabIndex = 2;
            this.daysLabel.Text = "учебных дней";
            this.daysLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lessonsUpDown
            // 
            this.lessonsUpDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.lessonsUpDown.Location = new System.Drawing.Point(200, 0);
            this.lessonsUpDown.Name = "lessonsUpDown";
            this.lessonsUpDown.Size = new System.Drawing.Size(110, 40);
            this.lessonsUpDown.TabIndex = 1;
            // 
            // lessonsLabel
            // 
            this.lessonsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lessonsLabel.Location = new System.Drawing.Point(0, 0);
            this.lessonsLabel.Name = "lessonsLabel";
            this.lessonsLabel.Size = new System.Drawing.Size(200, 60);
            this.lessonsLabel.TabIndex = 0;
            this.lessonsLabel.Text = "уроков";
            this.lessonsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rotateLabel
            // 
            this.rotateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotateLabel.Name = "rotateLabel";
            this.rotateLabel.Size = new System.Drawing.Size(414, 70);
            this.rotateLabel.TabIndex = 16;
            this.rotateLabel.Text = "Листать само";
            this.rotateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rotatePanel
            // 
            this.rotatePanel.Controls.Add(this.idleUpDown);
            this.rotatePanel.Controls.Add(this.idleLabel);
            this.rotatePanel.Controls.Add(this.rotateUpDown);
            this.rotatePanel.Controls.Add(this.autoRotateCheck);
            this.rotatePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotatePanel.Name = "rotatePanel";
            this.rotatePanel.Size = new System.Drawing.Size(1000, 70);
            this.rotatePanel.TabIndex = 17;
            // 
            // idleUpDown
            // 
            this.idleUpDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.idleUpDown.Location = new System.Drawing.Point(880, 0);
            this.idleUpDown.Name = "idleUpDown";
            this.idleUpDown.Size = new System.Drawing.Size(110, 40);
            this.idleUpDown.TabIndex = 3;
            // 
            // idleLabel
            // 
            this.idleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.idleLabel.Location = new System.Drawing.Point(480, 0);
            this.idleLabel.Name = "idleLabel";
            this.idleLabel.Size = new System.Drawing.Size(400, 60);
            this.idleLabel.TabIndex = 2;
            this.idleLabel.Text = "вернуться к этому показу через, с";
            this.idleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rotateUpDown
            // 
            this.rotateUpDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.rotateUpDown.Location = new System.Drawing.Point(370, 0);
            this.rotateUpDown.Name = "rotateUpDown";
            this.rotateUpDown.Size = new System.Drawing.Size(110, 40);
            this.rotateUpDown.TabIndex = 1;
            // 
            // autoRotateCheck
            // 
            this.autoRotateCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.autoRotateCheck.Location = new System.Drawing.Point(0, 0);
            this.autoRotateCheck.Name = "autoRotateCheck";
            this.autoRotateCheck.Size = new System.Drawing.Size(370, 60);
            this.autoRotateCheck.TabIndex = 0;
            this.autoRotateCheck.Text = "листать классы каждые, с";
            // 
            // extrasLabel
            // 
            this.extrasLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extrasLabel.Name = "extrasLabel";
            this.extrasLabel.Size = new System.Drawing.Size(414, 60);
            this.extrasLabel.TabIndex = 18;
            this.extrasLabel.Text = "Замены";
            this.extrasLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // replacementsCheck
            // 
            this.replacementsCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.replacementsCheck.Name = "replacementsCheck";
            this.replacementsCheck.Size = new System.Drawing.Size(900, 60);
            this.replacementsCheck.TabIndex = 19;
            this.replacementsCheck.Text = "подсвечивать оранжевым отличия от обычного расписания";
            // 
            // themeLabel
            // 
            this.themeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(414, 60);
            this.themeLabel.TabIndex = 20;
            this.themeLabel.Text = "Цвета экрана";
            this.themeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // themeCombo
            // 
            this.themeCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.themeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.themeCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.themeCombo.Name = "themeCombo";
            this.themeCombo.Size = new System.Drawing.Size(520, 40);
            this.themeCombo.TabIndex = 21;
            // 
            // displayApplyPanel
            // 
            this.displayApplyPanel.Controls.Add(this.applyDisplayButton);
            this.displayApplyPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.displayApplyPanel.Location = new System.Drawing.Point(0, 646);
            this.displayApplyPanel.Name = "displayApplyPanel";
            this.displayApplyPanel.Padding = new System.Windows.Forms.Padding(24, 10, 24, 16);
            this.displayApplyPanel.Size = new System.Drawing.Size(1492, 100);
            this.displayApplyPanel.TabIndex = 1;
            // 
            // applyDisplayButton
            // 
            this.applyDisplayButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.applyDisplayButton.Location = new System.Drawing.Point(908, 10);
            this.applyDisplayButton.Name = "applyDisplayButton";
            this.applyDisplayButton.Size = new System.Drawing.Size(560, 74);
            this.applyDisplayButton.TabIndex = 0;
            this.applyDisplayButton.Text = "Применить и показать на экране";
            this.applyDisplayButton.Click += new System.EventHandler(this.ApplyDisplayClicked);
            // 
            // tabAccess
            // 
            this.tabAccess.Controls.Add(this.accessInfoLabel);
            this.tabAccess.Controls.Add(this.accessButtonsPanel);
            this.tabAccess.Controls.Add(this.accessTable);
            this.tabAccess.Location = new System.Drawing.Point(4, 60);
            this.tabAccess.Name = "tabAccess";
            this.tabAccess.Size = new System.Drawing.Size(1492, 746);
            this.tabAccess.TabIndex = 5;
            this.tabAccess.Text = "Доступ";
            // 
            // accessInfoLabel
            // 
            this.accessInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accessInfoLabel.Location = new System.Drawing.Point(0, 340);
            this.accessInfoLabel.Name = "accessInfoLabel";
            this.accessInfoLabel.Padding = new System.Windows.Forms.Padding(24, 12, 24, 12);
            this.accessInfoLabel.Size = new System.Drawing.Size(1492, 306);
            this.accessInfoLabel.TabIndex = 2;
            // 
            // accessButtonsPanel
            // 
            this.accessButtonsPanel.Controls.Add(this.exitAppButton);
            this.accessButtonsPanel.Controls.Add(this.connectionButton);
            this.accessButtonsPanel.Controls.Add(this.changePasswordButton);
            this.accessButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.accessButtonsPanel.Location = new System.Drawing.Point(0, 646);
            this.accessButtonsPanel.Name = "accessButtonsPanel";
            this.accessButtonsPanel.Padding = new System.Windows.Forms.Padding(24, 10, 24, 16);
            this.accessButtonsPanel.Size = new System.Drawing.Size(1492, 100);
            this.accessButtonsPanel.TabIndex = 1;
            // 
            // exitAppButton
            // 
            this.exitAppButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.exitAppButton.Location = new System.Drawing.Point(1148, 10);
            this.exitAppButton.Name = "exitAppButton";
            this.exitAppButton.Size = new System.Drawing.Size(320, 74);
            this.exitAppButton.TabIndex = 2;
            this.exitAppButton.Text = "Выйти из программы";
            this.exitAppButton.Click += new System.EventHandler(this.ExitAppClicked);
            // 
            // connectionButton
            // 
            this.connectionButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.connectionButton.Location = new System.Drawing.Point(444, 10);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(420, 74);
            this.connectionButton.TabIndex = 1;
            this.connectionButton.Text = "Подключение к базе (ip.txt)";
            this.connectionButton.Click += new System.EventHandler(this.ConnectionClicked);
            // 
            // changePasswordButton
            // 
            this.changePasswordButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.changePasswordButton.Location = new System.Drawing.Point(24, 10);
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.Size = new System.Drawing.Size(420, 74);
            this.changePasswordButton.TabIndex = 0;
            this.changePasswordButton.Text = "Сменить пароль";
            this.changePasswordButton.Click += new System.EventHandler(this.ChangePasswordClicked);
            // 
            // accessTable
            // 
            this.accessTable.ColumnCount = 2;
            this.accessTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.accessTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.accessTable.Controls.Add(this.currentPasswordLabel, 0, 0);
            this.accessTable.Controls.Add(this.currentPasswordBox, 1, 0);
            this.accessTable.Controls.Add(this.newPasswordLabel, 0, 1);
            this.accessTable.Controls.Add(this.newPasswordBox, 1, 1);
            this.accessTable.Controls.Add(this.confirmPasswordLabel, 0, 2);
            this.accessTable.Controls.Add(this.confirmPasswordBox, 1, 2);
            this.accessTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.accessTable.Location = new System.Drawing.Point(0, 0);
            this.accessTable.Name = "accessTable";
            this.accessTable.Padding = new System.Windows.Forms.Padding(24, 20, 24, 10);
            this.accessTable.RowCount = 3;
            this.accessTable.Size = new System.Drawing.Size(1492, 340);
            this.accessTable.TabIndex = 0;
            // 
            // currentPasswordLabel
            // 
            this.currentPasswordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentPasswordLabel.Name = "currentPasswordLabel";
            this.currentPasswordLabel.Size = new System.Drawing.Size(414, 70);
            this.currentPasswordLabel.TabIndex = 0;
            this.currentPasswordLabel.Text = "Текущий пароль";
            this.currentPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentPasswordBox
            // 
            this.currentPasswordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentPasswordBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.currentPasswordBox.Name = "currentPasswordBox";
            this.currentPasswordBox.PasswordChar = '●';
            this.currentPasswordBox.Size = new System.Drawing.Size(520, 40);
            this.currentPasswordBox.TabIndex = 1;
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(414, 70);
            this.newPasswordLabel.TabIndex = 2;
            this.newPasswordLabel.Text = "Новый пароль";
            this.newPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // newPasswordBox
            // 
            this.newPasswordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newPasswordBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.newPasswordBox.Name = "newPasswordBox";
            this.newPasswordBox.PasswordChar = '●';
            this.newPasswordBox.Size = new System.Drawing.Size(520, 40);
            this.newPasswordBox.TabIndex = 3;
            // 
            // confirmPasswordLabel
            // 
            this.confirmPasswordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.confirmPasswordLabel.Name = "confirmPasswordLabel";
            this.confirmPasswordLabel.Size = new System.Drawing.Size(414, 70);
            this.confirmPasswordLabel.TabIndex = 4;
            this.confirmPasswordLabel.Text = "Ещё раз новый пароль";
            this.confirmPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // confirmPasswordBox
            // 
            this.confirmPasswordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.confirmPasswordBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.confirmPasswordBox.Name = "confirmPasswordBox";
            this.confirmPasswordBox.PasswordChar = '●';
            this.confirmPasswordBox.Size = new System.Drawing.Size(520, 40);
            this.confirmPasswordBox.TabIndex = 5;
            // 
            // statusPanel
            // 
            this.statusPanel.Controls.Add(this.statusLabel);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Location = new System.Drawing.Point(0, 900);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.statusPanel.Size = new System.Drawing.Size(1500, 60);
            this.statusPanel.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Location = new System.Drawing.Point(24, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(1452, 60);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.dbLabel);
            this.headerPanel.Controls.Add(this.closeButton);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(24, 10, 12, 10);
            this.headerPanel.Size = new System.Drawing.Size(1500, 90);
            this.headerPanel.TabIndex = 0;
            // 
            // dbLabel
            // 
            this.dbLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbLabel.Location = new System.Drawing.Point(424, 10);
            this.dbLabel.Name = "dbLabel";
            this.dbLabel.Size = new System.Drawing.Size(764, 70);
            this.dbLabel.TabIndex = 2;
            this.dbLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // closeButton
            // 
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeButton.Location = new System.Drawing.Point(1188, 10);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(300, 70);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Готово";
            this.closeButton.Click += new System.EventHandler(this.CloseClicked);
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.titleLabel.Location = new System.Drawing.Point(24, 10);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(400, 70);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Режим учителя";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1500, 960);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.editorKeyboard);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.headerPanel);
            this.KeyPreview = true;
            this.Name = "EditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Режим учителя";
            this.tabs.ResumeLayout(false);
            this.tabSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scheduleGrid)).EndInit();
            this.scheduleToolsPanel.ResumeLayout(false);
            this.scheduleTopPanel.ResumeLayout(false);
            this.tabClasses.ResumeLayout(false);
            this.classesSidePanel.ResumeLayout(false);
            this.classesSidePanel.PerformLayout();
            this.tabBells.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bellsGrid)).EndInit();
            this.bellsSidePanel.ResumeLayout(false);
            this.tabCalendar.ResumeLayout(false);
            this.calendarSidePanel.ResumeLayout(false);
            this.calendarSidePanel.PerformLayout();
            this.dayButtonsPanel.ResumeLayout(false);
            this.calendarCard.ResumeLayout(false);
            this.tabDisplay.ResumeLayout(false);
            this.displayTable.ResumeLayout(false);
            this.displayTable.PerformLayout();
            this.datePanel.ResumeLayout(false);
            this.variantPanel.ResumeLayout(false);
            this.numbersPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.perPageUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.daysUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessonsUpDown)).EndInit();
            this.rotatePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.idleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateUpDown)).EndInit();
            this.displayApplyPanel.ResumeLayout(false);
            this.tabAccess.ResumeLayout(false);
            this.accessButtonsPanel.ResumeLayout(false);
            this.accessTable.ResumeLayout(false);
            this.accessTable.PerformLayout();
            this.statusPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabSchedule;
        private System.Windows.Forms.DataGridView scheduleGrid;
        private System.Windows.Forms.Panel scheduleToolsPanel;
        private System.Windows.Forms.Label scheduleHintLabel;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button copyDayButton;
        private System.Windows.Forms.Panel scheduleTopPanel;
        private System.Windows.Forms.Button clearVariantButton;
        private System.Windows.Forms.Button copyVariantButton;
        private System.Windows.Forms.ComboBox classCombo;
        private System.Windows.Forms.Label classCaptionLabel;
        private System.Windows.Forms.Button modifiedVariantButton;
        private System.Windows.Forms.Button regularVariantButton;
        private System.Windows.Forms.TabPage tabClasses;
        private System.Windows.Forms.ListBox classesList;
        private System.Windows.Forms.Panel classesSidePanel;
        private System.Windows.Forms.Label classesHintLabel;
        private System.Windows.Forms.Button classDownButton;
        private System.Windows.Forms.Button classUpButton;
        private System.Windows.Forms.Button classDeleteButton;
        private System.Windows.Forms.Button classRenameButton;
        private System.Windows.Forms.Button classAddButton;
        private System.Windows.Forms.TextBox classNameBox;
        private SchoolSchedule.Controls.OnScreenKeyboard editorKeyboard;
        private System.Windows.Forms.TabPage tabBells;
        private System.Windows.Forms.DataGridView bellsGrid;
        private System.Windows.Forms.Panel bellsSidePanel;
        private System.Windows.Forms.Label bellsHintLabel;
        private System.Windows.Forms.Button bellDeleteButton;
        private System.Windows.Forms.Button bellAddButton;
        private System.Windows.Forms.Button bellsSaveButton;
        private System.Windows.Forms.TabPage tabCalendar;
        private System.Windows.Forms.Panel calendarSidePanel;
        private System.Windows.Forms.ListBox upcomingList;
        private System.Windows.Forms.Label upcomingLabel;
        private System.Windows.Forms.Panel dayButtonsPanel;
        private System.Windows.Forms.Button dayDeleteButton;
        private System.Windows.Forms.Button daySaveButton;
        private System.Windows.Forms.ComboBox dayVariantCombo;
        private System.Windows.Forms.Label dayVariantLabel;
        private System.Windows.Forms.TextBox dayTitleBox;
        private System.Windows.Forms.Label dayTitleCaption;
        private System.Windows.Forms.CheckBox holidayCheck;
        private System.Windows.Forms.Label dayHeaderLabel;
        private System.Windows.Forms.Panel calendarCard;
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.TabPage tabDisplay;
        private System.Windows.Forms.TableLayoutPanel displayTable;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.ComboBox modeCombo;
        private System.Windows.Forms.Label displayClassLabel;
        private System.Windows.Forms.ComboBox displayClassCombo;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Panel datePanel;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.ComboBox dateModeCombo;
        private System.Windows.Forms.Label tomorrowLabel;
        private System.Windows.Forms.TextBox tomorrowAfterBox;
        private System.Windows.Forms.Label variantCaptionLabel;
        private System.Windows.Forms.Panel variantPanel;
        private System.Windows.Forms.Button showModifiedButton;
        private System.Windows.Forms.Button showRegularButton;
        private System.Windows.Forms.Label schoolLabel;
        private System.Windows.Forms.TextBox schoolBox;
        private System.Windows.Forms.Label tickerLabel;
        private System.Windows.Forms.TextBox tickerBox;
        private System.Windows.Forms.Label numbersLabel;
        private System.Windows.Forms.Panel numbersPanel;
        private System.Windows.Forms.NumericUpDown perPageUpDown;
        private System.Windows.Forms.Label perPageLabel;
        private System.Windows.Forms.NumericUpDown daysUpDown;
        private System.Windows.Forms.Label daysLabel;
        private System.Windows.Forms.NumericUpDown lessonsUpDown;
        private System.Windows.Forms.Label lessonsLabel;
        private System.Windows.Forms.Label rotateLabel;
        private System.Windows.Forms.Panel rotatePanel;
        private System.Windows.Forms.NumericUpDown idleUpDown;
        private System.Windows.Forms.Label idleLabel;
        private System.Windows.Forms.NumericUpDown rotateUpDown;
        private System.Windows.Forms.CheckBox autoRotateCheck;
        private System.Windows.Forms.Label extrasLabel;
        private System.Windows.Forms.CheckBox replacementsCheck;
        private System.Windows.Forms.Label themeLabel;
        private System.Windows.Forms.ComboBox themeCombo;
        private System.Windows.Forms.Panel displayApplyPanel;
        private System.Windows.Forms.Button applyDisplayButton;
        private System.Windows.Forms.TabPage tabAccess;
        private System.Windows.Forms.Label accessInfoLabel;
        private System.Windows.Forms.Panel accessButtonsPanel;
        private System.Windows.Forms.Button exitAppButton;
        private System.Windows.Forms.Button connectionButton;
        private System.Windows.Forms.Button changePasswordButton;
        private System.Windows.Forms.TableLayoutPanel accessTable;
        private System.Windows.Forms.Label currentPasswordLabel;
        private System.Windows.Forms.TextBox currentPasswordBox;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.TextBox newPasswordBox;
        private System.Windows.Forms.Label confirmPasswordLabel;
        private System.Windows.Forms.TextBox confirmPasswordBox;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label dbLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label titleLabel;
    }
}
