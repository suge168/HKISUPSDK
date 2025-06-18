namespace EHomeDemo
{
    partial class GPSInfoSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GPSInfoSet));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSETGpsInfo = new System.Windows.Forms.Button();
            this.btnGETGpsInfo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_iMinSpeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.上传时间间隔 = new System.Windows.Forms.Label();
            this.m_iMaxSpeed = new System.Windows.Forms.TextBox();
            this.m_cmbInteral = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnSETGpsInfo);
            this.groupBox1.Controls.Add(this.btnGETGpsInfo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_iMinSpeed);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.上传时间间隔);
            this.groupBox1.Controls.Add(this.m_iMaxSpeed);
            this.groupBox1.Controls.Add(this.m_cmbInteral);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnSETGpsInfo
            // 
            resources.ApplyResources(this.btnSETGpsInfo, "btnSETGpsInfo");
            this.btnSETGpsInfo.Name = "btnSETGpsInfo";
            this.btnSETGpsInfo.UseVisualStyleBackColor = true;
            this.btnSETGpsInfo.Click += new System.EventHandler(this.btnSETGpsInfo_Click);
            // 
            // btnGETGpsInfo
            // 
            resources.ApplyResources(this.btnGETGpsInfo, "btnGETGpsInfo");
            this.btnGETGpsInfo.Name = "btnGETGpsInfo";
            this.btnGETGpsInfo.UseVisualStyleBackColor = true;
            this.btnGETGpsInfo.Click += new System.EventHandler(this.btnGETGpsInfo_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // m_iMinSpeed
            // 
            resources.ApplyResources(this.m_iMinSpeed, "m_iMinSpeed");
            this.m_iMinSpeed.Name = "m_iMinSpeed";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // 上传时间间隔
            // 
            resources.ApplyResources(this.上传时间间隔, "上传时间间隔");
            this.上传时间间隔.Name = "上传时间间隔";
            // 
            // m_iMaxSpeed
            // 
            resources.ApplyResources(this.m_iMaxSpeed, "m_iMaxSpeed");
            this.m_iMaxSpeed.Name = "m_iMaxSpeed";
            // 
            // m_cmbInteral
            // 
            resources.ApplyResources(this.m_cmbInteral, "m_cmbInteral");
            this.m_cmbInteral.Name = "m_cmbInteral";
            // 
            // GPSInfoSet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "GPSInfoSet";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSETGpsInfo;
        private System.Windows.Forms.Button btnGETGpsInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_iMinSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label 上传时间间隔;
        private System.Windows.Forms.TextBox m_iMaxSpeed;
        private System.Windows.Forms.TextBox m_cmbInteral;
    }
}