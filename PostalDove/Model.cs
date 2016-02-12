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
    }

    struct Info
    {
        public string _EmailLogin;
        public string _CompanyName;
        public string _Password;
        public string _SmtpAddress;
        public int _SmtpPort;
        public bool _EnableSSL;
        public int _IntervalBetween; //интервал в секундах между письмами
        public int _QuantityForDay;  //ограничение писем в день
        public string _TestAddress;
        public bool _EnableHTML;
        private List<string> _Destination;
        public List<string> Destination
        {
            get { return _Destination; }
            set { _Destination = value; }
        }
    }

    class BaseModel : IMailing
    {
        protected Info inf = new Info();

        public BaseModel(string login, string pass, List<string> dest, string smtpAddress, int port) : this("", login, pass, smtpAddress, port, true, false,
            10, 300, login, dest)
        {
            inf._EmailLogin = login;
            inf._Password = pass;
            inf.Destination = dest;
            inf._SmtpAddress = smtpAddress;
            inf._SmtpPort = port;
        }

        public BaseModel(string name, string login, string pass, string smtpAddress, int port, bool EnableSSL, bool EnableHTML,
            int between, int quant, string testAddress, List<string> dest)
        {
            inf._CompanyName = name;
            inf._EmailLogin = login;
            inf._Password = pass;
            inf._SmtpAddress = smtpAddress;
            inf._SmtpPort = port;
            inf._EnableSSL = EnableSSL;
            inf._EnableHTML = EnableHTML;
            inf._IntervalBetween = between;
            inf._QuantityForDay = quant;
            inf._TestAddress = testAddress;
            inf.Destination = dest;
        }
        public BaseModel() { }

        public virtual void sendMail(string subj, string body, object att)
        {
            try
            {
                MailAddress from = new MailAddress(inf._EmailLogin, inf._CompanyName);
                SmtpClient smtp = new SmtpClient(inf._SmtpAddress, inf._SmtpPort);
                if (inf._EnableSSL) smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(inf._EmailLogin, inf._Password);
                for (int i = 0; i < inf.Destination.Count; i++)
                {
                    MailAddress to = new MailAddress(inf.Destination[i]);
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = subj;
                    message.Body = body;
                    if (inf._EnableHTML) message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void backUPInformation()
        {

        }

        public void logging()
        {

        }

        public void getInfo()
        {

        }
    }

    class TestSending : BaseModel
    {
        public override void sendMail(string subj, string body, object att)
        {

        }
    }

    static class Service
    {
        static string _filePathAcc = "options/account.dll";
        static string _errorCaption = "Ошибка";
        static string _errorText = MethodBase.GetCurrentMethod().ToString() + " вызвал ошибку!\n";

        public static void getInfo()
        {
            try
            {
                StreamReader sr = new StreamReader(_filePathAcc, Encoding.UTF8);
                FieldInfo[] fi = typeof(Info).GetFields(BindingFlags.Public | BindingFlags.Instance);
                MessageBox.Show(fi.Length.ToString());
                foreach (FieldInfo info in fi)
                {
                    info.SetValue(info.Name,sr.ReadLine());
                }
                sr.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(Service._errorText+exc.Message, Service._errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
