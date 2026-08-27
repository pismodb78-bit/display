using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Выбор класса крупными кнопками. Выпадающий список на сенсорном экране
    /// неудобен: попасть пальцем в строку высотой в сантиметр проще, чем в
    /// пункт списка.
    /// </summary>
    public partial class ClassPickerForm : Form
    {
        /// <summary>Что выбрали: id класса или 0 — «все классы на день».</summary>
        public int SelectedClassId { get; private set; }

        public ClassPickerForm(IEnumerable<SchoolClass> classes, int currentClassId)
        {
            InitializeComponent();
            ApplyTheme();
            Fill(classes, currentClassId);
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(90);
            titleLabel.Font = Ui.F(24f, true);
            titleLabel.ForeColor = Ui.Accent;

            flowPanel.BackColor = Ui.Bg;
            flowPanel.Padding = new Padding(Ui.Px(20));

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.TouchButton(allClassesButton, Ui.AccentDark, Ui.OnAccent, 15f, true);
            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            allClassesButton.Width = Ui.Px(420);
            cancelButton.Width = Ui.Px(180);

            ClientSize = new Size(Ui.Px(900), Ui.Px(620));
        }

        private void Fill(IEnumerable<SchoolClass> classes, int currentClassId)
        {
            flowPanel.Controls.Clear();

            int count = 0;
            foreach (var schoolClass in classes)
            {
                var button = new Button
                {
                    Text = schoolClass.Name,
                    Tag = schoolClass.Id,
                    Width = Ui.Px(190),
                    Height = Ui.Px(110),
                    Margin = new Padding(Ui.Px(10))
                };

                bool current = schoolClass.Id == currentClassId;
                Ui.TouchButton(button, current ? Ui.AccentDark : Ui.Card, current ? Ui.OnAccent : Ui.Text, 26f, true);
                button.Click += ClassClicked;

                flowPanel.Controls.Add(button);
                count++;
            }

            if (count == 0)
            {
                // Классов ещё нет — это нормальное состояние сразу после
                // установки, и человеку надо сказать, куда идти.
                var empty = new Label
                {
                    Text = "Классы ещё не созданы." + Environment.NewLine +
                           "Нажмите «Учитель» на главном экране и добавьте их на вкладке «Классы».",
                    AutoSize = false,
                    Width = Ui.Px(820),
                    Height = Ui.Px(160),
                    Font = Ui.F(15f),
                    ForeColor = Ui.Muted,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                flowPanel.Controls.Add(empty);
            }
        }

        private void ClassClicked(object sender, EventArgs e)
        {
            SelectedClassId = Convert.ToInt32(((Button)sender).Tag);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AllClassesClicked(object sender, EventArgs e)
        {
            SelectedClassId = 0;
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
    }
}
