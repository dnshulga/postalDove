using System;
using System.Windows.Forms;

namespace PostalDove
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm view = new MainForm();
            BaseModel model = new BaseModel();

            MainPresenter presenter = new MainPresenter(model, view);
            Application.Run(view);
        }
    }
}
