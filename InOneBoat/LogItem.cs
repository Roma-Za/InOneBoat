using System;
using System.Data;
using System.Data.SqlClient;

namespace InOneBoat
{
    class LogItem
    {
        public int ID { set; get; }
        public int Task_id { set; get; }
        public DateTime Start_time { set; get; }
        public DateTime End_time { set; get; }
        public int Employee_id { set; get; }
        public string ConnString { set; get; }

        public LogItem(string cs, int IDTask, DateTime st, DateTime end, int emplId) 
        {
            ConnString = cs;
            Task_id = IDTask;
            Start_time = st;
            End_time = end;
            Employee_id = emplId;

            newLogItem();
        }

        public LogItem(string cs, int Id, int IDTask, DateTime st, DateTime end, int emplId)
        {
            ConnString = cs;
            ID = Id;
            Task_id = IDTask;
            Start_time = st;
            End_time = end;
            Employee_id = emplId;
        }

        private void newLogItem()
        {
            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                #region вызов хп AddLogItem
                using (SqlCommand cmd = new SqlCommand("AddLogItem", sql_connect))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Task_id";
                    param.SqlDbType = SqlDbType.Int;
                    param.Value = Task_id;
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@Start_time";
                    param1.SqlDbType = SqlDbType.DateTime;
                    param1.Value = Start_time;
                    param1.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param1);

                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = "@End_time";
                    param2.SqlDbType = SqlDbType.DateTime;
                    param2.Value = End_time;
                    param2.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param2);

                    SqlParameter param3 = new SqlParameter();
                    param3.ParameterName = "@Employee_id";
                    param3.SqlDbType = SqlDbType.Int;
                    param3.Value = Employee_id;
                    param3.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param3);

                    // Выполнение хранимой процедуры.
                    cmd.ExecuteNonQuery();
                }
                #endregion
            }
        }
    }
}
