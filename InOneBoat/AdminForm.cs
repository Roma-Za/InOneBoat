using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InOneBoat
{
    public partial class AdminForm : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;

        private Dictionary<String, int> DicBoss;
        private Dictionary<String, int> DicProj;
        private Dictionary<String, int> DicEmp;
        private Dictionary<String, int> DicCust;
        private List<Customer> CustList;
        private List<Employee> EmpList;
        private List<string> userType = new List<string>() { "Администратор", "Работник" };
        private List<string> prof = new List<string>() { "PM", "QA", "Dev", "Admin" };
        private Customer TempCust;
        private Employee TempEmp;
        private Employee I_am_Emp;
        private String tempTxt = "";
        public AdminForm(string login, string pass)
        {
            InitializeComponent();
            HideAllPanel();
            I_am_Emp = new Employee(connect, login, pass);
            this.Text = String.Format("{0} {1}, {2}", I_am_Emp.Name, I_am_Emp.Surname, I_am_Emp.Role);
        }


        private static void ClearAll(Control.ControlCollection c)
        {
            foreach (var item in c)
            {
                if (item is TextBox) ((TextBox)item).Text = "";
                try
                {
                    if (item is ComboBox) ((ComboBox)item).Items.Clear();
                }
                catch (Exception e)
                {
                }
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

        #region добавить работника
        private void работникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
            comboBoxUserType.DataSource = userType;
            comboBoxRole.DataSource = prof;
        }

        private void buttonOkP1_Click(object sender, EventArgs e)
        {
            string resultValid = "";

            #region валидация
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panel1.Controls)
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
                val.addValidator(new InOneBoat.Validators.NameValid());
                val.setText(textBoxSur.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

                val.setText(textBoxName.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

                val.setText(textBoxPat.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";
            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.PhoneValid());
                val.setText(textBoxPhone.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.EmailValid());
                val.setText(textBoxEmail.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.LoginValid());
                val.setText(textBoxLogin.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.UniqueLoginValid());
                val.setText(textBoxLogin.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.PassValid());
                val.setText(textBoxPass.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                if (comboBoxUserType.Text == "Администратор" && comboBoxRole.Text != "Admin")
                {
                    resultValid += "Если тип пользователя Администратор, роль должна быть Admin. \n";
                }
                if (comboBoxUserType.Text == "Работник" && comboBoxRole.Text == "Admin")
                {
                    resultValid += "Если тип пользователя Работник, роль не может быть Admin. \n";
                }

            }

            #endregion

            if (resultValid == "")
            {
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    int idPeop = 0;
                    int idUser = 0;

                    sql_connect.Open();

                    #region вызов хп AddPeople
                    using (SqlCommand cmd = new SqlCommand("AddPeople", sql_connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Выходной параметр.
                        SqlParameter param1 = new SqlParameter();
                        param1 = new SqlParameter();
                        param1.ParameterName = "@id";
                        param1.SqlDbType = SqlDbType.Int;
                        param1.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param1);

                        // Входной параметр.
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@surname";
                        param2.SqlDbType = SqlDbType.NVarChar;
                        param2.Value = textBoxSur.Text;
                        param2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@name";
                        param3.SqlDbType = SqlDbType.NVarChar;
                        param3.Value = textBoxName.Text;
                        param3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter();
                        param4.ParameterName = "@patronymic";
                        param4.SqlDbType = SqlDbType.NVarChar;
                        param4.Value = textBoxPat.Text;
                        param4.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param4);

                        SqlParameter param5 = new SqlParameter();
                        param5.ParameterName = "@phone_number";
                        param5.SqlDbType = SqlDbType.NVarChar;
                        param5.Value = textBoxPhone.Text;
                        param5.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param5);

                        SqlParameter param6 = new SqlParameter();
                        param6.ParameterName = "@email";
                        param6.SqlDbType = SqlDbType.NVarChar;
                        param6.Value = textBoxEmail.Text;
                        param6.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param6);

                        // Выполнение хранимой процедуры.
                        cmd.ExecuteNonQuery();
                        idPeop = (int)cmd.Parameters["@id"].Value;
                    }
                    #endregion

                    #region вызов хп AddUser
                    using (SqlCommand cmd = new SqlCommand("AddUser", sql_connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Выходной параметр.
                        SqlParameter param1 = new SqlParameter();
                        param1 = new SqlParameter();
                        param1.ParameterName = "@id";
                        param1.SqlDbType = SqlDbType.Int;
                        param1.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param1);

                        // Входной параметр.
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@user_name";
                        param2.SqlDbType = SqlDbType.NVarChar;
                        param2.Value = textBoxLogin.Text;
                        param2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@password";
                        param3.SqlDbType = SqlDbType.NVarChar;
                        param3.Value = textBoxPass.Text;
                        param3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter();
                        param4.ParameterName = "@user_type_id";
                        param4.SqlDbType = SqlDbType.Int;
                        param4.Value = comboBoxUserType.SelectedIndex == 0 ? 1 : 3;
                        param4.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param4);

                        // Выполнение хранимой процедуры.
                        cmd.ExecuteNonQuery();
                        idUser = (int)cmd.Parameters["@id"].Value;
                    }
                    #endregion

                    #region вызов хп AddEmployee
                    using (SqlCommand cmd = new SqlCommand("AddEmployee", sql_connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@user_id";
                        param1.SqlDbType = SqlDbType.Int;
                        param1.Value = idUser;
                        param1.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@people_id";
                        param2.SqlDbType = SqlDbType.Int;
                        param2.Value = idPeop;
                        param2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@role_id";
                        param3.SqlDbType = SqlDbType.Int;
                        param3.Value = comboBoxRole.SelectedIndex + 1;
                        param3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter();
                        param4.ParameterName = "@send_to_email";
                        param4.SqlDbType = SqlDbType.Bit;
                        param4.Value = checkBoxSend.Checked ? 1 : 0;
                        param4.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param4);

                        // Выполнение хранимой процедуры.
                        cmd.ExecuteNonQuery();
                    }
                    #endregion

                }
                Control.ControlCollection c = panel1.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else MessageBox.Show(resultValid);
        }

        private void buttonCancelP1_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel1.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

        #region добавить проект
        private void проектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel2.Controls;
            ClearAll(c);

            DicBoss = new Dictionary<String, int>();

            string commandText = "SELECT * FROM bosses";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    String str = "";
                    int Id = 0;
                    try
                    {
                        Id = rdr.GetInt32(0);

                    }
                    catch (Exception exep)
                    {
                        MessageBox.Show(exep.Message);
                    }
                    try
                    {
                        str += rdr.GetString(1);
                        str += " ";
                    }
                    catch (Exception)
                    {

                        str += " ";
                    }
                    try
                    {
                        str += rdr.GetString(2);
                        str += " ";
                    }
                    catch (Exception)
                    {
                        str += " ";

                    }
                    try
                    {
                        str += rdr.GetString(3);
                    }
                    catch (Exception)
                    {

                    }

                    DicBoss.Add(str, Id);
                    #endregion
                }

                foreach (var item in DicBoss.Keys)
                {
                    comboBoxCustoP2.Items.Add(item.ToString());
                }
            }

            panel2.Visible = true;
            panel2.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
        }

        private void buttonOkP2_Click(object sender, EventArgs e)
        {
            string resultValid = "";

            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panel2.Controls)
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
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    #region вызов хп AddProject
                    using (SqlCommand cmd = new SqlCommand("AddProject", sql_connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@project_name";
                        param1.SqlDbType = SqlDbType.NVarChar;
                        param1.Value = textBoxPrNameP2.Text;
                        param1.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@customer_id";
                        param2.SqlDbType = SqlDbType.Int;
                        int Id = 0;
                        if (!DicBoss.TryGetValue(comboBoxCustoP2.Text, out Id)) MessageBox.Show("ошибка поиска значения по ключу Id");
                        param2.Value = Id;
                        param2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@description";
                        param3.SqlDbType = SqlDbType.Text;
                        param3.Value = textBoxInfoP2.Text;
                        param3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param3);

                        // Выполнение хранимой процедуры.
                        cmd.ExecuteNonQuery();
                    }
                    #endregion

                }
                Control.ControlCollection c = panel2.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else MessageBox.Show(resultValid);
        }

        private void buttonCancelP2_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel2.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

        #region добавить работника в проект
        private void работникаВПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel3.Controls;
            ClearAll(c);

            DicProj = new Dictionary<String, int>();
            string commandText = "SELECT id, project_name FROM projects";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    String str = "";
                    int Id = 0;
                    try
                    {
                        Id = rdr.GetInt32(0);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    try
                    {
                        str += rdr.GetString(1);
                        str += " ";
                    }
                    catch (Exception)
                    {

                        str += " ";
                    }

                    DicProj.Add(str, Id);
                    #endregion
                }

                foreach (var item in DicProj.Keys)
                {
                    comboBoxProjP3.Items.Add(item.ToString());
                }
            }

            panel3.Visible = true;
            panel3.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
        }

        private void buttonOkP3_Click(object sender, EventArgs e)
        {
            string resultValid = "";
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());
            val.setText(comboBoxProjP3.Text);
            if (!val.check())
            {
                resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    #region вызов хп AddEmployeeInProject

                    List<String> chItems = new List<string>();
                    foreach (var item in checkedListBoxP3.CheckedItems)
                    {
                        chItems.Add(item.ToString());
                    }
                    for (int i = 0; i < chItems.Count; i++)
                    {

                        using (SqlCommand cmd = new SqlCommand("AddEmployeeInProject", sql_connect))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter param1 = new SqlParameter();
                            param1.ParameterName = "@project_id";
                            param1.SqlDbType = SqlDbType.Int;

                            int pId = 0;
                            if (!DicProj.TryGetValue(comboBoxProjP3.Text, out pId)) MessageBox.Show("ошибка поиска значения по ключу Id ");

                            param1.Value = pId;
                            param1.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param1);

                            SqlParameter param2 = new SqlParameter();
                            param2.ParameterName = "@employee_id";
                            param2.SqlDbType = SqlDbType.Int;


                            int eId = 0;
                            if (!DicEmp.TryGetValue(chItems[i], out eId)) MessageBox.Show("ошибка поиска значения по ключу Id ");

                            param2.Value = eId;
                            param2.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param2);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion

                }

                Control.ControlCollection c = panel3.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else MessageBox.Show(resultValid);
        }

        private void buttonCancelP3_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel3.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }

        private void comboBoxProjP3_SelectedIndexChanged(object sender, EventArgs e)
        {

            int IdProj = 0;
            if (!DicProj.TryGetValue(comboBoxProjP3.Text, out IdProj)) MessageBox.Show("ошибка поиска значения по ключу Id ");

            checkedListBoxP3.Items.Clear();

            #region все работники которые уже добавленны в выбранный проект
            string commandText = "SELECT employee_id FROM projects_employees WHERE project_id = @proj";
            List<int> arrIdEmpol = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@proj", IdProj);

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
            #endregion

            DicEmp = new Dictionary<String, int>();
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
                    String strEmp = "";
                    int Id = 0;

                    try
                    {
                        Id = rdr.GetInt32(0);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    DicEmp.Add(strEmp, Id);
                    #endregion
                }

                foreach (var item in DicEmp)
                {
                    if (!arrIdEmpol.Contains(item.Value))
                    {
                        checkedListBoxP3.Items.Add(item.Key, false);
                    }
                }

            }
        }

        #endregion

        #region добавить заказчика
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel4.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
        }

        private void buttonOkP4_Click(object sender, EventArgs e)
        {
            string resultValid = "";

            #region валидация
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panel4.Controls)
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
                val.addValidator(new InOneBoat.Validators.NameValid());
                val.setText(textBoxSurP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

                val.setText(textBoxNameP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

                val.setText(textBoxPatP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";
            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.PhoneValid());
                val.setText(textBoxPhoneP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.EmailValid());
                val.setText(textBoxEmailP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.LoginValid());
                val.setText(textBoxLogP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.UniqueLoginValid());
                val.setText(textBoxLogP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                val = new Validator();
                val.addValidator(new InOneBoat.Validators.PassValid());
                val.setText(textBoxPassP4.Text);
                if (!val.check()) resultValid += val.getCause() + "\n";

            }

            #endregion

            if (resultValid == "")
            {
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    int idPeop = 0;
                    int idUser = 0;

                    sql_connect.Open();

                    #region вызов хп AddPeople
                    using (SqlCommand cmd = new SqlCommand("AddPeople", sql_connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Выходной параметр.
                        SqlParameter param1 = new SqlParameter();
                        param1 = new SqlParameter();
                        param1.ParameterName = "@id";
                        param1.SqlDbType = SqlDbType.Int;
                        param1.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param1);

                        // Входной параметр.
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@surname";
                        param2.SqlDbType = SqlDbType.NVarChar;
                        param2.Value = textBoxSurP4.Text;
                        param2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@name";
                        param3.SqlDbType = SqlDbType.NVarChar;
                        param3.Value = textBoxNameP4.Text;
                        param3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter();
                        param4.ParameterName = "@patronymic";
                        param4.SqlDbType = SqlDbType.NVarChar;
                        param4.Value = textBoxPatP4.Text;
                        param4.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param4);

                        SqlParameter param5 = new SqlParameter();
                        param5.ParameterName = "@phone_number";
                        param5.SqlDbType = SqlDbType.NVarChar;
                        param5.Value = textBoxPhoneP4.Text;
                        param5.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param5);

                        SqlParameter param6 = new SqlParameter();
                        param6.ParameterName = "@email";
                        param6.SqlDbType = SqlDbType.NVarChar;
                        param6.Value = textBoxEmailP4.Text;
                        param6.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param6);

                        // Выполнение хранимой процедуры.
                        cmd.ExecuteNonQuery();
                        idPeop = (int)cmd.Parameters["@id"].Value;
                    }
                    #endregion

                    #region вызов хп AddUser
                    using (SqlCommand cmd = new SqlCommand("AddUser", sql_connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Выходной параметр.
                        SqlParameter param1 = new SqlParameter();
                        param1 = new SqlParameter();
                        param1.ParameterName = "@id";
                        param1.SqlDbType = SqlDbType.Int;
                        param1.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param1);

                        // Входной параметр.
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@user_name";
                        param2.SqlDbType = SqlDbType.NVarChar;
                        param2.Value = textBoxLogP4.Text;
                        param2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@password";
                        param3.SqlDbType = SqlDbType.NVarChar;
                        param3.Value = textBoxPassP4.Text;
                        param3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter();
                        param4.ParameterName = "@user_type_id";
                        param4.SqlDbType = SqlDbType.Int;
                        param4.Value = 2;
                        param4.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param4);

                        // Выполнение хранимой процедуры.
                        cmd.ExecuteNonQuery();
                        idUser = (int)cmd.Parameters["@id"].Value;
                    }
                    #endregion

                    #region вызов хп AddCustomer
                    using (SqlCommand cmd = new SqlCommand("AddCustomer", sql_connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@user_id";
                        param1.SqlDbType = SqlDbType.Int;
                        param1.Value = idUser;
                        param1.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@people_id";
                        param2.SqlDbType = SqlDbType.Int;
                        param2.Value = idPeop;
                        param2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@more_info";
                        param3.SqlDbType = SqlDbType.Text;
                        param3.Value = textBoxInfo.Text;
                        param3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param3);

                        // Выполнение хранимой процедуры.
                        cmd.ExecuteNonQuery();
                    }
                    #endregion

                }
                Control.ControlCollection c = panel4.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else MessageBox.Show(resultValid);
        }

        private void buttonCancelP4_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel4.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

        #region удалить работника из проекта
        private void работникаИзПроектаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel5.Controls;
            ClearAll(c);

            DicProj = new Dictionary<String, int>();
            string commandText = "SELECT id, project_name FROM projects";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int id = 0;
                    String str = "";

                    try
                    {
                        id = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ошибка конвертации id");
                    }
                    try
                    {
                        str += rdr.GetString(1);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("ошибка конвертации имени проекта");
                    }

                    DicProj.Add(str, id);
                    #endregion
                }
                foreach (var item in DicProj.Keys)
                {
                    comboBoxProjP5.Items.Add(item.ToString());
                }

            }

            panel5.Visible = true;
            panel5.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
        }

        private void comboBoxProjP5_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxEplP5.Items.Clear();
            #region все работники которые уже добавленны в выбранный проект
            string commandText = "SELECT employee_id FROM projects_employees WHERE project_id = @proj";
            List<int> arrIdEmpol = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                int value = 0;
                if (!DicProj.TryGetValue(comboBoxProjP5.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
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
            #endregion

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
                        checkedListBoxEplP5.Items.Add(item.Key, false);
                    }
                }

            }
        }

        private void buttonOkP5_Click(object sender, EventArgs e)
        {

            string resultValid = "";
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());
            val.setText(comboBoxProjP5.Text);
            if (!val.check())
            {
                resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();
                    #region вызов хп KoknutEmpInPr

                    List<String> chItems = new List<string>();
                    foreach (var item in checkedListBoxEplP5.CheckedItems)
                    {
                        chItems.Add(item.ToString());
                    }
                    for (int i = 0; i < chItems.Count; i++)
                    {

                        using (SqlCommand cmd = new SqlCommand("KoknutEmpInPr", sql_connect))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter param1 = new SqlParameter();
                            param1.ParameterName = "@project_id";
                            param1.SqlDbType = SqlDbType.Int;
                            int projId = 0;
                            if (!DicProj.TryGetValue(comboBoxProjP5.Text, out projId)) MessageBox.Show("ошибка поиска значения по ключу projId");
                            param1.Value = projId;
                            param1.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param1);

                            SqlParameter param2 = new SqlParameter();
                            param2.ParameterName = "@employee_id";
                            param2.SqlDbType = SqlDbType.Int;
                            int empId = 0;
                            if (!DicEmp.TryGetValue(chItems[i], out empId)) MessageBox.Show("ошибка поиска значения по ключу empId " + chItems[i]);
                            param2.Value = empId;
                            param2.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param2);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion
                }

                Control.ControlCollection c = panel5.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else MessageBox.Show(resultValid);
        }

        private void buttonCancelP5_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel5.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

        #region удалить пустой проекта
        private void comboBoxProjP6_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxEmplP6.Items.Clear();
            #region все работники которые уже добавленны в выбранный проект
            string commandText = "SELECT employee_id FROM projects_employees WHERE project_id = @proj";
            List<int> arrIdEmpol = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                int value = 0;
                if (!DicProj.TryGetValue(comboBoxProjP6.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
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
            #endregion

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
                        listBoxEmplP6.Items.Add(item.Key);
                    }
                }

            }

            listBoxCustP6.Items.Clear();
            #region все заказчики которые уже добавленны в выбранный проект
            string commandTextCust = "SELECT customer_id FROM projects WHERE id = @proj";
            List<int> arrIdCust = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandTextCust;

                int value = 0;
                if (!DicProj.TryGetValue(comboBoxProjP6.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
                cmd.Parameters.AddWithValue("@proj", value);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    int idCust = 0;

                    try
                    {
                        idCust = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        idCust = 0;
                    }

                    arrIdCust.Add(idCust);
                }

            }
            #endregion

            DicCust = new Dictionary<string, int>();
            string commandCu = "SELECT * FROM View_Customers WHERE id_proj = @proj";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {

                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                int value = 0;
                if (!DicProj.TryGetValue(comboBoxProjP6.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
                cmd.Parameters.AddWithValue("@proj", value);
                cmd.CommandText = commandCu;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int Id = 0;
                    String strEmpCus = "";

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
                        strEmpCus += rdr.GetString(1);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {

                        strEmpCus += " ";
                    }
                    try
                    {
                        strEmpCus += rdr.GetString(2);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {
                        strEmpCus += " ";

                    }
                    try
                    {
                        strEmpCus += rdr.GetString(3);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {
                        strEmpCus += " ";

                    }
                    try
                    {
                        rdr.GetInt32(4);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ошибка конвертации " + Id);
                    }
                    DicCust.Add(strEmpCus, Id);
                    #endregion
                }

                foreach (var item in DicCust)
                {
                    listBoxCustP6.Items.Add(item.Key);
                }

            }
        }

        private void buttonOkP6_Click(object sender, EventArgs e)
        {
            string resultValid = "";
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());
            val.setText(comboBoxProjP6.Text);
            if (!val.check())
            {
                resultValid += val.getCause() + "\n";

            }

            if (resultValid == "")
            {
                if (listBoxEmplP6.Items.Count == 0)
                {
                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();
                        #region вызов хп DelProj

                        using (SqlCommand cmd = new SqlCommand("DelProj", sql_connect))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter param1 = new SqlParameter();
                            param1.ParameterName = "@id";
                            param1.SqlDbType = SqlDbType.Int;
                            int Id = 0;
                            if (!DicProj.TryGetValue(comboBoxProjP6.Text, out Id)) MessageBox.Show("ошибка поиска значения по ключу projId");
                            param1.Value = Id;
                            param1.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param1);

                            cmd.ExecuteNonQuery();
                        }

                        #endregion

                    }

                    Control.ControlCollection c = panel5.Controls;
                    ClearAll(c);
                    HideAllPanel();
                    menuStrip1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Можно удалить только пустой проект.");
                }
            }
            else MessageBox.Show(resultValid);
        }

        private void buttonCancelP6_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel6.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }

        private void пустойПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel6.Controls;
            ClearAll(c);

            DicProj = new Dictionary<String, int>();
            string commandText = "SELECT id, project_name FROM projects";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int id = 0;
                    String str = "";

                    try
                    {
                        id = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ошибка конвертации id");
                    }
                    try
                    {
                        str += rdr.GetString(1);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("ошибка конвертации имени проекта");
                    }

                    DicProj.Add(str, id);
                    #endregion
                }
                foreach (var item in DicProj.Keys)
                {
                    comboBoxProjP6.Items.Add(item.ToString());
                }

            }

            panel6.Visible = true;
            panel6.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
        }

        #endregion

        #region удалить заказчика из базы
        private void заказчикаИзБазыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel7.Controls;
            ClearAll(c);

            DicCust = new Dictionary<string, int>();
            string commandCu = "SELECT * FROM bosses";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {

                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandCu;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int Id = 0;
                    String strEmpCus = "";

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
                        strEmpCus += rdr.GetString(1);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {

                        strEmpCus += " ";
                    }
                    try
                    {
                        strEmpCus += rdr.GetString(2);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {
                        strEmpCus += " ";

                    }
                    try
                    {
                        strEmpCus += rdr.GetString(3);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {
                        strEmpCus += " ";

                    }

                    try
                    {
                        DicCust.Add(strEmpCus, Id);
                    }
                    catch
                    {
                    }

                    #endregion
                }

                foreach (var item in DicCust)
                {
                    comboBoxCusP7.Items.Add(item.Key);
                }
            }

            panel7.Visible = true;
            panel7.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;
        }

        private void comboBoxCusP7_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxProjP7.Items.Clear();
            #region все id проектов заказчика
            string commandText = "SELECT id_proj FROM View_Customers WHERE id = @cust";
            List<int> arrIdProj = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                int value = 0;
                if (!DicCust.TryGetValue(comboBoxCusP7.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
                cmd.Parameters.AddWithValue("@cust", value);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    int idCust = 0;

                    try
                    {
                        idCust = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        idCust = 0;
                    }
                    if (idCust != 0) arrIdProj.Add(idCust);
                }

            }
            #endregion

            if (arrIdProj.Count > 0)
            {

                #region вывод всех проектов заказчика
                string commandText2 = "SELECT project_name FROM projects WHERE id = @pr_id";
                foreach (var item in arrIdProj)
                {
                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();
                        SqlCommand cmd = sql_connect.CreateCommand();
                        cmd.CommandText = commandText2;
                        cmd.Parameters.AddWithValue("@pr_id", item);
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            listBoxProjP7.Items.Add(rdr.GetString(0));
                        }
                    }
                }
                #endregion
            }
        }

        private void buttonOkP7_Click(object sender, EventArgs e)
        {

            if (listBoxProjP7.Items.Count == 0 && comboBoxCusP7.Text != "")
            {
                #region удалить заказчика
                int Id_cust = 0;
                if (!DicCust.TryGetValue(comboBoxCusP7.Text, out Id_cust)) MessageBox.Show("ошибка поиска значения по ключу");
                int Id_people = 0;
                int Id_user = 0;

                string commandText = "SELECT * FROM customers WHERE id = @cust";
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();

                    using (SqlCommand cmd = new SqlCommand(commandText, sql_connect))
                    {

                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@cust";
                        param1.SqlDbType = SqlDbType.Int;

                        param1.Value = Id_cust;
                        param1.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param1);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            try
                            {
                                rdr.GetInt32(0);
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {
                                Id_user = rdr.GetInt32(1);
                            }
                            catch (Exception)
                            {
                                Id_user = 0;
                            }
                            try
                            {
                                Id_people = rdr.GetInt32(2);
                            }
                            catch (Exception)
                            {
                                Id_people = 0;
                            }
                            try
                            {
                                rdr.GetString(3);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
                if (Id_cust != 0 && Id_people != 0 && Id_user != 0)
                {
                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();

                        #region вызов хп Del_Cust
                        using (SqlCommand cmd = new SqlCommand("Del_Cust", sql_connect))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Входной параметр.
                            SqlParameter param2 = new SqlParameter();
                            param2.ParameterName = "@Id_cust";
                            param2.SqlDbType = SqlDbType.Int;
                            param2.Value = Id_cust;
                            param2.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param2);

                            SqlParameter param3 = new SqlParameter();
                            param3.ParameterName = "@Id_people";
                            param3.SqlDbType = SqlDbType.Int;
                            param3.Value = Id_people;
                            param3.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param3);

                            SqlParameter param4 = new SqlParameter();
                            param4.ParameterName = "@Id_user";
                            param4.SqlDbType = SqlDbType.Int;
                            param4.Value = Id_user;
                            param4.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param4);

                            // Выполнение хранимой процедуры.
                            cmd.ExecuteNonQuery();
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("Удаление не выполненно, {0} {1} {2}", Id_cust, Id_people, Id_user));
                }
                #endregion

                Control.ControlCollection c = panel7.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Нельзя удалить заказчика с проектами, или заказчик не выбран");
            }
        }

        private void buttonCancelP7_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel7.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }

        #endregion

        #region удалить работника из базы
        private void работникаИзБазыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel9.Controls;
            ClearAll(c);

            DicEmp = new Dictionary<string, int>();
            string commandCu = "SELECT * FROM View_enployee";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {

                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandCu;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int Id = 0;
                    String strEmpCus = "";

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
                        strEmpCus += rdr.GetString(1);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {

                        strEmpCus += " ";
                    }
                    try
                    {
                        strEmpCus += rdr.GetString(2);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {
                        strEmpCus += " ";

                    }
                    try
                    {
                        strEmpCus += rdr.GetString(3);
                        strEmpCus += " ";
                    }
                    catch (Exception)
                    {
                        strEmpCus += " ";

                    }

                    try
                    {
                        DicEmp.Add(strEmpCus, Id);
                    }
                    catch
                    {
                    }

                    #endregion
                }

                foreach (var item in DicEmp)
                {
                    comboBoxEmplP9.Items.Add(item.Key);
                }
            }

            panel9.Visible = true;
            panel9.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;

        }

        private void comboBoxEmplP9_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxProjP9.Items.Clear();
            #region все проекты в которых учавствует работник
            string commandText = "SELECT project_id FROM projects_employees WHERE employee_id = @empl";
            List<int> arrIdProj = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;

                int value = 0;
                if (!DicEmp.TryGetValue(comboBoxEmplP9.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
                cmd.Parameters.AddWithValue("@empl", value);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    int idProj = 0;

                    try
                    {
                        idProj = rdr.GetInt32(0);
                    }
                    catch (Exception)
                    {
                        idProj = 0;
                    }
                    if (idProj != 0) arrIdProj.Add(idProj);
                }

            }
            #endregion

            if (arrIdProj.Count > 0)
            {

                #region вывод всех проектов
                string commandText2 = "SELECT project_name FROM projects WHERE id = @pr_id";
                foreach (var item in arrIdProj)
                {
                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();
                        SqlCommand cmd = sql_connect.CreateCommand();
                        cmd.CommandText = commandText2;
                        cmd.Parameters.AddWithValue("@pr_id", item);
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            listBoxProjP9.Items.Add(rdr.GetString(0));
                        }
                    }
                }
                #endregion
            }
        }

        private void buttonOkP9_Click(object sender, EventArgs e)
        {
            if (listBoxProjP9.Items.Count == 0 && comboBoxEmplP9.Text != "")
            {
                #region удалить работника

                int Id_people = 0;
                int Id_user = 0;
                if (!DicEmp.TryGetValue(comboBoxEmplP9.Text, out Id_user)) MessageBox.Show("ошибка поиска значения по ключу");

                string commandText = "SELECT * FROM employees WHERE user_id = @empl";
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {
                    sql_connect.Open();

                    using (SqlCommand cmd = new SqlCommand(commandText, sql_connect))
                    {

                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@empl";
                        param1.SqlDbType = SqlDbType.Int;

                        param1.Value = Id_user;
                        param1.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param1);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            try
                            {
                                rdr.GetInt32(0);
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {
                                Id_people = rdr.GetInt32(1);
                            }
                            catch (Exception)
                            {
                                Id_people = 0;
                            }
                            try
                            {
                                rdr.GetInt32(2);
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {
                                rdr.GetString(3);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }

                if (Id_people != 0 && Id_user != 0)
                {
                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();

                        #region вызов хп Del_Empl
                        using (SqlCommand cmd = new SqlCommand("Del_Empl", sql_connect))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter param3 = new SqlParameter();
                            param3.ParameterName = "@Id_people";
                            param3.SqlDbType = SqlDbType.Int;
                            param3.Value = Id_people;
                            param3.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param3);

                            SqlParameter param4 = new SqlParameter();
                            param4.ParameterName = "@Id_user";
                            param4.SqlDbType = SqlDbType.Int;
                            param4.Value = Id_user;
                            param4.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param4);

                            // Выполнение хранимой процедуры.
                            cmd.ExecuteNonQuery();
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("Удаление не выполненно, {0} {1} ", Id_people, Id_user));
                }

                #endregion

                Control.ControlCollection c = panel9.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Нельзя удалить работника участвующего в проекте или возможно работник не выбран");
            }

        }

        private void buttonCancelP9_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel9.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

        #region редактировать данные о пользователях
        private void информациюОПользователяхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel8.Controls;
            ClearAll(c);
            Control.ControlCollection c2 = panel1_in_P8.Controls;
            ClearAll(c2);
            Control.ControlCollection c3 = panel_2_in_P8.Controls;
            ClearAll(c3);

            fillComboBox();
            panel8.Visible = true;
            panel8.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;

            if (radioButtonCustP8.Checked)
            {
                panel_2_in_P8.Visible = false;
                panel1_in_P8.Visible = true;
            }
            else
            {
                panel_2_in_P8.Visible = true;
                panel1_in_P8.Visible = false;
            }
        }

        private void comboBoxUserP8_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (radioButtonCustP8.Checked)
            {
                TempCust = CustList[comboBoxUserP8.SelectedIndex];

                textBox_log_P1_in_P8.Text = TempCust.Login;
                textBox_pass_P1_in_P8.Text = TempCust.Password;
                textBox_user_type_P1_in_P8.Text = TempCust.UserType;
                textBox_Sur_P1_in_P8.Text = TempCust.Surname;
                textBox_name_P1_in_P8.Text = TempCust.Name;
                textBox_Pat_P1_in_P8.Text = TempCust.Patronymic;
                textBox_phon_P1_in_P8.Text = TempCust.Phone_number;
                textBox_email_P1_in_P8.Text = TempCust.Email;
                textBox_info_P1_in_P8.Text = TempCust.Info;
            }
            else
            {
                TempEmp = EmpList[comboBoxUserP8.SelectedIndex];

                textBox_log_P2_in_P8.Text = TempEmp.Login;
                textBox_pass_P2_in_P8.Text = TempEmp.Password;
                textBox_type_P2_in_P8.Text = TempEmp.UserType;
                textBox_Sur_P2_in_P8.Text = TempEmp.Surname;
                textBox_name_P2_in_P8.Text = TempEmp.Name;
                textBox_Pat_P2_in_P8.Text = TempEmp.Patronymic;
                textBox_Phone_P2_in_P8.Text = TempEmp.Phone_number;
                textBox_Email_P2_in_P8.Text = TempEmp.Email;
                comboBox_role_P2_in_P8.DataSource = prof;
                comboBox_role_P2_in_P8.SelectedIndex = prof.IndexOf(TempEmp.Role);
                checkBox_send_P2_in_P8.Checked = TempEmp.Send;
            }
        }

        private void buttonOkP8_Click(object sender, EventArgs e)
        {
            string resultValid = "";

            if (radioButtonCustP8.Checked)
            {

                #region валидация
                Validator val = new Validator();
                val.addValidator(new InOneBoat.Validators.IsFilled());

                foreach (var item in panel1_in_P8.Controls)
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
                    val.addValidator(new InOneBoat.Validators.NameValid());
                    val.setText(textBox_Sur_P1_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                    val.setText(textBox_name_P1_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                    val.setText(textBox_Pat_P1_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";
                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.PhoneValid());
                    val.setText(textBox_phon_P1_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.EmailValid());
                    val.setText(textBox_email_P1_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.LoginValid());
                    val.setText(textBox_log_P1_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.PassValid());
                    val.setText(textBox_pass_P1_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                #endregion

                if (resultValid == "")
                {
                    TempCust.SetAll(
                        textBox_log_P1_in_P8.Text,
                        textBox_pass_P1_in_P8.Text,
                        textBox_user_type_P1_in_P8.Text,
                        textBox_Sur_P1_in_P8.Text,
                        textBox_name_P1_in_P8.Text,
                        textBox_Pat_P1_in_P8.Text,
                        textBox_phon_P1_in_P8.Text,
                        textBox_email_P1_in_P8.Text,
                        textBox_info_P1_in_P8.Text
                        );
                }
                else MessageBox.Show(resultValid);
            }
            else
            {
                #region валидация
                Validator val = new Validator();
                val.addValidator(new InOneBoat.Validators.IsFilled());

                foreach (var item in panel_2_in_P8.Controls)
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
                    val.addValidator(new InOneBoat.Validators.NameValid());
                    val.setText(textBox_Sur_P2_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                    val.setText(textBox_name_P2_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                    val.setText(textBox_Pat_P2_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";
                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.PhoneValid());
                    val.setText(textBox_Phone_P2_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.EmailValid());
                    val.setText(textBox_Email_P2_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.LoginValid());
                    val.setText(textBox_log_P2_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                if (resultValid == "")
                {
                    val = new Validator();
                    val.addValidator(new InOneBoat.Validators.PassValid());
                    val.setText(textBox_pass_P2_in_P8.Text);
                    if (!val.check()) resultValid += val.getCause() + "\n";

                }

                if (resultValid == "")
                {
                    if (textBox_type_P2_in_P8.Text == "admin" && comboBox_role_P2_in_P8.Text != "Admin")
                        resultValid += "Админ не может быть ни кем, кроме как админом." + "\n";
                    if (textBox_type_P2_in_P8.Text == "employee" && comboBox_role_P2_in_P8.Text == "Admin")
                        resultValid += "Работник не может быть админом." + "\n";
                }

                #endregion
                if (resultValid == "")
                {
                    TempEmp.SetAll(
                    textBox_log_P2_in_P8.Text,
                    textBox_pass_P2_in_P8.Text,
                    textBox_type_P2_in_P8.Text,
                    textBox_Sur_P2_in_P8.Text,
                    textBox_name_P2_in_P8.Text,
                    textBox_Pat_P2_in_P8.Text,
                    textBox_Phone_P2_in_P8.Text,
                    textBox_Email_P2_in_P8.Text,
                    comboBox_role_P2_in_P8.Text,
                    checkBox_send_P2_in_P8.Checked
                        );
                }
                else MessageBox.Show(resultValid);
            }
            if (resultValid == "")
            {
                Control.ControlCollection c = panel8.Controls;
                ClearAll(c);
                Control.ControlCollection c2 = panel1_in_P8.Controls;
                ClearAll(c2);
                Control.ControlCollection c3 = panel_2_in_P8.Controls;
                ClearAll(c3);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
        }

        private void buttonCancelP8_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel8.Controls;
            ClearAll(c);
            Control.ControlCollection c2 = panel1_in_P8.Controls;
            ClearAll(c2);
            Control.ControlCollection c3 = panel_2_in_P8.Controls;
            ClearAll(c3);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }

        private void radioButtonCustP8_CheckedChanged(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel8.Controls;
            ClearAll(c);
            Control.ControlCollection c2 = panel1_in_P8.Controls;
            ClearAll(c2);
            Control.ControlCollection c3 = panel_2_in_P8.Controls;
            ClearAll(c3);

            if (radioButtonCustP8.Checked)
            {
                panel_2_in_P8.Visible = false;
                panel1_in_P8.Visible = true;
            }
            else
            {
                panel_2_in_P8.Visible = true;
                panel1_in_P8.Visible = false;
            }
            fillComboBox();
        }

        private void fillComboBox()
        {
            Control.ControlCollection c = panel8.Controls;
            ClearAll(c);

            if (radioButtonCustP8.Checked == true)
            {


                CustList = new List<Customer>();
                string commandCu = "SELECT id FROM customer";
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {

                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandCu;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        #region присвоение
                        int Id = 0;

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
                            CustList.Add(new Customer(connect, Id));
                        }
                        catch
                        {

                        }

                        #endregion
                    }

                    foreach (var item in CustList)
                    {
                        comboBoxUserP8.Items.Add(item.GetSNP());
                    }
                }
            }
            else
            {

                EmpList = new List<Employee>();
                string commandCu = "SELECT id FROM Employee";
                using (SqlConnection sql_connect = new SqlConnection(connect))
                {

                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = commandCu;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        #region присвоение
                        int Id = 0;

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
                            EmpList.Add(new Employee(connect, Id));
                        }
                        catch
                        {
                        }

                        #endregion
                    }

                    foreach (var item in EmpList)
                    {
                        comboBoxUserP8.Items.Add(item.GetSNP());
                    }
                }
            }
        }
        #endregion

        #region редактировать данные о проекте
        private void информациюОПроектеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel10.Controls;
            ClearAll(c);

            DicProj = new Dictionary<string, int>();
            string commandCu = "SELECT * FROM projects";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {

                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandCu;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int Id = 0;
                    String strPr = "";

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
                        strPr += rdr.GetString(1);
                    }
                    catch (Exception)
                    {

                        strPr += " ";
                    }

                    try
                    {
                        DicProj.Add(strPr, Id);
                    }
                    catch
                    {
                    }

                    #endregion
                }

                foreach (var item in DicProj)
                {
                    comboBoxProjP10.Items.Add(item.Key);
                }
            }

            panel10.Visible = true;
            panel10.Dock = DockStyle.Fill;
            menuStrip1.Enabled = false;

        }

        private void comboBoxProjP10_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempTxt = "";
            string commandTextCust = "SELECT description FROM projects WHERE id = @proj";
            textBoxNameP10.Text = comboBoxProjP10.Text;
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandTextCust;

                int value = 0;
                if (!DicProj.TryGetValue(comboBoxProjP10.Text, out value)) MessageBox.Show("ошибка поиска значения по ключу");
                cmd.Parameters.AddWithValue("@proj", value);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    try
                    {
                        textBoxDescrP10.Text = tempTxt = rdr.GetString(0);
                    }
                    catch (Exception)
                    {
                        textBoxDescrP10.Text = tempTxt = "";
                    }

                }

            }
        }

        private void buttonOkP10_Click(object sender, EventArgs e)
        {
            string resultValid = "";
            Validator val = new Validator();
            val.addValidator(new InOneBoat.Validators.IsFilled());

            foreach (var item in panel10.Controls)
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
                if (textBoxNameP10.Text != comboBoxProjP10.Text)
                {
                    string comm = "UPDATE projects SET project_name = @proj_name";
                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();
                        SqlCommand cmd = sql_connect.CreateCommand();
                        cmd.CommandText = comm;
                        cmd.Parameters.AddWithValue("@proj_name", textBoxNameP10.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (textBoxDescrP10.Text != tempTxt)
                {
                    string comm = "UPDATE projects SET description = @d";
                    using (SqlConnection sql_connect = new SqlConnection(connect))
                    {
                        sql_connect.Open();
                        SqlCommand cmd = sql_connect.CreateCommand();
                        cmd.CommandText = comm;
                        cmd.Parameters.AddWithValue("@d", textBoxDescrP10.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                Control.ControlCollection c = panel10.Controls;
                ClearAll(c);
                HideAllPanel();
                menuStrip1.Enabled = true;
            }
            else MessageBox.Show(resultValid);
        }

        private void buttonCancelP10_Click(object sender, EventArgs e)
        {
            Control.ControlCollection c = panel10.Controls;
            ClearAll(c);
            HideAllPanel();
            menuStrip1.Enabled = true;
        }
        #endregion

    }
}
