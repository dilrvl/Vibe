using Microsoft.EntityFrameworkCore;

namespace Vibe
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Создаем контекст базы данных
            Application.Run(new Entrance());
        }
    }
}
