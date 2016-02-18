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
            for (int i = 0; i < 5; i++)
                listViewJournal.Items.Add(i.ToString());
        }
    }
}
