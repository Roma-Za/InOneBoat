using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class EmployeeForm : Form
    {
        private Dictionary<String, int> DicProj;
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        protected Employee I_am_Emp;
        private List<string> prof = new List<string>() { "PM", "QA", "Dev", "Admin" };
        private List<Task> tasksList;
        private string currentProj = "";
        public EmployeeForm(string login, string pass)
        {
            InitializeComponent();
            HideAllPanel();
            I_am_Emp = new Employee(connect, login, pass);
            this.Text = String.Format("{0} {1}, {2}", I_am_Emp.Name, I_am_Emp.Surname, I_am_Emp.Role);
            panelProj.Visible = true;
            panelProj.Dock = DockStyle.Fill;
            filProj();
            if (I_am_Emp.Role != "" && I_am_Emp.Role == "PM")
            {
                FormPM pm = new FormPM(I_am_Emp);
                this.Visible = false;
                pm.ShowDialog();
                this.Close();
            }

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
        private void изменитьЛичныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panelData.Controls;
            ClearAll(c);
            HideAllPanel();
            panelData.Visible = true;
            panelData.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;

            textBox_log.Text = I_am_Emp.Login;
            textBox_type.Text = I_am_Emp.UserType;
            textBox_Sur.Text = I_am_Emp.Surname;
            textBox_name.Text = I_am_Emp.Name;
            textBox_Pat.Text = I_am_Emp.Patronymic;
            textBox_Phone.Text = I_am_Emp.Phone_number;
            textBox_Email.Text = I_am_Emp.Email;
            textBoxRole.Text = I_am_Emp.Role;
            checkBox_send.Checked = I_am_Emp.Send;

        }

        private void buttonOkP8_Click(object sender, EventArgs e)
        {
            string resultValid = "";

            #region валидация
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panelData.Controls)
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
                    if (maskedTextBoxPass.Text.Equals(I_am_Emp.Password))
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
                I_am_Emp.SetAll(
                textBox_log.Text,
                maskedTextBoxNewPass.Text,
                textBox_type.Text,
                textBox_Sur.Text,
                textBox_name.Text,
                textBox_Pat.Text,
                textBox_Phone.Text,
                textBox_Email.Text,
                textBoxRole.Text,
                checkBox_send.Checked
                    );
            }
            else MessageBox.Show(resultValid);
            if (resultValid == "")
            {
                Control.ControlCollection c = panelData.Controls;
                ClearAll(c);

                HideAllPanel();
                menuStrip1.Enabled = true;
            }
        }

        private void buttonCancelP8_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panelData.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

        #region Статистика
        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanel();
            Control.ControlCollection c = panelStat.Controls;
            ClearAll(c);
            panelStat.Visible = true;
            panelStat.Dock = DockStyle.Fill;
            //menuStrip1.Enabled = false;

            #region вывод всех проектов
            string commandText = "SELECT p.project_name FROM projects AS p INNER JOIN projects_employees AS pe ON p.id = pe.project_id  WHERE pe.employee_id = @empl";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                cmd.Parameters.AddWithValue("@empl", I_am_Emp.ID);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    comboBoxProj_PStat.Items.Add(rdr.GetString(0));
                }

            }
            #endregion
            panelStat.Visible = true;
            panelStat.Dock = DockStyle.Fill;
            // menuStrip1.Enabled = false;

        }

        private void comboBoxProj_PStat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void проектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanel();
            Control.ControlCollection c = panelProj.Controls;
            ClearAll(c);
            panelProj.Visible = true;
            panelProj.Dock = DockStyle.Fill;
            filProj();
        }

        private void filProj()
        {

            DicProj = new Dictionary<string, int>();
            #region вывод всех проектов
            string commandText = "SELECT p.id, p.project_name FROM projects AS p INNER JOIN projects_employees AS pe ON p.id = pe.project_id  WHERE pe.employee_id = @empl";

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
                    string pr_name = "";
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
                        pr_name = rdr.GetString(1);
                    }
                    catch (Exception)
                    {

                        pr_name = "";
                    }
                    DicProj.Add(pr_name, ID);
                    comboBoxProj_P_Proj.Items.Add(pr_name);
                }

            }
            #endregion
        }

        private void buttonAddTask_Click(object sender, EventArgs e)
        {
            if (currentProj != "")
            {
                FormTask ft = new FormTask(I_am_Emp, currentProj, null, null);
                this.Visible = false;

                Control.ControlCollection contr = ft.Controls;
                foreach (var item in contr)
                {
                    if (item is Panel && ((Panel)item).Name == "panel_New_Task")
                    {
                        ((Panel)item).Visible = true;
                        ((Panel)item).Dock = DockStyle.Fill;
                    }
                    if (item is MenuStrip) ((MenuStrip)item).Enabled = false;
                }

                ft.ShowDialog();
                this.Visible = true;
            }
        }

        private void comboBoxProj_P_Proj_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillListBoxes();
        }

        private void fillListBoxes()
        {
            foreach (var item in panelProj.Controls)
            {
                if (item is ListBox) ((ListBox)item).Items.Clear();
            }
            tasksList = new List<Task>();
            currentProj = comboBoxProj_P_Proj.Text;
            int Id_pr = 0;
            if (!DicProj.TryGetValue(currentProj, out Id_pr)) ;

            #region вывод всех задач в проекте
            string commandText = "SELECT tasks.id, tasks.parent_task_id, tasks.description, tasks.estimate, task_statuses.name AS status, priorities.name AS prior, tasks.summary FROM tasks INNER JOIN task_statuses ON tasks.status_id = task_statuses.id INNER JOIN priorities ON tasks.priority_id = priorities.id WHERE project_id = @id";

            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@id", Id_pr);
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

                    tasksList.Add(new Task(connect, ID, par_id, descr, est, status, Id_pr, prior, summary));
                }

                if (checkBox_PProj_only_my.Checked)
                {
                    foreach (var item in tasksList)
                    {
                        if (item.isInTask(I_am_Emp.ID))
                        {
                            if (item.Status == "not started")
                            {
                                listBoxNotStart.Items.Add(item.Summary);
                            }
                            else if (item.Status == "in progress" || item.Status == "reopened")
                            {
                                listBoxCurr.Items.Add(item.Summary);
                            }
                            else
                            {
                                listBoxClosed.Items.Add(item.Summary);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in tasksList)
                    {
                        if (item.Status == "not started")
                        {
                            listBoxNotStart.Items.Add(item.Summary);
                        }
                        else if (item.Status == "in progress" || item.Status == "reopened")
                        {
                            listBoxCurr.Items.Add(item.Summary);
                        }
                        else
                        {
                            listBoxClosed.Items.Add(item.Summary);
                        }
                    }
                }
            }
            #endregion
        }

        private void checkBox_PProj_only_my_CheckedChanged(object sender, EventArgs e)
        {
            if (currentProj != "") fillListBoxes();
        }

        private void listBoxNotStart_DoubleClick(object sender, EventArgs e)
        {
            try
            {
              foreach (var currTask in tasksList)
                {
                    if (currTask.Summary == ((ListBox)sender).SelectedItem.ToString())
                    {
                        FormTask ft = new FormTask(I_am_Emp, currentProj, null, currTask);
                        this.Visible = false;

                        Control.ControlCollection contr = ft.Controls;
                        foreach (var item in contr)
                        {
                            if (item is Panel && ((Panel)item).Name == "panel_Task")
                            {
                                ((Panel)item).Visible = true;
                                ((Panel)item).Dock = DockStyle.Fill;
                            }
                        }

                        ft.ShowDialog();
                        this.Visible = true;
                        fillListBoxes();
                    }
                }
            }
            catch (Exception)
            {
                
                
            }
  

        }

    }
}
