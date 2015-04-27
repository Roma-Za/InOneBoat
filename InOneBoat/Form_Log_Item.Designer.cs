namespace InOneBoat
{
    partial class Form_Log_Item
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
            this.dateTimePickerDay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_start = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_finish = new System.Windows.Forms.NumericUpDown();
            this.label_all = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.listBox_tasks_today = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_finish)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerDay
            // 
            this.dateTimePickerDay.Location = new System.Drawing.Point(12, 26);
            this.dateTimePickerDay.Name = "dateTimePickerDay";
            this.dateTimePickerDay.Size = new System.Drawing.Size(142, 20);
            this.dateTimePickerDay.TabIndex = 0;
            this.dateTimePickerDay.ValueChanged += new System.EventHandler(this.dateTimePickerDay_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Начало";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Окончание";
            // 
            // numericUpDown_start
            // 
            this.numericUpDown_start.Location = new System.Drawing.Point(15, 107);
            this.numericUpDown_start.Name = "numericUpDown_start";
            this.numericUpDown_start.Size = new System.Drawing.Size(41, 20);
            this.numericUpDown_start.TabIndex = 3;
            this.numericUpDown_start.ValueChanged += new System.EventHandler(this.numericUpDown_start_ValueChanged);
            // 
            // numericUpDown_finish
            // 
            this.numericUpDown_finish.Location = new System.Drawing.Point(114, 107);
            this.numericUpDown_finish.Name = "numericUpDown_finish";
            this.numericUpDown_finish.Size = new System.Drawing.Size(41, 20);
            this.numericUpDown_finish.TabIndex = 3;
            this.numericUpDown_finish.ValueChanged += new System.EventHandler(this.numericUpDown_start_ValueChanged);
            // 
            // label_all
            // 
            this.label_all.AutoSize = true;
            this.label_all.Location = new System.Drawing.Point(44, 158);
            this.label_all.Name = "label_all";
            this.label_all.Size = new System.Drawing.Size(49, 13);
            this.label_all.TabIndex = 4;
            this.label_all.Text = "Всего: 0";
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ok.Location = new System.Drawing.Point(114, 218);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 5;
            this.button_ok.Text = "Ок";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(298, 218);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // listBox_tasks_today
            // 
            this.listBox_tasks_today.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_tasks_today.FormattingEnabled = true;
            this.listBox_tasks_today.Location = new System.Drawing.Point(179, 52);
            this.listBox_tasks_today.Name = "listBox_tasks_today";
            this.listBox_tasks_today.Size = new System.Drawing.Size(302, 147);
            this.listBox_tasks_today.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Задачи сегодня";
            // 
            // Form_Log_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 269);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox_tasks_today);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label_all);
            this.Controls.Add(this.numericUpDown_finish);
            this.Controls.Add(this.numericUpDown_start);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerDay);
            this.Name = "Form_Log_Item";
            this.Text = "Логирование времени";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_finish)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_start;
        private System.Windows.Forms.NumericUpDown numericUpDown_finish;
        private System.Windows.Forms.Label label_all;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.ListBox listBox_tasks_today;
        private System.Windows.Forms.Label label3;
    }
}