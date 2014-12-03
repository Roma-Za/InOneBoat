using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace InOneBoat
{
    public partial class Form_authorization : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        internal AuthorizationClass Auth { set; get; }
        public Form_authorization()
        {
            InitializeComponent();           
        }

        private void buttonOkAuthor_Click(object sender, EventArgs e)
        {
            checkAut();
        }

        private void checkAut()
        {
            Auth = new AuthorizationClass(connect, textBoxLog.Text, maskedTextBoxPass.Text);
            if (Auth.Login != null)
            {
                MessageBox.Show(String.Format("Здравствуйте {0} {1}.", Auth.Name, Auth.Patronymic));
                switch (Auth.UserType)
                {
                    case "admin":
                        {
                            AdminForm adm = new AdminForm();
                            this.Visible = false;
                            adm.ShowDialog();
                            clearAuthorization();
                            this.Visible = true;
                        } break;
                    default:
                        break;
                }
            }
        }

        private void linkLabelRecover_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Recover rec = new Recover();
            this.Visible = false;
            rec.ShowDialog();
                clearAuthorization();
                this.Visible = true;
        }

        private void textBoxLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkAut();
            }
            
        }

        private void maskedTextBoxPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkAut();
            }
        }

        private void clearAuthorization()
        {
            Auth = null;
            textBoxLog.Text = "";
            maskedTextBoxPass.Text = "";
        }
    }
}
