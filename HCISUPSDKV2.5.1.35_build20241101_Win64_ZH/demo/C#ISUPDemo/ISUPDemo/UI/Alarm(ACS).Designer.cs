namespace ISUPDemo.UI
{
    partial class CMSAlarm
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
            this.CloseAlarmBtn = new System.Windows.Forms.Button();
            this.AlarmControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CmsPic = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.AlarmBox = new System.Windows.Forms.RichTextBox();
            this.TempOnly = new System.Windows.Forms.TabPage();
            this.TemperatureInfo = new System.Windows.Forms.RichTextBox();
            this.ThemalPicture = new System.Windows.Forms.TabPage();
            this.ThemalPicBox = new System.Windows.Forms.PictureBox();
            this.AlarmControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmsPic)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.TempOnly.SuspendLayout();
            this.ThemalPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThemalPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseAlarmBtn
            // 
            this.CloseAlarmBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseAlarmBtn.Location = new System.Drawing.Point(621, 513);
            this.CloseAlarmBtn.Name = "CloseAlarmBtn";
            this.CloseAlarmBtn.Size = new System.Drawing.Size(86, 29);
            this.CloseAlarmBtn.TabIndex = 14;
            this.CloseAlarmBtn.Text = "CloseAlarm";
            this.CloseAlarmBtn.UseVisualStyleBackColor = true;
            this.CloseAlarmBtn.Click += new System.EventHandler(this.CloseAlarmBtn_Click);
            // 
            // AlarmControl
            // 
            this.AlarmControl.Controls.Add(this.tabPage1);
            this.AlarmControl.Controls.Add(this.tabPage2);
            this.AlarmControl.Controls.Add(this.TempOnly);
            this.AlarmControl.Controls.Add(this.ThemalPicture);
            this.AlarmControl.Location = new System.Drawing.Point(39, 26);
            this.AlarmControl.Name = "AlarmControl";
            this.AlarmControl.SelectedIndex = 0;
            this.AlarmControl.Size = new System.Drawing.Size(668, 479);
            this.AlarmControl.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.CmsPic);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(660, 453);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "VisiblePicture";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // CmsPic
            // 
            this.CmsPic.Location = new System.Drawing.Point(25, 18);
            this.CmsPic.Name = "CmsPic";
            this.CmsPic.Size = new System.Drawing.Size(607, 429);
            this.CmsPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CmsPic.TabIndex = 0;
            this.CmsPic.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.AlarmBox);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(660, 453);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "AcsAlarmInfo";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // AlarmBox
            // 
            this.AlarmBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AlarmBox.Location = new System.Drawing.Point(26, 36);
            this.AlarmBox.Name = "AlarmBox";
            this.AlarmBox.Size = new System.Drawing.Size(596, 395);
            this.AlarmBox.TabIndex = 0;
            this.AlarmBox.Text = "";
            // 
            // TempOnly
            // 
            this.TempOnly.Controls.Add(this.TemperatureInfo);
            this.TempOnly.Location = new System.Drawing.Point(4, 22);
            this.TempOnly.Name = "TempOnly";
            this.TempOnly.Size = new System.Drawing.Size(660, 453);
            this.TempOnly.TabIndex = 2;
            this.TempOnly.Text = "TemperatureAlarmInfo";
            this.TempOnly.UseVisualStyleBackColor = true;
            // 
            // TemperatureInfo
            // 
            this.TemperatureInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TemperatureInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TemperatureInfo.Location = new System.Drawing.Point(32, 29);
            this.TemperatureInfo.Name = "TemperatureInfo";
            this.TemperatureInfo.Size = new System.Drawing.Size(596, 395);
            this.TemperatureInfo.TabIndex = 1;
            this.TemperatureInfo.Text = "";
            // 
            // ThemalPicture
            // 
            this.ThemalPicture.Controls.Add(this.ThemalPicBox);
            this.ThemalPicture.Location = new System.Drawing.Point(4, 22);
            this.ThemalPicture.Name = "ThemalPicture";
            this.ThemalPicture.Size = new System.Drawing.Size(660, 453);
            this.ThemalPicture.TabIndex = 3;
            this.ThemalPicture.Text = "ThemalPicture";
            this.ThemalPicture.UseVisualStyleBackColor = true;
            // 
            // ThemalPicBox
            // 
            this.ThemalPicBox.Location = new System.Drawing.Point(27, 12);
            this.ThemalPicBox.Name = "ThemalPicBox";
            this.ThemalPicBox.Size = new System.Drawing.Size(607, 429);
            this.ThemalPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ThemalPicBox.TabIndex = 1;
            this.ThemalPicBox.TabStop = false;
            // 
            // CMSAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(730, 575);
            this.Controls.Add(this.AlarmControl);
            this.Controls.Add(this.CloseAlarmBtn);
            this.Name = "CMSAlarm";
            this.Text = "Alarm";
            this.AlarmControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CmsPic)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.TempOnly.ResumeLayout(false);
            this.ThemalPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ThemalPicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseAlarmBtn;
        private System.Windows.Forms.TabControl AlarmControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox AlarmBox;
        private System.Windows.Forms.PictureBox CmsPic;
        private System.Windows.Forms.TabPage TempOnly;
        private System.Windows.Forms.RichTextBox TemperatureInfo;
        private System.Windows.Forms.TabPage ThemalPicture;
        private System.Windows.Forms.PictureBox ThemalPicBox;
    }
}