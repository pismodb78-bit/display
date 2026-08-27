using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Выбор из того, что уже вводили: предмет, учитель, кабинет.
    ///
    /// Раньше это был выпадающий список прямо в поле. На сенсорном экране он
    /// плох дважды: строки в нём с полсантиметра, и сам ComboBox по-своему
    /// обращается с выделением текста, из-за чего экранная клавиатура печатала
    /// в такое поле одну букву. Крупные кнопки решают обе беды сразу.
    /// </summary>
    public partial class ValuePickerForm : Form
    {
        public string SelectedValue { get; private set; }

        public ValuePickerForm(string title, IEnumerable<string> values)
        {
            InitializeComponent();
            ApplyTheme();

            titleLabel.Text = title;
            Fill(values);
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(90);
            titleLabel.Font = Ui.F(22f, true);
            titleLabel.ForeColor = Ui.Accent;

            flowPanel.BackColor = Ui.Bg;
            flowPanel.Padding = new Padding(Ui.Px(20));

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            cancelButton.Width = Ui.Px(180);

            ClientSize = Ui.Dialog(0.7, 0.8);
        }

        private void Fill(IEnumerable<string> values)
        {
            flowPanel.Controls.Clear();

            int count = 0;
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) continue;

                var button = new Button
                {
                    Text = value.Trim(),
                    Tag = value.Trim(),
                    Width = Ui.Px(380),
                    Height = Ui.Px(90),
                    Margin = new Padding(Ui.Px(8))
                };

                Ui.TouchButton(button, Ui.Card, Ui.Text, 16f, false);
                button.Click += ValueClicked;

                flowPanel.Controls.Add(button);
                count++;
            }

            if (count > 0) return;

            flowPanel.Controls.Add(new Label
            {
                Text = "Пока ничего не вводили — наберите на клавиатуре." + Environment.NewLine +
                       "Дальше это значение будет предлагаться здесь.",
                AutoSize = false,
                Width = Ui.Px(760),
                Height = Ui.Px(140),
                Font = Ui.F(15f),
                ForeColor = Ui.Muted,
                TextAlign = ContentAlignment.MiddleCenter
            });
        }

        private void ValueClicked(object sender, EventArgs e)
        {
            SelectedValue = Convert.ToString(((Button)sender).Tag);
            DialogResult = DialogResult.OK;
            Close();
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

        /// <summary>Спросить значение. null — отказались.</summary>
        public static string Ask(IWin32Window owner, string title, List<string> values)
        {
            using (var form = new ValuePickerForm(title, values))
                return form.ShowDialog(owner) == DialogResult.OK ? form.SelectedValue : null;
        }
    }
}
