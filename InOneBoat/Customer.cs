﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOneBoat
{
    public class Customer
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
        public string Info { set; get; }
        public string ConnString { set; get; }

        public Customer(string connect, int Id)
        {
            ConnString = connect;

            string commandText = "SELECT * FROM customer WHERE id = @ID";

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
                        Info = rdr.GetString(9);
                    }
                    catch (Exception)
                    {
                        Info = "";

                    }
                    #endregion
                }
            }
        }
        public Customer(string connect, string login, string pass)
        {
            ConnString = connect;

            string commandText = "SELECT * FROM customer WHERE user_name = @login AND password = @pass";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@pass", pass);
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
                        Info = rdr.GetString(9);
                    }
                    catch (Exception)
                    {
                        Info = "";

                    }
                    #endregion
                }
            }
        }
        public string GetSNP()
        {
            return String.Format("{0} {1} {2}", Surname, Name, Patronymic);
        }
        public void SetAll(string login, string pass, string type, string sur, string name, string pat, string phone, string em, string info) 
        {
            if (login != Login) 
            {
                string comm = "UPDATE customer SET user_name = @log WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@log", login);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Login = login;
            }

            if (pass != Password && pass != "")
            {
                string comm = "UPDATE customer SET password = @pass WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Password = pass;
            }

            if (type != UserType)
            {
                string comm = "UPDATE customer SET type = @type WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                UserType = type;
            }

            if (sur != Surname)
            {
                string comm = "UPDATE customer SET surname = @sur WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@sur", sur);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Surname = sur;
            }

            if (name != Name)
            {
                string comm = "UPDATE customer SET name = @name WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Name = name;
            }

            if (pat != Patronymic)
            {
                string comm = "UPDATE customer SET patronymic = @pat WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@pat", pat);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Patronymic = pat;
            }

            if (phone != Phone_number)
            {
                string comm = "UPDATE customer SET phone_number = @phone WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Phone_number = phone;
            }

            if (em != Email)
            {
                string comm = "UPDATE customer SET email = @em WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@em", em);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Email = em;
            }

            if (info != Info)
            {
                string comm = "UPDATE customer SET more_info = @info WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@info", info);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                Info = info;
            }
        }
    }
}
