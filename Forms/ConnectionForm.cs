using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Правка ip.txt из самой программы.
    ///
    /// Файл рядом с .exe остаётся главным способом — открыл Блокнотом, поменял
    /// адрес. Но телевизор в коридоре стоит без клавиатуры и без проводника,
    /// поэтому то же самое можно сделать пальцем отсюда. Записывается тот же
    /// файл, комментарии в нём сохраняются.
    /// </summary>
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
            ApplyTheme();

            serverBox.Text = AppConfig.Server;
            portBox.Text = AppConfig.Port.ToString();
            userBox.Text = AppConfig.User;
            passwordBox.Text = AppConfig.Password;
            databaseBox.Text = AppConfig.Database;

            pathLabel.Text = AppConfig.FilePath;

            foreach (var box in new[] { serverBox, portBox, userBox, passwordBox, databaseBox })
                box.Enter += FieldEntered;

            keyboard.Target = serverBox;
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(120);
            titleLabel.Font = Ui.F(22f, true);
            titleLabel.ForeColor = Ui.Accent;
            titleLabel.Height = Ui.Px(60);
            pathLabel.Font = Ui.F(11f);
            pathLabel.ForeColor = Ui.Muted;

            fieldsPanel.BackColor = Ui.Bg;
            fieldsPanel.Height = Ui.Px(370);
            fieldsPanel.Padding = new Padding(Ui.Px(30), Ui.Px(15), Ui.Px(30), Ui.Px(5));
            fieldsPanel.ColumnStyles[0].Width = Ui.Px(260);

            for (int i = 0; i < fieldsPanel.RowStyles.Count; i++)
                fieldsPanel.RowStyles[i].Height = Ui.Px(70);

            foreach (var label in new[] { serverLabel, portLabel, userLabel, passwordLabel, databaseLabel })
            {
                label.Font = Ui.F(15f, true);
                label.ForeColor = Ui.Muted;
            }

            foreach (var box in new[] { serverBox, portBox, userBox, passwordBox, databaseBox })
            {
                box.Font = Ui.F(16f);
                box.BackColor = Ui.Card;
                box.ForeColor = Ui.Text;
                box.Margin = new Padding(Ui.Px(6), Ui.Px(10), Ui.Px(6), Ui.Px(10));
            }

            statusPanel.BackColor = Ui.Bg;
            statusPanel.Height = Ui.Px(70);
            statusLabel.Font = Ui.F(13f, true);
            statusLabel.ForeColor = Ui.Muted;

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.TouchButton(testButton, Ui.Card, Ui.Text, 14f, false);
            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            Ui.PrimaryButton(saveButton);
            testButton.Width = Ui.Px(320);
            cancelButton.Width = Ui.Px(180);
            saveButton.Width = Ui.Px(280);

            ClientSize = new Size(Ui.Px(900), Ui.Px(900));
        }

        private void FieldEntered(object sender, EventArgs e)
        {
            keyboard.Target = (Control)sender;
        }

        private Dictionary<string, string> Values()
        {
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            values["server"] = (serverBox.Text ?? "").Trim();
            values["port"] = (portBox.Text ?? "").Trim();
            values["uid"] = (userBox.Text ?? "").Trim();
            values["password"] = passwordBox.Text ?? "";
            values["database"] = (databaseBox.Text ?? "").Trim();
            return values;
        }

        /// <summary>
        /// Проверка бьёт в тот адрес, который набран в полях. Файл при этом не
        /// трогается вовсе: нажали «Проверить», передумали, закрыли — в ip.txt
        /// осталось ровно то, что было.
        /// </summary>
        private void TestClicked(object sender, EventArgs e)
        {
            int port;
            if (!TryPort(out port)) return;

            string message;
            var ok = Db.TestConnection((serverBox.Text ?? "").Trim(), port, (userBox.Text ?? "").Trim(),
                                       passwordBox.Text ?? "", (databaseBox.Text ?? "").Trim(), out message);

            statusLabel.Text = message;
            statusLabel.ForeColor = ok ? Ui.Ok : Ui.Danger;
        }

        private bool TryPort(out int port)
        {
            if (int.TryParse((portBox.Text ?? "").Trim(), out port) && port >= 1 && port <= 65535) return true;

            statusLabel.Text = "Порт должен быть числом от 1 до 65535 (обычно 3306).";
            statusLabel.ForeColor = Ui.Danger;
            return false;
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            int port;
            if (!TryPort(out port)) return;

            try
            {
                AppConfig.Save(Values());
                Db.Reconfigure();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Не удалось записать " + AppConfig.FilePath + ": " + ex.Message;
                statusLabel.ForeColor = Ui.Danger;
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                CancelClicked(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref message, keyData);
        }
    }
}
