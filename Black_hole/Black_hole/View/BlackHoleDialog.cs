using System;
using System.Windows.Forms;

namespace Black_hole.View
{
    public partial class BlackHoleDialog : Form
    {
        public int result { get; private set; }
        public BlackHoleDialog( String text, String[] comboboxCollection)
        {
            InitializeComponent();
            lblText.Text = text;
            cmBox.Items.AddRange(comboboxCollection);
            result = -1;
        }

        private void BlackHoleDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(cmBox.SelectedItem == null)
            {
                MessageBox.Show("Nem választottál méretet!", "Figyelem");
                return;
            }
            result = Convert.ToInt32((cmBox.SelectedItem as string).Split("x")[0]);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
