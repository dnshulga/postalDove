using System;
using System.Windows.Forms;

namespace PostalDove
{
    class OwnExceptions : Exception
    {
        public OwnExceptions() : base() { }
        public OwnExceptions(string message) : base(message) { }
        public OwnExceptions(string message, Exception innerExc) : base(message, innerExc) { }

        public virtual void ShowMessage()
        {
            MessageBox.Show(base.Message,"Error!",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    sealed class EmptySubject : OwnExceptions
    {
        public override void ShowMessage()
        {
            MessageBox.Show("Не заполнено поле темы сообщения", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    sealed class EmptyBody : OwnExceptions
    {
        public override void ShowMessage()
        {
            MessageBox.Show("Не заполнено поле для текста сообщения", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
