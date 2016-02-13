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
        public virtual string ShowExc()
        {
            return base.Message;
        }
    }

    sealed class EmptySubject : OwnExceptions
    {
        public override string ShowExc()
        {
            return "Не заполнено поле темы сообщения";
        }
    }

    sealed class EmptyBody : OwnExceptions
    {
        public override string ShowExc()
        {
            return "Не заполнено поле текста сообщения";
        }
    }
}
