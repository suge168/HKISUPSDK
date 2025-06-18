namespace ISUPDemo
{
    partial class StorageServer
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
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSStype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMonitorPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMonitorIP = new System.Windows.Forms.TextBox();
            this.textBoxURLresult = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialogPath = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAK = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSK = new System.Windows.Forms.TextBox();
            this.groupBoxCloud = new System.Windows.Forms.GroupBox();
            this.PoolIDText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDownLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxBuffer = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxDownloadFilePath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxFileUrl = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxDownloadServerType = new System.Windows.Forms.ComboBox();
            this.textBoxMonitorAddress = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxCloudSK = new System.Windows.Forms.TextBox();
            this.textBoxCloudAK = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBoxCloud.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(20, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Path：";
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(91, 46);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(281, 21);
            this.textBoxFilePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(381, 34);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(8, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ServerType：";
            // 
            // comboBoxSStype
            // 
            this.comboBoxSStype.FormattingEnabled = true;
            this.comboBoxSStype.Items.AddRange(new object[] {
            "VRB",
            "KMS",
            "CLOUD"});
            this.comboBoxSStype.Location = new System.Drawing.Point(94, 71);
            this.comboBoxSStype.Name = "comboBoxSStype";
            this.comboBoxSStype.Size = new System.Drawing.Size(100, 20);
            this.comboBoxSStype.TabIndex = 4;
            this.comboBoxSStype.Text = "VRB";
            this.comboBoxSStype.SelectedIndexChanged += new System.EventHandler(this.comboBoxSStype_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(6, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "MonitorAddress：";
            // 
            // textBoxMonitorPort
            // 
            this.textBoxMonitorPort.Location = new System.Drawing.Point(94, 141);
            this.textBoxMonitorPort.Name = "textBoxMonitorPort";
            this.textBoxMonitorPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxMonitorPort.TabIndex = 6;
            this.textBoxMonitorPort.Text = "10003";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F);
            this.label4.Location = new System.Drawing.Point(8, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "MonitorPort：";
            // 
            // textBoxMonitorIP
            // 
            this.textBoxMonitorIP.Location = new System.Drawing.Point(107, 105);
            this.textBoxMonitorIP.Name = "textBoxMonitorIP";
            this.textBoxMonitorIP.Size = new System.Drawing.Size(87, 21);
            this.textBoxMonitorIP.TabIndex = 8;
            this.textBoxMonitorIP.Text = "10.21.84.34";
            // 
            // textBoxURLresult
            // 
            this.textBoxURLresult.Location = new System.Drawing.Point(106, 230);
            this.textBoxURLresult.Name = "textBoxURLresult";
            this.textBoxURLresult.Size = new System.Drawing.Size(269, 21);
            this.textBoxURLresult.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F);
            this.label5.Location = new System.Drawing.Point(390, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "SuccUrl";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialogPath
            // 
            this.openFileDialogPath.FileName = "openFileDialog1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "AK:";
            // 
            // textBoxAK
            // 
            this.textBoxAK.Location = new System.Drawing.Point(81, 27);
            this.textBoxAK.Name = "textBoxAK";
            this.textBoxAK.Size = new System.Drawing.Size(100, 21);
            this.textBoxAK.TabIndex = 13;
            this.textBoxAK.Text = "test";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "SK:";
            // 
            // textBoxSK
            // 
            this.textBoxSK.Location = new System.Drawing.Point(81, 54);
            this.textBoxSK.Name = "textBoxSK";
            this.textBoxSK.Size = new System.Drawing.Size(100, 21);
            this.textBoxSK.TabIndex = 15;
            this.textBoxSK.Text = "12345";
            // 
            // groupBoxCloud
            // 
            this.groupBoxCloud.Controls.Add(this.label6);
            this.groupBoxCloud.Controls.Add(this.textBoxSK);
            this.groupBoxCloud.Controls.Add(this.textBoxAK);
            this.groupBoxCloud.Controls.Add(this.label7);
            this.groupBoxCloud.Location = new System.Drawing.Point(235, 72);
            this.groupBoxCloud.Name = "groupBoxCloud";
            this.groupBoxCloud.Size = new System.Drawing.Size(220, 90);
            this.groupBoxCloud.TabIndex = 16;
            this.groupBoxCloud.TabStop = false;
            this.groupBoxCloud.Text = "Verify";
            this.groupBoxCloud.Visible = false;
            // 
            // PoolIDText
            // 
            this.PoolIDText.Location = new System.Drawing.Point(61, 173);
            this.PoolIDText.Name = "PoolIDText";
            this.PoolIDText.Size = new System.Drawing.Size(133, 21);
            this.PoolIDText.TabIndex = 17;
            this.PoolIDText.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "PoolId：";
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.Location = new System.Drawing.Point(6, 214);
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(75, 23);
            this.btnDownLoad.TabIndex = 19;
            this.btnDownLoad.Text = "Download";
            this.btnDownLoad.UseVisualStyleBackColor = true;
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxBuffer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBoxSStype);
            this.groupBox1.Controls.Add(this.PoolIDText);
            this.groupBox1.Controls.Add(this.textBoxMonitorIP);
            this.groupBox1.Controls.Add(this.groupBoxCloud);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.textBoxMonitorPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 253);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "UploadFile";
            // 
            // checkBoxBuffer
            // 
            this.checkBoxBuffer.AutoSize = true;
            this.checkBoxBuffer.Location = new System.Drawing.Point(247, 177);
            this.checkBoxBuffer.Name = "checkBoxBuffer";
            this.checkBoxBuffer.Size = new System.Drawing.Size(102, 16);
            this.checkBoxBuffer.TabIndex = 19;
            this.checkBoxBuffer.Text = "buffer upload";
            this.checkBoxBuffer.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 9F);
            this.label14.Location = new System.Drawing.Point(9, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "File Path：";
            // 
            // textBoxDownloadFilePath
            // 
            this.textBoxDownloadFilePath.Location = new System.Drawing.Point(80, 68);
            this.textBoxDownloadFilePath.Name = "textBoxDownloadFilePath";
            this.textBoxDownloadFilePath.Size = new System.Drawing.Size(281, 21);
            this.textBoxDownloadFilePath.TabIndex = 22;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonDelete);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBoxFileUrl);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.comboBoxDownloadServerType);
            this.groupBox3.Controls.Add(this.textBoxMonitorAddress);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.btnBrowse2);
            this.groupBox3.Controls.Add(this.textBoxPort);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.btnDownLoad);
            this.groupBox3.Controls.Add(this.textBoxDownloadFilePath);
            this.groupBox3.Location = new System.Drawing.Point(12, 302);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 256);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DownloadFile";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(380, 214);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 32;
            this.buttonDelete.Text = "DeleteFile";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F);
            this.label9.Location = new System.Drawing.Point(8, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "FileUrl：";
            // 
            // textBoxFileUrl
            // 
            this.textBoxFileUrl.Location = new System.Drawing.Point(79, 32);
            this.textBoxFileUrl.Name = "textBoxFileUrl";
            this.textBoxFileUrl.Size = new System.Drawing.Size(281, 21);
            this.textBoxFileUrl.TabIndex = 31;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 9F);
            this.label17.Location = new System.Drawing.Point(8, 106);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "ServerType：";
            // 
            // comboBoxDownloadServerType
            // 
            this.comboBoxDownloadServerType.FormattingEnabled = true;
            this.comboBoxDownloadServerType.Items.AddRange(new object[] {
            "VRB",
            "KMS",
            "CLOUD"});
            this.comboBoxDownloadServerType.Location = new System.Drawing.Point(94, 103);
            this.comboBoxDownloadServerType.Name = "comboBoxDownloadServerType";
            this.comboBoxDownloadServerType.Size = new System.Drawing.Size(100, 20);
            this.comboBoxDownloadServerType.TabIndex = 4;
            this.comboBoxDownloadServerType.Text = "VRB";
            this.comboBoxDownloadServerType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDownloadServerType_SelectedIndexChanged);
            // 
            // textBoxMonitorAddress
            // 
            this.textBoxMonitorAddress.Location = new System.Drawing.Point(107, 137);
            this.textBoxMonitorAddress.Name = "textBoxMonitorAddress";
            this.textBoxMonitorAddress.Size = new System.Drawing.Size(87, 21);
            this.textBoxMonitorAddress.TabIndex = 8;
            this.textBoxMonitorAddress.Text = "10.21.84.34";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.textBoxCloudSK);
            this.groupBox4.Controls.Add(this.textBoxCloudAK);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Location = new System.Drawing.Point(235, 104);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(154, 84);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Verify";
            this.groupBox4.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 12);
            this.label19.TabIndex = 12;
            this.label19.Text = "AK:";
            // 
            // textBoxCloudSK
            // 
            this.textBoxCloudSK.Location = new System.Drawing.Point(39, 55);
            this.textBoxCloudSK.Name = "textBoxCloudSK";
            this.textBoxCloudSK.Size = new System.Drawing.Size(100, 21);
            this.textBoxCloudSK.TabIndex = 15;
            this.textBoxCloudSK.Text = "12345";
            // 
            // textBoxCloudAK
            // 
            this.textBoxCloudAK.Location = new System.Drawing.Point(39, 24);
            this.textBoxCloudAK.Name = "textBoxCloudAK";
            this.textBoxCloudAK.Size = new System.Drawing.Size(100, 21);
            this.textBoxCloudAK.TabIndex = 13;
            this.textBoxCloudAK.Text = "test";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(10, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 12);
            this.label20.TabIndex = 14;
            this.label20.Text = "SK:";
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Location = new System.Drawing.Point(381, 66);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse2.TabIndex = 2;
            this.btnBrowse2.Text = "Browse";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(94, 173);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxPort.TabIndex = 6;
            this.textBoxPort.Text = "10003";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 9F);
            this.label21.Location = new System.Drawing.Point(8, 176);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 12);
            this.label21.TabIndex = 7;
            this.label21.Text = "MonitorPort：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 9F);
            this.label22.Location = new System.Drawing.Point(6, 140);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 12);
            this.label22.TabIndex = 5;
            this.label22.Text = "MonitorAddress：";
            // 
            // StorageServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 601);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxURLresult);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "StorageServer";
            this.Text = "StorageServer";
            this.groupBoxCloud.ResumeLayout(false);
            this.groupBoxCloud.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSStype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMonitorPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMonitorIP;
        private System.Windows.Forms.TextBox textBoxURLresult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialogPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAK;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSK;
        private System.Windows.Forms.GroupBox groupBoxCloud;
        private System.Windows.Forms.TextBox PoolIDText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDownLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxBuffer;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxDownloadFilePath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxFileUrl;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxDownloadServerType;
        private System.Windows.Forms.TextBox textBoxMonitorAddress;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxCloudSK;
        private System.Windows.Forms.TextBox textBoxCloudAK;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button buttonDelete;
    }
}