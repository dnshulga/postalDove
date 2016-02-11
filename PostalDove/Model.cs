using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Net;

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
        public string CompanyName { get; set; }
        public string EmailLogin { get; set; }
        public string Password { get; set; }
        public string SmtpAddress { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSSL { get; set; } 
        public bool EnableHTML { get; set; }
        public int IntervalBetween { get; set; } //интервал в секундах между письмами
        public int QuantityForDay { get; set; }  //ограничение писем в день
        public string TestAddress { get; set; }
        public List<string> Destination { get; set; }
    }

    class BaseModel : IMailing
    {
        protected Info inf = new Info();
        
        public BaseModel(string login, string pass, List<string> dest, string smtpAddress, int port):this("",login,pass,smtpAddress,port,true,false,
            10,300,login,dest)
        {
            inf.EmailLogin = login;
            inf.Password = pass;
            inf.Destination = dest;
            inf.SmtpAddress = smtpAddress;
            inf.SmtpPort = port;
        }

        public BaseModel(string name, string login, string pass, string smtpAddress, int port, bool EnableSSL, bool EnableHTML, 
            int between, int quant, string testAddress, List<string> dest)
        {
            inf.CompanyName = name;
            inf.EmailLogin = login;
            inf.Password = pass;
            inf.SmtpAddress = smtpAddress;
            inf.SmtpPort = port;
            inf.EnableSSL = EnableSSL;
            inf.EnableHTML = EnableHTML;
            inf.IntervalBetween = between;
            inf.QuantityForDay = quant;
            inf.TestAddress = testAddress;
            inf.Destination = dest;
        }
        public BaseModel(){}

        public virtual void sendMail(string subj, string body, object att)
        {
            MailAddress from = new MailAddress(inf.EmailLogin, inf.CompanyName);
            SmtpClient smtp = new SmtpClient(inf.SmtpAddress, inf.SmtpPort);
            if (inf.EnableSSL) smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(inf.EmailLogin, inf.Password);
            for (int i = 0; i < inf.Destination.Count; i++)
            {
                MailAddress to = new MailAddress(inf.Destination[i]);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subj;
                message.Body = body;
                if (inf.EnableHTML) message.IsBodyHtml = true;
                smtp.Send(message);
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

        event EventHandler<EventArgs> RefreshListView; //обновить список
        event EventHandler<EventArgs> ClickAbout;      //посмотреть информацию "О программе"
        event EventHandler<EventArgs> ShowSettings;    //посмотреть настройки
        event EventHandler<EventArgs> DoTestSending;   //сделать тестовую отправку
        event EventHandler<EventArgs> ExpandForm;      //развернуть форму по клику
        event EventHandler<EventArgs> DoMainSending;   //произвести основную рассылку

    }

    class TestSending : BaseModel
    {
        public override void sendMail(string subj, string body, object att)
        {

        }
    }

    sealed class Settings
    {

    }
}
