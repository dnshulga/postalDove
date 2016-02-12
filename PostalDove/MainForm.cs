using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostalDove
{
    public interface IMainForm
    {
        string Subject { set; get; }
        string Body { set; get; }
        bool isHtml { set; get; }

        void getInfoAccount();
        event EventHandler settingsStripClick;
        event EventHandler aboutStripClick;
        event EventHandler testingSendingStrip;
        event EventHandler mainSendingStrip;
    }

    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            sendBtn.Click += new EventHandler(SendBtn_Click);
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            if (mainSendingStrip != null) mainSendingStrip(this, EventArgs.Empty);
        }

        public string Subject
        {
            get { return subjTxtBox.Text; }
            set { subjTxtBox.Text = value; }
        }

        public string Body
        {
            get { return bodyTxtBox.Text; }
            set { bodyTxtBox.Text = value; }
        }

        public bool isHtml
        {
            get { return htmlCheckBox.Checked; }
            set { htmlCheckBox.Checked = value; }
        }

        public void getInfoAccount()
        {
            
        }

        public event EventHandler settingsStripClick;
        public event EventHandler aboutStripClick;
        public event EventHandler testingSendingStrip;
        public event EventHandler mainSendingStrip;

    }
}
