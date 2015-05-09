using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class Recover : Form
    {
        public int ID { set; private get; }
        public string Password { set; private get; }
        public string UserType { set; private get; }
        public string Email { set; private get; }
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        private MainEmail memail;
        public Recover()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            getPass();
            sendMail();
        }

        private void sendMail()
        {
            if (Password != "" && Email.Equals(textBoxEmail.Text))
            {
                string mText = "Ваш логин " + textBoxLogin.Text + " пароль " + Password;
                memail = new MainEmail(connect);
                memail.SendMail(new string[] { Email }, "Забытый пароль", mText);
                MessageBox.Show("Пароль отправлен на Ваш email.");
            }
            else
            {
                textBoxEmail.Text = "";
                textBoxLogin.Text = "";
                MessageBox.Show("Неверный логин или email.");
            }
        }

        private void getPass()
        {
            getUserType();

            if (UserType.Equals("customer"))
            {
                getEmailCustomer();
            }
            else
            {
                getEmailEmpl();
            }
        }

        private void getEmailCustomer()
        {
            string commandText = "SELECT email FROM customer WHERE user_name = @login";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);

                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                try
                {
                    Email = rdr.GetString(0);
                }
                catch (Exception)
                {

                    Email = "";
                }
            }
        }

        private void getEmailEmpl()
        {
            string commandText = "SELECT email FROM Employee WHERE user_name = @login";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);

                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                try
                {
                    Email = rdr.GetString(0);
                }
                catch (Exception)
                {

                    Email = "";
                }
            }
        }

        private void getUserType()
        {

            string commandText = "SELECT users.id, users.password, user_types.name FROM users INNER JOIN user_types ON users.user_type_id = user_types.id WHERE users.user_name = @login";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    try
                    {
                        ID = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {

                        ID = 0;
                    }

                    try
                    {
                        Password = rdr.GetString(1);
                    }
                    catch (Exception)
                    {

                        Password = "";
                    }

                    try
                    {
                        UserType = rdr.GetString(2);
                    }
                    catch (Exception)
                    {

                        UserType = "";
                    }
                }
            }
        }

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getPass();
                sendMail();
            }
        }

        private void textBoxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getPass();
                sendMail();
            }
        }

    }
}
