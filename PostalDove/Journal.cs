using System.Windows.Forms;

namespace PostalDove
{
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();
        }

        public void addToList()
        {
            listViewJournal.Items.Add("вход в ListView");
            listViewJournal.Refresh();
        }
    }
}
