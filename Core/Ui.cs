using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolSchedule.Core
{
    public enum AppTheme
    {
        Dark,
        Light
    }

    /// <summary>
    /// Общий вид программы: палитра, крупные шрифты, кнопки под палец.
    ///
    /// Тем две. Тёмная стоит по умолчанию не ради моды: экран висит в коридоре
    /// и светит целый день, а белое полотно на 65 дюймах слепит и выжигает
    /// матрицу. Светлая нужна там, где вокруг светло, и на обычном мониторе
    /// учителя.
    /// </summary>
    public static class Ui
    {
        /// <summary>Тёмная — для телевизора в коридоре, светлая — для кабинета.</summary>
        public static AppTheme Theme { get; private set; }

        public static Color Bg { get; private set; }
        public static Color Header { get; private set; }
        public static Color GridHeader { get; private set; }
        public static Color Card { get; private set; }
        public static Color CardLight { get; private set; }
        public static Color Line { get; private set; }
        public static Color Text { get; private set; }
        public static Color Muted { get; private set; }
        public static Color Accent { get; private set; }
        public static Color AccentDark { get; private set; }
        public static Color Warn { get; private set; }
        public static Color WarnBg { get; private set; }
        public static Color Ok { get; private set; }

        /// <summary>
        /// Зелёный для заливки кнопки. Отдельно от <see cref="Ok"/> нарочно:
        /// светло-зелёный хорош как текст на тёмном фоне, но белая надпись на
        /// нём почти не читается — на кнопке нужен тёмный оттенок.
        /// </summary>
        public static Color OkBg { get; private set; }
        public static Color Danger { get; private set; }
        public static Color DangerBg { get; private set; }
        public static Color RowEven { get; private set; }
        public static Color RowOdd { get; private set; }

        /// <summary>
        /// Цвет надписи на цветной кнопке. В обеих темах белый — иначе на
        /// светлой тёмный текст ложился бы на тёмно-синий фон и пропадал.
        /// </summary>
        public static Color OnAccent { get { return Color.White; } }

        private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();
        private static readonly object Sync = new object();

        /// <summary>Общий множитель размеров: из ip.txt и из высоты экрана.</summary>
        public static float Scale { get; private set; }

        /// <summary>Разобрать название темы: «light» / «светлая» → светлая, иначе тёмная.</summary>
        public static AppTheme ParseTheme(string value)
        {
            var text = (value ?? "").Trim().ToLowerInvariant();
            return text == "light" || text == "светлая" || text == "белая" ? AppTheme.Light : AppTheme.Dark;
        }

        /// <summary>
        /// Переключить палитру. Формы читают цвета в своём ApplyTheme, поэтому
        /// после смены их нужно попросить перекраситься — окна, открытые
        /// позже, подхватят новые цвета сами.
        /// </summary>
        public static void SetTheme(AppTheme theme)
        {
            Theme = theme;

            if (theme == AppTheme.Light)
            {
                // Светлая — для учительского компьютера и для экрана в светлом
                // холле, где тёмное полотно выглядит выключенным телевизором.
                Bg = Color.FromArgb(241, 245, 249);
                Header = Color.FromArgb(255, 255, 255);
                GridHeader = Color.FromArgb(226, 232, 240);
                Card = Color.FromArgb(255, 255, 255);
                CardLight = Color.FromArgb(226, 232, 240);
                Line = Color.FromArgb(203, 213, 225);
                Text = Color.FromArgb(15, 23, 42);
                Muted = Color.FromArgb(100, 116, 139);
                Accent = Color.FromArgb(2, 132, 199);
                AccentDark = Color.FromArgb(3, 105, 161);
                Warn = Color.FromArgb(180, 83, 9);
                WarnBg = Color.FromArgb(254, 243, 199);
                Ok = Color.FromArgb(21, 128, 61);
                OkBg = Color.FromArgb(21, 128, 61);
                Danger = Color.FromArgb(185, 28, 28);
                DangerBg = Color.FromArgb(185, 28, 28);
                RowEven = Color.FromArgb(248, 250, 252);
                RowOdd = Color.FromArgb(255, 255, 255);
                return;
            }

            // Тёмная по умолчанию: экран висит в коридоре и светит целый день,
            // а белое полотно на 65 дюймах слепит и выжигает матрицу.
            Bg = Color.FromArgb(15, 23, 42);
            Header = Color.FromArgb(2, 6, 23);
            GridHeader = Color.FromArgb(2, 6, 23);
            Card = Color.FromArgb(30, 41, 59);
            CardLight = Color.FromArgb(51, 65, 85);
            Line = Color.FromArgb(51, 65, 85);
            Text = Color.FromArgb(248, 250, 252);
            Muted = Color.FromArgb(148, 163, 184);
            Accent = Color.FromArgb(56, 189, 248);
            AccentDark = Color.FromArgb(3, 105, 161);
            Warn = Color.FromArgb(251, 191, 36);
            WarnBg = Color.FromArgb(69, 47, 8);
            Ok = Color.FromArgb(34, 197, 94);
            OkBg = Color.FromArgb(21, 128, 61);
            Danger = Color.FromArgb(239, 68, 68);
            DangerBg = Color.FromArgb(127, 29, 29);
            RowEven = Color.FromArgb(23, 33, 56);
            RowOdd = Color.FromArgb(15, 23, 42);
        }

        public static void Init()
        {
            SetTheme(Theme);

            float fromScreen = 1f;
            try
            {
                var screen = Screen.PrimaryScreen;
                if (screen != null && screen.Bounds.Height > 0)
                    fromScreen = screen.Bounds.Height / 1080f;
            }
            catch { }

            // Ниже единицы не опускаемся: на ноутбуке учителя окно должно
            // остаться читаемым, а не сжаться в петит.
            if (fromScreen < 1f) fromScreen = 1f;
            if (fromScreen > 2f) fromScreen = 2f;

            Scale = (float)AppConfig.Scale * fromScreen;
        }

        /// <summary>Шрифт нужного размера. Кэшируется: программа живёт сутками, GDI-объекты — нет.</summary>
        public static Font F(float size, bool bold)
        {
            return Fx(size, bold ? FontStyle.Bold : FontStyle.Regular);
        }

        public static Font F(float size)
        {
            return Fx(size, FontStyle.Regular);
        }

        public static Font Fx(float size, FontStyle style)
        {
            if (Scale <= 0) Init();

            var key = size.ToString("0.##") + "/" + (int)style;
            lock (Sync)
            {
                Font font;
                if (Fonts.TryGetValue(key, out font)) return font;

                font = new Font("Segoe UI", size * Scale, style, GraphicsUnit.Point);
                Fonts[key] = font;
                return font;
            }
        }

        /// <summary>
        /// Шрифт, заданный высотой в пикселях. Нужен таблице расписания:
        /// строки там растягиваются по высоте экрана, и текст должен идти
        /// следом, а не оставаться мелким на телевизоре и не вылезать на ноутбуке.
        /// </summary>
        public static Font Fp(float pixels, bool bold)
        {
            if (pixels < 10f) pixels = 10f;
            if (pixels > 96f) pixels = 96f;

            var rounded = (float)Math.Round(pixels);
            var key = "px" + rounded + "/" + (bold ? 1 : 0);

            lock (Sync)
            {
                Font font;
                if (Fonts.TryGetValue(key, out font)) return font;

                font = new Font("Segoe UI", rounded, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Pixel);
                Fonts[key] = font;
                return font;
            }
        }

        public static int Px(int value)
        {
            if (Scale <= 0) Init();
            return (int)Math.Round(value * Scale);
        }

        /// <summary>Кнопка под палец: без рамки фокуса, крупная, с заметным нажатием.</summary>
        public static void TouchButton(Button button, Color back, Color fore, float fontSize, bool bold)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Lighten(back, 0.12);
            button.FlatAppearance.MouseDownBackColor = Lighten(back, 0.25);
            button.BackColor = back;
            button.ForeColor = fore;
            button.Font = F(fontSize, bold);
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;

            // Пунктирная рамка фокуса на сенсорном экране только мешает:
            // палец «нажал и ушёл», а рамка осталась висеть.
            button.TabStop = false;
        }

        public static void TouchButton(Button button)
        {
            TouchButton(button, Card, Text, 14f, false);
        }

        public static void PrimaryButton(Button button)
        {
            TouchButton(button, AccentDark, OnAccent, 14f, true);
        }

        public static void DangerButton(Button button)
        {
            TouchButton(button, DangerBg, OnAccent, 14f, false);
        }

        public static Color Lighten(Color color, double amount)
        {
            return Color.FromArgb(
                color.A,
                (int)Math.Min(255, color.R + (255 - color.R) * amount),
                (int)Math.Min(255, color.G + (255 - color.G) * amount),
                (int)Math.Min(255, color.B + (255 - color.B) * amount));
        }

        public static Color Darken(Color color, double amount)
        {
            return Color.FromArgb(color.A, (int)(color.R * (1 - amount)), (int)(color.G * (1 - amount)),
                                  (int)(color.B * (1 - amount)));
        }

        /// <summary>Единый вид таблиц: без выделения, с крупными строками.</summary>
        public static void StyleGrid(DataGridView grid, bool readOnly)
        {
            grid.BackgroundColor = Bg;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Line;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToOrderColumns = false;
            grid.MultiSelect = false;
            grid.ReadOnly = readOnly;
            grid.ShowCellToolTips = false;
            grid.ScrollBars = ScrollBars.None;

            grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Accent;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeader;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Accent;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.ColumnHeadersDefaultCellStyle.Font = F(16f, true);

            grid.DefaultCellStyle.BackColor = RowOdd;
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = RowOdd;
            grid.DefaultCellStyle.SelectionForeColor = Text;
            grid.DefaultCellStyle.Font = F(14f, false);
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.DefaultCellStyle.Padding = new Padding(Px(6), Px(4), Px(6), Px(4));

            if (!readOnly)
            {
                // В таблице, которую правят, выделение наоборот нужно: без него
                // не видно, какую клетку сейчас редактируешь.
                grid.DefaultCellStyle.SelectionBackColor = Accent;
                grid.DefaultCellStyle.SelectionForeColor = OnAccent;
            }
        }

        /// <summary>Двойная буферизация таблицы — иначе перерисовка на 65 дюймах моргает.</summary>
        public static void EnableDoubleBuffer(Control control)
        {
            try
            {
                var property = control.GetType().GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (property != null) property.SetValue(control, true, null);
            }
            catch { }
        }

        /// <summary>Текст по центру прямоугольника — короткая запись для отрисовки ячеек.</summary>
        public static void DrawCentered(Graphics graphics, string text, Font font, Color color, Rectangle bounds)
        {
            using (var brush = new SolidBrush(color))
            using (var format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                format.Trimming = StringTrimming.EllipsisCharacter;
                graphics.DrawString(text, font, brush, bounds, format);
            }
        }
    }
}
