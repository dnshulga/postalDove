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
        public int IntervalBetween { get; set; }
        public int QuantityForDay { get; set; }
        public string TestAddress { get; set; }
        public string Destination { get; set; }
    }

    class BaseModel : IMailing
    {
        protected Info inf = new Info();
        
        public BaseModel(string login, string pass, string dest, string smtpAddress, int port):this("",login,pass,smtpAddress,port,false,false,
            10,300,login,dest)
        {
            inf.EmailLogin = login;
            inf.Password = pass;
            inf.Destination = dest;
            inf.SmtpAddress = smtpAddress;
            inf.SmtpPort = port;
        }

        public BaseModel(string name, string login, string pass, string smtpAddress, int port, bool EnableSSL, bool EnableHTML, 
            int between, int quant, string testAddress, string dest)
        {
                
        }

        public virtual void sendMail(string subj, string body, object att)
        {
            MailAddress from = new MailAddress(inf.EmailLogin, inf.CompanyName);
            MailAddress to = new MailAddress(inf.Destination);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subj;
            message.Body = body;
            SmtpClient smtp = new SmtpClient(inf.SmtpAddress, inf.SmtpPort);
            if (inf.EnableSSL) smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(inf.EmailLogin, inf.Password);
            if (inf.EnableHTML) message.IsBodyHtml = true;
            //
            smtp.Send(message);
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
        public override void sendMail()
        {

        }
    }
}
