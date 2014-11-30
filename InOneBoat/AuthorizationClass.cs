using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOneBoat
{
    internal class AuthorizationClass
    {
        public string Login { set; get; }
        public string Password { set; get; }
        public string UserType { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }
        public string Patronymic { set; get; }
        public string Phone_number { set; get; }
        public string Email { set; get; }

        public AuthorizationClass(string connect, string login, string password)
        {
            string commandText = "SELECT * FROM person WHERE user_name = @login AND password= @pass";

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
                    #region присвоение
                    try
                    {
                        Login = rdr.GetString(0);
                    }
                    catch (Exception)
                    {

                        Login = "";
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
                    try
                    {
                        Surname = rdr.GetString(3);
                    }
                    catch (Exception)
                    {
                        Surname = "";

                    }
                    try
                    {
                        Name = rdr.GetString(4);
                    }
                    catch (Exception)
                    {
                        Name = "";

                    }
                    try
                    {
                        Patronymic = rdr.GetString(5);
                    }
                    catch (Exception)
                    {

                        Patronymic = "";
                    }
                    try
                    {
                        Phone_number = rdr.GetString(6);
                    }
                    catch (Exception)
                    {
                        Phone_number = "";

                    }
                    try
                    {
                        Email = rdr.GetString(7);
                    }
                    catch (Exception)
                    {
                        Email = "";

                    }
                    #endregion
                }
            }
        }
    }
}
