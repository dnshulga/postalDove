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
    
    class BaseModel : IMailing
    {
        protected string CompanyName { get; set; }
        string EmailLogin { get; set; }
        string Password { get; set; }
        string SmtpAddress { get; set; }
        int SmtpPort { get; set; }
        bool EnableSSL { get; set; }
        bool EnableHTML { get; set; }
        int IntervalBetween { get; set; }
        int QuantityForDay { get; set; }
        string TestAddress { get; set; }
        string Destination { get; set; }

        public virtual void sendMail(string subj, string body, object att)
        {
            MailAddress from = new MailAddress(this.EmailLogin, this.CompanyName);
            MailAddress to = new MailAddress(this.Destination);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subj;
            message.Body = body;
            SmtpClient smtp = new SmtpClient(this.SmtpAddress, this.SmtpPort);
            if (EnableSSL) smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(this.EmailLogin, this.Password);
            if (EnableHTML) message.IsBodyHtml = true;
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
