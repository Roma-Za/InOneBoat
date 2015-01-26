using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace InOneBoat
{
    public partial class Form_authorization : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        private string ValidMessage = "";
        private string login = "";
        private string password = "";
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
            
            string commandText = "SELECT user_types.name FROM users INNER JOIN user_types ON users.user_type_id = user_types.id WHERE (((users.user_name) = @login) AND ((users.password) = @pass))";
            login = textBoxLog.Text;
            password = maskedTextBoxPass.Text;
            if (ValidOk(login) && ValidOk(password))
            {
                string type = "";
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandText;

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pass", password);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        try
                        {
                            type = rdr.GetString(0);
                        }
                        catch (Exception)
                        {
                            type = "";
                        }

                    }

                    if (type != "")
                    {
                        switch (type)
                        {
                            case "admin":
                                {
                                    AdminForm adm = new AdminForm(login, password);
                                    this.Visible = false;
                                    adm.ShowDialog();
                                    clearAuthorization();
                                    this.Visible = true;
                                } break;
                            case "customer":
                                {
                                    CustomerForm cust = new CustomerForm(login, password);
                                    this.Visible = false;
                                    cust.ShowDialog();
                                    clearAuthorization();
                                    this.Visible = true;
                                } break;
                            case "employee":
                                {
                                    EmployeeForm empl = new EmployeeForm(login, password);
                                    this.Visible = false;
                                    empl.ShowDialog();
                                    clearAuthorization();
                                    this.Visible = true;
                                } break;
                            default:
                                break;
                        }
                    }
                }
            }
            else {
                MessageBox.Show(ValidMessage);
            }
        }

        private bool ValidOk(string str)
        {
            Validator val = new Validator(str);
            val.addValidator(new InOneBoat.Validators.IsFilled());
            if (!val.check()) {
                ValidMessage += val.getCause(); 
                return false;
            }
            return true;
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
            textBoxLog.Text = "";
            maskedTextBoxPass.Text = "";
            ValidMessage = "";
        }
    }
}
