using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOneBoat
{
    class Comment
    {
        public int ID {set; get;}
        public string CommentText { set; get; }
        public int Task_id { set; get; }
        public int Employee_id { set; get; }
        private string author;
        public string ConnString { set; get; }

        public Comment(string connect, string comment, int task_id, int empl_id, string author) 
        {
            ConnString = connect;
            CommentText = comment;
            Task_id = task_id;
            Employee_id = empl_id;
            if (author != null)
            {
                this.author = author;
                CommentText += " \n\n" + author + " \n" + DateTime.Now + " \n\n\n"; 
            }
            newComm();
        }

        public Comment(string connect, int id, string comment, int task_id, int empl_id)
        {
            ConnString = connect;
            ID = id;
            CommentText = comment;
            Task_id = task_id;
            Employee_id = empl_id;
        }

        private void newComm()
        {
            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                #region вызов хп AddComment
                using (SqlCommand cmd = new SqlCommand("AddComment", sql_connect))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@comment";
                    param.SqlDbType = SqlDbType.Text;
                    param.Value = CommentText;
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@task_id";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = Task_id;
                    param1.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param1);
                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = "@empl_id";
                    param2.SqlDbType = SqlDbType.Int;
                    param2.Value = Employee_id;
                    param2.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param2);

                    // Выполнение хранимой процедуры.
                    cmd.ExecuteNonQuery();
                }
                #endregion
            }
        }

    }
}
