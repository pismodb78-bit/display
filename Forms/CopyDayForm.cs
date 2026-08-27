using System;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// «Во вторник как в понедельник». Мелочь, но без неё расписание набивают
    /// клетка за клеткой, а у многих классов половина недели одинаковая.
    /// </summary>
    public partial class CopyDayForm : Form
    {
        private sealed class DayItem
        {
            public int Day;
            public override string ToString() { return Ru.DayName(Day); }
        }

        public int FromDay { get; private set; }
        public int ToDay { get; private set; }

        public CopyDayForm(int daysCount)
        {
            InitializeComponent();
            ApplyTheme();

            for (int day = 1; day <= daysCount; day++)
            {
                fromCombo.Items.Add(new DayItem { Day = day });
                toCombo.Items.Add(new DayItem { Day = day });
            }

            if (fromCombo.Items.Count > 0) fromCombo.SelectedIndex = 0;
            if (toCombo.Items.Count > 1) toCombo.SelectedIndex = 1;
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(90);
            titleLabel.Font = Ui.F(20f, true);
            titleLabel.ForeColor = Ui.Accent;

            fieldsPanel.Padding = new Padding(Ui.Px(30), Ui.Px(20), Ui.Px(30), Ui.Px(10));
            fieldsPanel.ColumnStyles[0].Width = Ui.Px(260);
            for (int i = 0; i < fieldsPanel.RowStyles.Count; i++)
                fieldsPanel.RowStyles[i].Height = Ui.Px(80);

            foreach (var label in new[] { fromLabel, toLabel })
            {
                label.Font = Ui.F(15f, true);
                label.ForeColor = Ui.Muted;
            }

            foreach (var combo in new[] { fromCombo, toCombo })
            {
                combo.Font = Ui.F(15f);
                combo.BackColor = Ui.Card;
                combo.ForeColor = Ui.Text;
                combo.Width = Ui.Px(400);
                combo.Margin = new Padding(Ui.Px(6), Ui.Px(16), Ui.Px(6), Ui.Px(16));
            }

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            Ui.PrimaryButton(okButton);
            cancelButton.Width = Ui.Px(180);
            okButton.Width = Ui.Px(280);

            ClientSize = new Size(Ui.Px(760), Ui.Px(390));
        }

        private void OkClicked(object sender, EventArgs e)
        {
            var from = fromCombo.SelectedItem as DayItem;
            var to = toCombo.SelectedItem as DayItem;
            if (from == null || to == null || from.Day == to.Day)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            FromDay = from.Day;
            ToDay = to.Day;
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
