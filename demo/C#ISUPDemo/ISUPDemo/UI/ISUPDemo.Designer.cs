using System;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace ISUPDemo
{
    partial class ISUPDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ISUPDemo));
            this.versionInfo = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPlayBack = new System.Windows.Forms.Button();
            this.BtnConfig = new System.Windows.Forms.Button();
            this.BtnManage = new System.Windows.Forms.Button();
            this.alarmbtn = new System.Windows.Forms.Button();
            this.btnOthers = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.m_panelDeviceTree = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.m_previewPanelOne = new System.Windows.Forms.Panel();
            this.m_previewPanelTwo = new System.Windows.Forms.Panel();
            this.m_previewPanelThree = new System.Windows.Forms.Panel();
            this.m_previewPanelFour = new System.Windows.Forms.Panel();
            this.m_previewWnd = new System.Windows.Forms.Panel();
            this.m_rdoLocalLog = new System.Windows.Forms.RadioButton();
            this.m_rdoAlarmInformation = new System.Windows.Forms.RadioButton();
            this.m_MenuElse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GpsInfoSet = new System.Windows.Forms.ToolStripMenuItem();
            this.AcsEventStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.VoiceTalkStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.storageServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceMaintain = new System.Windows.Forms.ToolStripMenuItem();
            this.rebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.g_TotalPanel = new System.Windows.Forms.Panel();
            this.m_panelDeviceLog = new System.Windows.Forms.Panel();
            this.m_MenuMoudle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UserManageStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FingerManageStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.CardManageStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FaceManageStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.WeekPlanStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AlarmStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ConfigStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CommonConfigStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.XMLConfigStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.versionInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_previewWnd.SuspendLayout();
            this.m_MenuElse.SuspendLayout();
            this.g_TotalPanel.SuspendLayout();
            this.m_MenuMoudle.SuspendLayout();
            this.m_ConfigStrip.SuspendLayout();
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.buttonPlayBack);
            this.groupBox1.Controls.Add(this.BtnConfig);
            this.groupBox1.Controls.Add(this.BtnManage);
            this.groupBox1.Controls.Add(this.alarmbtn);
            this.groupBox1.Controls.Add(this.btnOthers);
            this.groupBox1.Controls.Add(this.buttonPreview);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonPlayBack
            // 
            resources.ApplyResources(this.buttonPlayBack, "buttonPlayBack");
            this.buttonPlayBack.Name = "buttonPlayBack";
            this.buttonPlayBack.UseVisualStyleBackColor = true;
            this.buttonPlayBack.Click += new System.EventHandler(this.buttonPlayBack_Click);
            // 
            // BtnConfig
            // 
            resources.ApplyResources(this.BtnConfig, "BtnConfig");
            this.BtnConfig.Name = "BtnConfig";
            this.BtnConfig.UseVisualStyleBackColor = true;
            this.BtnConfig.Click += new System.EventHandler(this.BtnConfig_Click);
            // 
            // BtnManage
            // 
            resources.ApplyResources(this.BtnManage, "BtnManage");
            this.BtnManage.Name = "BtnManage";
            this.BtnManage.UseVisualStyleBackColor = true;
            this.BtnManage.Click += new System.EventHandler(this.BtnManage_Click);
            // 
            // alarmbtn
            // 
            resources.ApplyResources(this.alarmbtn, "alarmbtn");
            this.alarmbtn.Name = "alarmbtn";
            this.alarmbtn.UseVisualStyleBackColor = true;
            this.alarmbtn.Click += new System.EventHandler(this.Alarm_Click);
            // 
            // btnOthers
            // 
            resources.ApplyResources(this.btnOthers, "btnOthers");
            this.btnOthers.Name = "btnOthers";
            this.btnOthers.UseVisualStyleBackColor = true;
            this.btnOthers.Click += new System.EventHandler(this.btnOthers_Click);
            // 
            // buttonPreview
            // 
            resources.ApplyResources(this.buttonPreview, "buttonPreview");
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
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
            this.GpsInfoSet,
            this.AcsEventStrip,
            this.VoiceTalkStrip,
            this.storageServerToolStripMenuItem,
            this.pTToolStripMenuItem,
            this.DeviceMaintain,
            this.ExitStrip});
            this.m_MenuElse.Name = "m_MenuElse";
            // 
            // GpsInfoSet
            // 
            resources.ApplyResources(this.GpsInfoSet, "GpsInfoSet");
            this.GpsInfoSet.Name = "GpsInfoSet";
            this.GpsInfoSet.Click += new System.EventHandler(this.GpsInfoSet_Click);
            // 
            // AcsEventStrip
            // 
            resources.ApplyResources(this.AcsEventStrip, "AcsEventStrip");
            this.AcsEventStrip.Name = "AcsEventStrip";
            this.AcsEventStrip.Click += new System.EventHandler(this.AcsEventStrip_Click);
            // 
            // VoiceTalkStrip
            // 
            resources.ApplyResources(this.VoiceTalkStrip, "VoiceTalkStrip");
            this.VoiceTalkStrip.Name = "VoiceTalkStrip";
            this.VoiceTalkStrip.Click += new System.EventHandler(this.VoiceTalkStrip_Click);
            // 
            // storageServerToolStripMenuItem
            // 
            resources.ApplyResources(this.storageServerToolStripMenuItem, "storageServerToolStripMenuItem");
            this.storageServerToolStripMenuItem.Name = "storageServerToolStripMenuItem";
            this.storageServerToolStripMenuItem.Click += new System.EventHandler(this.storageServerToolStripMenuItem_Click);
            // 
            // pTToolStripMenuItem
            // 
            resources.ApplyResources(this.pTToolStripMenuItem, "pTToolStripMenuItem");
            this.pTToolStripMenuItem.Name = "pTToolStripMenuItem";
            this.pTToolStripMenuItem.Click += new System.EventHandler(this.pTToolStripMenuItem_Click);
            // 
            // DeviceMaintain
            // 
            resources.ApplyResources(this.DeviceMaintain, "DeviceMaintain");
            this.DeviceMaintain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rebootToolStripMenuItem,
            this.updataToolStripMenuItem,
            this.tieToolStripMenuItem});
            this.DeviceMaintain.Name = "DeviceMaintain";
            // 
            // rebootToolStripMenuItem
            // 
            resources.ApplyResources(this.rebootToolStripMenuItem, "rebootToolStripMenuItem");
            this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
            this.rebootToolStripMenuItem.Click += new System.EventHandler(this.RebooTooltStrip_Click);
            // 
            // updataToolStripMenuItem
            // 
            resources.ApplyResources(this.updataToolStripMenuItem, "updataToolStripMenuItem");
            this.updataToolStripMenuItem.Name = "updataToolStripMenuItem";
            this.updataToolStripMenuItem.Click += new System.EventHandler(this.UpdataToolStrip_Click);
            // 
            // tieToolStripMenuItem
            // 
            resources.ApplyResources(this.tieToolStripMenuItem, "tieToolStripMenuItem");
            this.tieToolStripMenuItem.Name = "tieToolStripMenuItem";
            this.tieToolStripMenuItem.Click += new System.EventHandler(this.TimingToolStrip_Click);
            // 
            // ExitStrip
            // 
            resources.ApplyResources(this.ExitStrip, "ExitStrip");
            this.ExitStrip.Name = "ExitStrip";
            this.ExitStrip.Click += new System.EventHandler(this.ExitStrip_Click);
            // 
            // g_TotalPanel
            // 
            resources.ApplyResources(this.g_TotalPanel, "g_TotalPanel");
            this.g_TotalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.g_TotalPanel.Controls.Add(this.m_rdoAlarmInformation);
            this.g_TotalPanel.Controls.Add(this.m_rdoLocalLog);
            this.g_TotalPanel.Controls.Add(this.m_previewWnd);
            this.g_TotalPanel.Controls.Add(this.m_panelDeviceLog);
            this.g_TotalPanel.Name = "g_TotalPanel";
            // 
            // m_panelDeviceLog
            // 
            resources.ApplyResources(this.m_panelDeviceLog, "m_panelDeviceLog");
            this.m_panelDeviceLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelDeviceLog.Name = "m_panelDeviceLog";
            this.m_panelDeviceLog.TabStop = true;
            // 
            // m_MenuMoudle
            // 
            resources.ApplyResources(this.m_MenuMoudle, "m_MenuMoudle");
            this.m_MenuMoudle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserManageStrip,
            this.FingerManageStrip,
            this.CardManageStrip,
            this.FaceManageStrip,
            this.WeekPlanStrip,
            this.AlarmStrip,
            this.toolStripMenuItem1});
            this.m_MenuMoudle.Name = "m_MenuElse";
            // 
            // UserManageStrip
            // 
            resources.ApplyResources(this.UserManageStrip, "UserManageStrip");
            this.UserManageStrip.Name = "UserManageStrip";
            this.UserManageStrip.Click += new System.EventHandler(this.UserManageStrip_Click);
            // 
            // FingerManageStrip
            // 
            resources.ApplyResources(this.FingerManageStrip, "FingerManageStrip");
            this.FingerManageStrip.Name = "FingerManageStrip";
            this.FingerManageStrip.Click += new System.EventHandler(this.FingerManageStrip_Click);
            // 
            // CardManageStrip
            // 
            resources.ApplyResources(this.CardManageStrip, "CardManageStrip");
            this.CardManageStrip.Name = "CardManageStrip";
            this.CardManageStrip.Click += new System.EventHandler(this.CardManageStrip_Click);
            // 
            // FaceManageStrip
            // 
            resources.ApplyResources(this.FaceManageStrip, "FaceManageStrip");
            this.FaceManageStrip.Name = "FaceManageStrip";
            this.FaceManageStrip.Click += new System.EventHandler(this.FaceManageStrip_Click);
            // 
            // WeekPlanStrip
            // 
            resources.ApplyResources(this.WeekPlanStrip, "WeekPlanStrip");
            this.WeekPlanStrip.Name = "WeekPlanStrip";
            this.WeekPlanStrip.Click += new System.EventHandler(this.WeekPlanStrip_Click);
            // 
            // AlarmStrip
            // 
            resources.ApplyResources(this.AlarmStrip, "AlarmStrip");
            this.AlarmStrip.Name = "AlarmStrip";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // m_ConfigStrip
            // 
            resources.ApplyResources(this.m_ConfigStrip, "m_ConfigStrip");
            this.m_ConfigStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CommonConfigStrip,
            this.XMLConfigStrip});
            this.m_ConfigStrip.Name = "m_MenuElse";
            // 
            // CommonConfigStrip
            // 
            resources.ApplyResources(this.CommonConfigStrip, "CommonConfigStrip");
            this.CommonConfigStrip.Name = "CommonConfigStrip";
            this.CommonConfigStrip.Click += new System.EventHandler(this.CommonConfigStrip_Click);
            // 
            // XMLConfigStrip
            // 
            resources.ApplyResources(this.XMLConfigStrip, "XMLConfigStrip");
            this.XMLConfigStrip.Name = "XMLConfigStrip";
            this.XMLConfigStrip.Click += new System.EventHandler(this.XMLConfigStrip_Click);
            // 
            // ISUPDemo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.versionInfo);
            this.Controls.Add(this.g_TotalPanel);
            this.Controls.Add(this.m_panelDeviceTree);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ISUPDemo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ISUPDemo_FormClosing);
            this.Load += new System.EventHandler(this.ISUPDemo_Load);
            this.versionInfo.ResumeLayout(false);
            this.versionInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.m_previewWnd.ResumeLayout(false);
            this.m_MenuElse.ResumeLayout(false);
            this.g_TotalPanel.ResumeLayout(false);
            this.g_TotalPanel.PerformLayout();
            this.m_MenuMoudle.ResumeLayout(false);
            this.m_ConfigStrip.ResumeLayout(false);
            this.ResumeLayout(false);

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
        public static byte m_byAlarmSecureAccessType;
        byte m_byStreamSecureAccessType;
        #endregion

        private System.Windows.Forms.GroupBox versionInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Panel m_panelDeviceTree;
        private System.Windows.Forms.TreeView treeView1;
        private Button alarmbtn;
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
        private ToolStripMenuItem CommonConfigureMenu;
        private Panel g_TotalPanel;
        //新增定制的内容
        private ToolStripMenuItem storageServerToolStripMenuItem;
        private ToolStripMenuItem pTToolStripMenuItem;
        private ContextMenuStrip m_MenuMoudle;
        private ToolStripMenuItem UserManageStrip;
        private ToolStripMenuItem FingerManageStrip;
        private ToolStripMenuItem CardManageStrip;
        private ToolStripMenuItem FaceManageStrip;
        private ToolStripMenuItem WeekPlanStrip;
        private ToolStripMenuItem AlarmStrip;
        private Button BtnManage;
        private Button BtnConfig;
        private ContextMenuStrip m_ConfigStrip;
        private ToolStripMenuItem CommonConfigStrip;
        private ToolStripMenuItem XMLConfigStrip;
        private ToolStripMenuItem ExitStrip;
        private ToolStripMenuItem AcsEventStrip;
        private ToolStripMenuItem VoiceTalkStrip;
        private Button buttonPlayBack;
        private ToolStripMenuItem DeviceMaintain;
        private ToolStripMenuItem rebootToolStripMenuItem;
        private ToolStripMenuItem updataToolStripMenuItem;
        private ToolStripMenuItem tieToolStripMenuItem;
        private Panel m_panelDeviceLog;
        private ToolStripMenuItem toolStripMenuItem1;

    }
}

