using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SchoolSchedule.Core;
using SchoolSchedule.Forms;

namespace SchoolSchedule
{
    internal static class Program
    {
        /// <summary>
        /// Программа умеет запускаться в двух видах:
        ///
        ///   обычный  — витрина во весь экран на телевизоре в коридоре;
        ///   редактор — сразу окно учителя, без полноэкранного показа.
        ///
        /// Второй нужен, чтобы править расписание с другого компьютера: та же
        /// программа, тот же ip.txt с адресом школьного сервера, но вместо
        /// витрины открывается редактор. Включается строкой «mode = editor»
        /// в ip.txt или ключом командной строки /editor в ярлыке.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Excel в русской Windows сохраняет CSV в кодировке 1251; без этой
            // строки .NET её просто не знает, и загруженное расписание выглядит
            // как «Ìàòåìàòèêà».
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            AppConfig.Load();
            Ui.Init();

            // Тема из ip.txt действует с первого кадра; ту, что выбрана в базе,
            // окна подхватят, как только прочитают настройки.
            if (!string.IsNullOrWhiteSpace(AppConfig.Theme)) Ui.SetTheme(Ui.ParseTheme(AppConfig.Theme));

            Application.ThreadException += delegate (object sender, System.Threading.ThreadExceptionEventArgs e)
            {
                Report(e.Exception);
            };
            AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs e)
            {
                Report(e.ExceptionObject as Exception);
            };

            if (IsEditorMode(args)) RunEditor();
            else Application.Run(new DisplayForm());
        }

        private static bool IsEditorMode(string[] args)
        {
            foreach (var arg in args)
            {
                var value = (arg ?? "").TrimStart('-', '/').ToLowerInvariant();
                if (value == "editor" || value == "редактор") return true;
            }

            var mode = AppConfig.Get("", "mode", "режим").Trim().ToLowerInvariant();
            return mode == "editor" || mode == "редактор";
        }

        /// <summary>
        /// Режим редактора. Пароль спрашиваем и здесь: программа стоит на
        /// учительском компьютере, за которым сидят не только учителя.
        /// </summary>
        private static void RunEditor()
        {
            string error;
            if (!Schema.Ensure(out error))
            {
                var answer = MessageBox.Show(
                    error + Environment.NewLine + Environment.NewLine + "Открыть настройки подключения?",
                    "Нет связи с базой", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (answer != DialogResult.Yes) return;

                // Пароль спрашиваем до окна подключения: иначе, выключив
                // сервер, к настройкам базы мог бы добраться кто угодно.
                // Пароля из базы сейчас не достать, поэтому действует
                // запасной из ip.txt.
                if (!PasswordForm.Ask(null, "")) return;

                using (var connection = new ConnectionForm())
                {
                    if (connection.ShowDialog() != DialogResult.OK) return;
                }

                if (!Schema.Ensure(out error))
                {
                    MessageBox.Show(error, "Нет связи с базой", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Application.Run(new EditorForm());
                return;
            }

            var storedHash = "";
            try
            {
                var settings = DisplaySettings.From(Repo.Settings());
                storedHash = settings.AdminPasswordHash;
                if (!string.IsNullOrWhiteSpace(settings.Theme) && string.IsNullOrWhiteSpace(AppConfig.Theme))
                    Ui.SetTheme(Ui.ParseTheme(settings.Theme));
            }
            catch { /* база только что поднялась — пустим по паролю из ip.txt */ }

            if (!PasswordForm.Ask(null, storedHash)) return;

            Application.Run(new EditorForm());
        }

        /// <summary>
        /// Записать ошибку рядом с .exe и показать по-человечески. На стене
        /// в коридоре стандартное окно .NET с трассировкой стека бесполезно,
        /// а файл потом можно прислать.
        /// </summary>
        private static void Report(Exception ex)
        {
            if (ex == null) return;

            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "ошибки.log");
                File.AppendAllText(path,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + ex + Environment.NewLine + Environment.NewLine,
                    new UTF8Encoding(true));
            }
            catch { }

            try
            {
                MessageBox.Show(Db.Explain(ex), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
    }
}
