using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostalDove
{
    public class MessageMembers
    {
        string _subject;
        string _body;
        object _attachment;
        ToolStripMenuItem _activeBtn;

        public MessageMembers(string subj, string body, object att, ToolStripMenuItem activeBtn)
        {
            this.Subject = subj;
            this.Body = body;
            this.Attachment = att;
            this._activeBtn = activeBtn;
        }
        public MessageMembers(){ }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public object Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }

        public ToolStripMenuItem actBtnTest
        {
            get { return _activeBtn; }
            set { _activeBtn = value; }
        }
    }
}
