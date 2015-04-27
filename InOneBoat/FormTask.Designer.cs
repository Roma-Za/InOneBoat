namespace InOneBoat
{
    partial class FormTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.логированиеВремениToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статусToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTaskName = new System.Windows.Forms.Label();
            this.panel_Task = new System.Windows.Forms.Panel();
            this.button_P_Task_Cancel = new System.Windows.Forms.Button();
            this.button_Add_SubTask = new System.Windows.Forms.Button();
            this.listBox_Attach_List = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.richTextBoxComments = new System.Windows.Forms.RichTextBox();
            this.label_P_Task_prior = new System.Windows.Forms.Label();
            this.label_P_Task_status = new System.Windows.Forms.Label();
            this.button_watch = new System.Windows.Forms.Button();
            this.button_add_com = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.panel_New_Task = new System.Windows.Forms.Panel();
            this.button_P_New_Task_Cancel = new System.Windows.Forms.Button();
            this.button_P_New_Task_Ok = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.checkedListBox_P_New_Task_Partic = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_P_New_Task_prior = new System.Windows.Forms.ComboBox();
            this.checkBox_P_new_Task_Start = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_P_New_Task_Est = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_P_New_Task_Descrip = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_P_New_Task_Summ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_Edit_Task = new System.Windows.Forms.Panel();
            this.buttonDelAtta = new System.Windows.Forms.Button();
            this.checkedListBox_P_Del_Attach = new System.Windows.Forms.CheckedListBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label15 = new System.Windows.Forms.Label();
            this.dateTimePicker_P_Edit = new System.Windows.Forms.DateTimePicker();
            this.button_P_Edit_Del_Log = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.checkedListBox_P_Edit_Log = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_P_Edit_Status = new System.Windows.Forms.ComboBox();
            this.comboBox_P_Edit_Prior = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_P_Edit_Estimate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_P_Edit_Discr = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_P_Edit_Summ = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.panel_Task.SuspendLayout();
            this.panel_New_Task.SuspendLayout();
            this.panel_Edit_Task.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.логированиеВремениToolStripMenuItem,
            this.статусToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(899, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // логированиеВремениToolStripMenuItem
            // 
            this.логированиеВремениToolStripMenuItem.Name = "логированиеВремениToolStripMenuItem";
            this.логированиеВремениToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.логированиеВремениToolStripMenuItem.Text = "Логирование времени";
            this.логированиеВремениToolStripMenuItem.Click += new System.EventHandler(this.логированиеВремениToolStripMenuItem_Click);
            // 
            // статусToolStripMenuItem
            // 
            this.статусToolStripMenuItem.Name = "статусToolStripMenuItem";
            this.статусToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.статусToolStripMenuItem.Text = "Редактирование";
            this.статусToolStripMenuItem.Click += new System.EventHandler(this.статусToolStripMenuItem_Click);
            // 
            // labelTaskName
            // 
            this.labelTaskName.AutoSize = true;
            this.labelTaskName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTaskName.ForeColor = System.Drawing.Color.Blue;
            this.labelTaskName.Location = new System.Drawing.Point(3, 10);
            this.labelTaskName.Name = "labelTaskName";
            this.labelTaskName.Size = new System.Drawing.Size(45, 18);
            this.labelTaskName.TabIndex = 1;
            this.labelTaskName.Text = "Task";
            // 
            // panel_Task
            // 
            this.panel_Task.Controls.Add(this.button_P_Task_Cancel);
            this.panel_Task.Controls.Add(this.button_Add_SubTask);
            this.panel_Task.Controls.Add(this.listBox_Attach_List);
            this.panel_Task.Controls.Add(this.label16);
            this.panel_Task.Controls.Add(this.richTextBoxComments);
            this.panel_Task.Controls.Add(this.label_P_Task_prior);
            this.panel_Task.Controls.Add(this.label_P_Task_status);
            this.panel_Task.Controls.Add(this.button_watch);
            this.panel_Task.Controls.Add(this.button_add_com);
            this.panel_Task.Controls.Add(this.label2);
            this.panel_Task.Controls.Add(this.labelTaskName);
            this.panel_Task.Controls.Add(this.label1);
            this.panel_Task.Controls.Add(this.textBoxDescription);
            this.panel_Task.Location = new System.Drawing.Point(12, 27);
            this.panel_Task.Name = "panel_Task";
            this.panel_Task.Size = new System.Drawing.Size(32, 402);
            this.panel_Task.TabIndex = 2;
            this.panel_Task.Visible = false;
            // 
            // button_P_Task_Cancel
            // 
            this.button_P_Task_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_P_Task_Cancel.Location = new System.Drawing.Point(445, 376);
            this.button_P_Task_Cancel.Name = "button_P_Task_Cancel";
            this.button_P_Task_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_P_Task_Cancel.TabIndex = 13;
            this.button_P_Task_Cancel.Text = "Назад";
            this.button_P_Task_Cancel.UseVisualStyleBackColor = true;
            this.button_P_Task_Cancel.Click += new System.EventHandler(this.button_P_Task_Cancel_Click);
            // 
            // button_Add_SubTask
            // 
            this.button_Add_SubTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Add_SubTask.Location = new System.Drawing.Point(254, 376);
            this.button_Add_SubTask.Name = "button_Add_SubTask";
            this.button_Add_SubTask.Size = new System.Drawing.Size(134, 23);
            this.button_Add_SubTask.TabIndex = 12;
            this.button_Add_SubTask.Text = "Добавить подзадачу";
            this.button_Add_SubTask.UseVisualStyleBackColor = true;
            this.button_Add_SubTask.Click += new System.EventHandler(this.button_Add_SubTask_Click);
            // 
            // listBox_Attach_List
            // 
            this.listBox_Attach_List.FormattingEnabled = true;
            this.listBox_Attach_List.Location = new System.Drawing.Point(522, 68);
            this.listBox_Attach_List.Name = "listBox_Attach_List";
            this.listBox_Attach_List.Size = new System.Drawing.Size(301, 108);
            this.listBox_Attach_List.TabIndex = 11;
            this.listBox_Attach_List.DoubleClick += new System.EventHandler(this.listBox_Attach_List_DoubleClick);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(442, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "Приложения";
            // 
            // richTextBoxComments
            // 
            this.richTextBoxComments.AcceptsTab = true;
            this.richTextBoxComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxComments.Location = new System.Drawing.Point(6, 200);
            this.richTextBoxComments.Name = "richTextBoxComments";
            this.richTextBoxComments.ReadOnly = true;
            this.richTextBoxComments.Size = new System.Drawing.Size(20, 170);
            this.richTextBoxComments.TabIndex = 8;
            this.richTextBoxComments.Text = "";
            // 
            // label_P_Task_prior
            // 
            this.label_P_Task_prior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_P_Task_prior.AutoSize = true;
            this.label_P_Task_prior.Location = new System.Drawing.Point(-201, 39);
            this.label_P_Task_prior.Name = "label_P_Task_prior";
            this.label_P_Task_prior.Size = new System.Drawing.Size(64, 13);
            this.label_P_Task_prior.TabIndex = 7;
            this.label_P_Task_prior.Text = "Приоритет:";
            // 
            // label_P_Task_status
            // 
            this.label_P_Task_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_P_Task_status.AutoSize = true;
            this.label_P_Task_status.Location = new System.Drawing.Point(-201, 15);
            this.label_P_Task_status.Name = "label_P_Task_status";
            this.label_P_Task_status.Size = new System.Drawing.Size(82, 13);
            this.label_P_Task_status.TabIndex = 6;
            this.label_P_Task_status.Text = "Статус задачи:";
            // 
            // button_watch
            // 
            this.button_watch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_watch.Location = new System.Drawing.Point(-74, 376);
            this.button_watch.Name = "button_watch";
            this.button_watch.Size = new System.Drawing.Size(103, 23);
            this.button_watch.TabIndex = 5;
            this.button_watch.Text = "Подписаться";
            this.button_watch.UseVisualStyleBackColor = true;
            this.button_watch.Click += new System.EventHandler(this.button_watch_Click);
            // 
            // button_add_com
            // 
            this.button_add_com.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_add_com.Location = new System.Drawing.Point(6, 376);
            this.button_add_com.Name = "button_add_com";
            this.button_add_com.Size = new System.Drawing.Size(153, 23);
            this.button_add_com.TabIndex = 4;
            this.button_add_com.Text = "Добавить комментарий";
            this.button_add_com.UseVisualStyleBackColor = true;
            this.button_add_com.Click += new System.EventHandler(this.button_add_com_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Комментарии";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Описание";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(3, 68);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.Size = new System.Drawing.Size(411, 108);
            this.textBoxDescription.TabIndex = 0;
            // 
            // panel_New_Task
            // 
            this.panel_New_Task.Controls.Add(this.button_P_New_Task_Cancel);
            this.panel_New_Task.Controls.Add(this.button_P_New_Task_Ok);
            this.panel_New_Task.Controls.Add(this.label8);
            this.panel_New_Task.Controls.Add(this.checkedListBox_P_New_Task_Partic);
            this.panel_New_Task.Controls.Add(this.label7);
            this.panel_New_Task.Controls.Add(this.comboBox_P_New_Task_prior);
            this.panel_New_Task.Controls.Add(this.checkBox_P_new_Task_Start);
            this.panel_New_Task.Controls.Add(this.label6);
            this.panel_New_Task.Controls.Add(this.textBox_P_New_Task_Est);
            this.panel_New_Task.Controls.Add(this.label5);
            this.panel_New_Task.Controls.Add(this.textBox_P_New_Task_Descrip);
            this.panel_New_Task.Controls.Add(this.label4);
            this.panel_New_Task.Controls.Add(this.textBox_P_New_Task_Summ);
            this.panel_New_Task.Controls.Add(this.label3);
            this.panel_New_Task.Location = new System.Drawing.Point(66, 27);
            this.panel_New_Task.Name = "panel_New_Task";
            this.panel_New_Task.Size = new System.Drawing.Size(780, 402);
            this.panel_New_Task.TabIndex = 3;
            this.panel_New_Task.Visible = false;
            // 
            // button_P_New_Task_Cancel
            // 
            this.button_P_New_Task_Cancel.Location = new System.Drawing.Point(156, 337);
            this.button_P_New_Task_Cancel.Name = "button_P_New_Task_Cancel";
            this.button_P_New_Task_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_P_New_Task_Cancel.TabIndex = 13;
            this.button_P_New_Task_Cancel.Text = "Отмена";
            this.button_P_New_Task_Cancel.UseVisualStyleBackColor = true;
            this.button_P_New_Task_Cancel.Click += new System.EventHandler(this.button_P_New_Task_Cancel_Click);
            // 
            // button_P_New_Task_Ok
            // 
            this.button_P_New_Task_Ok.Location = new System.Drawing.Point(20, 337);
            this.button_P_New_Task_Ok.Name = "button_P_New_Task_Ok";
            this.button_P_New_Task_Ok.Size = new System.Drawing.Size(75, 23);
            this.button_P_New_Task_Ok.TabIndex = 12;
            this.button_P_New_Task_Ok.Text = "Ок";
            this.button_P_New_Task_Ok.UseVisualStyleBackColor = true;
            this.button_P_New_Task_Ok.Click += new System.EventHandler(this.button_P_New_Task_Ok_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Участники";
            // 
            // checkedListBox_P_New_Task_Partic
            // 
            this.checkedListBox_P_New_Task_Partic.FormattingEnabled = true;
            this.checkedListBox_P_New_Task_Partic.Location = new System.Drawing.Point(410, 184);
            this.checkedListBox_P_New_Task_Partic.Name = "checkedListBox_P_New_Task_Partic";
            this.checkedListBox_P_New_Task_Partic.Size = new System.Drawing.Size(248, 139);
            this.checkedListBox_P_New_Task_Partic.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Приоритет";
            // 
            // comboBox_P_New_Task_prior
            // 
            this.comboBox_P_New_Task_prior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_P_New_Task_prior.FormattingEnabled = true;
            this.comboBox_P_New_Task_prior.Items.AddRange(new object[] {
            "Высокий",
            "Средний",
            "Низкий"});
            this.comboBox_P_New_Task_prior.Location = new System.Drawing.Point(410, 121);
            this.comboBox_P_New_Task_prior.Name = "comboBox_P_New_Task_prior";
            this.comboBox_P_New_Task_prior.Size = new System.Drawing.Size(129, 21);
            this.comboBox_P_New_Task_prior.TabIndex = 8;
            // 
            // checkBox_P_new_Task_Start
            // 
            this.checkBox_P_new_Task_Start.AutoSize = true;
            this.checkBox_P_new_Task_Start.Location = new System.Drawing.Point(445, 67);
            this.checkBox_P_new_Task_Start.Name = "checkBox_P_new_Task_Start";
            this.checkBox_P_new_Task_Start.Size = new System.Drawing.Size(85, 17);
            this.checkBox_P_new_Task_Start.TabIndex = 7;
            this.checkBox_P_new_Task_Start.Text = "Стартовал?";
            this.checkBox_P_new_Task_Start.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(326, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Статус задачи";
            // 
            // textBox_P_New_Task_Est
            // 
            this.textBox_P_New_Task_Est.Location = new System.Drawing.Point(528, 8);
            this.textBox_P_New_Task_Est.Name = "textBox_P_New_Task_Est";
            this.textBox_P_New_Task_Est.Size = new System.Drawing.Size(130, 20);
            this.textBox_P_New_Task_Est.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ожидаемое время на выполнение (ч)";
            // 
            // textBox_P_New_Task_Descrip
            // 
            this.textBox_P_New_Task_Descrip.Location = new System.Drawing.Point(20, 55);
            this.textBox_P_New_Task_Descrip.Multiline = true;
            this.textBox_P_New_Task_Descrip.Name = "textBox_P_New_Task_Descrip";
            this.textBox_P_New_Task_Descrip.Size = new System.Drawing.Size(279, 217);
            this.textBox_P_New_Task_Descrip.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Описание";
            // 
            // textBox_P_New_Task_Summ
            // 
            this.textBox_P_New_Task_Summ.Location = new System.Drawing.Point(78, 8);
            this.textBox_P_New_Task_Summ.Name = "textBox_P_New_Task_Summ";
            this.textBox_P_New_Task_Summ.Size = new System.Drawing.Size(221, 20);
            this.textBox_P_New_Task_Summ.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Название";
            // 
            // panel_Edit_Task
            // 
            this.panel_Edit_Task.Controls.Add(this.buttonDelAtta);
            this.panel_Edit_Task.Controls.Add(this.checkedListBox_P_Del_Attach);
            this.panel_Edit_Task.Controls.Add(this.linkLabel1);
            this.panel_Edit_Task.Controls.Add(this.label15);
            this.panel_Edit_Task.Controls.Add(this.dateTimePicker_P_Edit);
            this.panel_Edit_Task.Controls.Add(this.button_P_Edit_Del_Log);
            this.panel_Edit_Task.Controls.Add(this.button1);
            this.panel_Edit_Task.Controls.Add(this.button2);
            this.panel_Edit_Task.Controls.Add(this.label9);
            this.panel_Edit_Task.Controls.Add(this.checkedListBox_P_Edit_Log);
            this.panel_Edit_Task.Controls.Add(this.label10);
            this.panel_Edit_Task.Controls.Add(this.comboBox_P_Edit_Status);
            this.panel_Edit_Task.Controls.Add(this.comboBox_P_Edit_Prior);
            this.panel_Edit_Task.Controls.Add(this.label11);
            this.panel_Edit_Task.Controls.Add(this.textBox_P_Edit_Estimate);
            this.panel_Edit_Task.Controls.Add(this.label12);
            this.panel_Edit_Task.Controls.Add(this.textBox_P_Edit_Discr);
            this.panel_Edit_Task.Controls.Add(this.label13);
            this.panel_Edit_Task.Controls.Add(this.textBox_P_Edit_Summ);
            this.panel_Edit_Task.Controls.Add(this.label14);
            this.panel_Edit_Task.Location = new System.Drawing.Point(865, 24);
            this.panel_Edit_Task.Name = "panel_Edit_Task";
            this.panel_Edit_Task.Size = new System.Drawing.Size(34, 402);
            this.panel_Edit_Task.TabIndex = 5;
            this.panel_Edit_Task.Visible = false;
            // 
            // buttonDelAtta
            // 
            this.buttonDelAtta.Location = new System.Drawing.Point(18, 278);
            this.buttonDelAtta.Name = "buttonDelAtta";
            this.buttonDelAtta.Size = new System.Drawing.Size(75, 28);
            this.buttonDelAtta.TabIndex = 19;
            this.buttonDelAtta.Text = "удалить ";
            this.buttonDelAtta.UseVisualStyleBackColor = true;
            this.buttonDelAtta.Click += new System.EventHandler(this.buttonDelAtta_Click);
            // 
            // checkedListBox_P_Del_Attach
            // 
            this.checkedListBox_P_Del_Attach.FormattingEnabled = true;
            this.checkedListBox_P_Del_Attach.HorizontalScrollbar = true;
            this.checkedListBox_P_Del_Attach.Location = new System.Drawing.Point(99, 248);
            this.checkedListBox_P_Del_Attach.Name = "checkedListBox_P_Del_Attach";
            this.checkedListBox_P_Del_Attach.Size = new System.Drawing.Size(302, 109);
            this.checkedListBox_P_Del_Attach.TabIndex = 18;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(19, 321);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(57, 13);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Добавить";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 259);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Приложения:";
            // 
            // dateTimePicker_P_Edit
            // 
            this.dateTimePicker_P_Edit.Location = new System.Drawing.Point(477, 180);
            this.dateTimePicker_P_Edit.Name = "dateTimePicker_P_Edit";
            this.dateTimePicker_P_Edit.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_P_Edit.TabIndex = 15;
            this.dateTimePicker_P_Edit.ValueChanged += new System.EventHandler(this.dateTimePicker_P_Edit_ValueChanged);
            // 
            // button_P_Edit_Del_Log
            // 
            this.button_P_Edit_Del_Log.Location = new System.Drawing.Point(477, 366);
            this.button_P_Edit_Del_Log.Name = "button_P_Edit_Del_Log";
            this.button_P_Edit_Del_Log.Size = new System.Drawing.Size(129, 23);
            this.button_P_Edit_Del_Log.TabIndex = 14;
            this.button_P_Edit_Del_Log.Text = "Удалить LogItem";
            this.button_P_Edit_Del_Log.UseVisualStyleBackColor = true;
            this.button_P_Edit_Del_Log.Click += new System.EventHandler(this.button_P_Edit_Del_Log_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 366);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Ок";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(407, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "LogItems";
            // 
            // checkedListBox_P_Edit_Log
            // 
            this.checkedListBox_P_Edit_Log.FormattingEnabled = true;
            this.checkedListBox_P_Edit_Log.HorizontalScrollbar = true;
            this.checkedListBox_P_Edit_Log.Location = new System.Drawing.Point(477, 214);
            this.checkedListBox_P_Edit_Log.Name = "checkedListBox_P_Edit_Log";
            this.checkedListBox_P_Edit_Log.Size = new System.Drawing.Size(333, 139);
            this.checkedListBox_P_Edit_Log.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(458, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Приоритет";
            // 
            // comboBox_P_Edit_Status
            // 
            this.comboBox_P_Edit_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_P_Edit_Status.FormattingEnabled = true;
            this.comboBox_P_Edit_Status.Items.AddRange(new object[] {
            "не стартовавшая",
            "в процессе",
            "выполнена",
            "переоткрыта",
            "закрыта"});
            this.comboBox_P_Edit_Status.Location = new System.Drawing.Point(610, 63);
            this.comboBox_P_Edit_Status.Name = "comboBox_P_Edit_Status";
            this.comboBox_P_Edit_Status.Size = new System.Drawing.Size(200, 21);
            this.comboBox_P_Edit_Status.TabIndex = 8;
            // 
            // comboBox_P_Edit_Prior
            // 
            this.comboBox_P_Edit_Prior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_P_Edit_Prior.FormattingEnabled = true;
            this.comboBox_P_Edit_Prior.Items.AddRange(new object[] {
            "высокий",
            "средний",
            "низкий"});
            this.comboBox_P_Edit_Prior.Location = new System.Drawing.Point(610, 116);
            this.comboBox_P_Edit_Prior.Name = "comboBox_P_Edit_Prior";
            this.comboBox_P_Edit_Prior.Size = new System.Drawing.Size(200, 21);
            this.comboBox_P_Edit_Prior.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(458, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Статус задачи";
            // 
            // textBox_P_Edit_Estimate
            // 
            this.textBox_P_Edit_Estimate.Location = new System.Drawing.Point(694, 12);
            this.textBox_P_Edit_Estimate.Name = "textBox_P_Edit_Estimate";
            this.textBox_P_Edit_Estimate.Size = new System.Drawing.Size(116, 20);
            this.textBox_P_Edit_Estimate.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(455, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(196, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Ожидаемое время на выполнение (ч)";
            // 
            // textBox_P_Edit_Discr
            // 
            this.textBox_P_Edit_Discr.Location = new System.Drawing.Point(20, 55);
            this.textBox_P_Edit_Discr.Multiline = true;
            this.textBox_P_Edit_Discr.Name = "textBox_P_Edit_Discr";
            this.textBox_P_Edit_Discr.Size = new System.Drawing.Size(381, 187);
            this.textBox_P_Edit_Discr.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Описание";
            // 
            // textBox_P_Edit_Summ
            // 
            this.textBox_P_Edit_Summ.Location = new System.Drawing.Point(78, 8);
            this.textBox_P_Edit_Summ.Name = "textBox_P_Edit_Summ";
            this.textBox_P_Edit_Summ.Size = new System.Drawing.Size(323, 20);
            this.textBox_P_Edit_Summ.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Название";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 441);
            this.Controls.Add(this.panel_Edit_Task);
            this.Controls.Add(this.panel_New_Task);
            this.Controls.Add(this.panel_Task);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormTask";
            this.Text = "FormTask";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_Task.ResumeLayout(false);
            this.panel_Task.PerformLayout();
            this.panel_New_Task.ResumeLayout(false);
            this.panel_New_Task.PerformLayout();
            this.panel_Edit_Task.ResumeLayout(false);
            this.panel_Edit_Task.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label labelTaskName;
        private System.Windows.Forms.ToolStripMenuItem логированиеВремениToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статусToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Task;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Panel panel_New_Task;
        private System.Windows.Forms.TextBox textBox_P_New_Task_Descrip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_P_New_Task_Summ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_P_New_Task_Est;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox_P_new_Task_Start;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_P_New_Task_prior;
        private System.Windows.Forms.Button button_P_New_Task_Cancel;
        private System.Windows.Forms.Button button_P_New_Task_Ok;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox checkedListBox_P_New_Task_Partic;
        private System.Windows.Forms.Label label_P_Task_prior;
        private System.Windows.Forms.Label label_P_Task_status;
        private System.Windows.Forms.Button button_watch;
        private System.Windows.Forms.Button button_add_com;
        private System.Windows.Forms.RichTextBox richTextBoxComments;
        private System.Windows.Forms.Panel panel_Edit_Task;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckedListBox checkedListBox_P_Edit_Log;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_P_Edit_Status;
        private System.Windows.Forms.ComboBox comboBox_P_Edit_Prior;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_P_Edit_Estimate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_P_Edit_Discr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_P_Edit_Summ;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dateTimePicker_P_Edit;
        private System.Windows.Forms.Button button_P_Edit_Del_Log;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox listBox_Attach_List;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonDelAtta;
        private System.Windows.Forms.CheckedListBox checkedListBox_P_Del_Attach;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_P_Task_Cancel;
        private System.Windows.Forms.Button button_Add_SubTask;
    }
}