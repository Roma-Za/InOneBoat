using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class Recover : Form
    {
        public string Password { set; get; }
        public Recover()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            getPass();
            if (Password != "")
            {

                string email = textBoxEmail.Text;
                //послать пароль на почту

            }
            else {
                textBoxEmail.Text = "";
                textBoxLogin.Text = "";
                MessageBox.Show("Неверный логин или email.");
            }
        }

        private void getPass()
        {
            string commandText = "SELECT password FROM person WHERE user_name = @login AND email= @email";
            string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);
                cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);

                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                try
                {
                    Password = rdr["password"].ToString();
                }
                catch (Exception)
                {

                    Password = "";
                }
                finally
                {
                    sql_connect.Close();
                }
            }
        }
    }
}
