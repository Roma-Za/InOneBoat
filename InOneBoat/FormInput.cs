using System;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class FormInput : Form
    {
        public string ReturnValue1 { get; set; }

        public FormInput()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.ReturnValue1 = richTextBoxComm.Text;
            this.Close();
        }
    }
}
