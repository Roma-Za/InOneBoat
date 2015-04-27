using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class FormPM : Form
    {
        public FormPM(Employee pm)
        {
            InitializeComponent();
            this.Text = String.Format("{0} {1}, {2}", pm.Name, pm.Surname, pm.Role);
        }
    }
}
