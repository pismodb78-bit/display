namespace SchoolSchedule.Forms
{
    partial class LessonEditForm
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
            this.fieldsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.subjectBox = new System.Windows.Forms.TextBox();
            this.subjectPickButton = new System.Windows.Forms.Button();
            this.teacherLabel = new System.Windows.Forms.Label();
            this.teacherBox = new System.Windows.Forms.TextBox();
            this.teacherPickButton = new System.Windows.Forms.Button();
            this.roomLabel = new System.Windows.Forms.Label();
            this.roomBox = new System.Windows.Forms.TextBox();
            this.roomPickButton = new System.Windows.Forms.Button();
            this.keyboard = new SchoolSchedule.Controls.OnScreenKeyboard();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.fieldsPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fieldsPanel
            // 
            this.fieldsPanel.ColumnCount = 3;
            this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.fieldsPanel.Controls.Add(this.subjectLabel, 0, 0);
            this.fieldsPanel.Controls.Add(this.subjectBox, 1, 0);
            this.fieldsPanel.Controls.Add(this.subjectPickButton, 2, 0);
            this.fieldsPanel.Controls.Add(this.teacherLabel, 0, 1);
            this.fieldsPanel.Controls.Add(this.teacherBox, 1, 1);
            this.fieldsPanel.Controls.Add(this.teacherPickButton, 2, 1);
            this.fieldsPanel.Controls.Add(this.roomLabel, 0, 2);
            this.fieldsPanel.Controls.Add(this.roomBox, 1, 2);
            this.fieldsPanel.Controls.Add(this.roomPickButton, 2, 2);
            this.fieldsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.fieldsPanel.Location = new System.Drawing.Point(0, 110);
            this.fieldsPanel.Name = "fieldsPanel";
            this.fieldsPanel.Padding = new System.Windows.Forms.Padding(30, 20, 30, 10);
            this.fieldsPanel.RowCount = 3;
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.fieldsPanel.Size = new System.Drawing.Size(900, 270);
            this.fieldsPanel.TabIndex = 1;
            // 
            // subjectLabel
            // 
            this.subjectLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectLabel.Location = new System.Drawing.Point(33, 20);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(214, 80);
            this.subjectLabel.TabIndex = 0;
            this.subjectLabel.Text = "Предмет";
            this.subjectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subjectBox
            // 
            this.subjectBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subjectBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectBox.Location = new System.Drawing.Point(253, 23);
            this.subjectBox.Name = "subjectBox";
            this.subjectBox.Size = new System.Drawing.Size(614, 40);
            this.subjectBox.TabIndex = 1;
            // 
            // subjectPickButton
            // 
            this.subjectPickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectPickButton.Name = "subjectPickButton";
            this.subjectPickButton.Size = new System.Drawing.Size(214, 74);
            this.subjectPickButton.TabIndex = 2;
            this.subjectPickButton.Text = "Список…";
            this.subjectPickButton.Click += new System.EventHandler(this.SubjectPickClicked);
            // 
            // teacherLabel
            // 
            this.teacherLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teacherLabel.Location = new System.Drawing.Point(33, 100);
            this.teacherLabel.Name = "teacherLabel";
            this.teacherLabel.Size = new System.Drawing.Size(214, 80);
            this.teacherLabel.TabIndex = 2;
            this.teacherLabel.Text = "Учитель";
            this.teacherLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teacherBox
            // 
            this.teacherBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacherBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teacherBox.Location = new System.Drawing.Point(253, 103);
            this.teacherBox.Name = "teacherBox";
            this.teacherBox.Size = new System.Drawing.Size(614, 40);
            this.teacherBox.TabIndex = 3;
            // 
            // teacherPickButton
            // 
            this.teacherPickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teacherPickButton.Name = "teacherPickButton";
            this.teacherPickButton.Size = new System.Drawing.Size(214, 74);
            this.teacherPickButton.TabIndex = 4;
            this.teacherPickButton.Text = "Список…";
            this.teacherPickButton.Click += new System.EventHandler(this.TeacherPickClicked);
            // 
            // roomLabel
            // 
            this.roomLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomLabel.Location = new System.Drawing.Point(33, 180);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(214, 80);
            this.roomLabel.TabIndex = 4;
            this.roomLabel.Text = "Кабинет";
            this.roomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // roomBox
            // 
            this.roomBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roomBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomBox.Location = new System.Drawing.Point(253, 183);
            this.roomBox.Name = "roomBox";
            this.roomBox.Size = new System.Drawing.Size(614, 40);
            this.roomBox.TabIndex = 5;
            // 
            // roomPickButton
            // 
            this.roomPickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomPickButton.Name = "roomPickButton";
            this.roomPickButton.Size = new System.Drawing.Size(214, 74);
            this.roomPickButton.TabIndex = 6;
            this.roomPickButton.Text = "Список…";
            this.roomPickButton.Click += new System.EventHandler(this.RoomPickClicked);
            // 
            // keyboard
            // 
            this.keyboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyboard.Location = new System.Drawing.Point(0, 380);
            this.keyboard.Name = "keyboard";
            this.keyboard.ShowEnterKey = false;
            this.keyboard.Size = new System.Drawing.Size(900, 250);
            this.keyboard.TabIndex = 2;
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.saveButton);
            this.footerPanel.Controls.Add(this.cancelButton);
            this.footerPanel.Controls.Add(this.clearButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 630);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 18);
            this.footerPanel.Size = new System.Drawing.Size(900, 110);
            this.footerPanel.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveButton.Location = new System.Drawing.Point(600, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(280, 80);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Сохранить";
            this.saveButton.Click += new System.EventHandler(this.SaveClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelButton.Location = new System.Drawing.Point(420, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(180, 80);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.Click += new System.EventHandler(this.CancelClicked);
            // 
            // clearButton
            // 
            this.clearButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearButton.Location = new System.Drawing.Point(20, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(250, 80);
            this.clearButton.TabIndex = 0;
            this.clearButton.Text = "Убрать урок";
            this.clearButton.Click += new System.EventHandler(this.ClearClicked);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(900, 110);
            this.headerPanel.TabIndex = 0;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subtitleLabel.Location = new System.Drawing.Point(0, 60);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(900, 50);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(900, 60);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Урок";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LessonEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(900, 740);
            this.Controls.Add(this.keyboard);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.fieldsPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "LessonEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Урок";
            this.fieldsPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel fieldsPanel;
        private System.Windows.Forms.Label subjectLabel;
        private System.Windows.Forms.TextBox subjectBox;
        private System.Windows.Forms.Button subjectPickButton;
        private System.Windows.Forms.Label teacherLabel;
        private System.Windows.Forms.TextBox teacherBox;
        private System.Windows.Forms.Button teacherPickButton;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.TextBox roomBox;
        private System.Windows.Forms.Button roomPickButton;
        private SchoolSchedule.Controls.OnScreenKeyboard keyboard;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label titleLabel;
    }
}
