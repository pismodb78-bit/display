using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Forms
{
    /// <summary>
    /// Одна клетка расписания: предмет, учитель, кабинет.
    ///
    /// Правится в отдельном окне, а не прямо в таблице: попасть пальцем в
    /// ячейку сетки 6×8 на сенсорном экране и не промахнуться мимо соседней —
    /// задача не для человека, который просто хочет заменить физику на алгебру.
    /// </summary>
    public partial class LessonEditForm : Form
    {
        public string Subject { get; private set; }
        public string Teacher { get; private set; }
        public string Room { get; private set; }

        /// <summary>Нажали «Убрать урок» — клетку надо очистить.</summary>
        public bool Cleared { get; private set; }

        private readonly List<string> _subjects;
        private readonly List<string> _teachers;
        private readonly List<string> _rooms;

        public LessonEditForm(string title, string subtitle, Lesson lesson,
                              List<string> subjects, List<string> teachers, List<string> rooms)
        {
            _subjects = subjects ?? new List<string>();
            _teachers = teachers ?? new List<string>();
            _rooms = rooms ?? new List<string>();

            InitializeComponent();
            ApplyTheme();

            titleLabel.Text = title;
            subtitleLabel.Text = subtitle;

            if (lesson != null)
            {
                subjectBox.Text = lesson.Subject ?? "";
                teacherBox.Text = lesson.Teacher ?? "";
                roomBox.Text = lesson.Room ?? "";
            }

            // Клавиатура печатает в то поле, которое сейчас выбрано.
            foreach (var box in new[] { subjectBox, teacherBox, roomBox })
                box.Enter += FieldEntered;

            keyboard.Target = subjectBox;
        }

        private void ApplyTheme()
        {
            BackColor = Ui.Bg;
            ForeColor = Ui.Text;

            headerPanel.BackColor = Ui.Header;
            headerPanel.Height = Ui.Px(110);
            titleLabel.Font = Ui.F(22f, true);
            titleLabel.ForeColor = Ui.Accent;
            titleLabel.Height = Ui.Px(60);
            subtitleLabel.Font = Ui.F(14f);
            subtitleLabel.ForeColor = Ui.Muted;

            fieldsPanel.BackColor = Ui.Bg;
            fieldsPanel.Height = Ui.Px(270);
            fieldsPanel.Padding = new Padding(Ui.Px(30), Ui.Px(20), Ui.Px(30), Ui.Px(10));
            fieldsPanel.ColumnStyles[0].Width = Ui.Px(220);
            fieldsPanel.ColumnStyles[2].Width = Ui.Px(220);

            for (int i = 0; i < fieldsPanel.RowStyles.Count; i++)
                fieldsPanel.RowStyles[i].Height = Ui.Px(80);

            foreach (var label in new[] { subjectLabel, teacherLabel, roomLabel })
            {
                label.Font = Ui.F(16f, true);
                label.ForeColor = Ui.Muted;
            }

            foreach (var box in new[] { subjectBox, teacherBox, roomBox })
            {
                box.Font = Ui.F(18f);
                box.BackColor = Ui.Card;
                box.ForeColor = Ui.Text;
                box.Margin = new Padding(Ui.Px(6), Ui.Px(12), Ui.Px(6), Ui.Px(12));
            }

            foreach (var button in new[] { subjectPickButton, teacherPickButton, roomPickButton })
            {
                Ui.TouchButton(button, Ui.Card, Ui.Text, 13f, false);
                button.Margin = new Padding(Ui.Px(6), Ui.Px(10), Ui.Px(6), Ui.Px(10));
            }

            footerPanel.BackColor = Ui.Header;
            footerPanel.Height = Ui.Px(110);
            footerPanel.Padding = new Padding(Ui.Px(20), Ui.Px(12), Ui.Px(20), Ui.Px(18));

            Ui.DangerButton(clearButton);
            Ui.TouchButton(cancelButton, Ui.Card, Ui.Text, 14f, false);
            Ui.PrimaryButton(saveButton);
            clearButton.Width = Ui.Px(250);
            cancelButton.Width = Ui.Px(180);
            saveButton.Width = Ui.Px(280);

            ClientSize = Ui.Dialog(0.8, 0.9);
        }

        /// <summary>
        /// Выбор из уже введённого: «Математика» набирается один раз за год,
        /// дальше берётся кнопкой «Список…».
        /// </summary>
        private void Pick(TextBox box, string title, List<string> values)
        {
            var chosen = ValuePickerForm.Ask(this, title, values);
            if (chosen == null) return;

            box.Text = chosen;
            box.SelectionStart = box.Text.Length;
            box.SelectionLength = 0;
            keyboard.Target = box;
            box.Focus();
        }

        private void SubjectPickClicked(object sender, EventArgs e) { Pick(subjectBox, "Предмет", _subjects); }

        private void TeacherPickClicked(object sender, EventArgs e) { Pick(teacherBox, "Учитель", _teachers); }

        private void RoomPickClicked(object sender, EventArgs e) { Pick(roomBox, "Кабинет", _rooms); }

        private void FieldEntered(object sender, EventArgs e)
        {
            keyboard.Target = (Control)sender;
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            Subject = (subjectBox.Text ?? "").Trim();
            Teacher = (teacherBox.Text ?? "").Trim();
            Room = (roomBox.Text ?? "").Trim();

            if (Subject.Length == 0)
            {
                // Пустой предмет — это и есть «убрать урок», отдельного
                // предупреждения тут не нужно.
                Cleared = true;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            Cleared = true;
            Subject = "";
            Teacher = "";
            Room = "";
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
