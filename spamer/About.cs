using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace spamer
{
    public partial class About : Form
    {
        ToolTip t = new ToolTip();
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("http://vk.com/dnshulga");
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            t.SetToolTip(label2, "Перейти в профиль ВК");
            this.Cursor = Cursors.Hand;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label6.Text); 
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            t.SetToolTip(label6,"Скопировать e-mail в буфер обмена");
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Process.Start("http://dmitryshul.ga");
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            t.SetToolTip(label7,"Перейти на личный сайт разработчика");
            this.Cursor = Cursors.Hand;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void About_Load(object sender, EventArgs e)
        {

        }


        
    }
}
