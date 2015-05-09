using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class CustomerForm : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        protected Customer I_am_сust;
        private Dictionary<String, int> DicProj;
        private Dictionary<String, int> DicEmp;
        private List<Task> tasksList;
        private List<LogItem> logItemList;
        private int currentEmplId;
        private string[] statusesRuAnd = { "не стартовавшая", "в процессе", "выполнена", "переоткрыта", "закрыта" };

        public CustomerForm(string login, string pass)
        {
            InitializeComponent();
            I_am_сust = new Customer(connect, login, pass);
            this.Text = String.Format("{0} {1}, Customer", I_am_сust.Name, I_am_сust.Surname);
            statStart();
        }

        private static void ClearAll(Control.ControlCollection c)
        {
            foreach (var item in c)
            {
                if (item is TextBox) ((TextBox)item).Text = "";
                if (item is MaskedTextBox) ((MaskedTextBox)item).Text = "";
                try
                {
                    if (item is ComboBox) ((ComboBox)item).Items.Clear();
                }
                catch { }
                if (item is CheckBox) ((CheckBox)item).Checked = false;
                if (item is RichTextBox) ((RichTextBox)item).Text = "";
                if (item is CheckedListBox) ((CheckedListBox)item).Items.Clear();
                if (item is ListBox) ((ListBox)item).Items.Clear();
            }
        }

        private void HideAllPanel()
        {
            Control.ControlCollection contr = this.Controls;
            foreach (var item in contr)
            {
                if (item is Panel) ((Panel)item).Visible = false;
            }
        }

        #region Изменить личные данные
        private void личныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel_Edit_Cust.Controls;
            ClearAll(c);
            HideAllPanel();
            panel_Edit_Cust.Visible = true;
            panel_Edit_Cust.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;

            textBox_log.Text = I_am_сust.Login;
            textBox_type.Text = I_am_сust.UserType;
            textBox_Sur.Text = I_am_сust.Surname;
            textBox_name.Text = I_am_сust.Name;
            textBox_Pat.Text = I_am_сust.Patronymic;
            textBox_Phone.Text = I_am_сust.Phone_number;
            textBox_Email.Text = I_am_сust.Email;
            textBoxInfo.Text = I_am_сust.Info;

        }

        private void buttonOkP8_Click(object sender, EventArgs e)
        {
            string resultValid = "";

            #region валидация
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panel_Edit_Cust.Controls)
            {
                if (item is TextBox)
                {
                    val.setText(((TextBox)item).Text);
                    if (!val.check())
                    {
                        resultValid += val.getCause() + "\n";
                        break;
                    }
                }
            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.NameValid());
                val.setText(textBox_Sur.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

                val.setText(textBox_name.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

                val.setText(textBox_Pat.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";
            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.PhoneValid());
                val.setText(textBox_Phone.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.EmailValid());
                val.setText(textBox_Email.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }
            if (resultValid == "")
            {
                if (maskedTextBoxPass.Text != "" && maskedTextBoxNewPass.Text != "")
                {
                    if (maskedTextBoxPass.Text.Equals(I_am_сust.Password))
                    {
                        val = new Validator();
                        val.addValidator(new InOneBoat.Validators.PassValid());
                        val.setText(maskedTextBoxNewPass.Text);
                        if (!val.check()) resultValid += val.getCause() + "\n";
                    }
                    else
                    {
                        resultValid += "Старый пароль введён неверно \n";
                    }
                }
                else if (maskedTextBoxPass.Text != "" && maskedTextBoxNewPass.Text == "" || maskedTextBoxPass.Text == "" && maskedTextBoxNewPass.Text != "")
                {
                    resultValid += "Если хотите изменить пароль, введите старый и новый пароль, если нет, оставте поля пустыми \n";
                }

            }

            #endregion
            if (resultValid == "")
            {
                I_am_сust.SetAll(
                textBox_log.Text,
                maskedTextBoxNewPass.Text,
                textBox_type.Text,
                textBox_Sur.Text,
                textBox_name.Text,
                textBox_Pat.Text,
                textBox_Phone.Text,
                textBox_Email.Text,
                textBoxInfo.Text
                    );
            }
            else MessageBox.Show(resultValid);
            if (resultValid == "")
            {
                Control.ControlCollection c = panel_Edit_Cust.Controls;
                ClearAll(c);

                HideAllPanel();
                menuStrip1.Enabled = true;
            }
        }

        private void buttonCancelP8_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel_Edit_Cust.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

        #region Статистика сотрудников
        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statStart();
        }

        private void statStart()
        {
            HideAllPanel();
            Control.ControlCollection c = panel_more_empl.Controls;
            ClearAll(c);
            panel_more_empl.Visible = true;
            panel_more_empl.Dock = DockStyle.Fill;
            DicProj = new Dictionary<String, int>();

            #region вывод всех проектов
            string commandText = "SELECT id, project_name FROM projects WHERE customer_id = @cust";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                cmd.Parameters.AddWithValue("@cust", I_am_сust.ID);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int idProj = 0;
                    string name = "";
                    try
                    {
                        idProj = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        idProj = 0;
                    }

                    try
                    {
                        name = rdr.GetString(1);
                    }
                    catch (Exception)
                    {
                        name = "";
                    }
                    comboBox_Proj.Items.Add(name);
                    DicProj.Add(name, idProj);
                }
            }
            #endregion

            foreach (var s in statusesRuAnd)
            {
                checkedListBox_task_select.Items.Add(s, false);
            }
        }

        private void comboBox_Proj_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox_Empl.Items.Clear();

            #region все работники которые уже добавленны в выбранный проект
            string commandText = "SELECT employee_id FROM projects_employees WHERE project_id = @proj";
            List<int> arrIdEmpol = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                int value = 0;

                if (!DicProj.TryGetValue(comboBox_Proj.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
                cmd.Parameters.AddWithValue("@proj", value);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    int idEmp = 0;

                    try
                    {
                        idEmp = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        idEmp = 0;
                    }

                    arrIdEmpol.Add(idEmp);
                }

            }

            DicEmp = new Dictionary<string, int>();
            string commandT = "SELECT * FROM View_enployee";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandT;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int Id = 0;
                    String strEmp = "";

                    try
                    {
                        Id = rdr.GetInt32(0);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ошибка конвертации " + Id);
                    }
                    try
                    {
                        strEmp += rdr.GetString(1);
                        strEmp += " ";
                    }
                    catch (Exception)
                    {

                        strEmp += " ";
                    }
                    try
                    {
                        strEmp += rdr.GetString(2);
                        strEmp += " ";
                    }
                    catch (Exception)
                    {
                        strEmp += " ";

                    }
                    try
                    {
                        strEmp += rdr.GetString(3);
                    }
                    catch (Exception)
                    {

                    }

                    DicEmp.Add(strEmp, Id);
                    #endregion
                }

                foreach (var item in DicEmp)
                {
                    if (arrIdEmpol.Contains(item.Value))
                    {
                        comboBox_Empl.Items.Add(item.Key);
                    }
                }

            }
            #endregion

            fillTaskList();
        }

        private void button_show_stat_Click(object sender, EventArgs e)
        {
            if (comboBox_Empl.Text != "")
            {
                int emplID = 0;
                if (!DicEmp.TryGetValue(comboBox_Empl.Text, out emplID)) MessageBox.Show("ошибка поиска значения по ключу");
                int prID = 0;
                if (!DicProj.TryGetValue(comboBox_Proj.Text, out prID)) MessageBox.Show("ошибка поиска значения по ключу");
                FormMoreStat pm = new FormMoreStat(emplID, prID);
                this.Visible = false;
                pm.ShowDialog();
                this.Visible = true;
            }
        }

        private void button_show_table_Click(object sender, EventArgs e)
        {
            if (comboBox_Empl.Text != "")
            {
                int emplID = 0;
                if (!DicEmp.TryGetValue(comboBox_Empl.Text, out emplID)) MessageBox.Show("ошибка поиска значения по ключу");
                currentEmplId = emplID;
                List<Task> selectedTaskList = new List<Task>();

                foreach (var item in tasksList)
                {
                    foreach (var i in checkedListBox_task_select.CheckedItems)
                    {
                        if (item.getStatusRu().Equals(i.ToString()))
                        {
                            selectedTaskList.Add(item);
                            break;
                        }
                    }
                }

                DataTable dt = new DataTable();

                DataColumn summColumn = new DataColumn("Название", typeof(string));
                DataColumn discrColumn = new DataColumn("Описание", typeof(string));
                DataColumn estColumn = new DataColumn("Время на вып.(ч)", typeof(float));
                DataColumn statColumn = new DataColumn("Статус", typeof(string));
                DataColumn priorColumn = new DataColumn("Приоритет", typeof(string));
                DataColumn timeColumn = new DataColumn("логированное время(ч)", typeof(float));
                List<DataColumn> dcl = new List<DataColumn>() { summColumn, discrColumn, estColumn, statColumn, priorColumn, timeColumn };

                dt.Columns.AddRange(dcl.ToArray());

                foreach (var i in selectedTaskList)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Название"] = i.Summary;
                    newRow["Описание"] = i.Description;
                    newRow["Время на вып.(ч)"] = i.Estimate;
                    newRow["Статус"] = i.getStatusRu();
                    newRow["Приоритет"] = i.getPrioritiRu();

                    fillLogItems(i.ID);
                    float logTime = 0;

                    foreach (var item in logItemList)
                    {
                        logTime += item.End_time.Hour - item.Start_time.Hour;
                    }
                    newRow["логированное время(ч)"] = logTime;

                    dt.Rows.Add(newRow);
                }
                dataGridView_task.DataSource = dt;
            }
        }

        private void fillTaskList()
        {
            int prID = 0;
            if (!DicProj.TryGetValue(comboBox_Proj.Text, out prID)) MessageBox.Show("ошибка поиска значения по ключу");
            tasksList = new List<Task>();

            string commandText = "SELECT tasks.id, tasks.parent_task_id, tasks.description, tasks.estimate, task_statuses.name AS status, priorities.name AS prior, tasks.summary FROM tasks INNER JOIN task_statuses ON tasks.status_id = task_statuses.id INNER JOIN priorities ON tasks.priority_id = priorities.id WHERE project_id = @id";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@id", prID);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int ID = 0;
                    int par_id = 0;
                    string descr = "";
                    float est = 0;
                    string status = "";
                    string prior = "";
                    string summary = "";

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
                        par_id = rdr.GetInt32(1);
                    }
                    catch (Exception)
                    {

                        par_id = 0;
                    }

                    try
                    {
                        descr = rdr.GetString(2);
                    }
                    catch (Exception)
                    {

                        descr = "";
                    }

                    try
                    {
                        est = (float)rdr.GetDouble(3);
                    }
                    catch (Exception)
                    {
                        est = 0;
                    }

                    try
                    {
                        status = rdr.GetString(4);
                    }
                    catch (Exception)
                    {

                        status = "";
                    }

                    try
                    {
                        prior = rdr.GetString(5);
                    }
                    catch (Exception)
                    {

                        prior = "";
                    }

                    try
                    {
                        summary = rdr.GetString(6);
                    }
                    catch (Exception)
                    {

                        summary = "";
                    }

                    tasksList.Add(new Task(connect, ID, par_id, descr, est, status, prID, prior, summary));
                }
            }
        }

        private void fillLogItems(int task)
        {
            logItemList = new List<LogItem>();

            #region все логиайтемы работника
            string commandText = "SELECT * FROM log_items WHERE employee_id = @empl AND task_id = @taskId";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                cmd.Parameters.AddWithValue("@empl", currentEmplId);
                cmd.Parameters.AddWithValue("@taskId", task);
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
                    logItemList.Add(new LogItem(connect, ID, tId, st, fin, currentEmplId));
                }

            }
            #endregion
        }

        #endregion
    }
}
