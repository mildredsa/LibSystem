namespace LibSystem
{
    partial class Login
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnReg = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lnkChangePass = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lnkChangePass);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.btnReg);
            this.groupBox1.Controls.Add(this.btnLog);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 522);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LibSystem.Properties.Resources.profile;
            this.pictureBox1.Location = new System.Drawing.Point(109, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(40, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 19);
            this.label2.TabIndex = 40;
            this.label2.Text = "Password";
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Font = new System.Drawing.Font("Cascadia Code", 8F);
            this.txtPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtPass.Location = new System.Drawing.Point(44, 313);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(272, 23);
            this.txtPass.TabIndex = 39;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // btnReg
            // 
            this.btnReg.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReg.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.btnReg.Location = new System.Drawing.Point(44, 439);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(272, 43);
            this.btnReg.TabIndex = 38;
            this.btnReg.Text = "Register";
            this.btnReg.UseVisualStyleBackColor = false;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLog.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.btnLog.Location = new System.Drawing.Point(44, 390);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(272, 43);
            this.btnLog.TabIndex = 37;
            this.btnLog.Text = "Login";
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(40, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 19);
            this.label1.TabIndex = 36;
            this.label1.Text = "Username";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.AliceBlue;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Cascadia Code", 8F);
            this.txtUser.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtUser.Location = new System.Drawing.Point(44, 232);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(272, 23);
            this.txtUser.TabIndex = 35;
            // 
            // lnkChangePass
            // 
            this.lnkChangePass.AutoSize = true;
            this.lnkChangePass.Location = new System.Drawing.Point(200, 348);
            this.lnkChangePass.Name = "lnkChangePass";
            this.lnkChangePass.Size = new System.Drawing.Size(116, 16);
            this.lnkChangePass.TabIndex = 42;
            this.lnkChangePass.TabStop = true;
            this.lnkChangePass.Text = "Forgot Password?";
            this.lnkChangePass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChangePass_LinkClicked);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(385, 540);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lnkChangePass;
    }
}

