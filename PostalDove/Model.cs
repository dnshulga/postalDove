using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Reflection;

namespace PostalDove
{
    public interface IMailing
    {
        void sendMail(string subj, string body, object attachment = null);
        void backUPInformation();
        void logging();
        void getInfo();

        void showAbout();
    }

    static class Data
    {
        public static string _CompanyName;
        public static string _EmailLogin;
        public static string _Password;
        public static string _SmtpAddress;
        public static int _SmtpPort;
        public static bool _EnableSSL;
        public static bool _EnableHTML;
        public static int _IntervalBetween;
        public static int _QuantityForDay;
        public static string _TestAddress;
        public static List<string> _Destination;

        static Data()
        {
           _Destination = new List<string>();
        }

        public static void print()
        {
            var flds = typeof(Data).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var fld in flds)
            {
                object value = fld.GetValue(typeof(Data));
                MessageBox.Show(value.ToString());
            }
        }
    }

    class BaseModel : IMailing
    {
        public BaseModel(string login, string pass, List<string> dest, string smtpAddress, int port) : this("", login, pass, smtpAddress, port, true, false,
            10, 300, login, dest)
        {
            Data._EmailLogin = login;
            Data._Password = pass;
            Data._Destination = dest;
            Data._SmtpAddress = smtpAddress;
            Data._SmtpPort = port;
        }

        public BaseModel(string name, string login, string pass, string smtpAddress, int port, bool EnableSSL, bool EnableHTML,
            int between, int quant, string testAddress, List<string> dest)
        {
            Data._CompanyName = name;
            Data._EmailLogin = login;
            Data._Password = pass;
            Data._SmtpAddress = smtpAddress;
            Data._SmtpPort = port;
            Data._EnableSSL = EnableSSL;
            Data._EnableHTML = EnableHTML;
            Data._IntervalBetween = between;
            Data._QuantityForDay = quant;
            Data._TestAddress = testAddress;
            Data._Destination = dest;
        }
        public BaseModel() { }

        public virtual void sendMail(string subj, string body, object att)
        {
            try
            {

                MailAddress from = new MailAddress(Data._EmailLogin, Data._CompanyName);
                SmtpClient smtp = new SmtpClient(Data._SmtpAddress, Data._SmtpPort);
                if (Data._EnableSSL) smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(Data._EmailLogin, Data._Password);
                for (int i = 0; i < Data._Destination.Count; i++)
                {
                    MailAddress to = new MailAddress(Data._Destination[i]);
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = subj;
                    message.Body = body;
                    if (Data._EnableHTML) message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void showAbout()
        {
            AboutForm s = new AboutForm();
            s.ShowDialog();
        }

        public void backUPInformation()
        {

        }

        public void logging()
        {

        }

        public void getInfo()
        {
            try
            {
                StreamReader sr = new StreamReader("options/account.dll", Encoding.UTF8);
                Data._EmailLogin = sr.ReadLine();
                Data._CompanyName = sr.ReadLine();
                Data._Password = sr.ReadLine();
                Data._SmtpAddress = sr.ReadLine();
                Data._SmtpPort = Convert.ToInt32(sr.ReadLine());
                Data._EnableSSL = Convert.ToBoolean(sr.ReadLine());
                Data._IntervalBetween = Convert.ToInt32(sr.ReadLine());
                Data._QuantityForDay = Convert.ToInt32(sr.ReadLine());
                Data._TestAddress = sr.ReadLine();
                Data._EnableHTML = Convert.ToBoolean(sr.ReadLine());
                sr.Close();
                StreamReader sr1 = new StreamReader("database.txt", Encoding.UTF8);
                while (!sr1.EndOfStream)
                    Data._Destination.Add(sr1.ReadLine());
                sr1.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }

    class TestSending : BaseModel
    {
        public override void sendMail(string subj, string body, object att)
        {
            try
            {
                MailAddress from = new MailAddress(Data._EmailLogin, Data._CompanyName);
                SmtpClient smtp = new SmtpClient(Data._SmtpAddress, Data._SmtpPort);
                if (Data._EnableSSL) smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(Data._EmailLogin, Data._Password);
                MailAddress to = new MailAddress(Data._TestAddress);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subj;
                message.Body = body;
                if (Data._EnableHTML) message.IsBodyHtml = true;
                smtp.Send(message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
