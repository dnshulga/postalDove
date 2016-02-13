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
        public virtual void ShowExc()
        {
            MessageBox.Show(base.Message);
        }
    }

    class EmptySubject : OwnExceptions
    {
        public override void ShowExc()
        {
            MessageBox.Show("");
        }
    }
}
