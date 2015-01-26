using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class EmployeeForm : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        private Employee I_am_Emp;
        public EmployeeForm(string login, string pass)
        {
            InitializeComponent();
            I_am_Emp = new Employee(connect, login, pass);
            this.Text = String.Format("{0} {1}, {2}", I_am_Emp.Name, I_am_Emp.Surname, I_am_Emp.Role);
        }
    }
}
