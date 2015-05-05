using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class FormTask : Form
    {
        protected Employee I_am_Emp;
        private string projectName;
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        private Dictionary<String, int> DicEmp;
        private Task task;
        private Task parent_task;
        private string[] priorities = { "high", "normal", "low" };
        private List<Attachment> attList = new List<Attachment>();
        private List<LogItem> logItemList = new List<LogItem>();
        public FormTask(Employee I_am, string projName, Task parent_task, Task task)
        {
            InitializeComponent();
            I_am_Emp = I_am;
            projectName = projName;
            this.parent_task = parent_task;
            this.task = task;
           

            if (task != null)
            { 
                if (!task.isInTask(I_am_Emp.ID)) menuStrip1.Enabled = false;
                labelTaskName.Text = task.Summary;
                textBoxDescription.Text = task.Description;
                label_P_Task_status.Text = "Статус задачи: " + task.getStatusRu();
                label_P_Task_prior.Text = "Приоритет: " + task.getPrioritiRu();
                addAttach();

                foreach (var item in task.getComments())
                {
                    richTextBoxComments.Text += item;
                    richTextBoxComments.Text += "\n";
                }
                if (task.isInTask(I_am_Emp.ID))
                {
                    button_watch.Enabled = false;
                }
                else
                {
                    if (task.isInWatch(I_am_Emp.ID))
                    {
                        button_watch.Text = "Отписаться";
                    }
                    else
                    {
                        button_watch.Text = "Подписаться";
                    }
                }
            }
            else
            {
                fillCLB();

            }

        }
        private static void ClearAll(Control.ControlCollection c)
        {
            foreach (var item in c)
            {
                if (item is TextBox) ((TextBox)item).Text = "";
                if (item is MaskedTextBox) ((MaskedTextBox)item).Text = "";
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
        private void addAttach()
        {

            #region все приложения
            listBox_Attach_List.Items.Clear();
            attList = new List<Attachment>();
            string commandText = "SELECT * FROM attachments WHERE task_id = @tID";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@tID", task.ID);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string path = "";
                    int id = 0;
                    try
                    {
                        id = rdr.GetInt32(0);

                    }
                    catch (Exception)
                    {
                        id = 0;
                    }
                    try
                    {
                        int tid = rdr.GetInt32(1);

                    }
                    catch (Exception)
                    { }
                    try
                    {
                        path = rdr.GetString(2);

                    }
                    catch (Exception)
                    {
                        path = "";
                    }
                    if (path != "")
                    {
                        attList.Add(new Attachment(connect, id, task.ID, path));
                        listBox_Attach_List.Items.Add(path);
                    }
                }
            }
            #endregion
        }

        #region новый таск
        private void fillCLB()
        {
            #region все работники
            checkedListBox_P_New_Task_Partic.Items.Clear();
            DicEmp = new Dictionary<string, int>();
            string commandText = "SELECT surname, name, role, user_id FROM emplInProj WHERE project_name = @proj";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@proj", projectName);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string role = "";
                    string empl = "";
                    int id = 0;
                    try
                    {
                        empl += rdr.GetString(0);
                        empl += " ";
                    }
                    catch (Exception)
                    {
                        empl += "";
                    }
                    try
                    {
                        empl += rdr.GetString(1);
                        empl += " ";
                    }
                    catch (Exception)
                    {
                        empl += "";
                    }
                    try
                    {
                        role += rdr.GetString(2);
                        empl += role;
                        empl += " ";

                    }
                    catch (Exception)
                    {
                        empl += "";
                    }
                    try
                    {
                        id = rdr.GetInt32(3);

                    }
                    catch (Exception)
                    {
                        id = 0;
                    }
                    if (role == "Dev" || role == "QA")
                    {
                        if (id != I_am_Emp.ID)
                        {
                            DicEmp.Add(empl, id);
                            checkedListBox_P_New_Task_Partic.Items.Add(empl, false);
                        }
                    }
                }

            }
            #endregion
        }

        private void button_P_New_Task_Ok_Click(object sender, EventArgs e)
        {
            string resultValid = "";

            #region валидация
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panel_New_Task.Controls)
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
                if (item is ComboBox)
                {
                    val.setText(((ComboBox)item).Text);
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
                val.addValidator(new InOneBoat.Validators.IsNumberValid());
                val.setText(textBox_P_New_Task_Est.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                #region проверка уникальности имени таска в проекте
                string commandText1 = "SELECT COUNT(*) FROM tasks WHERE summary = @summ";

                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandText1;
                    cmd.Parameters.AddWithValue("@summ", textBox_P_New_Task_Summ.Text);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    int count = 0;
                    rdr.Read();
                    try
                    {
                        count = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        count = 0;
                    }
                    if (count > 0)
                    {
                        resultValid += " такое название уже существует!";
                    }
                }
                #endregion
            }
            #endregion

            if (resultValid == "")
            {
                task = new Task(connect);
                int parent_id = 0;
                try
                {
                    parent_id = parent_task.ID;
                }
                catch (Exception)
                {
                    parent_id = 0;
                }
                float est = Convert.ToInt32(textBox_P_New_Task_Est.Text);
                string st = checkBox_P_new_Task_Start.Checked ? "in progress" : "not started";
                int pr_id = 0;
                string commandText = "SELECT id FROM projects WHERE project_name = @proj";
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandText;
                    cmd.Parameters.AddWithValue("@proj", projectName);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    try
                    {
                        pr_id = rdr.GetInt32(0);

                    }
                    catch (Exception)
                    {
                        pr_id = 0;
                    }
                }
                string pr = priorities[comboBox_P_New_Task_prior.SelectedIndex];
                List<int> chItems = new List<int>();
                foreach (var item in checkedListBox_P_New_Task_Partic.CheckedItems)
                {
                    int emplId = 0;
                    if (!DicEmp.TryGetValue(item.ToString(), out emplId)) MessageBox.Show("ошибка поиска значения по ключу emplId");
                    chItems.Add(emplId);
                }
                if (!chItems.Contains(I_am_Emp.ID)) chItems.Add(I_am_Emp.ID);
                task.SetAll(parent_id, textBox_P_New_Task_Descrip.Text, est, st, pr_id, pr, textBox_P_New_Task_Summ.Text, chItems);
                this.Close();
            }
            else MessageBox.Show(resultValid);
        }

        private void button_P_New_Task_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void button_add_com_Click(object sender, EventArgs e)
        {
            String MessageText = "";
            using (var form = new FormInput())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    MessageText = form.ReturnValue1;
                    string author = I_am_Emp.Name + " " + I_am_Emp.Surname + " " + I_am_Emp.Role + " ";
                    Comment c = new Comment(connect, MessageText, task.ID, I_am_Emp.ID, author);
                    richTextBoxComments.Text += c.CommentText;
                }
            }

        }

        private void button_watch_Click(object sender, EventArgs e)
        {
            if (button_watch.Text.Equals("Подписаться"))
            {
                if (!task.isInWatch(I_am_Emp.ID))
                {
                    task.addWatcher(I_am_Emp.ID);
                    button_watch.Text = "Отписаться";
                }
            }
            else
            {
                if (task.isInWatch(I_am_Emp.ID))
                {
                    task.delWatcher(I_am_Emp.ID);
                    button_watch.Text = "Подписаться";
                }
            }
        }

        private void listBox_Attach_List_DoubleClick(object sender, EventArgs e)
        {
  
        }

        private void логированиеВремениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Log_Item fli = new Form_Log_Item(task, I_am_Emp);
            this.Visible = false;
            fli.ShowDialog();
            this.Visible = true;
            HideAllPanel();
            panel_Task.Visible = true;
            panel_Task.Dock = DockStyle.Fill;
        }

        private void статусToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanel();
            Control.ControlCollection c = panel_Edit_Task.Controls;
            ClearAll(c);
            panel_Edit_Task.Visible = true;
            panel_Edit_Task.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
            textBox_P_Edit_Summ.Text = task.Summary;
            textBox_P_Edit_Discr.Text = task.Description;
            textBox_P_Edit_Estimate.Text = Convert.ToString(task.Estimate);
            comboBox_P_Edit_Status.Text = task.getStatusRu();
            comboBox_P_Edit_Prior.Text = task.getPrioritiRu();
            foreach (var item in attList)
            {
                checkedListBox_P_Del_Attach.Items.Add(item.File_path, false);
            }
            fillLogItems(dateTimePicker_P_Edit.Value);

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                attList.Add(new Attachment(connect, this.task.ID, openFileDialog1.FileName));
            }
            addAttach();
            foreach (var item in attList)
            {
                checkedListBox_P_Del_Attach.Items.Add(item.File_path, false);
            }
        }

        private void fillLogItems(DateTime date)
        {
            checkedListBox_P_Edit_Log.Items.Clear();
            logItemList.Clear();

            #region все логиайтемы работника
            string commandText = "SELECT * FROM log_items WHERE employee_id = @empl AND task_id = @tid";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                cmd.Parameters.AddWithValue("@empl", I_am_Emp.ID);
                cmd.Parameters.AddWithValue("@tid", task.ID);
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
                    if (s != "") checkedListBox_P_Edit_Log.Items.Add(s + " c " + item.Start_time.Hour + "ч. по " + item.End_time.Hour + "ч.", false);
                }
            }
            #endregion
        }

        private void buttonDelAtta_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox_P_Del_Attach.CheckedItems)
            {

                string commandText = "DELETE FROM attachments WHERE file_path = @path";

                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandText;

                    cmd.Parameters.AddWithValue("@path", item.ToString());
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления " + item.ToString());
                    }

                }
               
            }
            checkedListBox_P_Del_Attach.Items.Clear();
            addAttach();
            foreach (var item in attList)
            {
                checkedListBox_P_Del_Attach.Items.Add(item.File_path, false);
            }
        }

        private void button_P_Edit_Del_Log_Click(object sender, EventArgs e)
        {
            foreach (int item in checkedListBox_P_Edit_Log.CheckedIndices)
            {
                string commandText = "DELETE FROM log_items WHERE id = @id";

                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandText;

                    cmd.Parameters.AddWithValue("@id", logItemList[item].ID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления " + item.ToString());
                    }

                }
                logItemList.RemoveAt(item);
            }

            fillLogItems(dateTimePicker_P_Edit.Value);
        }

        private void dateTimePicker_P_Edit_ValueChanged(object sender, EventArgs e)
        {
            fillLogItems(dateTimePicker_P_Edit.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HideAllPanel();
            Control.ControlCollection c = panel_Edit_Task.Controls;
            ClearAll(c);
            panel_Task.Visible = true;
            panel_Task.Dock = DockStyle.Fill;
            menuStrip1.Enabled = true;
            attList.Clear();
            logItemList.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string resultValid = "";

            #region валидация
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panel_Edit_Task.Controls)
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
                if (item is ComboBox)
                {
                    val.setText(((ComboBox)item).Text);
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
                val.addValidator(new InOneBoat.Validators.IsNumberValid());
                val.setText(textBox_P_Edit_Estimate.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "" && textBox_P_Edit_Summ.Text != task.Summary)
            {
                #region проверка уникальности имени таска в проекте
                string commandText1 = "SELECT COUNT(*) FROM tasks WHERE summary = @summ";

                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandText1;
                    cmd.Parameters.AddWithValue("@summ", textBox_P_Edit_Summ.Text);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    int count = 0;
                    rdr.Read();
                    try
                    {
                        count = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        count = 0;
                    }
                    if (count > 0)
                    {
                        resultValid += " такое название уже существует!";
                    }
                }
                #endregion
            }
            #endregion

            if (resultValid == "")
            {
                float est = Convert.ToInt32(textBox_P_Edit_Estimate.Text);
                string st = task.getStatusEn(comboBox_P_Edit_Status.Text);
                string pr = task.getPrioritiEn(comboBox_P_Edit_Prior.Text);
                string summ = textBox_P_Edit_Summ.Text;
                string des = textBox_P_Edit_Discr.Text;
                task.EditTask(des, est, st, pr, summ);
                this.Close();
            }
            else MessageBox.Show(resultValid);
        }

        private void button_Add_SubTask_Click(object sender, EventArgs e)
        {
            HideAllPanel();
            panel_New_Task.Visible = true;
            panel_New_Task.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
            parent_task = task;
            fillCLB();
        }

        private void button_P_Task_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
