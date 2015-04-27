using System.Data;
using System.Data.SqlClient;

namespace InOneBoat
{
    class Attachment
    {
        public int ID { set; get; }
        public int Task_id { set; get; }
        public string File_path { set; get; }
      
        public string ConnString { set; get; }

        public Attachment(string cs, int IDTask, string path) 
        {
            ConnString = cs;
            Task_id = IDTask;
            File_path = path;
            newAttachment();
        }

        public Attachment(string cs, int Id, int IDTask, string path)
        {
            ConnString = cs;
            ID = Id;
            Task_id = IDTask;
            File_path = path;
        }

        private void newAttachment()
        {
            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                #region вызов хп AddAttach
                using (SqlCommand cmd = new SqlCommand("AddAttach", sql_connect))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Task_id";
                    param.SqlDbType = SqlDbType.Int;
                    param.Value = Task_id;
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@File_path";
                    param1.SqlDbType = SqlDbType.Text;
                    param1.Value = File_path;
                    param1.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param1);

                    // Выполнение хранимой процедуры.
                    cmd.ExecuteNonQuery();
                }
                #endregion
            }
        }
    }
}
