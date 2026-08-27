using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SchoolSchedule.Core;

namespace SchoolSchedule.Controls
{
    /// <summary>
    /// Экранная клавиатура.
    ///
    /// Нужна потому, что программа живёт на сенсорном экране в коридоре:
    /// клавиатуру туда никто не потащит, а название предмета и пароль вводить
    /// как-то надо. Windows-овская экранная клавиатура поверх полноэкранного
    /// окна ведёт себя непредсказуемо, поэтому своя.
    ///
    /// Кладётся на форму как обычный элемент, в свойстве <see cref="Target"/>
    /// указывается поле ввода.
    /// </summary>
    public partial class OnScreenKeyboard : UserControl
    {
        private static readonly string[] Russian =
        {
            "1234567890",
            "йцукенгшщзхъ",
            "фывапролджэ",
            "ячсмитьбюё"
        };

        private static readonly string[] Latin =
        {
            "1234567890",
            "qwertyuiop",
            "asdfghjkl",
            "zxcvbnm"
        };

        private static readonly string[] Symbols =
        {
            "1234567890",
            "-–.,№()/",
            "«»\"':;!?",
            "+*=%@#&"
        };

        private enum KeyLayout { Russian, Latin, Symbols }

        private KeyLayout _layout = KeyLayout.Russian;
        private bool _shift = true;   // первую букву обычно пишут с заглавной
        private Button _shiftKey;

        /// <summary>Поле, в которое печатает клавиатура.</summary>
        public Control Target { get; set; }

        /// <summary>Нажали «Ввод» — форма обычно закрывается сохранением.</summary>
        public event EventHandler EnterPressed;

        /// <summary>Показывать ли кнопку «Ввод» (в форме с несколькими полями она мешает).</summary>
        public bool ShowEnterKey { get; set; }

        public OnScreenKeyboard()
        {
            InitializeComponent();
            ShowEnterKey = true;
            Ui.EnableDoubleBuffer(layoutPanel);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Build();
        }

        /// <summary>Пересобрать под новую тему.</summary>
        public void Restyle()
        {
            if (IsHandleCreated) Build();
        }

        /// <summary>Собрать раскладку заново — при смене языка и при первом показе.</summary>
        private void Build()
        {
            var rows = Rows();

            layoutPanel.SuspendLayout();

            // Старые кнопки именно освобождаем, а не просто убираем из списка:
            // программа живёт на экране сутками, и брошенные окна кнопок
            // копились бы до перезапуска.
            var previous = new List<Control>();
            foreach (Control child in layoutPanel.Controls) previous.Add(child);

            layoutPanel.Controls.Clear();
            foreach (var child in previous) child.Dispose();

            _shiftKey = null;
            layoutPanel.ColumnStyles.Clear();
            layoutPanel.RowStyles.Clear();

            layoutPanel.ColumnCount = 1;
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPanel.RowCount = rows.Length + 1;

            for (int i = 0; i < rows.Length; i++)
            {
                layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / (rows.Length + 1)));
                layoutPanel.Controls.Add(BuildLetterRow(rows[i]), 0, i);
            }

            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / (rows.Length + 1)));
            layoutPanel.Controls.Add(BuildServiceRow(), 0, rows.Length);

            layoutPanel.ResumeLayout(true);
            ScaleKeyFonts();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ScaleKeyFonts();
        }

        /// <summary>
        /// Размер надписи берём от высоты клавиши. Фиксированный кегль на
        /// большом экране выглядел бы бисером на кнопке размером с ладонь.
        /// </summary>
        private void ScaleKeyFonts()
        {
            foreach (Control child in layoutPanel.Controls)
            {
                var row = child as TableLayoutPanel;
                if (row == null) continue;

                foreach (Control key in row.Controls)
                {
                    var button = key as Button;
                    if (button == null || button.Height < 8) continue;

                    var short_ = (button.Text ?? "").Length <= 2;
                    button.Font = Ui.Fp(button.Height * (short_ ? 0.46f : 0.28f), short_);
                }
            }
        }

        private string[] Rows()
        {
            switch (_layout)
            {
                case KeyLayout.Latin: return Latin;
                case KeyLayout.Symbols: return Symbols;
                default: return Russian;
            }
        }

        private TableLayoutPanel BuildLetterRow(string keys)
        {
            var row = NewRow(keys.Length);
            for (int i = 0; i < keys.Length; i++)
            {
                var text = _shift ? keys[i].ToString().ToUpperInvariant() : keys[i].ToString();
                row.Controls.Add(Key(text, text, 0), i, 0);
            }
            return row;
        }

        private TableLayoutPanel BuildServiceRow()
        {
            // Ширины подобраны так, чтобы пробел был самым большим — в него
            // и целятся, не глядя.
            var row = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                RowCount = 1,
                ColumnCount = ShowEnterKey ? 6 : 5,
                BackColor = Color.Transparent
            };
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var widths = ShowEnterKey
                ? new float[] { 14, 10, 40, 14, 10, 12 }
                : new float[] { 16, 12, 44, 16, 12 };

            foreach (var width in widths) row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));

            int column = 0;
            row.Controls.Add(Key(LanguageName(), "@lang", 1), column++, 0);

            _shiftKey = Key("Аа", "@shift", _shift ? 2 : 1);
            row.Controls.Add(_shiftKey, column++, 0);
            row.Controls.Add(Key("пробел", " ", 0), column++, 0);
            row.Controls.Add(Key("⌫", "@back", 1), column++, 0);
            row.Controls.Add(Key("Стереть", "@clear", 1), column++, 0);
            if (ShowEnterKey) row.Controls.Add(Key("Ввод", "@enter", 3), column, 0);

            return row;
        }

        private TableLayoutPanel NewRow(int columns)
        {
            var row = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                RowCount = 1,
                ColumnCount = columns,
                BackColor = Color.Transparent
            };
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            for (int i = 0; i < columns; i++)
                row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columns));

            return row;
        }

        /// <summary>kind: 0 — буква, 1 — служебная, 2 — включённая служебная, 3 — «Ввод».</summary>
        private Button Key(string text, string tag, int kind)
        {
            var button = new NoFocusButton
            {
                Text = text,
                Tag = tag,
                Dock = DockStyle.Fill,
                Margin = new Padding(Ui.Px(2))
            };

            Color back = Ui.CardLight;
            if (kind == 1) back = Ui.Card;
            if (kind == 2) back = Ui.AccentDark;
            if (kind == 3) back = Ui.OkBg;

            Ui.TouchButton(button, back, kind >= 2 ? Ui.OnAccent : Ui.Text, kind == 0 ? 15f : 12f, kind == 0);
            button.Click += KeyClicked;
            return button;
        }

        /// <summary>
        /// Сменить регистр на уже собранных кнопках.
        ///
        /// Заглавная гасится после каждой буквы, и пересобирать ради этого всю
        /// клавиатуру — полсотни кнопок на каждое нажатие — заметно и глазу,
        /// и памяти. Здесь меняются только подписи.
        /// </summary>
        private void ApplyShift()
        {
            var rows = Rows();

            for (int i = 0; i < rows.Length; i++)
            {
                var row = layoutPanel.GetControlFromPosition(0, i) as TableLayoutPanel;
                if (row == null) continue;

                for (int column = 0; column < rows[i].Length; column++)
                {
                    var button = row.GetControlFromPosition(column, 0) as Button;
                    if (button == null) continue;

                    var text = _shift ? rows[i][column].ToString().ToUpperInvariant()
                                      : rows[i][column].ToString();
                    button.Text = text;
                    button.Tag = text;
                }
            }

            if (_shiftKey != null)
                Ui.TouchButton(_shiftKey, _shift ? Ui.AccentDark : Ui.Card, _shift ? Ui.OnAccent : Ui.Text, 12f, false);
        }

        private string LanguageName()
        {
            switch (_layout)
            {
                case KeyLayout.Latin: return "Eng";
                case KeyLayout.Symbols: return "!@#";
                default: return "Рус";
            }
        }

        private void KeyClicked(object sender, EventArgs e)
        {
            var tag = Convert.ToString(((Button)sender).Tag);

            switch (tag)
            {
                case "@lang":
                    _layout = _layout == KeyLayout.Russian ? KeyLayout.Latin
                            : _layout == KeyLayout.Latin ? KeyLayout.Symbols
                            : KeyLayout.Russian;
                    Build();
                    return;

                case "@shift":
                    _shift = !_shift;
                    ApplyShift();
                    return;

                case "@back":
                    Backspace();
                    return;

                case "@clear":
                    SetText("");
                    return;

                case "@enter":
                    var handler = EnterPressed;
                    if (handler != null) handler(this, EventArgs.Empty);
                    return;

                default:
                    Insert(tag);
                    if (_shift && tag != " ")
                    {
                        // Заглавная — только на одну букву, как на телефоне.
                        _shift = false;
                        ApplyShift();
                    }
                    return;
            }
        }

        private TextBoxBase Box()
        {
            var box = Target as TextBoxBase;
            if (box != null) return box;

            var combo = Target as ComboBox;
            if (combo != null) return null;

            return null;
        }

        private void Insert(string text)
        {
            var box = Box();
            if (box != null)
            {
                // SelectedText сам заменяет выделенное и ставит курсор после
                // вставки — ручная арифметика с позициями тут только плодит
                // ошибки вроде «в поле остаётся одна буква».
                box.SelectedText = text;
                box.Focus();
                return;
            }

            var combo = Target as ComboBox;
            if (combo == null) return;

            var current = combo.Text ?? "";
            var from = Clamp(combo.SelectionStart, current.Length);
            var count = Clamp(combo.SelectionLength, current.Length - from);

            combo.Text = current.Remove(from, count).Insert(from, text);
            Caret(combo, from + text.Length);
        }

        private static int Clamp(int value, int max)
        {
            if (value < 0) return 0;
            return value > max ? max : value;
        }

        /// <summary>
        /// Поставить курсор в поле со списком и снять выделение.
        ///
        /// Нужно вот зачем: присваивание ComboBox.Text выделяет весь текст
        /// целиком. Следующая нажатая буква попадала «поверх выделения» и
        /// затирала набранное — в поле навсегда оставалась одна буква.
        /// В обычном текстовом поле такого нет, поэтому баг вылезал только
        /// там, где предмет и учитель выбираются из списка.
        /// </summary>
        private static void Caret(ComboBox combo, int position)
        {
            var text = combo.Text ?? "";

            combo.SelectionStart = Clamp(position, text.Length);
            combo.SelectionLength = 0;
            combo.Focus();
        }

        private void Backspace()
        {
            var box = Box();
            if (box != null)
            {
                if (box.SelectionLength > 0)
                {
                    box.SelectedText = "";
                }
                else if (box.SelectionStart > 0)
                {
                    var caret = box.SelectionStart;
                    box.Text = box.Text.Remove(caret - 1, 1);
                    box.SelectionStart = caret - 1;
                    box.SelectionLength = 0;
                }

                box.Focus();
                return;
            }

            var combo = Target as ComboBox;
            if (combo == null) return;

            var current = combo.Text ?? "";
            var from = Clamp(combo.SelectionStart, current.Length);
            var count = Clamp(combo.SelectionLength, current.Length - from);

            if (count > 0)
            {
                combo.Text = current.Remove(from, count);
                Caret(combo, from);
            }
            else if (from > 0)
            {
                combo.Text = current.Remove(from - 1, 1);
                Caret(combo, from - 1);
            }
        }

        private void SetText(string text)
        {
            if (Target != null) Target.Text = text;

            var box = Box();
            if (box != null)
            {
                box.SelectionStart = box.Text.Length;
                box.SelectionLength = 0;
                box.Focus();
                return;
            }

            var combo = Target as ComboBox;
            if (combo != null) Caret(combo, combo.Text.Length);
        }
    }
}
