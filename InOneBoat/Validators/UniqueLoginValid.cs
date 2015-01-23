using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class UniqueLoginValid : IValidate
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        private string text = "";
        public bool check(string str)
        {
            text = "";
                string commandText = "SELECT id FROM users WHERE user_name = @login";
            List<int> arrIdEmpol = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@login", str);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int idEmp = 0;

                    try
                    {
                        idEmp = rdr.GetInt32(0); 
                        arrIdEmpol.Add(idEmp);
                    }
                    catch (Exception)
                    {
                        idEmp = 0;
                    }
                }

            }
            if (arrIdEmpol.Count==0)
            {
                return true;
            }
            else
            {
                text = "такой логин уже существует";
                return false;
            }
        }

        public string getCause()
        {
            return text;
        }
    }
}
