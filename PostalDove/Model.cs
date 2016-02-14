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
using System.Threading;

namespace PostalDove
{
    public interface IMailing
    {
        void sendMail(MessageMembers mb);
        void backUPInformation();
        void logging();
        void getInfo();

        void showAbout();
        void showSettings();
        void testMail(MessageMembers mb);
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

        public virtual void sendMail(MessageMembers mb)
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
                    message.Subject = mb.Subject;
                    message.Body = mb.Body;
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

        public void showSettings()
        {
            Settings st = new Settings();
            st.ShowDialog();
        }

        public void testMail(MessageMembers mb)
        {
            TestSending ts = new TestSending();
            ts.sendMail(mb);
        }
    }

    class TestSending : BaseModel
    {
        public override void sendMail(MessageMembers md)
        {
            try
            {
                if (md.Subject.Length == 0)
                    throw new EmptySubject();
                if (md.Body.Length == 0)
                    throw new EmptyBody();
                md.actBtnTest.Enabled = false;
                MailAddress from = new MailAddress(Data._EmailLogin, Data._CompanyName);
                SmtpClient smtp = new SmtpClient(Data._SmtpAddress, Data._SmtpPort);
                if (Data._EnableSSL) smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(Data._EmailLogin, Data._Password);
                MailAddress to = new MailAddress(Data._TestAddress);
                MailMessage message = new MailMessage(from, to);
                message.Subject = md.Subject;
                message.Body = md.Body;
                if (Data._EnableHTML) message.IsBodyHtml = true;
                smtp.Send(message);
                MessageBox.Show("Отправлено успешно на " + Data._TestAddress, "Тестовая отправка", MessageBoxButtons.OK);
                md.actBtnTest.Enabled = true;
            }
            catch (Exception exc)
            {
                if (exc is EmptySubject)
                    (exc as EmptySubject).ShowMessage();
                else if (exc is EmptyBody)
                    (exc as EmptyBody).ShowMessage();
                else
                    (exc as OwnExceptions).ShowMessage();
            }
        }
        
    }
}
