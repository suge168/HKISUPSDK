using System;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace EHomeDemo
{
    partial class EHomeDemo
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EHomeDemo));
            this.versionInfo = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_previewListen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOthers = new System.Windows.Forms.Button();
            this.audioTalk = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.EHomeDemoExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.m_panelDeviceTree = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.m_panelDeviceLog = new System.Windows.Forms.Panel();
            this.m_previewPanelOne = new System.Windows.Forms.Panel();
            this.m_previewPanelTwo = new System.Windows.Forms.Panel();
            this.m_previewPanelThree = new System.Windows.Forms.Panel();
            this.m_previewPanelFour = new System.Windows.Forms.Panel();
            this.m_previewWnd = new System.Windows.Forms.Panel();
            this.m_rdoLocalLog = new System.Windows.Forms.RadioButton();
            this.m_rdoAlarmInformation = new System.Windows.Forms.RadioButton();
            this.m_MenuElse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GpsInfoSet = new System.Windows.Forms.ToolStripMenuItem();
            this.g_TotalPanel = new System.Windows.Forms.Panel();
            this.versionInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_previewWnd.SuspendLayout();
            this.m_MenuElse.SuspendLayout();
            this.g_TotalPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // versionInfo
            // 
            resources.ApplyResources(this.versionInfo, "versionInfo");
            this.versionInfo.Controls.Add(this.label2);
            this.versionInfo.Name = "versionInfo";
            this.versionInfo.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // m_previewListen
            // 
            resources.ApplyResources(this.m_previewListen, "m_previewListen");
            this.m_previewListen.Name = "m_previewListen";
            this.m_previewListen.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnOthers);
            this.groupBox1.Controls.Add(this.audioTalk);
            this.groupBox1.Controls.Add(this.buttonPreview);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnOthers
            // 
            resources.ApplyResources(this.btnOthers, "btnOthers");
            this.btnOthers.Name = "btnOthers";
            this.btnOthers.UseVisualStyleBackColor = true;
            this.btnOthers.Click += new System.EventHandler(this.btnOthers_Click);
            // 
            // audioTalk
            // 
            resources.ApplyResources(this.audioTalk, "audioTalk");
            this.audioTalk.Name = "audioTalk";
            this.audioTalk.UseVisualStyleBackColor = true;
            this.audioTalk.Click += new System.EventHandler(this.audioTalk_Click);
            // 
            // buttonPreview
            // 
            resources.ApplyResources(this.buttonPreview, "buttonPreview");
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // EHomeDemoExit
            // 
            resources.ApplyResources(this.EHomeDemoExit, "EHomeDemoExit");
            this.EHomeDemoExit.Name = "EHomeDemoExit";
            this.EHomeDemoExit.UseVisualStyleBackColor = true;
            this.EHomeDemoExit.Click += new System.EventHandler(this.EHomeDemoExit_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_previewListen);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // m_panelDeviceTree
            // 
            resources.ApplyResources(this.m_panelDeviceTree, "m_panelDeviceTree");
            this.m_panelDeviceTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelDeviceTree.Name = "m_panelDeviceTree";
            // 
            // treeView1
            // 
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.LineColor = System.Drawing.Color.Empty;
            this.treeView1.Name = "treeView1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeView1.Nodes")))});
            // 
            // m_panelDeviceLog
            // 
            resources.ApplyResources(this.m_panelDeviceLog, "m_panelDeviceLog");
            this.m_panelDeviceLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelDeviceLog.Name = "m_panelDeviceLog";
            this.m_panelDeviceLog.TabStop = true;
            // 
            // m_previewPanelOne
            // 
            resources.ApplyResources(this.m_previewPanelOne, "m_previewPanelOne");
            this.m_previewPanelOne.Name = "m_previewPanelOne";
            this.m_previewPanelOne.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_previewPanelOne_MouseDown);
            // 
            // m_previewPanelTwo
            // 
            resources.ApplyResources(this.m_previewPanelTwo, "m_previewPanelTwo");
            this.m_previewPanelTwo.Name = "m_previewPanelTwo";
            this.m_previewPanelTwo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_previewPanelTwo_MouseDown);
            // 
            // m_previewPanelThree
            // 
            resources.ApplyResources(this.m_previewPanelThree, "m_previewPanelThree");
            this.m_previewPanelThree.Name = "m_previewPanelThree";
            this.m_previewPanelThree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_previewPanelThree_MouseDown);
            // 
            // m_previewPanelFour
            // 
            resources.ApplyResources(this.m_previewPanelFour, "m_previewPanelFour");
            this.m_previewPanelFour.Name = "m_previewPanelFour";
            this.m_previewPanelFour.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_previewPanelFour_MouseDown);
            // 
            // m_previewWnd
            // 
            resources.ApplyResources(this.m_previewWnd, "m_previewWnd");
            this.m_previewWnd.Controls.Add(this.m_previewPanelFour);
            this.m_previewWnd.Controls.Add(this.m_previewPanelOne);
            this.m_previewWnd.Controls.Add(this.m_previewPanelThree);
            this.m_previewWnd.Controls.Add(this.m_previewPanelTwo);
            this.m_previewWnd.Name = "m_previewWnd";
            // 
            // m_rdoLocalLog
            // 
            resources.ApplyResources(this.m_rdoLocalLog, "m_rdoLocalLog");
            this.m_rdoLocalLog.Name = "m_rdoLocalLog";
            this.m_rdoLocalLog.TabStop = true;
            this.m_rdoLocalLog.UseVisualStyleBackColor = true;
            this.m_rdoLocalLog.CheckedChanged += new System.EventHandler(this.radioBtnLocalLog_CheckedChanged);
            // 
            // m_rdoAlarmInformation
            // 
            resources.ApplyResources(this.m_rdoAlarmInformation, "m_rdoAlarmInformation");
            this.m_rdoAlarmInformation.Name = "m_rdoAlarmInformation";
            this.m_rdoAlarmInformation.TabStop = true;
            this.m_rdoAlarmInformation.UseVisualStyleBackColor = true;
            this.m_rdoAlarmInformation.CheckedChanged += new System.EventHandler(this.m_rdoAlarmInformation_CheckedChanged);
            // 
            // m_MenuElse
            // 
            resources.ApplyResources(this.m_MenuElse, "m_MenuElse");
            this.m_MenuElse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GpsInfoSet});
            this.m_MenuElse.Name = "m_MenuElse";
            // 
            // GpsInfoSet
            // 
            resources.ApplyResources(this.GpsInfoSet, "GpsInfoSet");
            this.GpsInfoSet.Name = "GpsInfoSet";
            this.GpsInfoSet.Click += new System.EventHandler(this.GpsInfoSet_Click);
            // 
            // g_TotalPanel
            // 
            resources.ApplyResources(this.g_TotalPanel, "g_TotalPanel");
            this.g_TotalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.g_TotalPanel.Controls.Add(this.m_previewWnd);
            this.g_TotalPanel.Controls.Add(this.m_panelDeviceLog);
            this.g_TotalPanel.Name = "g_TotalPanel";
            // 
            // EHomeDemo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.g_TotalPanel);
            this.Controls.Add(this.m_rdoAlarmInformation);
            this.Controls.Add(this.m_rdoLocalLog);
            this.Controls.Add(this.m_panelDeviceTree);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.EHomeDemoExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.versionInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EHomeDemo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EHomeDemo_FormClosing);
            this.versionInfo.ResumeLayout(false);
            this.versionInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.m_previewWnd.ResumeLayout(false);
            this.m_MenuElse.ResumeLayout(false);
            this.g_TotalPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        //LOCAL_DEVICE_INFO g_struDeviceInfo[MAX_DEVICES];
        //IntPtr g_pMainDlg = this.Handle;
        //BOOL g_bExitDemo = FALSE;//control post message when exit, so can release the buffer.
        //LOCAL_PARAM g_struLocalParam;        //demo local configure
        GlobalDefinition.LISTEN_INFO[] g_struPreviewListen = new GlobalDefinition.LISTEN_INFO[GlobalDefinition.MAX_LISTEN_NUM];
        Int32 g_pCycleTimer = 0;
        bool g_bTCPLink = true;
        Int32 g_lCmsAlarm = -1;
        // 用于应用程序“关于”菜单项的 CAboutDlg 对话框
        int g_iCurScreenWidth = 0;        //current screen width
        int g_iCurScreenHeight = 0;       //current screen height

        byte m_byCmsSecureAccessType;
        byte m_byAlarmSecureAccessType;
        byte m_byStreamSecureAccessType;
        #endregion

        private System.Windows.Forms.GroupBox versionInfo;
        private System.Windows.Forms.Button m_previewListen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button EHomeDemoExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Panel m_panelDeviceTree;
        private System.Windows.Forms.TreeView treeView1;
        private Button audioTalk;
        private Panel m_previewPanelFour;
        private Panel m_previewPanelThree;
        private Panel m_previewPanelTwo;
        private Panel m_previewPanelOne;
        private Panel m_previewWnd;
        private RadioButton m_rdoLocalLog;
        private RadioButton m_rdoAlarmInformation;
        private Button btnOthers;
        private ContextMenuStrip m_MenuElse;
        private ToolStripMenuItem GpsInfoSet;
        private Panel m_panelDeviceLog;
        private Panel g_TotalPanel;

    }
}

