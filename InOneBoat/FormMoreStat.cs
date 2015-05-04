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
    public partial class FormMoreStat : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["dbProject"].ConnectionString;
        private DateTime dateMon;
        private Label[] Monday;
        private Label[] Tuesday;
        private Label[] Wednesday;
        private Label[] Thursday;
        private Label[] Friday;
        private Label[] Saturday;
        private Label[] Sunday;
        private List<Color> colArr = new List<Color>() { Color.Red, Color.Orange, Color.Purple, Color.Blue, Color.Brown, Color.DarkGreen, Color.DarkGoldenrod, Color.Yellow, Color.Gray, Color.Black };
        private List<LogItem> logItemList = new List<LogItem>();
        private List<int> listTaskSumm = new List<int>();
        private int empl_id;
        private int proj_id;
        public FormMoreStat(int id_Empl, int id_proj)
        {
            InitializeComponent();
            empl_id = id_Empl;
            proj_id = id_proj;
            Monday = new Label[] { label_1_8, label_1_9, label_1_10, label_1_11, label_1_12, label_1_13, label_1_14, label_1_15, label_1_16, label_1_17, label_1_18, label_1_19, label_1_20 };
            Tuesday = new Label[] { label_2_8, label_2_9, label_2_10, label_2_11, label_2_12, label_2_13, label_2_14, label_2_15, label_2_16, label_2_17, label_2_18, label_2_19, label_2_20 };
            Wednesday = new Label[] { label_3_8, label_3_9, label_3_10, label_3_11, label_3_12, label_3_13, label_3_14, label_3_15, label_3_16, label_3_17, label_3_18, label_3_19, label_3_20 };
            Thursday = new Label[] { label_4_8, label_4_9, label_4_10, label_4_11, label_4_12, label_4_13, label_4_14, label_4_15, label_4_16, label_4_17, label_4_18, label_4_19, label_4_20 };
            Friday = new Label[] { label_5_8, label_5_9, label_5_10, label_5_11, label_5_12, label_5_13, label_5_14, label_5_15, label_5_16, label_5_17, label_5_18, label_5_19, label_5_20 };
            Saturday = new Label[] { label_6_8, label_6_9, label_6_10, label_6_11, label_6_12, label_6_13, label_6_14, label_6_15, label_6_16, label_6_17, label_6_18, label_6_19, label_6_20 };
            Sunday = new Label[] { label_7_8, label_7_9, label_7_10, label_7_11, label_7_12, label_7_13, label_7_14, label_7_15, label_7_16, label_7_17, label_7_18, label_7_19, label_7_20 };
            ProjAndEmplLabelFill();
        }

        private void ProjAndEmplLabelFill()
        {

            string commandText = "SELECT project_name FROM projects WHERE id = @proj";
            //List<int> arrIdEmpol = new List<int>();
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("@proj", proj_id);
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                string prName = "";
                try
                {
                    prName = "Проект: " + rdr.GetString(0);
                }
                catch (Exception)
                {
                    prName = "Проект: ";
                }
                label_Proj_Name.Text = prName;

            }


            string commandT = "SELECT * FROM View_enployee WHERE user_id = @id";
            using (SqlConnection sql_connect = new SqlConnection(connect))
            {
                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandT;
                cmd.Parameters.AddWithValue("@id", empl_id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int Id = 0;
                    String strEmp = "Сотрудник: ";

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
                    label_Empl_Name.Text = strEmp;
                    #endregion
                }
            }
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

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
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

                cmd.Parameters.AddWithValue("@empl", empl_id);
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
                    logItemList.Add(new LogItem(connect, ID, tId, st, fin, empl_id));
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
                        if (!listTaskSumm.Contains(item.Task_id))
                        {
                            listBox_legend.Items.Add(s);
                            listTaskSumm.Add(item.Task_id);
                        }
                    }
                }

            }
            #endregion
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

    }
}
