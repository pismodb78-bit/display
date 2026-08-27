using System.Windows.Forms;

namespace SchoolSchedule.Controls
{
    /// <summary>
    /// Кнопка, которая не забирает фокус.
    ///
    /// Нужна экранной клавиатуре: если нажатие «й» уводит фокус с поля ввода,
    /// поле теряет курсор, а сама клавиатура прячется — печатать становится
    /// невозможно. Обычная кнопка ведёт себя именно так.
    /// </summary>
    public class NoFocusButton : Button
    {
        public NoFocusButton()
        {
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
        }
    }
}
