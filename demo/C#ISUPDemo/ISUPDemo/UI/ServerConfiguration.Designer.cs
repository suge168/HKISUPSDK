namespace ISUPDemo.UI
{
    partial class ServerConfiguration
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
            this.label14 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxPicServerType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPicServerIP = new System.Windows.Forms.TextBox();
            this.textBoxPicServerPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxNTPIP = new System.Windows.Forms.TextBox();
            this.textBoxVTDUINPORT = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMQTTIP = new System.Windows.Forms.TextBox();
            this.textBoxMQTTPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTCPIP = new System.Windows.Forms.TextBox();
            this.textBoxTcpPort = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBoxAlarmServerType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxUDPIP = new System.Windows.Forms.TextBox();
            this.textBoxUdpPort = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(226, 439);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 12);
            this.label14.TabIndex = 34;
            this.label14.Text = "(use ISUPDemo.xml)";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(39, 405);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 31);
            this.buttonOK.TabIndex = 32;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 503);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(317, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "内外网情况下，本页面所有地址需填写外网地址(端口映射)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(40, 476);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "Attention:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 12);
            this.label13.TabIndex = 21;
            this.label13.Text = "PictureServerType:";
            // 
            // comboBoxPicServerType
            // 
            this.comboBoxPicServerType.FormattingEnabled = true;
            this.comboBoxPicServerType.Items.AddRange(new object[] {
            "VRB",
            "Tomcat",
            "KMS",
            "CloudStorage"});
            this.comboBoxPicServerType.Location = new System.Drawing.Point(127, 22);
            this.comboBoxPicServerType.Name = "comboBoxPicServerType";
            this.comboBoxPicServerType.Size = new System.Drawing.Size(105, 20);
            this.comboBoxPicServerType.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "ServerPort:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "ServerIP:";
            // 
            // textBoxPicServerIP
            // 
            this.textBoxPicServerIP.Location = new System.Drawing.Point(90, 64);
            this.textBoxPicServerIP.Name = "textBoxPicServerIP";
            this.textBoxPicServerIP.Size = new System.Drawing.Size(100, 21);
            this.textBoxPicServerIP.TabIndex = 11;
            // 
            // textBoxPicServerPort
            // 
            this.textBoxPicServerPort.Location = new System.Drawing.Point(310, 64);
            this.textBoxPicServerPort.Name = "textBoxPicServerPort";
            this.textBoxPicServerPort.Size = new System.Drawing.Size(73, 21);
            this.textBoxPicServerPort.TabIndex = 13;
            this.textBoxPicServerPort.Text = "9660";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "ServerPort:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "ServerIP:";
            // 
            // textBoxNTPIP
            // 
            this.textBoxNTPIP.Location = new System.Drawing.Point(105, 25);
            this.textBoxNTPIP.Name = "textBoxNTPIP";
            this.textBoxNTPIP.Size = new System.Drawing.Size(100, 21);
            this.textBoxNTPIP.TabIndex = 11;
            // 
            // textBoxVTDUINPORT
            // 
            this.textBoxVTDUINPORT.Location = new System.Drawing.Point(311, 25);
            this.textBoxVTDUINPORT.Name = "textBoxVTDUINPORT";
            this.textBoxVTDUINPORT.Size = new System.Drawing.Size(73, 21);
            this.textBoxVTDUINPORT.TabIndex = 13;
            this.textBoxVTDUINPORT.Text = "8000";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(230, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 31);
            this.button1.TabIndex = 33;
            this.button1.Text = "Skip";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBoxMQTTIP);
            this.groupBox3.Controls.Add(this.textBoxMQTTPort);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBoxTCPIP);
            this.groupBox3.Controls.Add(this.textBoxTcpPort);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.comboBoxAlarmServerType);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.textBoxUDPIP);
            this.groupBox3.Controls.Add(this.textBoxUdpPort);
            this.groupBox3.Location = new System.Drawing.Point(12, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(397, 155);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AlarmServer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "MQTTServerPort:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "ServerIP:";
            // 
            // textBoxMQTTIP
            // 
            this.textBoxMQTTIP.Location = new System.Drawing.Point(82, 109);
            this.textBoxMQTTIP.Name = "textBoxMQTTIP";
            this.textBoxMQTTIP.Size = new System.Drawing.Size(100, 21);
            this.textBoxMQTTIP.TabIndex = 25;
            // 
            // textBoxMQTTPort
            // 
            this.textBoxMQTTPort.Location = new System.Drawing.Point(315, 112);
            this.textBoxMQTTPort.Name = "textBoxMQTTPort";
            this.textBoxMQTTPort.Size = new System.Drawing.Size(69, 21);
            this.textBoxMQTTPort.TabIndex = 27;
            this.textBoxMQTTPort.Text = "7663";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "TCPServerPort:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "ServerIP:";
            // 
            // textBoxTCPIP
            // 
            this.textBoxTCPIP.Location = new System.Drawing.Point(82, 76);
            this.textBoxTCPIP.Name = "textBoxTCPIP";
            this.textBoxTCPIP.Size = new System.Drawing.Size(100, 21);
            this.textBoxTCPIP.TabIndex = 21;
            // 
            // textBoxTcpPort
            // 
            this.textBoxTcpPort.Location = new System.Drawing.Point(315, 79);
            this.textBoxTcpPort.Name = "textBoxTcpPort";
            this.textBoxTcpPort.Size = new System.Drawing.Size(69, 21);
            this.textBoxTcpPort.TabIndex = 23;
            this.textBoxTcpPort.Text = "7662";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(11, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 12);
            this.label19.TabIndex = 19;
            this.label19.Text = "AlarmServerType:";
            // 
            // comboBoxAlarmServerType
            // 
            this.comboBoxAlarmServerType.FormattingEnabled = true;
            this.comboBoxAlarmServerType.Items.AddRange(new object[] {
            "UDP协议",
            "UDP和TCP协议"});
            this.comboBoxAlarmServerType.Location = new System.Drawing.Point(114, 14);
            this.comboBoxAlarmServerType.Name = "comboBoxAlarmServerType";
            this.comboBoxAlarmServerType.Size = new System.Drawing.Size(121, 20);
            this.comboBoxAlarmServerType.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "UdpServerPort:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "ServerIP:";
            // 
            // textBoxUDPIP
            // 
            this.textBoxUDPIP.Location = new System.Drawing.Point(82, 44);
            this.textBoxUDPIP.Name = "textBoxUDPIP";
            this.textBoxUDPIP.Size = new System.Drawing.Size(100, 21);
            this.textBoxUDPIP.TabIndex = 11;
            // 
            // textBoxUdpPort
            // 
            this.textBoxUdpPort.Location = new System.Drawing.Point(315, 47);
            this.textBoxUdpPort.Name = "textBoxUdpPort";
            this.textBoxUdpPort.Size = new System.Drawing.Size(68, 21);
            this.textBoxUdpPort.TabIndex = 13;
            this.textBoxUdpPort.Text = "7661";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.comboBoxPicServerType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPicServerIP);
            this.groupBox1.Controls.Add(this.textBoxPicServerPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 197);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 91);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Picture Server";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxNTPIP);
            this.groupBox2.Controls.Add(this.textBoxVTDUINPORT);
            this.groupBox2.Location = new System.Drawing.Point(12, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 64);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NTPServer";
            // 
            // ServerConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 541);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ServerConfiguration";
            this.Text = "ServerConfiguration";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxPicServerType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPicServerIP;
        private System.Windows.Forms.TextBox textBoxPicServerPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxNTPIP;
        private System.Windows.Forms.TextBox textBoxVTDUINPORT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMQTTIP;
        private System.Windows.Forms.TextBox textBoxMQTTPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTCPIP;
        private System.Windows.Forms.TextBox textBoxTcpPort;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboBoxAlarmServerType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxUDPIP;
        private System.Windows.Forms.TextBox textBoxUdpPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}