namespace ISUPDemo.UI
{
    partial class Update
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
            this.csFTPServerPort = new System.Windows.Forms.TextBox();
            this.csFTPServerIP = new System.Windows.Forms.TextBox();
            this.FTPServerPort = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.csPassword = new System.Windows.Forms.TextBox();
            this.csAccount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.csUpgradePackage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStartFTPUpdate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // csFTPServerPort
            // 
            this.csFTPServerPort.Location = new System.Drawing.Point(339, 30);
            this.csFTPServerPort.Name = "csFTPServerPort";
            this.csFTPServerPort.Size = new System.Drawing.Size(107, 21);
            this.csFTPServerPort.TabIndex = 15;
            // 
            // csFTPServerIP
            // 
            this.csFTPServerIP.Location = new System.Drawing.Point(102, 30);
            this.csFTPServerIP.Name = "csFTPServerIP";
            this.csFTPServerIP.Size = new System.Drawing.Size(121, 21);
            this.csFTPServerIP.TabIndex = 14;
            // 
            // FTPServerPort
            // 
            this.FTPServerPort.AutoSize = true;
            this.FTPServerPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FTPServerPort.Location = new System.Drawing.Point(250, 33);
            this.FTPServerPort.Name = "FTPServerPort";
            this.FTPServerPort.Size = new System.Drawing.Size(83, 12);
            this.FTPServerPort.TabIndex = 13;
            this.FTPServerPort.Text = "FTPServerPort";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(51, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "FTPServerIP";
            // 
            // csPassword
            // 
            this.csPassword.Location = new System.Drawing.Point(339, 73);
            this.csPassword.Name = "csPassword";
            this.csPassword.Size = new System.Drawing.Size(107, 21);
            this.csPassword.TabIndex = 19;
            // 
            // csAccount
            // 
            this.csAccount.Location = new System.Drawing.Point(102, 73);
            this.csAccount.Name = "csAccount";
            this.csAccount.Size = new System.Drawing.Size(121, 21);
            this.csAccount.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(250, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "Password";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label.Location = new System.Drawing.Point(31, 76);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(47, 12);
            this.label.TabIndex = 16;
            this.label.Text = "Account";
            // 
            // csUpgradePackage
            // 
            this.csUpgradePackage.Location = new System.Drawing.Point(130, 130);
            this.csUpgradePackage.Name = "csUpgradePackage";
            this.csUpgradePackage.Size = new System.Drawing.Size(316, 21);
            this.csUpgradePackage.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(25, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "UpgradePackage";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnStartFTPUpdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.csUpgradePackage);
            this.groupBox1.Controls.Add(this.label);
            this.groupBox1.Controls.Add(this.csFTPServerPort);
            this.groupBox1.Controls.Add(this.FTPServerPort);
            this.groupBox1.Controls.Add(this.csFTPServerIP);
            this.groupBox1.Controls.Add(this.csPassword);
            this.groupBox1.Controls.Add(this.csAccount);
            this.groupBox1.Location = new System.Drawing.Point(26, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 251);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FTPUpdate";
            // 
            // btnStartFTPUpdate
            // 
            this.btnStartFTPUpdate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStartFTPUpdate.Location = new System.Drawing.Point(45, 198);
            this.btnStartFTPUpdate.Name = "btnStartFTPUpdate";
            this.btnStartFTPUpdate.Size = new System.Drawing.Size(121, 28);
            this.btnStartFTPUpdate.TabIndex = 13;
            this.btnStartFTPUpdate.Text = "StartUpdate";
            this.btnStartFTPUpdate.UseVisualStyleBackColor = true;
            this.btnStartFTPUpdate.Click += new System.EventHandler(this.btnStartFTPUpdate_Click);
            // 
            // button1
            // 
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(281, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 28);
            this.button1.TabIndex = 22;
            this.button1.Text = "Cancle";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(51, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "HTTPServerIP";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Location = new System.Drawing.Point(26, 327);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 210);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HTTPUpdate";
            // 
            // button2
            // 
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(281, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 28);
            this.button2.TabIndex = 22;
            this.button2.Text = "Cancle";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(25, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "UpgradePackage";
            // 
            // button3
            // 
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(45, 141);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 28);
            this.button3.TabIndex = 13;
            this.button3.Text = "StartUpdate";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(130, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(316, 21);
            this.textBox1.TabIndex = 21;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(339, 30);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(107, 21);
            this.textBox2.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(250, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "HTTPServerPort";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(102, 30);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(121, 21);
            this.textBox3.TabIndex = 14;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Name = "Update";
            this.Size = new System.Drawing.Size(517, 554);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox csFTPServerPort;
        private System.Windows.Forms.TextBox csFTPServerIP;
        private System.Windows.Forms.Label FTPServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox csPassword;
        private System.Windows.Forms.TextBox csAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox csUpgradePackage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStartFTPUpdate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;
    }
}