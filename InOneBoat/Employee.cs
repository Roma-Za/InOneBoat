using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOneBoat
{
    class Employee
    {
        public int ID { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string UserType { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }
        public string Patronymic { set; get; }
        public string Phone_number { set; get; }
        public string Email { set; get; }
        public string Role { set; get; }
        public bool Send { set; get; }

        public string ConnString { set; get; }

        public Employee(string connect, int Id)
        {
            ConnString = connect;

            string commandText = "SELECT * FROM Employee WHERE id = @ID";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@ID", Id);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
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
                        Login = rdr.GetString(1);
                    }
                    catch (Exception)
                    {

                        Login = "";
                    }
                    try
                    {
                        Password = rdr.GetString(2);
                    }
                    catch (Exception)
                    {

                        Password = "";
                    }
                    try
                    {
                        UserType = rdr.GetString(3);
                    }
                    catch (Exception)
                    {
                        UserType = "";

                    }
                    try
                    {
                        Surname = rdr.GetString(4);
                    }
                    catch (Exception)
                    {
                        Surname = "";

                    }
                    try
                    {
                        Name = rdr.GetString(5);
                    }
                    catch (Exception)
                    {
                        Name = "";

                    }
                    try
                    {
                        Patronymic = rdr.GetString(6);
                    }
                    catch (Exception)
                    {

                        Patronymic = "";
                    }
                    try
                    {
                        Phone_number = rdr.GetString(7);
                    }
                    catch (Exception)
                    {
                        Phone_number = "";

                    }
                    try
                    {
                        Email = rdr.GetString(8);
                    }
                    catch (Exception)
                    {
                        Email = "";

                    }
                    try
                    {
                        Role = rdr.GetString(9);
                    }
                    catch (Exception)
                    {
                        Role = "";

                    }
                    try
                    {
                        Send = (bool)rdr.GetSqlBoolean(10);
                        
                    }
                    catch (Exception)
                    {
                        Send = false;

                    }
                    #endregion
                }
            }
        }
        public string GetSNP()
        {
            return String.Format("{0} {1} {2}", Surname, Name, Patronymic);
        }
        public void SetAll(string login, string pass, string type, string sur, string name, string pat, string phone, string em, string role, bool send) 
        {
            if (login != Login) 
            {
                string comm = "UPDATE Employee SET user_name = @log WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@log", login);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (pass != Password)
            {
                string comm = "UPDATE Employee SET password = @pass WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (type != UserType)
            {
                string comm = "UPDATE Employee SET type = @type WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (sur != Surname)
            {
                string comm = "UPDATE Employee SET surname = @sur WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@sur", sur);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (name != Name)
            {
                string comm = "UPDATE Employee SET name = @name WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (pat != Patronymic)
            {
                string comm = "UPDATE Employee SET patronymic = @pat WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@pat", pat);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (phone != Phone_number)
            {
                string comm = "UPDATE Employee SET phone_number = @phone WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (em != Email)
            {
                string comm = "UPDATE Employee SET email = @em WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@em", em);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (role != Role)
            {
                string comm = "UPDATE Employee SET role = @role WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (send != Send)
            {

                string comm = "UPDATE Employee SET send_to_email = @send WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@send", send ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
