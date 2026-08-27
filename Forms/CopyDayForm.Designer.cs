namespace SchoolSchedule.Forms
{
    partial class CopyDayForm
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
            this.fromLabel = new System.Windows.Forms.Label();
            this.fromCombo = new System.Windows.Forms.ComboBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.toCombo = new System.Windows.Forms.ComboBox();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.fieldsPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fieldsPanel
            // 
            this.fieldsPanel.ColumnCount = 2;
            this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fieldsPanel.Controls.Add(this.fromLabel, 0, 0);
            this.fieldsPanel.Controls.Add(this.fromCombo, 1, 0);
            this.fieldsPanel.Controls.Add(this.toLabel, 0, 1);
            this.fieldsPanel.Controls.Add(this.toCombo, 1, 1);
            this.fieldsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldsPanel.Location = new System.Drawing.Point(0, 90);
            this.fieldsPanel.Name = "fieldsPanel";
            this.fieldsPanel.Padding = new System.Windows.Forms.Padding(30, 20, 30, 10);
            this.fieldsPanel.RowCount = 2;
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.fieldsPanel.Size = new System.Drawing.Size(760, 190);
            this.fieldsPanel.TabIndex = 1;
            // 
            // fromLabel
            // 
            this.fromLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(254, 80);
            this.fromLabel.TabIndex = 0;
            this.fromLabel.Text = "Скопировать день";
            this.fromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fromCombo
            // 
            this.fromCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.fromCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fromCombo.Name = "fromCombo";
            this.fromCombo.Size = new System.Drawing.Size(400, 40);
            this.fromCombo.TabIndex = 1;
            // 
            // toLabel
            // 
            this.toLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(254, 80);
            this.toLabel.TabIndex = 2;
            this.toLabel.Text = "В день";
            this.toLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toCombo
            // 
            this.toCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.toCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toCombo.Name = "toCombo";
            this.toCombo.Size = new System.Drawing.Size(400, 40);
            this.toCombo.TabIndex = 3;
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.okButton);
            this.footerPanel.Controls.Add(this.cancelButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 280);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 18);
            this.footerPanel.Size = new System.Drawing.Size(760, 110);
            this.footerPanel.TabIndex = 2;
            // 
            // okButton
            // 
            this.okButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.okButton.Location = new System.Drawing.Point(460, 12);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(280, 80);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Скопировать";
            this.okButton.Click += new System.EventHandler(this.OkClicked);
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
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(760, 90);
            this.headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(760, 90);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Копирование дня";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CopyDayForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(760, 390);
            this.Controls.Add(this.fieldsPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "CopyDayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Копирование дня";
            this.fieldsPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel fieldsPanel;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.ComboBox fromCombo;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.ComboBox toCombo;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
    }
}
