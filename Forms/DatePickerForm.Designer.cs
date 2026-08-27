namespace SchoolSchedule.Forms
{
    partial class DatePickerForm
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
            this.calendarCard = new System.Windows.Forms.Panel();
            this.calendar = new SchoolSchedule.Controls.TouchCalendar();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.infoLabel = new System.Windows.Forms.Label();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.quickPanel = new System.Windows.Forms.Panel();
            this.nextSchoolDayButton = new System.Windows.Forms.Button();
            this.tomorrowButton = new System.Windows.Forms.Button();
            this.todayButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.calendarCard.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.quickPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // calendarCard
            // 
            this.calendarCard.Controls.Add(this.calendar);
            this.calendarCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarCard.Location = new System.Drawing.Point(0, 190);
            this.calendarCard.Name = "calendarCard";
            this.calendarCard.Size = new System.Drawing.Size(880, 430);
            this.calendarCard.TabIndex = 2;
            // 
            // calendar
            // 
            this.calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendar.Location = new System.Drawing.Point(0, 0);
            this.calendar.Name = "calendar";
            this.calendar.Size = new System.Drawing.Size(880, 430);
            this.calendar.TabIndex = 0;
            this.calendar.DateChanged += new System.EventHandler(this.CalendarDateChanged);
            this.calendar.MonthChanged += new System.EventHandler(this.CalendarMonthChanged);
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.infoLabel);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoPanel.Location = new System.Drawing.Point(0, 620);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(880, 70);
            this.infoPanel.TabIndex = 3;
            // 
            // infoLabel
            // 
            this.infoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLabel.Location = new System.Drawing.Point(0, 0);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(880, 70);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.cancelButton);
            this.footerPanel.Controls.Add(this.okButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 690);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 18);
            this.footerPanel.Size = new System.Drawing.Size(880, 110);
            this.footerPanel.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.cancelButton.Location = new System.Drawing.Point(20, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(180, 80);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.Click += new System.EventHandler(this.CancelClicked);
            // 
            // okButton
            // 
            this.okButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.okButton.Location = new System.Drawing.Point(560, 12);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(300, 80);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Показать";
            this.okButton.Click += new System.EventHandler(this.OkClicked);
            // 
            // quickPanel
            // 
            this.quickPanel.Controls.Add(this.nextSchoolDayButton);
            this.quickPanel.Controls.Add(this.tomorrowButton);
            this.quickPanel.Controls.Add(this.todayButton);
            this.quickPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickPanel.Location = new System.Drawing.Point(0, 90);
            this.quickPanel.Name = "quickPanel";
            this.quickPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.quickPanel.Size = new System.Drawing.Size(880, 100);
            this.quickPanel.TabIndex = 1;
            // 
            // 
            // nextSchoolDayButton
            // 
            this.nextSchoolDayButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.nextSchoolDayButton.Location = new System.Drawing.Point(360, 15);
            this.nextSchoolDayButton.Name = "nextSchoolDayButton";
            this.nextSchoolDayButton.Size = new System.Drawing.Size(280, 70);
            this.nextSchoolDayButton.TabIndex = 2;
            this.nextSchoolDayButton.Text = "Ближайший учебный";
            this.nextSchoolDayButton.Click += new System.EventHandler(this.NextSchoolDayClicked);
            // 
            // tomorrowButton
            // 
            this.tomorrowButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.tomorrowButton.Location = new System.Drawing.Point(190, 15);
            this.tomorrowButton.Name = "tomorrowButton";
            this.tomorrowButton.Size = new System.Drawing.Size(170, 70);
            this.tomorrowButton.TabIndex = 1;
            this.tomorrowButton.Text = "Завтра";
            this.tomorrowButton.Click += new System.EventHandler(this.TomorrowClicked);
            // 
            // todayButton
            // 
            this.todayButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.todayButton.Location = new System.Drawing.Point(20, 15);
            this.todayButton.Name = "todayButton";
            this.todayButton.Size = new System.Drawing.Size(170, 70);
            this.todayButton.TabIndex = 0;
            this.todayButton.Text = "Сегодня";
            this.todayButton.Click += new System.EventHandler(this.TodayClicked);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(880, 90);
            this.headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(880, 90);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Выберите дату";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DatePickerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(880, 800);
            this.Controls.Add(this.calendarCard);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.quickPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "DatePickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор даты";
            this.calendarCard.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.quickPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel calendarCard;
        private SchoolSchedule.Controls.TouchCalendar calendar;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel quickPanel;
        private System.Windows.Forms.Button nextSchoolDayButton;
        private System.Windows.Forms.Button tomorrowButton;
        private System.Windows.Forms.Button todayButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
    }
}
