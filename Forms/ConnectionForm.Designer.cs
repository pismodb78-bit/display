namespace SchoolSchedule.Forms
{
    partial class ConnectionForm
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
            this.serverLabel = new System.Windows.Forms.Label();
            this.serverBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.userBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.databaseBox = new System.Windows.Forms.TextBox();
            this.keyboard = new SchoolSchedule.Controls.OnScreenKeyboard();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.pathLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.fieldsPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fieldsPanel
            // 
            this.fieldsPanel.ColumnCount = 2;
            this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fieldsPanel.Controls.Add(this.serverLabel, 0, 0);
            this.fieldsPanel.Controls.Add(this.serverBox, 1, 0);
            this.fieldsPanel.Controls.Add(this.portLabel, 0, 1);
            this.fieldsPanel.Controls.Add(this.portBox, 1, 1);
            this.fieldsPanel.Controls.Add(this.userLabel, 0, 2);
            this.fieldsPanel.Controls.Add(this.userBox, 1, 2);
            this.fieldsPanel.Controls.Add(this.passwordLabel, 0, 3);
            this.fieldsPanel.Controls.Add(this.passwordBox, 1, 3);
            this.fieldsPanel.Controls.Add(this.databaseLabel, 0, 4);
            this.fieldsPanel.Controls.Add(this.databaseBox, 1, 4);
            this.fieldsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.fieldsPanel.Location = new System.Drawing.Point(0, 120);
            this.fieldsPanel.Name = "fieldsPanel";
            this.fieldsPanel.Padding = new System.Windows.Forms.Padding(30, 15, 30, 5);
            this.fieldsPanel.RowCount = 5;
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.fieldsPanel.Size = new System.Drawing.Size(900, 370);
            this.fieldsPanel.TabIndex = 1;
            // 
            // serverLabel
            // 
            this.serverLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverLabel.Location = new System.Drawing.Point(33, 15);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(254, 70);
            this.serverLabel.TabIndex = 0;
            this.serverLabel.Text = "Сервер (IP)";
            this.serverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // serverBox
            // 
            this.serverBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serverBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverBox.Location = new System.Drawing.Point(293, 18);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(574, 40);
            this.serverBox.TabIndex = 1;
            // 
            // portLabel
            // 
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portLabel.Location = new System.Drawing.Point(33, 85);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(254, 70);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Порт";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portBox
            // 
            this.portBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portBox.Location = new System.Drawing.Point(293, 88);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(574, 40);
            this.portBox.TabIndex = 3;
            // 
            // userLabel
            // 
            this.userLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userLabel.Location = new System.Drawing.Point(33, 155);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(254, 70);
            this.userLabel.TabIndex = 4;
            this.userLabel.Text = "Пользователь";
            this.userLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // userBox
            // 
            this.userBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userBox.Location = new System.Drawing.Point(293, 158);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(574, 40);
            this.userBox.TabIndex = 5;
            // 
            // passwordLabel
            // 
            this.passwordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordLabel.Location = new System.Drawing.Point(33, 225);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(254, 70);
            this.passwordLabel.TabIndex = 6;
            this.passwordLabel.Text = "Пароль";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // passwordBox
            // 
            this.passwordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordBox.Location = new System.Drawing.Point(293, 228);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '●';
            this.passwordBox.Size = new System.Drawing.Size(574, 40);
            this.passwordBox.TabIndex = 7;
            // 
            // databaseLabel
            // 
            this.databaseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databaseLabel.Location = new System.Drawing.Point(33, 295);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(254, 70);
            this.databaseLabel.TabIndex = 8;
            this.databaseLabel.Text = "База данных";
            this.databaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // databaseBox
            // 
            this.databaseBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.databaseBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databaseBox.Location = new System.Drawing.Point(293, 298);
            this.databaseBox.Name = "databaseBox";
            this.databaseBox.Size = new System.Drawing.Size(574, 40);
            this.databaseBox.TabIndex = 9;
            // 
            // keyboard
            // 
            this.keyboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyboard.Location = new System.Drawing.Point(0, 490);
            this.keyboard.Name = "keyboard";
            this.keyboard.ShowEnterKey = false;
            this.keyboard.Size = new System.Drawing.Size(900, 230);
            this.keyboard.TabIndex = 2;
            // 
            // statusPanel
            // 
            this.statusPanel.Controls.Add(this.statusLabel);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Location = new System.Drawing.Point(0, 720);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.statusPanel.Size = new System.Drawing.Size(900, 70);
            this.statusPanel.TabIndex = 3;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Location = new System.Drawing.Point(30, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(840, 70);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.saveButton);
            this.footerPanel.Controls.Add(this.cancelButton);
            this.footerPanel.Controls.Add(this.testButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 790);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 18);
            this.footerPanel.Size = new System.Drawing.Size(900, 110);
            this.footerPanel.TabIndex = 4;
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
            // testButton
            // 
            this.testButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.testButton.Location = new System.Drawing.Point(20, 12);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(320, 80);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Проверить подключение";
            this.testButton.Click += new System.EventHandler(this.TestClicked);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.pathLabel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(900, 120);
            this.headerPanel.TabIndex = 0;
            // 
            // pathLabel
            // 
            this.pathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathLabel.Location = new System.Drawing.Point(0, 60);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(900, 60);
            this.pathLabel.TabIndex = 1;
            this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(900, 60);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Подключение к базе";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConnectionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(900, 900);
            this.Controls.Add(this.keyboard);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.fieldsPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подключение к базе";
            this.fieldsPanel.ResumeLayout(false);
            this.fieldsPanel.PerformLayout();
            this.statusPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel fieldsPanel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.TextBox serverBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.TextBox databaseBox;
        private SchoolSchedule.Controls.OnScreenKeyboard keyboard;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label titleLabel;
    }
}
