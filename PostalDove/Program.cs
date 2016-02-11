using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ///testing
            /*BaseModel b = new BaseModel("mylogin@gmail.com", "mypass", new List<string> { "dmitriishulga95@gmail.com" }, "smtp.gmail.com", 587);
            try
            {
                b.sendMail("hello", "foobar", null);
                MessageBox.Show("Success");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }*/
            Application.Run(new MainForm());
        }
    }
}
