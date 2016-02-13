using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostalDove
{
    class OwnExceptions : Exception
    {
        public OwnExceptions():base(){}
        public OwnExceptions(string message):base(message) {}
        public OwnExceptions(string message, Exception innerExc):base(message,innerExc) {}

        public virtual string ShowMessage()
        {
            return base.Message;
        }
    }

    sealed class EmptySubject : OwnExceptions
    {
        public override string ShowMessage()
        {
            return "Не заполнено поле темы сообщения";
        }
    }

    sealed class EmptyBody : OwnExceptions
    {
        public override string ShowMessage()
        {
            return "Не заполнено поле текста сообщения";
        }
    }
}
