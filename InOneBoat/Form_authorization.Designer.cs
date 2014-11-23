namespace InOneBoat
{
    partial class Form_authorization
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.maskedTextBoxPass = new System.Windows.Forms.MaskedTextBox();
            this.buttonOkAuthor = new System.Windows.Forms.Button();
            this.linkLabelRecover = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "пароль";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(107, 51);
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(374, 20);
            this.textBoxLog.TabIndex = 2;
            this.textBoxLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLog_KeyDown);
            // 
            // maskedTextBoxPass
            // 
            this.maskedTextBoxPass.Location = new System.Drawing.Point(107, 108);
            this.maskedTextBoxPass.Name = "maskedTextBoxPass";
            this.maskedTextBoxPass.PasswordChar = '*';
            this.maskedTextBoxPass.Size = new System.Drawing.Size(374, 20);
            this.maskedTextBoxPass.TabIndex = 3;
            this.maskedTextBoxPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBoxPass_KeyDown);
            // 
            // buttonOkAuthor
            // 
            this.buttonOkAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOkAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOkAuthor.Location = new System.Drawing.Point(406, 221);
            this.buttonOkAuthor.Name = "buttonOkAuthor";
            this.buttonOkAuthor.Size = new System.Drawing.Size(75, 23);
            this.buttonOkAuthor.TabIndex = 4;
            this.buttonOkAuthor.Text = "Вход";
            this.buttonOkAuthor.UseVisualStyleBackColor = true;
            this.buttonOkAuthor.Click += new System.EventHandler(this.buttonOkAuthor_Click);
            // 
            // linkLabelRecover
            // 
            this.linkLabelRecover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelRecover.AutoSize = true;
            this.linkLabelRecover.Location = new System.Drawing.Point(37, 221);
            this.linkLabelRecover.Name = "linkLabelRecover";
            this.linkLabelRecover.Size = new System.Drawing.Size(116, 13);
            this.linkLabelRecover.TabIndex = 5;
            this.linkLabelRecover.TabStop = true;
            this.linkLabelRecover.Text = "восстановить пароль";
            this.linkLabelRecover.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRecover_LinkClicked);
            // 
            // Form_authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 269);
            this.Controls.Add(this.linkLabelRecover);
            this.Controls.Add(this.buttonOkAuthor);
            this.Controls.Add(this.maskedTextBoxPass);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход в систему";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxPass;
        private System.Windows.Forms.Button buttonOkAuthor;
        private System.Windows.Forms.LinkLabel linkLabelRecover;
    }
}