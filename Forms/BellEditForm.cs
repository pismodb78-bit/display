using System;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Время одного урока — кнопками, без ввода с клавиатуры.
    ///
    /// Раньше звонки правились прямо в таблице. На сенсорном экране это не
    /// работает вовсе: чтобы набрать «08:30» в клетке, нужна клавиатура,
    /// которой у стены нет, а экранную в клетку таблицы не подставить.
    /// Кнопками «± час» и «± 5 минут» неправильное время ввести попросту
    /// нечем — и проверять на выходе нечего.
    /// </summary>
    public partial class BellEditForm : Form
    {
        private const int MinLesson = 5;      // урок короче пяти минут не бывает

        private TimeSpan _start;
        private TimeSpan _end;

        public TimeSpan Start { get { return _start; } }

        public TimeSpan End { get { return _end; } }

        /// <summary>Нажали «Убрать этот урок».</summary>
        public bool Removed { get; private set; }

        public BellEditForm(int lessonNo, TimeSpan start, TimeSpan end)
        {
            _start = start;
            _end = end;

            InitializeComponent();
            ApplyTheme();

            titleLabel.Text = lessonNo + "-й урок";
            UpdateTimes();
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(110);
            titleLabel.Font = Ui.F(26f, true);
            titleLabel.ForeColor = Ui.Accent;

            rowsPanel.BackColor = Ui.Bg;
            rowsPanel.Padding = new Padding(Ui.Px(24), Ui.Px(16), Ui.Px(24), Ui.Px(8));
            rowsPanel.ColumnStyles[0].Width = Ui.Px(200);
            rowsPanel.ColumnStyles[1].Width = Ui.Px(150);
            rowsPanel.ColumnStyles[2].Width = Ui.Px(150);
            rowsPanel.ColumnStyles[4].Width = Ui.Px(170);
            rowsPanel.ColumnStyles[5].Width = Ui.Px(170);

            foreach (var label in new[] { startLabel, endLabel })
            {
                label.Font = Ui.F(18f, true);
                label.ForeColor = Ui.Muted;
            }

            foreach (var label in new[] { startTimeLabel, endTimeLabel })
            {
                label.Font = Ui.F(40f, true);
                label.ForeColor = Ui.Text;
            }

            foreach (var button in new[] { startHourDownButton, startHourUpButton, startMinuteDownButton,
                                           startMinuteUpButton, endHourDownButton, endHourUpButton,
                                           endMinuteDownButton, endMinuteUpButton })
            {
                Ui.TouchButton(button, Ui.Card, Ui.Text, 15f, true);
                button.Margin = new Padding(Ui.Px(6));
            }

            hintLabel.Font = Ui.F(13f);
            hintLabel.ForeColor = Ui.Muted;
            hintLabel.Height = Ui.Px(60);

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.DangerButton(deleteButton);
            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            Ui.PrimaryButton(saveButton);
            deleteButton.Width = Ui.Px(300);
            cancelButton.Width = Ui.Px(180);
            saveButton.Width = Ui.Px(280);

            ClientSize = Ui.Dialog(0.72, 0.55);
        }

        private void UpdateTimes()
        {
            startTimeLabel.Text = Ru.Time(_start);
            endTimeLabel.Text = Ru.Time(_end);
            hintLabel.Text = "Урок идёт " + (int)(_end - _start).TotalMinutes + " мин";
        }

        // --- Правка времени -----------------------------------------------

        private static TimeSpan Wrap(TimeSpan time)
        {
            // Сутки по кругу: с 23:50 «+20 минут» уводит в 00:10, а не в минус.
            var minutes = (int)time.TotalMinutes % (24 * 60);
            if (minutes < 0) minutes += 24 * 60;
            return TimeSpan.FromMinutes(minutes);
        }

        /// <summary>
        /// Сдвинуть начало. Конец едет следом, чтобы урок не оказался
        /// «с 9:00 до 8:30» — длительность при этом сохраняется.
        /// </summary>
        private void MoveStart(int minutes)
        {
            var length = (int)(_end - _start).TotalMinutes;
            if (length < MinLesson) length = 45;

            _start = Wrap(_start.Add(TimeSpan.FromMinutes(minutes)));
            _end = Wrap(_start.Add(TimeSpan.FromMinutes(length)));
            UpdateTimes();
        }

        /// <summary>Сдвинуть конец, не давая ему заехать раньше начала.</summary>
        private void MoveEnd(int minutes)
        {
            var candidate = Wrap(_end.Add(TimeSpan.FromMinutes(minutes)));
            var length = (int)(candidate - _start).TotalMinutes;

            // Урок короче пяти минут и длиннее восьми часов не бывает —
            // упираемся в границу, а не перескакиваем через неё.
            if (length < MinLesson) candidate = Wrap(_start.Add(TimeSpan.FromMinutes(MinLesson)));
            else if (length > 8 * 60) candidate = Wrap(_start.Add(TimeSpan.FromMinutes(8 * 60)));

            _end = candidate;
            UpdateTimes();
        }

        private void StartHourDown(object sender, EventArgs e) { MoveStart(-60); }

        private void StartHourUp(object sender, EventArgs e) { MoveStart(60); }

        private void StartMinuteDown(object sender, EventArgs e) { MoveStart(-5); }

        private void StartMinuteUp(object sender, EventArgs e) { MoveStart(5); }

        private void EndHourDown(object sender, EventArgs e) { MoveEnd(-60); }

        private void EndHourUp(object sender, EventArgs e) { MoveEnd(60); }

        private void EndMinuteDown(object sender, EventArgs e) { MoveEnd(-5); }

        private void EndMinuteUp(object sender, EventArgs e) { MoveEnd(5); }

        // --- Кнопки -------------------------------------------------------

        private void SaveClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            Removed = true;
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
