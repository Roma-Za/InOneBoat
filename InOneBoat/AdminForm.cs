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
    public partial class AdminForm : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        public AuthorizationClass myData { set; get; }

        private Dictionary<String, int> DicBoss;
        private Dictionary<String, int> DicProj;
        private Dictionary<String, int> DicEmp;
        private Dictionary<String, int> DicCust;
        public AdminForm(AuthorizationClass ac)
        {
            InitializeComponent();
            myData = ac;

            HideAllPanel();
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
                catch (Exception e) {
                    MessageBox.Show(e.Message);
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
        }

        private void buttonOkP1_Click(object sender, EventArgs e)
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
            comboBoxUserTypeP4.SelectedIndex = 1;
            comboBoxUserTypeP4.Enabled = false;

        }

        private void buttonOkP4_Click(object sender, EventArgs e)
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
                    param4.Value = comboBoxUserTypeP4.SelectedIndex + 1;
                    param4.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param4);

                    // Выполнение хранимой процедуры.
                    cmd.ExecuteNonQuery();
                    idUser = (int)cmd.Parameters["@id"].Value;
                }
                #endregion

                #region вызов хп AddEmployee
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

            DicEmp = new Dictionary<string,int>();
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

                    DicEmp.Add(strEmp,Id);
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
                        MessageBox.Show("ProjId = " + projId);
                        param1.Value = projId;
                        param1.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@employee_id";
                        param2.SqlDbType = SqlDbType.Int;
                        int empId = 0;
                        if (!DicEmp.TryGetValue(chItems[i], out empId)) MessageBox.Show("ошибка поиска значения по ключу empId " + chItems[i]);
                        MessageBox.Show("empId = " + empId);
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
            if (listBoxEmplP6.Items.Count == 0 && listBoxCustP6.Items.Count==0)
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
                        //MessageBox.Show("ProjId = " + projId);
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
            else {
                MessageBox.Show("Можно удалить только пустой проект.");
            }
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
    }
}
