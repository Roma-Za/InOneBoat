using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InOneBoat
{
    public class Task
    {
        public int ID { set; get; }
        public int ID_parent_task { set; get; }
        public string Description { set; get; }
        public float Estimate { set; get; }
        public string Status { set; get; }
        public int Proj_id { set; get; }
        public string Priority { set; get; }
        public string Summary { set; get; }

        public string ConnString { set; get; }
        private string[] priorities = { "high", "normal", "low" };
        private string[] prioritiesRu = { "высокий", "нормальный", "низкий" };
        private string[] statusesRu = { "не стартовавшая", "в процессе", "выполнена", "переоткрыта", "закрыта" };
        private string[] statuses = { "not started", "in progress", "completed", "reopened", "closed" };
        private List<int> participants;
        private List<Comment> commentsList = new List<Comment>();
        private List<int> watchersList = new List<int>();

        public Task(string connect)
        {
            ConnString = connect;
        }


        public Task(string connect, int id, int id_parent_task, string desc, float estimate, string status, int proj_id, string priority, string summary)
        {
            ConnString = connect;
            ID = id;
            ID_parent_task = id_parent_task;
            Description = desc;
            Estimate = estimate;
            Status = status;
            Proj_id = proj_id;
            Priority = priority;
            Summary = summary;

            string commandText = "SELECT employee_id FROM participants WHERE task_id = @id";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@id", ID);
                SqlDataReader rdr = cmd.ExecuteReader();
                List<int> listPart = new List<int>();
                int _id = 0;
                while (rdr.Read())
                {

                    try
                    {
                        _id = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {

                        _id = 0;
                    }
                    if (_id != 0) listPart.Add(_id);

                }
                participants = listPart;
            }
            loadWatchers();
        }

        public void SetAll(int id_parent_task, string desc, float estimate, string status, int proj_id, string priority, string summary, List<int> partic)
        {
            ID_parent_task = id_parent_task;
            Description = desc;
            Estimate = estimate;
            Status = status;
            Proj_id = proj_id;
            Priority = priority;
            Summary = summary;
            participants = partic;

            if (ID_parent_task != 0)
            {
                newTaskChild();
            }
            else
            {
                newTask();
            }

            addParticipants();
        }

        private void addParticipants()
        {
            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                string sql = string.Format("Insert Into participants (task_id, employee_id) Values ( @ID, @employee_id)");
                for (int i = 0; i < participants.Count; i++)
                {
                    using (SqlCommand cmd = new SqlCommand(sql, sql_connect))
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@ID";
                        param.Value = ID;
                        param.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param);
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@employee_id";
                        param2.Value = participants[i];
                        param2.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param2);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void newTask()
        {
            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                #region вызов хп AddTask
                using (SqlCommand cmd = new SqlCommand("AddTask", sql_connect))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Выходной параметр.
                    SqlParameter param0 = new SqlParameter();
                    param0 = new SqlParameter();
                    param0.ParameterName = "@id";
                    param0.SqlDbType = SqlDbType.Int;
                    param0.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param0);

                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = "@description";
                    param2.SqlDbType = SqlDbType.Text;
                    param2.Value = Description;
                    param2.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param2);

                    SqlParameter param3 = new SqlParameter();
                    param3.ParameterName = "@estimate";
                    param3.SqlDbType = SqlDbType.Float;
                    param3.Value = Estimate;
                    param3.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param3);

                    SqlParameter param4 = new SqlParameter();
                    param4.ParameterName = "@status_id";
                    param4.SqlDbType = SqlDbType.Int;
                    for (int i = 0; i < statuses.Length; i++)
                    {
                        if (statuses[i] == Status)
                        {
                            param4.Value = i + 1;
                        }
                    }

                    param4.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param4);

                    SqlParameter param5 = new SqlParameter();
                    param5.ParameterName = "@project_id";
                    param5.SqlDbType = SqlDbType.Int;
                    param5.Value = Proj_id;
                    param5.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param5);

                    SqlParameter param6 = new SqlParameter();
                    param6.ParameterName = "@priority_id";
                    param6.SqlDbType = SqlDbType.Int;
                    for (int i = 0; i < priorities.Length; i++)
                    {
                        if (priorities[i] == Priority)
                        {
                            param6.Value = i + 1;
                        }
                    }

                    param6.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param6);

                    SqlParameter param7 = new SqlParameter();
                    param7.ParameterName = "@summary";
                    param7.SqlDbType = SqlDbType.Text;
                    param7.Value = Summary;
                    param7.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param7);

                    // Выполнение хранимой процедуры.
                    cmd.ExecuteNonQuery();
                    ID = (int)cmd.Parameters["@id"].Value;
                }
                #endregion

            }
        }

        private void newTaskChild()
        {
            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                #region вызов хп AddTaskChild
                using (SqlCommand cmd = new SqlCommand("AddTaskChild", sql_connect))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Выходной параметр.
                    SqlParameter param0 = new SqlParameter();
                    param0 = new SqlParameter();
                    param0.ParameterName = "@id";
                    param0.SqlDbType = SqlDbType.Int;
                    param0.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param0);


                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@parent_task_id";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = ID_parent_task;
                    param1.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param1);

                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = "@description";
                    param2.SqlDbType = SqlDbType.Text;
                    param2.Value = Description;
                    param2.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param2);

                    SqlParameter param3 = new SqlParameter();
                    param3.ParameterName = "@estimate";
                    param3.SqlDbType = SqlDbType.Float;
                    param3.Value = Estimate;
                    param3.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param3);

                    SqlParameter param4 = new SqlParameter();
                    param4.ParameterName = "@status_id";
                    param4.SqlDbType = SqlDbType.Int;
                    for (int i = 0; i < statuses.Length; i++)
                    {
                        if (statuses[i] == Status)
                        {
                            param4.Value = i + 1;
                        }
                    }

                    param4.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param4);

                    SqlParameter param5 = new SqlParameter();
                    param5.ParameterName = "@project_id";
                    param5.SqlDbType = SqlDbType.Int;
                    param5.Value = Proj_id;
                    param5.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param5);

                    SqlParameter param6 = new SqlParameter();
                    param6.ParameterName = "@priority_id";
                    param6.SqlDbType = SqlDbType.Int;
                    for (int i = 0; i < priorities.Length; i++)
                    {
                        if (priorities[i] == Priority)
                        {
                            param6.Value = i + 1;
                        }
                    }

                    param6.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param6);

                    SqlParameter param7 = new SqlParameter();
                    param7.ParameterName = "@summary";
                    param7.SqlDbType = SqlDbType.Text;
                    param7.Value = Summary;
                    param7.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param7);

                    // Выполнение хранимой процедуры.
                    cmd.ExecuteNonQuery();
                    ID = (int)cmd.Parameters["@id"].Value;
                }
                #endregion

            }
        }

        public bool isInTask(int idP)
        {
            return participants.Contains(idP);
        }

        public string getPrioritiRu()
        {
            for (int i = 0; i < priorities.Length; i++)
            {
                if (priorities[i] == Priority)
                {
                    return prioritiesRu[i];
                }
            }
            return "нет";
        }

        public string getPrioritiEn(string prRu)
        {
            for (int i = 0; i < prioritiesRu.Length; i++)
            {
                if (prioritiesRu[i] == prRu)
                {
                    return priorities[i];
                }
            }
            return "нет";
        }

        public string getStatusRu()
        {
            for (int i = 0; i < statuses.Length; i++)
            {
                if (statuses[i] == Status)
                {
                    return statusesRu[i];
                }
            }
            return "нет";
        }

        public string getStatusEn(string statRu)
        {
            for (int i = 0; i < statusesRu.Length; i++)
            {
                if (statusesRu[i] == statRu)
                {
                    return statuses[i];
                }
            }
            return "нет";
        }

        public List<string> getComments()
        {
            string commandText = "SELECT * FROM comments WHERE task_id = @id";

            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@id", ID);
                SqlDataReader rdr = cmd.ExecuteReader();
                List<string> ls = new List<string>();
                int _id = 0;
                string comm;
                int emp_id = 0;
                while (rdr.Read())
                {
                    try
                    {
                        _id = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {

                        _id = 0;
                    }
                    try
                    {
                        comm = rdr.GetString(1);
                        ls.Add(comm);
                    }
                    catch (Exception)
                    {

                        comm = "";
                    }
                    try
                    {
                        emp_id = rdr.GetInt32(3);
                    }
                    catch (Exception)
                    {

                        emp_id = 0;
                    }
                    commentsList.Add(new Comment(ConnString, _id, comm, ID, emp_id));

                }
                return ls;
            }
        }

        public void loadWatchers()
        {
            string commandText = "SELECT employee_id FROM watchers WHERE task_id = @id";

            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@id", ID);
                SqlDataReader rdr = cmd.ExecuteReader();
                List<int> listPart = new List<int>();
                int _id = 0;
                while (rdr.Read())
                {

                    try
                    {
                        _id = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {

                        _id = 0;
                    }
                    if (_id != 0) watchersList.Add(_id);

                }
            }
        }

        public bool isInWatch(int id)
        {
            return watchersList.Contains(id);
        }

        public void addWatcher(int id)
        {
            if (!watchersList.Contains(id))
            {
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    string sql = string.Format("Insert Into watchers (employee_id, task_id) Values (@employee_id,  @ID)");

                    using (SqlCommand cmd = new SqlCommand(sql, sql_connect))
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@employee_id";
                        param.Value = id;
                        param.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param);
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@ID";
                        param2.Value = ID;
                        param2.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param2);
                        cmd.ExecuteNonQuery();
                    }

                }
                watchersList.Add(id);
            }
        }

        public void delWatcher(int id)
        {
            if (watchersList.Contains(id))
            {
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    string sql = string.Format("DELETE FROM watchers WHERE employee_id = @employee_id AND task_id = @ID");

                    using (SqlCommand cmd = new SqlCommand(sql, sql_connect))
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@employee_id";
                        param.Value = id;
                        param.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param);
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@ID";
                        param2.Value = ID;
                        param2.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param2);
                        cmd.ExecuteNonQuery();
                    }

                }
                watchersList.Remove(id);
            }
        }

        public void EditTask(string desc, float estimate, string status, string priority, string summary)
        {
            if (desc != Description)
            {
                Description = desc;
                string comm = "UPDATE tasks SET description = @des WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@des", desc);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }
            if (estimate != Estimate)
            {
                Estimate = estimate;
                string comm = "UPDATE tasks SET estimate = @est WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@est", estimate);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }
            if (status != Status)
            {
                Status = status;
                int stID = 0;
                for (int i = 0; i < statuses.Length; i++)
                {
                    if (statuses[i] == status)
                    {
                        stID = i;
                        stID++;
                    }
                }

                string comm = "UPDATE tasks SET status_id = @sid WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@sid", stID);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }
            if (priority != Priority)
            {
                Priority = priority;
                int prID = 0;
                for (int i = 0; i < priorities.Length; i++)
                {
                    if (priorities[i] == priority)
                    {
                        prID = i;
                        prID++;
                    }
                }

                string comm = "UPDATE tasks SET priority_id = @prID WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@prID", prID);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }
            if (summary != Summary)
            {
                Summary = summary;
                string comm = "UPDATE tasks SET summary = @summ WHERE id = @ID";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@summ", summary);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<int> getWatchersList()
        {
            return watchersList;
        }
    }
}
