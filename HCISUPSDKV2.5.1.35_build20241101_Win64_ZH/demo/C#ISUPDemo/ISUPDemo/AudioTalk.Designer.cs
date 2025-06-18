namespace EHomeDemo
{
    partial class AudioTalk
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioTalk));
            this.AudioTalkGoup = new System.Windows.Forms.GroupBox();
            this.group = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbAudioChannel = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStartTransmit = new System.Windows.Forms.Button();
            this.btnStopTransmit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStopAudioTalk = new System.Windows.Forms.Button();
            this.btnStopListen = new System.Windows.Forms.Button();
            this.btnStartPushStream = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.m_nPort = new System.Windows.Forms.TextBox();
            this.m_csIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReqAudioTalk = new System.Windows.Forms.Button();
            this.m_cmbLocalType = new System.Windows.Forms.ComboBox();
            this.btnStartListen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCMSStopTalk = new System.Windows.Forms.Button();
            this.m_cmbCBDataType = new System.Windows.Forms.ComboBox();
            this.btnCMSStartTalk = new System.Windows.Forms.Button();
            this.AudioTalkGoup.SuspendLayout();
            this.group.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AudioTalkGoup
            // 
            resources.ApplyResources(this.AudioTalkGoup, "AudioTalkGoup");
            this.AudioTalkGoup.Controls.Add(this.group);
            this.AudioTalkGoup.Controls.Add(this.groupBox3);
            this.AudioTalkGoup.Controls.Add(this.groupBox2);
            this.AudioTalkGoup.Controls.Add(this.groupBox1);
            this.AudioTalkGoup.Name = "AudioTalkGoup";
            this.AudioTalkGoup.TabStop = false;
            // 
            // group
            // 
            resources.ApplyResources(this.group, "group");
            this.group.Controls.Add(this.label1);
            this.group.Controls.Add(this.m_cmbAudioChannel);
            this.group.Name = "group";
            this.group.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // m_cmbAudioChannel
            // 
            resources.ApplyResources(this.m_cmbAudioChannel, "m_cmbAudioChannel");
            this.m_cmbAudioChannel.FormattingEnabled = true;
            this.m_cmbAudioChannel.Name = "m_cmbAudioChannel";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.btnStartTransmit);
            this.groupBox3.Controls.Add(this.btnStopTransmit);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnStartTransmit
            // 
            resources.ApplyResources(this.btnStartTransmit, "btnStartTransmit");
            this.btnStartTransmit.Name = "btnStartTransmit";
            this.btnStartTransmit.UseVisualStyleBackColor = true;
            this.btnStartTransmit.Click += new System.EventHandler(this.btnStartTransmit_Click);
            // 
            // btnStopTransmit
            // 
            resources.ApplyResources(this.btnStopTransmit, "btnStopTransmit");
            this.btnStopTransmit.Name = "btnStopTransmit";
            this.btnStopTransmit.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.btnStopAudioTalk);
            this.groupBox2.Controls.Add(this.btnStopListen);
            this.groupBox2.Controls.Add(this.btnStartPushStream);
            this.groupBox2.Controls.Add(this.btnTest);
            this.groupBox2.Controls.Add(this.m_nPort);
            this.groupBox2.Controls.Add(this.m_csIP);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnReqAudioTalk);
            this.groupBox2.Controls.Add(this.m_cmbLocalType);
            this.groupBox2.Controls.Add(this.btnStartListen);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnStopAudioTalk
            // 
            resources.ApplyResources(this.btnStopAudioTalk, "btnStopAudioTalk");
            this.btnStopAudioTalk.Name = "btnStopAudioTalk";
            this.btnStopAudioTalk.UseVisualStyleBackColor = true;
            this.btnStopAudioTalk.Click += new System.EventHandler(this.btnStopAudioTalk_Click);
            // 
            // btnStopListen
            // 
            resources.ApplyResources(this.btnStopListen, "btnStopListen");
            this.btnStopListen.Name = "btnStopListen";
            this.btnStopListen.UseVisualStyleBackColor = true;
            this.btnStopListen.Click += new System.EventHandler(this.btnStopListen_Click);
            // 
            // btnStartPushStream
            // 
            resources.ApplyResources(this.btnStartPushStream, "btnStartPushStream");
            this.btnStartPushStream.Name = "btnStartPushStream";
            this.btnStartPushStream.UseVisualStyleBackColor = true;
            this.btnStartPushStream.Click += new System.EventHandler(this.btnStartPushStream_Click);
            // 
            // btnTest
            // 
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.UseVisualStyleBackColor = true;
            // 
            // m_nPort
            // 
            resources.ApplyResources(this.m_nPort, "m_nPort");
            this.m_nPort.Name = "m_nPort";
            // 
            // m_csIP
            // 
            resources.ApplyResources(this.m_csIP, "m_csIP");
            this.m_csIP.Name = "m_csIP";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnReqAudioTalk
            // 
            resources.ApplyResources(this.btnReqAudioTalk, "btnReqAudioTalk");
            this.btnReqAudioTalk.Name = "btnReqAudioTalk";
            this.btnReqAudioTalk.UseVisualStyleBackColor = true;
            this.btnReqAudioTalk.Click += new System.EventHandler(this.btnReqAudioTalk_Click);
            // 
            // m_cmbLocalType
            // 
            resources.ApplyResources(this.m_cmbLocalType, "m_cmbLocalType");
            this.m_cmbLocalType.FormattingEnabled = true;
            this.m_cmbLocalType.Name = "m_cmbLocalType";
            // 
            // btnStartListen
            // 
            resources.ApplyResources(this.btnStartListen, "btnStartListen");
            this.btnStartListen.Name = "btnStartListen";
            this.btnStartListen.UseVisualStyleBackColor = true;
            this.btnStartListen.Click += new System.EventHandler(this.btnStartListen_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnCMSStopTalk);
            this.groupBox1.Controls.Add(this.m_cmbCBDataType);
            this.groupBox1.Controls.Add(this.btnCMSStartTalk);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnCMSStopTalk
            // 
            resources.ApplyResources(this.btnCMSStopTalk, "btnCMSStopTalk");
            this.btnCMSStopTalk.Name = "btnCMSStopTalk";
            this.btnCMSStopTalk.UseVisualStyleBackColor = true;
            this.btnCMSStopTalk.Click += new System.EventHandler(this.btnCMSStopTalk_Click);
            // 
            // m_cmbCBDataType
            // 
            resources.ApplyResources(this.m_cmbCBDataType, "m_cmbCBDataType");
            this.m_cmbCBDataType.FormattingEnabled = true;
            this.m_cmbCBDataType.Name = "m_cmbCBDataType";
            // 
            // btnCMSStartTalk
            // 
            resources.ApplyResources(this.btnCMSStartTalk, "btnCMSStartTalk");
            this.btnCMSStartTalk.Name = "btnCMSStartTalk";
            this.btnCMSStartTalk.UseVisualStyleBackColor = true;
            this.btnCMSStartTalk.Click += new System.EventHandler(this.btnCMSStartTalk_Click);
            // 
            // AudioTalk
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AudioTalkGoup);
            this.Name = "AudioTalk";
            this.Load += new System.EventHandler(this.AudioTalk_Load);
            this.AudioTalkGoup.ResumeLayout(false);
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox AudioTalkGoup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCMSStopTalk;
        private System.Windows.Forms.ComboBox m_cmbCBDataType;
        private System.Windows.Forms.Button btnCMSStartTalk;
        private System.Windows.Forms.ComboBox m_cmbAudioChannel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStartTransmit;
        private System.Windows.Forms.Button btnStopTransmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStopAudioTalk;
        private System.Windows.Forms.Button btnStopListen;
        private System.Windows.Forms.Button btnStartPushStream;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox m_nPort;
        private System.Windows.Forms.TextBox m_csIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReqAudioTalk;
        private System.Windows.Forms.ComboBox m_cmbLocalType;
        private System.Windows.Forms.Button btnStartListen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox group;
    }
}
