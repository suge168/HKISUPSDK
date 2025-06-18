namespace ISUPDemo
{
    partial class XMLConfig
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rtbOutputXml = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.rtbInputXml = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbUrl = new System.Windows.Forms.TextBox();
			this.cbCmd = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonOperate = new System.Windows.Forms.Button();
			this.treeProtocol = new System.Windows.Forms.TreeView();
			this.treeImageList = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(163, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "URL";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rtbOutputXml);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.rtbInputXml);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(15, 41);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(531, 470);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "parameters";
			// 
			// rtbOutputXml
			// 
			this.rtbOutputXml.Location = new System.Drawing.Point(73, 243);
			this.rtbOutputXml.Name = "rtbOutputXml";
			this.rtbOutputXml.Size = new System.Drawing.Size(449, 207);
			this.rtbOutputXml.TabIndex = 3;
			this.rtbOutputXml.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(2, 246);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "output xml";
			// 
			// rtbInputXml
			// 
			this.rtbInputXml.Location = new System.Drawing.Point(73, 27);
			this.rtbInputXml.Name = "rtbInputXml";
			this.rtbInputXml.Size = new System.Drawing.Size(449, 194);
			this.rtbInputXml.TabIndex = 1;
			this.rtbInputXml.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "input xml";
			// 
			// tbUrl
			// 
			this.tbUrl.Location = new System.Drawing.Point(192, 14);
			this.tbUrl.Name = "tbUrl";
			this.tbUrl.Size = new System.Drawing.Size(345, 21);
			this.tbUrl.TabIndex = 2;
			// 
			// cbCmd
			// 
			this.cbCmd.FormattingEnabled = true;
			this.cbCmd.Items.AddRange(new object[] {
            "GET",
            "PUT",
            "POST",
            "DELETE"});
			this.cbCmd.Location = new System.Drawing.Point(75, 14);
			this.cbCmd.Name = "cbCmd";
			this.cbCmd.Size = new System.Drawing.Size(73, 20);
			this.cbCmd.TabIndex = 3;
			this.cbCmd.Text = "GET";
			this.cbCmd.SelectedIndexChanged += new System.EventHandler(this.cbCmd_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "Method";
			// 
			// buttonOperate
			// 
			this.buttonOperate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.buttonOperate.Location = new System.Drawing.Point(677, 553);
			this.buttonOperate.Name = "buttonOperate";
			this.buttonOperate.Size = new System.Drawing.Size(137, 33);
			this.buttonOperate.TabIndex = 5;
			this.buttonOperate.Text = "operate";
			this.buttonOperate.UseVisualStyleBackColor = true;
			this.buttonOperate.Click += new System.EventHandler(this.buttonOperate_Click);
			// 
			// treeProtocol
			// 
			this.treeProtocol.ImageList = this.treeImageList;
			this.treeProtocol.Location = new System.Drawing.Point(6, 20);
			this.treeProtocol.Name = "treeProtocol";
			this.treeProtocol.Size = new System.Drawing.Size(230, 471);
			this.treeProtocol.TabIndex = 0;
			this.treeProtocol.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeProtocolDoubleClicked);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.AutoScroll = true;
			this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Panel2.Controls.Add(this.tbUrl);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.cbCmd);
			this.splitContainer1.Size = new System.Drawing.Size(802, 526);
			this.splitContainer1.SplitterDistance = 255;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 7;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.treeProtocol);
			this.groupBox2.Location = new System.Drawing.Point(12, 14);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(242, 497);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Collection";
			// 
			// XMLConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(826, 593);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.buttonOperate);
			this.Name = "XMLConfig";
			this.Text = "XMLConfig";
			this.Load += new System.EventHandler(this.XMLConfig_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.RichTextBox rtbOutputXml;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbInputXml;
        private System.Windows.Forms.ComboBox cbCmd;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonOperate;
		private System.Windows.Forms.TreeView treeProtocol;
		private System.Windows.Forms.ImageList treeImageList;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox2;
    }
}