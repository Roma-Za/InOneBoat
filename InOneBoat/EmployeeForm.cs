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
        private List<LogItem> logItemList = new List<LogItem>();
        private List<int> listTaskSumm = new List<int>();
        private DateTime dateMon;
        private Label[] Monday;
        private Label[] Tuesday;
        private Label[] Wednesday;
        private Label[] Thursday;
        private Label[] Friday;
        private Label[] Saturday;
        private Label[] Sunday;
        private List<Color> colArr = new List<Color>() { Color.Red, Color.Orange, Color.Purple, Color.Blue, Color.Brown, Color.DarkGreen, Color.DarkGoldenrod, Color.Yellow, Color.Gray, Color.Black };

        public EmployeeForm(string login, string pass)
        {
            InitializeComponent();
            HideAllPanel();
            I_am_Emp = new Employee(connect, login, pass);
            
   
            if (I_am_Emp.Role != "" && I_am_Emp.Role == "PM")
            {
                FormPM pm = new FormPM(I_am_Emp);
                this.Visible = false;
                pm.ShowDialog();
                this.Visible = true;
                this.Close();
            }
            else 
            { 
                this.Text = String.Format("{0} {1}, {2}", I_am_Emp.Name, I_am_Emp.Surname, I_am_Emp.Role);
            panelProj.Visible = true;
            panelProj.Dock = DockStyle.Fill;
            fillProj();
            Monday = new Label[] { label_1_8, label_1_9, label_1_10, label_1_11, label_1_12, label_1_13, label_1_14, label_1_15, label_1_16, label_1_17, label_1_18, label_1_19, label_1_20 };
            Tuesday = new Label[] { label_2_8, label_2_9, label_2_10, label_2_11, label_2_12, label_2_13, label_2_14, label_2_15, label_2_16, label_2_17, label_2_18, label_2_19, label_2_20 };
            Wednesday = new Label[] { label_3_8, label_3_9, label_3_10, label_3_11, label_3_12, label_3_13, label_3_14, label_3_15, label_3_16, label_3_17, label_3_18, label_3_19, label_3_20 };
            Thursday = new Label[] { label_4_8, label_4_9, label_4_10, label_4_11, label_4_12, label_4_13, label_4_14, label_4_15, label_4_16, label_4_17, label_4_18, label_4_19, label_4_20 };
            Friday = new Label[] { label_5_8, label_5_9, label_5_10, label_5_11, label_5_12, label_5_13, label_5_14, label_5_15, label_5_16, label_5_17, label_5_18, label_5_19, label_5_20 };
            Saturday = new Label[] { label_6_8, label_6_9, label_6_10, label_6_11, label_6_12, label_6_13, label_6_14, label_6_15, label_6_16, label_6_17, label_6_18, label_6_19, label_6_20 };
            Sunday = new Label[] { label_7_8, label_7_9, label_7_10, label_7_11, label_7_12, label_7_13, label_7_14, label_7_15, label_7_16, label_7_17, label_7_18, label_7_19, label_7_20 };

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

        private void fillLogItems()
        {
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
        }

        private void fillListBoxLegend()
        {
            listBox_legend.Items.Clear();
            listTaskSumm.Clear();
            #region записываем имена тасков

            DateTime date = dateTimePickerFrom.Value;
            int dOfW = 0;

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dOfW = 0;
                    break;
                case DayOfWeek.Tuesday:
                    dOfW = 1;
                    break;
                case DayOfWeek.Wednesday:
                    dOfW = 2;
                    break;
                case DayOfWeek.Thursday:
                    dOfW = 3;
                    break;
                case DayOfWeek.Friday:
                    dOfW = 4;
                    break;
                case DayOfWeek.Saturday:
                    dOfW = 5;
                    break;
                case DayOfWeek.Sunday:
                    dOfW = 6;
                    break;
                default:
                    break;
            }

            dateMon = date.AddDays(0 - dOfW);
            for (int i = 0; i < 7; i++)
            {
                foreach (var item in logItemList)
                {
                    if (item.Start_time.Date == dateMon.AddDays(i).Date)
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

                        listBox_legend.Items.Add(s);
                        listTaskSumm.Add(item.Task_id);
                    }
                }

            }
            #endregion
        }

        private void listBox_legend_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Cтираем все перерисовкой фона			
            e.DrawBackground();
            //Создем кисть с черным цветом
            //Если пунтов более 4х - она будет кистью по умолчанию 			
            Brush brush = Brushes.Black;
            //При перерисовке пункта задаем цвет кисти
            switch (e.Index)
            {
                case 0:
                    brush = Brushes.Red;
                    break;
                case 1:
                    brush = Brushes.Orange;
                    break;
                case 2:
                    brush = Brushes.Purple;
                    break;
                case 3:
                    brush = Brushes.Blue;
                    break;
                case 4:
                    brush = Brushes.Brown;
                    break;
                case 5:
                    brush = Brushes.DarkGreen;
                    break;
                case 6:
                    brush = Brushes.DarkGoldenrod;
                    break;
                case 7:
                    brush = Brushes.Gray;
                    break;
                case 8:
                    brush = Brushes.Yellow;
                    break;
            }
            // Выводим конкретный пункт
            e.Graphics.DrawString(listBox_legend.Items[e.Index].ToString(),
                     e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            //Если ListBox в фокусе выбирается данный пункт
            e.DrawFocusRectangle();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            clearGraf(Friday);
            clearGraf(Monday);
            clearGraf(Saturday);
            clearGraf(Sunday);
            clearGraf(Thursday);
            clearGraf(Tuesday);
            clearGraf(Wednesday);
            fillLogItems();
            fillListBoxLegend();
            drowGraf();
        }
        private void clearGraf(Label[] collection)
        {
            foreach (var item in collection)
            {
                item.BackColor = Color.White;
            }
        }
        private void drowGraf()
        {

            foreach (var item in logItemList)
            {
                int inxT = 0;
                for (int i = 0; i < listTaskSumm.Count; i++)
                {
                    if (listTaskSumm[i] == item.Task_id) inxT = i;
                }

                if (item.Start_time.Date >= dateMon && item.Start_time.Date <= dateMon.AddDays(6))
                {
                    switch (item.Start_time.DayOfWeek)
                    {
                        case DayOfWeek.Friday:
                            paintCel(item, inxT, Friday);
                            break;
                        case DayOfWeek.Monday:
                            paintCel(item, inxT, Monday);
                            break;
                        case DayOfWeek.Saturday:
                            paintCel(item, inxT, Saturday);
                            break;
                        case DayOfWeek.Sunday:
                            paintCel(item, inxT, Sunday);
                            break;
                        case DayOfWeek.Thursday:
                            paintCel(item, inxT, Thursday);
                            break;
                        case DayOfWeek.Tuesday:
                            paintCel(item, inxT, Tuesday);
                            break;
                        case DayOfWeek.Wednesday:
                            paintCel(item, inxT, Wednesday);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void paintCel(LogItem item, int inxT, Label[] arr)
        {
            int st = item.Start_time.Hour;
            int fin = item.End_time.Hour;
            bool f = false;
            for (int i = 0; i < arr.Length; i++)
            {
                int indx = arr[i].Name.LastIndexOf("_");
                string hs = arr[i].Name.Substring(indx + 1);
                int hor = Convert.ToInt32(hs);
                if (hor == st) f = true;
                if (f && hor < fin)
                {
                    arr[i].BackColor = colArr[inxT];
                }
            }
        }

        #endregion

        #region Проекты
        private void проектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAllPanel();
            Control.ControlCollection c = panelProj.Controls;
            ClearAll(c);
            panelProj.Visible = true;
            panelProj.Dock = DockStyle.Fill;
            fillProj();
        }

        private void fillProj()
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
            DicProj.TryGetValue(currentProj, out Id_pr);

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
                    break;
                }
            }
            this.Visible = true;
            fillListBoxes();
        }
        #endregion
    }
}
