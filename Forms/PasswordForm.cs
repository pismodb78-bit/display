using System;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Пароль на вход в режим учителя.
    ///
    /// Экран висит в открытом коридоре, и подобрать четыре цифры школьнику —
    /// дело нескольких минут, поэтому после пяти промахов ввод замирает на
    /// полминуты. Счётчик статический: закрыть и открыть окно заново не помогает.
    /// </summary>
    public partial class PasswordForm : Form
    {
        private const int MaxAttempts = 5;
        private const int LockSeconds = 30;

        private static int _failedAttempts;
        private static DateTime _lockedUntil = DateTime.MinValue;

        private readonly string _storedHash;

        public PasswordForm(string storedHash)
        {
            _storedHash = storedHash;
            InitializeComponent();
            ApplyTheme();
            BuildKeypad();

            keyboard.Target = passwordBox;
            keyboard.EnterPressed += delegate { OkClicked(this, EventArgs.Empty); };
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            titleLabel.Font = Ui.F(26f, true);
            titleLabel.ForeColor = Ui.Accent;
            titleLabel.Height = Ui.Px(60);

            hintLabel.Font = Ui.F(15f);
            hintLabel.ForeColor = Ui.Muted;

            headerPanel.Height = Ui.Px(120);
            headerPanel.BackColor = Ui.Header;

            passwordBox.Font = Ui.F(28f, true);
            passwordBox.BackColor = Ui.Card;
            passwordBox.ForeColor = Ui.Text;
            passwordBox.Height = Ui.Px(56);

            errorLabel.Font = Ui.F(13f);
            errorLabel.Height = Ui.Px(34);
            inputPanel.Height = Ui.Px(110);
            inputPanel.Padding = new Padding(Ui.Px(60), Ui.Px(10), Ui.Px(60), 0);

            keypadPanel.Padding = new Padding(Ui.Px(60), 0, Ui.Px(60), 0);
            keyboard.Height = Ui.Px(240);

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.TouchButton(lettersButton, Ui.Card, Ui.Text, 14f, false);
            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            Ui.PrimaryButton(okButton);

            lettersButton.Width = Ui.Px(170);
            cancelButton.Width = Ui.Px(170);
            okButton.Width = Ui.Px(200);

            ClientSize = new Size(Ui.Px(620), Ui.Px(720));
        }

        /// <summary>Цифровая клавиатура — пароль на стене обычно всё-таки цифровой.</summary>
        private void BuildKeypad()
        {
            var keys = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "Стереть", "0", "⌫" };

            keypadPanel.Controls.Clear();
            for (int i = 0; i < keys.Length; i++)
            {
                var button = new Button
                {
                    Text = keys[i],
                    Tag = keys[i],
                    Dock = DockStyle.Fill,
                    Margin = new Padding(Ui.Px(6))
                };

                bool digit = keys[i].Length == 1 && char.IsDigit(keys[i][0]);
                Ui.TouchButton(button, digit ? Ui.CardLight : Ui.Card, Ui.Text, digit ? 22f : 13f, digit);
                button.Click += KeypadClicked;

                keypadPanel.Controls.Add(button, i % 3, i / 3);
            }
        }

        private void KeypadClicked(object sender, EventArgs e)
        {
            var key = Convert.ToString(((Button)sender).Tag);

            if (key == "Стереть") passwordBox.Text = "";
            else if (key == "⌫")
            {
                if (passwordBox.Text.Length > 0)
                    passwordBox.Text = passwordBox.Text.Substring(0, passwordBox.Text.Length - 1);
            }
            else passwordBox.Text += key;

            passwordBox.SelectionStart = passwordBox.Text.Length;
            errorLabel.Text = "";
        }

        private void LettersClicked(object sender, EventArgs e)
        {
            keyboard.Visible = !keyboard.Visible;
            keypadPanel.Visible = !keyboard.Visible;
            lettersButton.Text = keyboard.Visible ? "Цифры" : "Буквы";
        }

        private void PasswordBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OkClicked(sender, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CancelClicked(sender, EventArgs.Empty);
            }
        }

        private void OkClicked(object sender, EventArgs e)
        {
            var wait = (int)Math.Ceiling((_lockedUntil - DateTime.Now).TotalSeconds);
            if (wait > 0)
            {
                errorLabel.Text = "Слишком много попыток. Подождите " + wait + " с.";
                return;
            }

            if (Check(passwordBox.Text))
            {
                _failedAttempts = 0;
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            _failedAttempts++;
            passwordBox.Text = "";

            if (_failedAttempts >= MaxAttempts)
            {
                _lockedUntil = DateTime.Now.AddSeconds(LockSeconds);
                _failedAttempts = 0;
                errorLabel.Text = "Слишком много попыток. Подождите " + LockSeconds + " с.";
            }
            else
            {
                errorLabel.Text = "Пароль не подошёл. Осталось попыток: " + (MaxAttempts - _failedAttempts);
            }
        }

        /// <summary>
        /// Сначала пароль из базы; если его там нет — запасной из ip.txt.
        /// Так программа работает сразу после установки, а сменённый пароль
        /// сразу же становится единственным рабочим.
        /// </summary>
        private bool Check(string entered)
        {
            if (!string.IsNullOrWhiteSpace(_storedHash))
                return PasswordHasher.Verify(entered, _storedHash);

            return entered == AppConfig.FallbackAdminPassword;
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

        /// <summary>Спросить пароль. true — пустили.</summary>
        public static bool Ask(IWin32Window owner, string storedHash)
        {
            using (var form = new PasswordForm(storedHash))
                return form.ShowDialog(owner) == DialogResult.OK;
        }
    }
}
