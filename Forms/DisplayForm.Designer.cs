namespace SchoolSchedule.Forms
{
    partial class DisplayForm
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.messageLabel = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.dateLabel = new System.Windows.Forms.Label();
            this.variantLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.clockLabel = new System.Windows.Forms.Label();
            this.schoolLabel = new System.Windows.Forms.Label();
            this.subHeaderPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.tickerPanel = new System.Windows.Forms.Panel();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.navFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.classButton = new System.Windows.Forms.Button();
            this.modeButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.todayButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.calendarButton = new System.Windows.Forms.Button();
            this.pagePrevButton = new System.Windows.Forms.Button();
            this.pageLabel = new System.Windows.Forms.Label();
            this.pageNextButton = new System.Windows.Forms.Button();
            this.teacherButton = new System.Windows.Forms.Button();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            this.pollTimer = new System.Windows.Forms.Timer(this.components);
            this.rotateTimer = new System.Windows.Forms.Timer(this.components);
            this.tickerTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.subHeaderPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.navFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 170);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.Size = new System.Drawing.Size(1600, 630);
            this.grid.TabIndex = 2;
            this.grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.GridCellPainting);
            this.grid.SelectionChanged += new System.EventHandler(this.GridSelectionChanged);
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCellClick);
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLabel.Location = new System.Drawing.Point(0, 170);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(1600, 630);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.messageLabel.Visible = false;
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.dateLabel);
            this.headerPanel.Controls.Add(this.variantLabel);
            this.headerPanel.Controls.Add(this.statusLabel);
            this.headerPanel.Controls.Add(this.clockLabel);
            this.headerPanel.Controls.Add(this.schoolLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1600, 110);
            this.headerPanel.TabIndex = 0;
            this.headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnyTouch);
            // 
            // dateLabel
            // 
            this.dateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateLabel.Location = new System.Drawing.Point(420, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(340, 110);
            this.dateLabel.TabIndex = 4;
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // variantLabel
            // 
            this.variantLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.variantLabel.Location = new System.Drawing.Point(760, 0);
            this.variantLabel.Name = "variantLabel";
            this.variantLabel.Size = new System.Drawing.Size(380, 110);
            this.variantLabel.TabIndex = 3;
            this.variantLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.statusLabel.Location = new System.Drawing.Point(1140, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(240, 110);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clockLabel
            // 
            this.clockLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.clockLabel.Location = new System.Drawing.Point(1380, 0);
            this.clockLabel.Name = "clockLabel";
            this.clockLabel.Size = new System.Drawing.Size(220, 110);
            this.clockLabel.TabIndex = 1;
            this.clockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // schoolLabel
            // 
            this.schoolLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.schoolLabel.Location = new System.Drawing.Point(0, 0);
            this.schoolLabel.Name = "schoolLabel";
            this.schoolLabel.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.schoolLabel.Size = new System.Drawing.Size(420, 110);
            this.schoolLabel.TabIndex = 0;
            this.schoolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subHeaderPanel
            // 
            this.subHeaderPanel.Controls.Add(this.titleLabel);
            this.subHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.subHeaderPanel.Location = new System.Drawing.Point(0, 110);
            this.subHeaderPanel.Name = "subHeaderPanel";
            this.subHeaderPanel.Size = new System.Drawing.Size(1600, 60);
            this.subHeaderPanel.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1600, 60);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tickerPanel
            // 
            this.tickerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tickerPanel.Location = new System.Drawing.Point(0, 800);
            this.tickerPanel.Name = "tickerPanel";
            this.tickerPanel.Size = new System.Drawing.Size(1600, 56);
            this.tickerPanel.TabIndex = 4;
            this.tickerPanel.Visible = false;
            this.tickerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TickerPaint);
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.navFlow);
            this.footerPanel.Controls.Add(this.teacherButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 856);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.footerPanel.Size = new System.Drawing.Size(1600, 104);
            this.footerPanel.TabIndex = 5;
            // 
            // navFlow
            // 
            this.navFlow.Controls.Add(this.classButton);
            this.navFlow.Controls.Add(this.modeButton);
            this.navFlow.Controls.Add(this.prevButton);
            this.navFlow.Controls.Add(this.todayButton);
            this.navFlow.Controls.Add(this.nextButton);
            this.navFlow.Controls.Add(this.calendarButton);
            this.navFlow.Controls.Add(this.pagePrevButton);
            this.navFlow.Controls.Add(this.pageLabel);
            this.navFlow.Controls.Add(this.pageNextButton);
            this.navFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navFlow.Location = new System.Drawing.Point(16, 10);
            this.navFlow.Name = "navFlow";
            this.navFlow.Size = new System.Drawing.Size(1348, 84);
            this.navFlow.TabIndex = 0;
            this.navFlow.WrapContents = false;
            // 
            // classButton
            // 
            this.classButton.Location = new System.Drawing.Point(3, 3);
            this.classButton.Name = "classButton";
            this.classButton.Size = new System.Drawing.Size(240, 78);
            this.classButton.TabIndex = 0;
            this.classButton.Text = "Класс";
            this.classButton.Click += new System.EventHandler(this.ClassClicked);
            // 
            // modeButton
            // 
            this.modeButton.Location = new System.Drawing.Point(249, 3);
            this.modeButton.Name = "modeButton";
            this.modeButton.Size = new System.Drawing.Size(240, 78);
            this.modeButton.TabIndex = 1;
            this.modeButton.Text = "Неделя / День";
            this.modeButton.Click += new System.EventHandler(this.ModeClicked);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(495, 3);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(100, 78);
            this.prevButton.TabIndex = 2;
            this.prevButton.Text = "◀";
            this.prevButton.Click += new System.EventHandler(this.PrevClicked);
            // 
            // todayButton
            // 
            this.todayButton.Location = new System.Drawing.Point(601, 3);
            this.todayButton.Name = "todayButton";
            this.todayButton.Size = new System.Drawing.Size(180, 78);
            this.todayButton.TabIndex = 3;
            this.todayButton.Text = "Сегодня";
            this.todayButton.Click += new System.EventHandler(this.TodayClicked);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(787, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(100, 78);
            this.nextButton.TabIndex = 4;
            this.nextButton.Text = "▶";
            this.nextButton.Click += new System.EventHandler(this.NextClicked);
            // 
            // calendarButton
            // 
            this.calendarButton.Location = new System.Drawing.Point(893, 3);
            this.calendarButton.Name = "calendarButton";
            this.calendarButton.Size = new System.Drawing.Size(220, 78);
            this.calendarButton.TabIndex = 5;
            this.calendarButton.Text = "Календарь";
            this.calendarButton.Click += new System.EventHandler(this.CalendarClicked);
            // 
            // pagePrevButton
            // 
            this.pagePrevButton.Location = new System.Drawing.Point(1119, 3);
            this.pagePrevButton.Name = "pagePrevButton";
            this.pagePrevButton.Size = new System.Drawing.Size(80, 78);
            this.pagePrevButton.TabIndex = 6;
            this.pagePrevButton.Text = "‹";
            this.pagePrevButton.Visible = false;
            this.pagePrevButton.Click += new System.EventHandler(this.PagePrevClicked);
            // 
            // pageLabel
            // 
            this.pageLabel.Location = new System.Drawing.Point(1205, 3);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(120, 78);
            this.pageLabel.TabIndex = 7;
            this.pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pageLabel.Visible = false;
            // 
            // pageNextButton
            // 
            this.pageNextButton.Location = new System.Drawing.Point(1331, 3);
            this.pageNextButton.Name = "pageNextButton";
            this.pageNextButton.Size = new System.Drawing.Size(80, 78);
            this.pageNextButton.TabIndex = 8;
            this.pageNextButton.Text = "›";
            this.pageNextButton.Visible = false;
            this.pageNextButton.Click += new System.EventHandler(this.PageNextClicked);
            // 
            // teacherButton
            // 
            this.teacherButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.teacherButton.Location = new System.Drawing.Point(1364, 10);
            this.teacherButton.Name = "teacherButton";
            this.teacherButton.Size = new System.Drawing.Size(220, 84);
            this.teacherButton.TabIndex = 1;
            this.teacherButton.Text = "Учитель";
            this.teacherButton.Click += new System.EventHandler(this.TeacherClicked);
            // 
            // clockTimer
            // 
            this.clockTimer.Enabled = true;
            this.clockTimer.Interval = 1000;
            this.clockTimer.Tick += new System.EventHandler(this.ClockTick);
            // 
            // pollTimer
            // 
            this.pollTimer.Enabled = true;
            this.pollTimer.Interval = 10000;
            this.pollTimer.Tick += new System.EventHandler(this.PollTick);
            // 
            // rotateTimer
            // 
            this.rotateTimer.Interval = 20000;
            this.rotateTimer.Tick += new System.EventHandler(this.RotateTick);
            // 
            // tickerTimer
            // 
            this.tickerTimer.Interval = 40;
            this.tickerTimer.Tick += new System.EventHandler(this.TickerTick);
            // 
            // DisplayForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1600, 960);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.tickerPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.subHeaderPanel);
            this.Controls.Add(this.headerPanel);
            this.KeyPreview = true;
            this.Name = "DisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расписание уроков";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.subHeaderPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.navFlow.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label variantLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label clockLabel;
        private System.Windows.Forms.Label schoolLabel;
        private System.Windows.Forms.Panel subHeaderPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel tickerPanel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.FlowLayoutPanel navFlow;
        private System.Windows.Forms.Button classButton;
        private System.Windows.Forms.Button modeButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button todayButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button calendarButton;
        private System.Windows.Forms.Button pagePrevButton;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Button pageNextButton;
        private System.Windows.Forms.Button teacherButton;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Timer pollTimer;
        private System.Windows.Forms.Timer rotateTimer;
        private System.Windows.Forms.Timer tickerTimer;
    }
}
