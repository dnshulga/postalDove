using System;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;

namespace spamer
{
    public partial class Main : Form
    {
        ToolTip t = new ToolTip();  //экземпляр класса для всплывающих подсказок

        /*--------------Отправка сообщений массово--------------------------------*/
        string from, nameOfCompany, pass, smtp, checkSSL; //поля для работы метода отправки сообщений
        int port, interval; //порт, интервал

        string quantity;
        string testMail;
        string[] continueArray;

        int logPlan;
        int logDone;

        int columnID = 0;
        string[] mails;
        int quantOfAbonents = 0;
        string criticalError = "\"Все имеют право на ошибку!\"\nРазработчик Дмитрий Шульга что-то не предусмотрел. Пожалуйста, сообщите об этом ему!\nИнформация для Дмитрия: ошибка в методе\n";

        /*----------------поля для лога-----------------*/
        string[] brokenMails; //массив битых ящиков
        string[] dates; //массив дат
        string[] errors; //массив ошибок исключений

        int forDefNumber = 0;
        int i = 0;
        int r = 0;
        int forRep = 0;

        bool showReview = false;
        bool checkTestingMail = false;
        bool repeatingSendingOrNot = false;
        bool timerChecked = false;
        
        public class AdditionalInfo
        {
            string _address;
            string _error;

            public string address 
            {
                set
                {
                    _address = value;
                }
                get 
                { 
                    return _address; 
                }
            }
            public string error 
            {
                set
                {
                    _error = value;
                }
                get
                {
                    return _error;
                }
            }
        }

        AdditionalInfo ad = new AdditionalInfo();
        
        public Main()
        {
            InitializeComponent();
        } //конструктор по умолчанию

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OptionsToolStripMenuItem.Enabled = false;
                TestSendingToolStripMenuItem1.Enabled = false;
                checkTestingMail = false;
                button2.Enabled = true;
                listView1.Enabled = true;
                if (subject.Text == "") MessageBox.Show("Не заполнено поле \"Тема сообщения\"");
                else if (body.Text == "") MessageBox.Show("Не заполнено поле \"Сообщение\"");
                else
                {
                    body.Enabled = false;
                    subject.Enabled = false;
                    button1.Enabled = false;
                    timer1.Start();
                    timer1.Enabled = true;
                    timer1.Interval = interval;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(criticalError + MethodBase.GetCurrentMethod() + "\nИсключение:" + exc.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } //кнопка "Отправить"

        private void sendOfSpamMessage(object address)
        {
            try
            {
                label3.Visible = true;
                mailLbl.Text = Convert.ToString(address);


                FileStream FSbackUpOfSubject = new FileStream("options/backupSubject.dll", FileMode.Create);
                StreamWriter SWbackUpOfSubject = new StreamWriter(FSbackUpOfSubject, Encoding.UTF8);
                SWbackUpOfSubject.Write(subject.Text);
                SWbackUpOfSubject.Close();
                FSbackUpOfSubject.Close();

                FileStream FSbackUpOfMessage = new FileStream("options/backupMessage.dll", FileMode.Create);
                StreamWriter SWbackUpOfMessage = new StreamWriter(FSbackUpOfMessage, Encoding.UTF8);
                SWbackUpOfMessage.Write(body.Text);
                SWbackUpOfMessage.Close();
                FSbackUpOfMessage.Close();

                MailAddress fromAddr = new MailAddress(from, nameOfCompany);
                MailAddress toAddr = new MailAddress(Convert.ToString(address));
                MailMessage mail = new MailMessage(fromAddr, toAddr);
                mail.Subject = subject.Text;
                mail.Body = body.Text;
                SmtpClient client = new SmtpClient(smtp, port);
                
                if (checkSSL == "yes")
                    client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(from, pass);
                mail.IsBodyHtml = true;
                client.Send(mail);

                listView1.Items.Add((columnID + 1).ToString());
                listView1.Items[columnID].SubItems.Add(Convert.ToString(address));
                listView1.Items[columnID].SubItems.Add("√");
                listView1.Items[columnID].SubItems.Add("-");
                listView1.Refresh();
                if (listView1.Items.Count == quantOfAbonents)
                {
                    MessageBox.Show("Поздравляем! Письма отправлены");
                    FileStream fs = new FileStream("options/check.log", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.Write("0\n0");
                    sw.Close(); fs.Close();
                }

            }
            catch (Exception exc)
            {
                if (!checkTestingMail)
                {
                    listView1.Items.Add((columnID + 1).ToString());
                    listView1.Items[columnID].SubItems.Add(Convert.ToString(address));
                    listView1.Items[columnID].SubItems.Add("x");
                    listView1.Items[columnID].SubItems.Add(exc.Message);
                    listView1.Refresh();
                }

                ad.address = Convert.ToString(address);
                ad.error = exc.Message;

                var thread = new Thread(new ParameterizedThreadStart(writingInXlsMethod));
                thread.Start(ad);
                
                if (listView1.Items.Count == quantOfAbonents)
                {
                    MessageBox.Show("Поздравляем! Письма отправлены");
                    FileStream fs12 = new FileStream("options/check.log", FileMode.Create);
                    StreamWriter sw2 = new StreamWriter(fs12, Encoding.UTF8);
                    sw2.Write("0\n0");
                    sw2.Close();
                    fs12.Close();
                }
            }
            if (!checkTestingMail)
                columnID++;
        } //метод отправки сообщения
        
        private void writingInXlsMethod(object _ad)
        {
            try
            {
                _ad = ad;
                string id = "№ п/п";
                string emailColumn = "Электронный ящик";
                string currentDate = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
                string dateColumn = "Дата";
                string status = "Статус";
                string error = "Причина ошибки";
                string[] defaultColumns = { id, emailColumn, dateColumn, status, error };
                int countOfValues = 0;

                if (File.Exists(Path.GetFullPath("log.xlsx")))
                {
                    Excel.Application excel = new Excel.Application();
                    Excel.Workbook wb = excel.Workbooks.Open(Path.GetFullPath("log.xlsx"));
                    Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets.Item[1];
                    double temp;
                    int k = 0;
                    while ((temp = Convert.ToDouble(((ws.Cells[k + 2, 1] as Excel.Range).Value))) != 0)
                    {
                        k++;
                        countOfValues++;
                    }
                    brokenMails = new string[countOfValues];
                    dates = new string[countOfValues];
                    errors = new string[countOfValues];
                    for (int i = 0; i < countOfValues; i++)
                    {
                        brokenMails[i] = (string)(ws.Cells[i + 2, 2] as Excel.Range).Value;
                        DateTime tempDate = Convert.ToDateTime((ws.Cells[i + 2, 3] as Excel.Range).Value);
                        dates[i] = tempDate.ToString("dd/MM/yyyy");
                        errors[i] = (string)(ws.Cells[i + 2, 5] as Excel.Range).Value;
                    }
                    wb.Close();
                    excel.Quit();
                    File.Delete("log.xlsx");
                }

                Excel.Application ObjExcel = new Excel.Application();
                Excel.Workbook ObjWorkBook;
                Excel.Worksheet ObjWorkSheet;
                object start = 0, end;
                ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
                ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Worksheets.Item[1];
                for (int i = 1; i <= defaultColumns.Length; i++)//шапка
                {
                    ObjWorkSheet.Cells[1, i] = defaultColumns[i - 1];
                    Excel.Range rng = (Excel.Range)ObjWorkSheet.Cells[1, i];
                    end = defaultColumns[i - 1].Length + 1;
                    rng.get_Characters(start, end).Font.Size = 14;
                    rng.get_Characters(start, end).Font.Bold = true;
                    ObjWorkSheet.Columns.AutoFit();

                    rng.BorderAround();
                }
                for (int i = 0; i < countOfValues; i++)
                {
                    ObjWorkSheet.Cells[i + 2, 1] = (i + 1).ToString(); //индекс
                    ObjWorkSheet.Cells[i + 2, 2] = brokenMails[i].ToString(); //мылко
                    ObjWorkSheet.Cells[i + 2, 3] = dates[i].ToString(); //дата
                    ObjWorkSheet.Cells[i + 2, 4] = "X".ToString();//статус
                    ObjWorkSheet.Cells[i + 2, 5] = errors[i].ToString(); //ошибка
                    ObjWorkSheet.Columns.AutoFit();
                    for (int j = 0; j < 5; j++) //форматирование
                    {
                        Formatting(ObjWorkSheet.Cells[i + 2, (j + 1)]);
                    }
                }
                int newId = countOfValues + 1;
                ObjWorkSheet.Cells[countOfValues + 2, 1] = newId.ToString();
                ObjWorkSheet.Cells[countOfValues + 2, 2] = ad.address.ToString();
                ObjWorkSheet.Cells[countOfValues + 2, 3] = currentDate.ToString();
                ObjWorkSheet.Cells[countOfValues + 2, 4] = "X";
                ObjWorkSheet.Cells[countOfValues + 2, 5] = ad.error.ToString();
                ObjWorkSheet.Columns.AutoFit();
                for (int j = 0; j < 5; j++) //форматирование
                {
                    Formatting(ObjWorkSheet.Cells[countOfValues + 2, (j + 1)]);
                }
                ObjWorkBook.SaveAs(Path.GetFullPath("log.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                ObjWorkBook.Close();
                ObjExcel.Quit();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при составлении лога. Обратитесь к разработчику ПО\n" + exc.Message);
            }
        } //метод составления лога

        private void Formatting(object obj)
        {
            object end = obj.ToString().Length;
            Excel.Range rng = (Excel.Range)obj;
            rng.get_Characters(0, end).Font.Italic = true;
            rng.BorderAround();
        } //форматирование в логе

        private void Form1_Load(object sender, EventArgs e)
        {
            CorrectTextBoxes(this);
            mailLbl.Text = "";
            label3.Visible = false;
            button2.Enabled = false;
            this.Size = new System.Drawing.Size(365, 380);
            t.SetToolTip(button1, "Начать рассылку");
            t.SetToolTip(button2, "Показать отчет");
            listView1.Enabled = false;
            string temp1;
            StreamReader sr = new StreamReader("database.txt", Encoding.UTF8);
            while ((temp1 = sr.ReadLine()) != null)
            {
                quantOfAbonents++;
            }
            sr.Close();
            StreamReader sr2 = new StreamReader("options/account.dll", Encoding.UTF8);
            while ((temp1 = sr2.ReadLine()) != null)
            {
                from = temp1;
                nameOfCompany = sr2.ReadLine();
                pass = sr2.ReadLine();
                smtp = sr2.ReadLine();
                port = Convert.ToInt32(sr2.ReadLine());
                checkSSL = sr2.ReadLine();
                interval = Convert.ToInt32(sr2.ReadLine()) * 1000;
                quantity = sr2.ReadLine();
                testMail = sr2.ReadLine();
            }
            int n;
            if (int.TryParse(quantity, out n))
            {
                if (Convert.ToInt32(quantity) <= quantOfAbonents)
                    quantOfAbonents = Convert.ToInt32(quantity);
            }
            sr2.Close();
            mails = new string[quantOfAbonents];
            StreamReader sr1 = new StreamReader("database.txt", Encoding.UTF8);
            for (int i = 0; i < quantOfAbonents; i++)
            {
                mails[i] = sr1.ReadLine();
            }
            sr1.Close();
            forDefNumber = quantOfAbonents + 1;
            StreamReader log = new StreamReader("options/check.log", Encoding.UTF8);
            logPlan = Convert.ToInt32(log.ReadLine());
            logDone = Convert.ToInt32(log.ReadLine());
            log.Close();
            DialogResult result = new DialogResult();
            if ((logPlan - logDone) > 0)
                result = MessageBox.Show("Последняя отправка писем не завершена!\nПрограмма аварийно завершила свою работу.\nПродолжить отправку?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                button2.Enabled = true;
                button1.Enabled = false;
                listView1.Enabled = true;
                StreamReader readBackUpSubject = new StreamReader("options/backupSubject.dll", Encoding.UTF8);
                subject.Text = readBackUpSubject.ReadToEnd();
                readBackUpSubject.Close();
                subject.Enabled = false;
                StreamReader readBackUpBody = new StreamReader("options/backupMessage.dll", Encoding.UTF8);
                body.Text = readBackUpBody.ReadToEnd();
                readBackUpBody.Close();
                body.Enabled = false;
                repeatingSendingOrNot = true;
                int buf = logPlan - logDone;
                var thread = new Thread(new ParameterizedThreadStart(sendOfSpamMessage));
                StreamReader cont = new StreamReader("database.txt", Encoding.UTF8);
                continueArray = new string[buf];
                for (int i = 0; i < logDone; i++)
                {
                    cont.ReadLine();
                }
                for (int j = 0; j < continueArray.Length; j++)
                    continueArray[j] = cont.ReadLine();
                cont.Close();
                timer1.Start();
                timer1.Enabled = true;
                timer1.Interval = interval;
            }
            if (result == DialogResult.No)
            {//перечистить все нахрен
                FileStream fs = new FileStream("options/check.log", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write("0\n0");
                sw.Close(); fs.Close();

                FileStream fs1 = new FileStream("options/backupMessage.dll", FileMode.Create);
                StreamWriter sw1 = new StreamWriter(fs1, Encoding.UTF8);
                sw1.Write("");
                sw1.Close(); fs1.Close();

                FileStream fs2 = new FileStream("options/backupSubject.dll", FileMode.Create);
                StreamWriter sw2 = new StreamWriter(fs2, Encoding.UTF8);
                sw2.Write("");
                sw2.Close(); fs2.Close();
            }
        } //загрузка формы

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!repeatingSendingOrNot)
            {
                forDefNumber--;

                var thread = new Thread(new ParameterizedThreadStart(sendOfSpamMessage));
                if (forDefNumber > 0)
                {
                    i = quantOfAbonents - forDefNumber;
                    thread.Start(mails[i]);
                    r++;
                    FileStream fs = new FileStream("options/check.log", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.Write(quantOfAbonents + "\n");
                    sw.Write(r.ToString());
                    sw.Close(); fs.Close();
                }
                else
                {
                    timerChecked = true;
                }
                checkedTimer();
            }
            else
            {
                checkTestingMail = false;
                button2.Enabled = true;
                listView1.Enabled = true;
                var thread = new Thread(new ParameterizedThreadStart(sendOfSpamMessage));
                thread.Start(continueArray[forRep]);
                forRep++;
                int tempQuant = 0;
                int tempR = 0;
                StreamReader sr = new StreamReader("options/check.log", Encoding.UTF8);
                tempQuant = Convert.ToInt32(sr.ReadLine());
                tempR = Convert.ToInt32(sr.ReadLine());
                sr.Close();
                FileStream fs = new FileStream("options/check.log", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(tempQuant + "\n");
                tempR++;
                sw.Write(tempR.ToString());
                sw.Close(); fs.Close();
                if (listView1.Items.Count + 1 == continueArray.Length)
                {
                    FileStream fs1 = new FileStream("options/check.log", FileMode.Create);
                    MessageBox.Show("Все письма доотправлены");
                    StreamWriter sw1 = new StreamWriter(fs1, Encoding.UTF8);
                    sw1.Write("0\n0");
                    sw1.Close(); fs1.Close();
                }
                if (forRep == continueArray.Length)
                    timer1.Stop();
            }

        } //таймер

        private void checkedTimer()
        {
            if (timerChecked)
            {
                timer1.Stop();
            }
        } //остановка таймера

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsYourMail opt = new OptionsYourMail();
            opt.ShowDialog();
        } //меню стрип - опции

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        } //меню-стрип - о программе

        private void button2_Click(object sender, EventArgs e)
        {
            if (!showReview)
            {
                this.Size = new System.Drawing.Size(965, 380);
                button2.Text = "<";
                showReview = true;
                t.SetToolTip(button2, "Спрятать отчет");
            }
            else if (showReview)
            {
                this.Size = new System.Drawing.Size(365, 380);
                button2.Text = ">";
                showReview = false;
                t.SetToolTip(button2, "Показать отчет");
            }
        } //кнопка показа отчетов

        private void CorrectTextBoxes(Control Base)
        {
            if (Base.Controls != null)
            {
                foreach (Control Ctrl in Base.Controls)
                {
                    CorrectTextBoxes(Ctrl);
                    TextBox Txt = Ctrl as TextBox;
                    if (Txt != null)
                    {
                        Txt.KeyDown += Txt_KeyDown;
                    }
                }
            }
        } //методы КТРЛ+А

        private void Txt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A & e.Control)
                ((TextBox)sender).SelectAll();
        } //методы КТРЛ+А

        private void TestSendingToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (subject.Text == "" || body.Text == "")
                MessageBox.Show("Не заполнено поле Тема и/или Текст сообщения", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                checkTestingMail = true;
                var thread = new Thread(new ParameterizedThreadStart(sendOfTestMessage));
                thread.Start(testMail);
                MessageBox.Show("Отправлено в обработку: отправка на " + testMail + "!");
            }
        }     //меню-стрип - тестовая отправка

        private void sendOfTestMessage(object address)
        {
            try
            {
                MailAddress fromAddr = new MailAddress(from, nameOfCompany);
                MailAddress toAddr = new MailAddress(Convert.ToString(address));
                MailMessage mail = new MailMessage(fromAddr, toAddr);
                mail.Subject = subject.Text;
                mail.Body = body.Text;
                SmtpClient client = new SmtpClient(smtp, port);
                if (checkSSL == "yes")
                    client.EnableSsl = true;
                // client.Credentials = new System.Net.NetworkCredential(from, pass);
                System.Net.NetworkCredential nw = new System.Net.NetworkCredential();
                nw.UserName = from;
                nw.Password = pass;
                client.Credentials = nw;
                mail.IsBodyHtml = true;
                System.Net.Configuration.SmtpNetworkElement s = new System.Net.Configuration.SmtpNetworkElement();
               // s.ClientDomain = "name";

                client.Send(mail);
               // MessageBox.Show(s.ClientDomain);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        } //тестовая отправка сообщения на тестовый ящик
    }
}