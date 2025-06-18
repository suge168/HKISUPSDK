
using System.Net;

namespace ISUPDemo
{
    partial class SelectIP
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
            this.localIP = new System.Windows.Forms.Label();
            this.m_cmbLocalIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_textPort = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // localIP
            // 
            this.localIP.AutoSize = true;
            this.localIP.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.localIP.Location = new System.Drawing.Point(29, 52);
            this.localIP.Name = "localIP";
            this.localIP.Size = new System.Drawing.Size(31, 14);
            this.localIP.TabIndex = 0;
            this.localIP.Text = "IP:";
            // 
            // m_cmbLocalIP
            // 
            this.m_cmbLocalIP.FormattingEnabled = true;
            this.m_cmbLocalIP.Items.AddRange(new object[] {
            "0.0.0.0"});
            this.m_cmbLocalIP.Location = new System.Drawing.Point(81, 49);
            this.m_cmbLocalIP.Name = "m_cmbLocalIP";
            this.m_cmbLocalIP.Size = new System.Drawing.Size(140, 20);
            this.m_cmbLocalIP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "port:";
            // 
            // m_textPort
            // 
            this.m_textPort.Location = new System.Drawing.Point(81, 88);
            this.m_textPort.Name = "m_textPort";
            this.m_textPort.Size = new System.Drawing.Size(110, 21);
            this.m_textPort.TabIndex = 3;
            this.m_textPort.Text = "7660";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(85, 137);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 31);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // SelectIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 180);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.m_textPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cmbLocalIP);
            this.Controls.Add(this.localIP);
            this.MaximizeBox = false;
            this.Name = "SelectIP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectIP_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public void GetLocalIP()
        {
            IpAddressList.Clear();

            string hostName = Dns.GetHostName();
            IPAddress[] addressList = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in addressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)//判断是否IP4协议
                {
                    this.m_cmbLocalIP.Items.Add(ip);
                    IpAddressList.Add(ip);
                }
                
            }
            this.m_cmbLocalIP.SelectedIndex = 0;
            Update();
        }

        #endregion

        public System.Windows.Forms.Label  localIP;
        public System.Windows.Forms.ComboBox m_cmbLocalIP;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox m_textPort;
        private System.Windows.Forms.Button buttonOK;
    }
}