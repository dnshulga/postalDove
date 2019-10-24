using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace spamer
{
    public partial class OptionsYourMail : Form
    {
        bool change = false;
        public OptionsYourMail()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (change == false)
                this.Close();
            else
            {
                DialogResult result = MessageBox.Show("Вы уверены?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    TextBox[] tb = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8 };
                    for (int i = 0; i < tb.Length; i++)
                    {
                        if (tb[i].Text == "")
                        {
                            check = true;
                            MessageBox.Show("Присутствует пустое поле");
                        }
                    }

                    if (!check)
                    {
                        FileStream fs = new FileStream("options/account.dll", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                        sw.Write(textBox1.Text + "\n"); //логин
                        sw.Write(textBox6.Text + "\n");  //представление
                        sw.Write(textBox2.Text + "\n"); //
                        sw.Write(textBox3.Text + "\n");
                        sw.Write(textBox4.Text + "\n");
                        if (checkBox1.Checked == true)
                            sw.Write("yes" + "\n");
                        else if (checkBox1.Checked == false)
                            sw.Write("no" + "\n");
                        sw.Write(textBox5.Text + "\n");
                        sw.Write(textBox7.Text + "\n");
                        sw.Write(textBox8.Text);
                        
                        MessageBox.Show("Произойдет перезапуск программы");
                        sw.Close();
                        fs.Close();
                        this.Close();
                        Application.Restart();
                        
                    }
                }
            }
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void OptionsYourMail_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)
                if (ctrl is TextBox)
                    ctrl.Enabled = false;
            this.HelpButton = true;
            checkBox1.Enabled = false;
            StreamReader sr = new StreamReader("options/account.dll", Encoding.UTF8);
            string temp;
            while ((temp = sr.ReadLine()) != null)
            {
                textBox1.Text = temp;
                textBox6.Text = sr.ReadLine();
                textBox2.Text = sr.ReadLine();
                textBox3.Text = sr.ReadLine();
                textBox4.Text = sr.ReadLine();
                if (sr.ReadLine() == "yes") checkBox1.Checked = true;
                textBox5.Text = (Convert.ToInt32(sr.ReadLine())).ToString();
                textBox7.Text = sr.ReadLine();
                textBox8.Text = sr.ReadLine();
            }
            sr.Close();
        }

        private void ChangeBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                change = true;
                foreach (Control ctrl in panel1.Controls)
                    if (ctrl is TextBox)
                        ctrl.Enabled = true;
                checkBox1.Enabled = true;
                ChangeBtn.Enabled = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)   //имя смтп, тестовое мыло
        {
            char l = e.KeyChar;
            if ((l < 'A' || l > 'z') && l != '\b' && l != '.' && !Char.IsDigit(e.KeyChar) && l != '@')
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) //пароль, сервак
        {
            if (e.KeyChar == Convert.ToChar(32))
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e) //порт, интервал
        {
            if (e.KeyChar != Convert.ToChar(32) && !Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e) //количество
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8) && e.KeyChar != 'I' && e.KeyChar != 'N' && e.KeyChar != 'F')
                e.Handled = true;
        }

        private void OptionsYourMail_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("notepad.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.Arguments = "options/help.dll";
            Process.Start(startInfo);
            e.Cancel = true;
        }
    }
}
