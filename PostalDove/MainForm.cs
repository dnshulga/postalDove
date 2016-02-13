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
        string Subject { get; }
        string Body { get; }
        bool isHtml { get; }
        
        event EventHandler settingsStripClick;
        event EventHandler aboutStripClick;
        event EventHandler testingSendingStrip;
        event EventHandler mainSendingClick;
        event EventHandler getInfoLoad;
    }

    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            sendBtn.Click += new EventHandler(SendBtn_Click);
            OptionsStrip.Click += new EventHandler(OptionsStrip_Click);
            AboutStrip.Click += new EventHandler(AboutStrip_Click);
            testSendingStrip.Click += new EventHandler(TestSendingStrip_Click);
            this.Load += new EventHandler(MainForm_Load);
        }
        
        #region Проброс событий
        private void TestSendingStrip_Click(object sender, EventArgs e)
        {
            if (testingSendingStrip != null) testingSendingStrip(this, EventArgs.Empty);
        }

        private void AboutStrip_Click(object sender, EventArgs e)
        {
            if (aboutStripClick != null) aboutStripClick(this, EventArgs.Empty);
        }

        private void OptionsStrip_Click(object sender, EventArgs e)
        {
            if (settingsStripClick != null) settingsStripClick(this, EventArgs.Empty);
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            if (mainSendingClick != null) mainSendingClick(this, EventArgs.Empty);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (getInfoLoad != null) getInfoLoad(this, EventArgs.Empty);
        }
        #endregion

        #region IMainForm
        public string Subject
        {
            get { return subjTxtBox.Text; }
        }

        public string Body
        {
            get { return bodyTxtBox.Text; }
        }

        public bool isHtml
        {
            get { return htmlCheckBox.Checked; }
        }
        
        public event EventHandler settingsStripClick;
        public event EventHandler aboutStripClick;
        public event EventHandler testingSendingStrip;
        public event EventHandler mainSendingClick;
        public event EventHandler getInfoLoad;
        #endregion
    }
}
