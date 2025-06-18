namespace ISUPDemo.UI
{
    partial class Alarm
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
            this.csAlarmBox = new System.Windows.Forms.RichTextBox();
            this.listViewAlarmInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.csPicUrl = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.StopResultText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // csAlarmBox
            // 
            this.csAlarmBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.csAlarmBox.Location = new System.Drawing.Point(441, 12);
            this.csAlarmBox.Name = "csAlarmBox";
            this.csAlarmBox.Size = new System.Drawing.Size(349, 523);
            this.csAlarmBox.TabIndex = 2;
            this.csAlarmBox.Text = "";
            // 
            // listViewAlarmInfo
            // 
            this.listViewAlarmInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAlarmInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.listViewAlarmInfo.FullRowSelect = true;
            this.listViewAlarmInfo.GridLines = true;
            this.listViewAlarmInfo.HideSelection = false;
            this.listViewAlarmInfo.Location = new System.Drawing.Point(14, 12);
            this.listViewAlarmInfo.Name = "listViewAlarmInfo";
            this.listViewAlarmInfo.Size = new System.Drawing.Size(421, 523);
            this.listViewAlarmInfo.TabIndex = 94;
            this.listViewAlarmInfo.UseCompatibleStateImageBehavior = false;
            this.listViewAlarmInfo.View = System.Windows.Forms.View.Details;
            this.listViewAlarmInfo.Click += new System.EventHandler(this.listViewAlarmInfo_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 132;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "EventType";
            this.columnHeader2.Width = 121;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "DeviceID";
            this.columnHeader4.Width = 76;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Msg";
            this.columnHeader3.Width = 200;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(12, 555);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 14);
            this.label6.TabIndex = 96;
            this.label6.Text = "PicUrl:";
            // 
            // csPicUrl
            // 
            this.csPicUrl.AutoSize = true;
            this.csPicUrl.BackColor = System.Drawing.SystemColors.Control;
            this.csPicUrl.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csPicUrl.ForeColor = System.Drawing.Color.Black;
            this.csPicUrl.Location = new System.Drawing.Point(74, 555);
            this.csPicUrl.Name = "csPicUrl";
            this.csPicUrl.Size = new System.Drawing.Size(35, 14);
            this.csPicUrl.TabIndex = 97;
            this.csPicUrl.Text = "null";
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(468, 574);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(166, 33);
            this.btnStop.TabIndex = 99;
            this.btnStop.Text = "StopAlarmListen";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // StopResultText
            // 
            this.StopResultText.AutoSize = true;
            this.StopResultText.BackColor = System.Drawing.SystemColors.Control;
            this.StopResultText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopResultText.ForeColor = System.Drawing.Color.Black;
            this.StopResultText.Location = new System.Drawing.Point(648, 584);
            this.StopResultText.Name = "StopResultText";
            this.StopResultText.Size = new System.Drawing.Size(0, 14);
            this.StopResultText.TabIndex = 100;
            // 
            // Alarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StopResultText);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.csPicUrl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listViewAlarmInfo);
            this.Controls.Add(this.csAlarmBox);
            this.Name = "Alarm";
            this.Size = new System.Drawing.Size(793, 606);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox csAlarmBox;
        private System.Windows.Forms.ListView listViewAlarmInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label csPicUrl;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label StopResultText;
    }
}