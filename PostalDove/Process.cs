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
    public interface IProcess
    {
        ListView listView { get; }
        void AddToList(ListView listView, bool isSuccess, string exceptionMessage);
    }

    public partial class Process : Form, IProcess
    {
        List<string> _ListOfDestination = new List<string>();

        public ListView listView
        {
            get { return listJournal; }
        }

        public Process(List<string> destination)
        {
            this._ListOfDestination = destination;
            InitializeComponent();
        }

        public void AddToList(ListView listView, bool isSuccess, string exceptionMessage)
        {
            try
            {
                /*int currentNum = listView.Items.Count + 1;
                listView.Items.Add(currentNum.ToString());
                listView.Items[currentNum].SubItems.Add(_ListOfDestination[currentNum]);
                if (isSuccess) listView.Items[currentNum].SubItems.Add("✓");
                else listView.Items[currentNum].SubItems.Add("x");
                if (!isSuccess) listView.Items[currentNum].SubItems.Add(exceptionMessage);
                listView.Refresh();*/  //проблема в потоках отправки, поэтому виснет форма
            }
            catch
            {
                throw new OwnExceptions();
            }
        }
    }
}
