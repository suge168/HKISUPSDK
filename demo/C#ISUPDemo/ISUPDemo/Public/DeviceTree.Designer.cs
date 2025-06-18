namespace ISUPDemo.Public
{
    partial class DeviceTree
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
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeImageList = new System.Windows.Forms.ImageList(this.components);
            this.treeEhomeKeyMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setEhomeKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeEhomeKeyMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.treeImageList;
            this.treeView1.Location = new System.Drawing.Point(1, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(138, 590);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // treeImageList
            // 
            this.treeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.treeImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // treeEhomeKeyMenuStrip
            // 
            this.treeEhomeKeyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setEhomeKeyToolStripMenuItem});
            this.treeEhomeKeyMenuStrip.Name = "treeEhomeKeyMenuStrip";
            this.treeEhomeKeyMenuStrip.Size = new System.Drawing.Size(164, 26);
            // 
            // setEhomeKeyToolStripMenuItem
            // 
            this.setEhomeKeyToolStripMenuItem.Name = "setEhomeKeyToolStripMenuItem";
            this.setEhomeKeyToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.setEhomeKeyToolStripMenuItem.Text = "Set Ehome Key";
            this.setEhomeKeyToolStripMenuItem.Click += new System.EventHandler(this.setEhomeKeyToolStripMenuItem_Click);
            // 
            // DeviceTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Name = "DeviceTree";
            this.Size = new System.Drawing.Size(142, 590);
            this.treeEhomeKeyMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList treeImageList;
        private System.Windows.Forms.ContextMenuStrip treeEhomeKeyMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setEhomeKeyToolStripMenuItem;
    }
}
