namespace SchoolSchedule.Forms
{
    partial class BellEditForm
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
            this.rowsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.startLabel = new System.Windows.Forms.Label();
            this.startHourDownButton = new System.Windows.Forms.Button();
            this.startHourUpButton = new System.Windows.Forms.Button();
            this.startTimeLabel = new System.Windows.Forms.Label();
            this.startMinuteDownButton = new System.Windows.Forms.Button();
            this.startMinuteUpButton = new System.Windows.Forms.Button();
            this.endLabel = new System.Windows.Forms.Label();
            this.endHourDownButton = new System.Windows.Forms.Button();
            this.endHourUpButton = new System.Windows.Forms.Button();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.endMinuteDownButton = new System.Windows.Forms.Button();
            this.endMinuteUpButton = new System.Windows.Forms.Button();
            this.hintLabel = new System.Windows.Forms.Label();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.rowsPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rowsPanel
            // 
            this.rowsPanel.ColumnCount = 6;
            this.rowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.rowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.rowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.rowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.rowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.rowsPanel.Controls.Add(this.startLabel, 0, 0);
            this.rowsPanel.Controls.Add(this.startHourDownButton, 1, 0);
            this.rowsPanel.Controls.Add(this.startHourUpButton, 2, 0);
            this.rowsPanel.Controls.Add(this.startTimeLabel, 3, 0);
            this.rowsPanel.Controls.Add(this.startMinuteDownButton, 4, 0);
            this.rowsPanel.Controls.Add(this.startMinuteUpButton, 5, 0);
            this.rowsPanel.Controls.Add(this.endLabel, 0, 1);
            this.rowsPanel.Controls.Add(this.endHourDownButton, 1, 1);
            this.rowsPanel.Controls.Add(this.endHourUpButton, 2, 1);
            this.rowsPanel.Controls.Add(this.endTimeLabel, 3, 1);
            this.rowsPanel.Controls.Add(this.endMinuteDownButton, 4, 1);
            this.rowsPanel.Controls.Add(this.endMinuteUpButton, 5, 1);
            this.rowsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowsPanel.Location = new System.Drawing.Point(0, 110);
            this.rowsPanel.Name = "rowsPanel";
            this.rowsPanel.Padding = new System.Windows.Forms.Padding(24, 16, 24, 8);
            this.rowsPanel.RowCount = 2;
            this.rowsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rowsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rowsPanel.Size = new System.Drawing.Size(1100, 260);
            this.rowsPanel.TabIndex = 1;
            // 
            // startLabel
            // 
            this.startLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(194, 122);
            this.startLabel.TabIndex = 0;
            this.startLabel.Text = "Начало";
            this.startLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startHourDownButton
            // 
            this.startHourDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startHourDownButton.Name = "startHourDownButton";
            this.startHourDownButton.Size = new System.Drawing.Size(124, 122);
            this.startHourDownButton.TabIndex = 1;
            this.startHourDownButton.Text = "− 1 ч";
            this.startHourDownButton.Click += new System.EventHandler(this.StartHourDown);
            // 
            // startHourUpButton
            // 
            this.startHourUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startHourUpButton.Name = "startHourUpButton";
            this.startHourUpButton.Size = new System.Drawing.Size(124, 122);
            this.startHourUpButton.TabIndex = 2;
            this.startHourUpButton.Text = "+ 1 ч";
            this.startHourUpButton.Click += new System.EventHandler(this.StartHourUp);
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(300, 122);
            this.startTimeLabel.TabIndex = 3;
            this.startTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startMinuteDownButton
            // 
            this.startMinuteDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startMinuteDownButton.Name = "startMinuteDownButton";
            this.startMinuteDownButton.Size = new System.Drawing.Size(124, 122);
            this.startMinuteDownButton.TabIndex = 4;
            this.startMinuteDownButton.Text = "− 5 мин";
            this.startMinuteDownButton.Click += new System.EventHandler(this.StartMinuteDown);
            // 
            // startMinuteUpButton
            // 
            this.startMinuteUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startMinuteUpButton.Name = "startMinuteUpButton";
            this.startMinuteUpButton.Size = new System.Drawing.Size(124, 122);
            this.startMinuteUpButton.TabIndex = 5;
            this.startMinuteUpButton.Text = "+ 5 мин";
            this.startMinuteUpButton.Click += new System.EventHandler(this.StartMinuteUp);
            // 
            // endLabel
            // 
            this.endLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(194, 122);
            this.endLabel.TabIndex = 6;
            this.endLabel.Text = "Конец";
            this.endLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // endHourDownButton
            // 
            this.endHourDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endHourDownButton.Name = "endHourDownButton";
            this.endHourDownButton.Size = new System.Drawing.Size(124, 122);
            this.endHourDownButton.TabIndex = 7;
            this.endHourDownButton.Text = "− 1 ч";
            this.endHourDownButton.Click += new System.EventHandler(this.EndHourDown);
            // 
            // endHourUpButton
            // 
            this.endHourUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endHourUpButton.Name = "endHourUpButton";
            this.endHourUpButton.Size = new System.Drawing.Size(124, 122);
            this.endHourUpButton.TabIndex = 8;
            this.endHourUpButton.Text = "+ 1 ч";
            this.endHourUpButton.Click += new System.EventHandler(this.EndHourUp);
            // 
            // endTimeLabel
            // 
            this.endTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(300, 122);
            this.endTimeLabel.TabIndex = 9;
            this.endTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // endMinuteDownButton
            // 
            this.endMinuteDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endMinuteDownButton.Name = "endMinuteDownButton";
            this.endMinuteDownButton.Size = new System.Drawing.Size(124, 122);
            this.endMinuteDownButton.TabIndex = 10;
            this.endMinuteDownButton.Text = "− 5 мин";
            this.endMinuteDownButton.Click += new System.EventHandler(this.EndMinuteDown);
            // 
            // endMinuteUpButton
            // 
            this.endMinuteUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endMinuteUpButton.Name = "endMinuteUpButton";
            this.endMinuteUpButton.Size = new System.Drawing.Size(124, 122);
            this.endMinuteUpButton.TabIndex = 11;
            this.endMinuteUpButton.Text = "+ 5 мин";
            this.endMinuteUpButton.Click += new System.EventHandler(this.EndMinuteUp);
            // 
            // hintLabel
            // 
            this.hintLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hintLabel.Location = new System.Drawing.Point(0, 370);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(1100, 60);
            this.hintLabel.TabIndex = 2;
            this.hintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.saveButton);
            this.footerPanel.Controls.Add(this.cancelButton);
            this.footerPanel.Controls.Add(this.deleteButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 430);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 18);
            this.footerPanel.Size = new System.Drawing.Size(1100, 110);
            this.footerPanel.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveButton.Location = new System.Drawing.Point(800, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(280, 80);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Сохранить";
            this.saveButton.Click += new System.EventHandler(this.SaveClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelButton.Location = new System.Drawing.Point(620, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(180, 80);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.Click += new System.EventHandler(this.CancelClicked);
            // 
            // deleteButton
            // 
            this.deleteButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.deleteButton.Location = new System.Drawing.Point(20, 12);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(280, 80);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.Text = "Убрать этот урок";
            this.deleteButton.Click += new System.EventHandler(this.DeleteClicked);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1100, 110);
            this.headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1100, 110);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BellEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1100, 540);
            this.Controls.Add(this.rowsPanel);
            this.Controls.Add(this.hintLabel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "BellEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Время урока";
            this.rowsPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rowsPanel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Button startHourDownButton;
        private System.Windows.Forms.Button startHourUpButton;
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Button startMinuteDownButton;
        private System.Windows.Forms.Button startMinuteUpButton;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Button endHourDownButton;
        private System.Windows.Forms.Button endHourUpButton;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.Button endMinuteDownButton;
        private System.Windows.Forms.Button endMinuteUpButton;
        private System.Windows.Forms.Label hintLabel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
    }
}
