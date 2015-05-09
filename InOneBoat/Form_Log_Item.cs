using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class Form_Log_Item : Form
    {
        protected Employee I_am_Emp;
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        private Task task;
        private List<LogItem> logItemList = new List<LogItem>();
        private decimal dec = 0;
        public Form_Log_Item(Task _task, Employee _empl)
        {
            InitializeComponent();
            task = _task;
            I_am_Emp = _empl;
            numericUpDown_start.Minimum = 8;
            numericUpDown_start.Maximum = 20;
            numericUpDown_finish.Minimum = 8;
            numericUpDown_finish.Maximum = 20;
            fillLogItems(dateTimePickerDay.Value);
        }

        private void fillLogItems(DateTime date)
        {
            listBox_tasks_today.Items.Clear();
            logItemList.Clear();

            #region все логиайтемы работника
            string commandText = "SELECT * FROM log_items WHERE employee_id = @empl";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                cmd.Parameters.AddWithValue("@empl", I_am_Emp.ID);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int ID = 0;
                    int tId = 0;
                    DateTime st = DateTime.Now;
                    DateTime fin = DateTime.Now;
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
                        tId = rdr.GetInt32(1);
                    }
                    catch (Exception)
                    {

                        tId = 0;
                    }
                    try
                    {
                        st = rdr.GetDateTime(2);
                    }
                    catch (Exception)
                    {
                        st = DateTime.Now;
                    }
                    try
                    {
                        fin = rdr.GetDateTime(3);
                    }
                    catch (Exception)
                    {
                        fin = DateTime.Now;
                    }
                    logItemList.Add(new LogItem(connect, ID, tId, st, fin, I_am_Emp.ID));
                }

            }
            #endregion

            #region записываем имена тасков
            foreach (var item in logItemList)
            {
                if (item.Start_time.Day == date.Day)
                {

                    string s = "";
                    string commandText2 = "SELECT summary FROM tasks WHERE id = @id";

                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();
                        SqlCommand cmd = sql_connect.CreateCommand();
                        cmd.CommandText = commandText2;
                        cmd.Parameters.AddWithValue("@id", item.Task_id);
                        SqlDataReader rdr = cmd.ExecuteReader();
                        rdr.Read();
                        s += rdr.GetString(0);
                    }
                    if (s != "") listBox_tasks_today.Items.Add(s + " c " + item.Start_time.Hour + "ч. по " + item.End_time.Hour + "ч.");
                }
            }
            #endregion
        }

        private void dateTimePickerDay_ValueChanged(object sender, EventArgs e)
        {
            fillLogItems(dateTimePickerDay.Value);
        }

        private void numericUpDown_start_ValueChanged(object sender, EventArgs e)
        {
            dec = numericUpDown_finish.Value - numericUpDown_start.Value;
            label_all.Text = "Всего: " + dec + " ч.";
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePickerDay.Value;
            DateTime dtSt = new DateTime(dt.Year, dt.Month, dt.Day, (int)numericUpDown_start.Value, 0, 0);
            DateTime dtFin = new DateTime(dt.Year, dt.Month, dt.Day, (int)numericUpDown_finish.Value, 0, 0);
            bool flag = true;
            foreach (var item in logItemList)
            {
                if (item.Start_time.Day == dt.Day)
                {
                    if (((dtSt.Hour >= item.Start_time.Hour && dtSt.Hour < item.End_time.Hour) ||
                        (dtFin.Hour > item.Start_time.Hour && dtFin.Hour <= item.End_time.Hour)))
                    {
                        flag = false;
                    }
                }
            }

            if (flag && dec > 0)
            {
                new LogItem(connect, task.ID, dtSt, dtFin, I_am_Emp.ID);
                if (!(task.getStatusRu() == "в процессе" || task.getStatusRu() == "переоткрыта")) MessageBox.Show("Статус задачи: " + task.getStatusRu() + ", измените статус на более актуальный.");
                this.Close();
            }
            else MessageBox.Show("Время накладывается на другие задачи.");
        }

    }
}
