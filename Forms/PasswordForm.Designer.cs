namespace SchoolSchedule.Forms
{
    partial class PasswordForm
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
            this.keypadPanel = new System.Windows.Forms.TableLayoutPanel();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.hintLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.lettersButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.keyboard = new SchoolSchedule.Controls.OnScreenKeyboard();
            this.inputPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // keypadPanel
            // 
            this.keypadPanel.ColumnCount = 3;
            this.keypadPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.keypadPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.keypadPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.keypadPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keypadPanel.Location = new System.Drawing.Point(0, 230);
            this.keypadPanel.Name = "keypadPanel";
            this.keypadPanel.Padding = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.keypadPanel.RowCount = 4;
            this.keypadPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.keypadPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.keypadPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.keypadPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.keypadPanel.Size = new System.Drawing.Size(620, 380);
            this.keypadPanel.TabIndex = 3;
            // 
            // inputPanel
            // 
            this.inputPanel.Controls.Add(this.errorLabel);
            this.inputPanel.Controls.Add(this.passwordBox);
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputPanel.Location = new System.Drawing.Point(0, 120);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(620, 110);
            this.inputPanel.TabIndex = 1;
            // 
            // errorLabel
            // 
            this.errorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.errorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.errorLabel.Location = new System.Drawing.Point(0, 78);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(620, 32);
            this.errorLabel.TabIndex = 1;
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // passwordBox
            // 
            this.passwordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordBox.Location = new System.Drawing.Point(0, 0);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '●';
            this.passwordBox.Size = new System.Drawing.Size(620, 50);
            this.passwordBox.TabIndex = 0;
            this.passwordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passwordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordBoxKeyDown);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.hintLabel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(620, 120);
            this.headerPanel.TabIndex = 0;
            // 
            // hintLabel
            // 
            this.hintLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hintLabel.Location = new System.Drawing.Point(0, 60);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(620, 60);
            this.hintLabel.TabIndex = 1;
            this.hintLabel.Text = "Введите пароль учителя";
            this.hintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(620, 60);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Режим учителя";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.lettersButton);
            this.footerPanel.Controls.Add(this.cancelButton);
            this.footerPanel.Controls.Add(this.okButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 610);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.footerPanel.Size = new System.Drawing.Size(620, 110);
            this.footerPanel.TabIndex = 4;
            // 
            // lettersButton
            // 
            this.lettersButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.lettersButton.Location = new System.Drawing.Point(20, 10);
            this.lettersButton.Name = "lettersButton";
            this.lettersButton.Size = new System.Drawing.Size(170, 80);
            this.lettersButton.TabIndex = 0;
            this.lettersButton.Text = "Буквы";
            this.lettersButton.Click += new System.EventHandler(this.LettersClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelButton.Location = new System.Drawing.Point(240, 10);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(160, 80);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.Click += new System.EventHandler(this.CancelClicked);
            // 
            // okButton
            // 
            this.okButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.okButton.Location = new System.Drawing.Point(400, 10);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(200, 80);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Войти";
            this.okButton.Click += new System.EventHandler(this.OkClicked);
            // 
            // keyboard
            // 
            this.keyboard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keyboard.Location = new System.Drawing.Point(0, 380);
            this.keyboard.Name = "keyboard";
            this.keyboard.ShowEnterKey = true;
            this.keyboard.Size = new System.Drawing.Size(620, 230);
            this.keyboard.TabIndex = 2;
            this.keyboard.Visible = false;
            // 
            // PasswordForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(620, 720);
            this.Controls.Add(this.keypadPanel);
            this.Controls.Add(this.keyboard);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "PasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Режим учителя";
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel keypadPanel;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label hintLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button lettersButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private SchoolSchedule.Controls.OnScreenKeyboard keyboard;
    }
}
