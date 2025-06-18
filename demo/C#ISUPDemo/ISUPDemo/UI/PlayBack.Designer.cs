namespace ISUPDemo.UI
{
    partial class PlayBack
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
            this.buttonSearch = new System.Windows.Forms.Button();
            this.RecordFileEndTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.RecordFileStartTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonStartPlay = new System.Windows.Forms.Button();
            this.buttonStopPlay = new System.Windows.Forms.Button();
            this.m_PlayBackPanel = new System.Windows.Forms.Panel();
            this.listViewRecordFile = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RecordFileStartTime2 = new System.Windows.Forms.DateTimePicker();
            this.RecordFileEndTime2 = new System.Windows.Forms.DateTimePicker();
            this.checkBoxDownload = new System.Windows.Forms.CheckBox();
            this.m_cmbPlayType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textBoxSaveFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            this.buttonSearch.Enabled = false;
            this.buttonSearch.Location = new System.Drawing.Point(605, 299);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 81;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // RecordFileEndTime
            // 
            this.RecordFileEndTime.CalendarFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordFileEndTime.Location = new System.Drawing.Point(388, 257);
            this.RecordFileEndTime.Name = "RecordFileEndTime";
            this.RecordFileEndTime.Size = new System.Drawing.Size(132, 21);
            this.RecordFileEndTime.TabIndex = 82;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 85;
            this.label6.Text = "End Time:";
            // 
            // RecordFileStartTime
            // 
            this.RecordFileStartTime.CalendarFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordFileStartTime.Location = new System.Drawing.Point(387, 201);
            this.RecordFileStartTime.Name = "RecordFileStartTime";
            this.RecordFileStartTime.Size = new System.Drawing.Size(135, 21);
            this.RecordFileStartTime.TabIndex = 84;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(386, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 83;
            this.label5.Text = "Start Time:";
            // 
            // buttonStartPlay
            // 
            this.buttonStartPlay.Location = new System.Drawing.Point(415, 403);
            this.buttonStartPlay.Name = "buttonStartPlay";
            this.buttonStartPlay.Size = new System.Drawing.Size(73, 23);
            this.buttonStartPlay.TabIndex = 88;
            this.buttonStartPlay.Text = "StartPlay";
            this.buttonStartPlay.UseVisualStyleBackColor = true;
            this.buttonStartPlay.Click += new System.EventHandler(this.buttonStartPlay_Click);
            // 
            // buttonStopPlay
            // 
            this.buttonStopPlay.Location = new System.Drawing.Point(544, 403);
            this.buttonStopPlay.Name = "buttonStopPlay";
            this.buttonStopPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonStopPlay.TabIndex = 89;
            this.buttonStopPlay.Text = "StopPlay";
            this.buttonStopPlay.UseVisualStyleBackColor = true;
            this.buttonStopPlay.Click += new System.EventHandler(this.buttonStopPlay_Click);
            // 
            // m_PlayBackPanel
            // 
            this.m_PlayBackPanel.BackColor = System.Drawing.Color.Silver;
            this.m_PlayBackPanel.Location = new System.Drawing.Point(15, 184);
            this.m_PlayBackPanel.Name = "m_PlayBackPanel";
            this.m_PlayBackPanel.Size = new System.Drawing.Size(352, 252);
            this.m_PlayBackPanel.TabIndex = 87;
            // 
            // listViewRecordFile
            // 
            this.listViewRecordFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRecordFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listViewRecordFile.FullRowSelect = true;
            this.listViewRecordFile.GridLines = true;
            this.listViewRecordFile.HideSelection = false;
            this.listViewRecordFile.Location = new System.Drawing.Point(3, 15);
            this.listViewRecordFile.Name = "listViewRecordFile";
            this.listViewRecordFile.Size = new System.Drawing.Size(677, 152);
            this.listViewRecordFile.TabIndex = 93;
            this.listViewRecordFile.UseCompatibleStateImageBehavior = false;
            this.listViewRecordFile.View = System.Windows.Forms.View.Details;
            this.listViewRecordFile.Click += new System.EventHandler(this.listViewFileInfo_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 46;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "FileName";
            this.columnHeader2.Width = 104;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "FileSize";
            this.columnHeader3.Width = 83;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "StartTime";
            this.columnHeader4.Width = 118;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "EndTime";
            this.columnHeader5.Width = 122;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "MajorType";
            this.columnHeader6.Width = 68;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "MinorType";
            this.columnHeader7.Width = 74;
            // 
            // RecordFileStartTime2
            // 
            this.RecordFileStartTime2.CalendarFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordFileStartTime2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.RecordFileStartTime2.Location = new System.Drawing.Point(528, 201);
            this.RecordFileStartTime2.Name = "RecordFileStartTime2";
            this.RecordFileStartTime2.Size = new System.Drawing.Size(139, 21);
            this.RecordFileStartTime2.TabIndex = 94;
            // 
            // RecordFileEndTime2
            // 
            this.RecordFileEndTime2.CalendarFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordFileEndTime2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.RecordFileEndTime2.Location = new System.Drawing.Point(526, 257);
            this.RecordFileEndTime2.Name = "RecordFileEndTime2";
            this.RecordFileEndTime2.Size = new System.Drawing.Size(141, 21);
            this.RecordFileEndTime2.TabIndex = 95;
            // 
            // checkBoxDownload
            // 
            this.checkBoxDownload.AutoSize = true;
            this.checkBoxDownload.Location = new System.Drawing.Point(386, 354);
            this.checkBoxDownload.Name = "checkBoxDownload";
            this.checkBoxDownload.Size = new System.Drawing.Size(72, 16);
            this.checkBoxDownload.TabIndex = 96;
            this.checkBoxDownload.Text = "Download";
            this.checkBoxDownload.UseVisualStyleBackColor = true;
            this.checkBoxDownload.CheckedChanged += new System.EventHandler(this.checkBoxDownload_CheckedChanged);
            // 
            // m_cmbPlayType
            // 
            this.m_cmbPlayType.FormattingEnabled = true;
            this.m_cmbPlayType.Location = new System.Drawing.Point(473, 299);
            this.m_cmbPlayType.Name = "m_cmbPlayType";
            this.m_cmbPlayType.Size = new System.Drawing.Size(116, 20);
            this.m_cmbPlayType.TabIndex = 97;
            this.m_cmbPlayType.Text = "Time";
            this.m_cmbPlayType.SelectedIndexChanged += new System.EventHandler(this.m_cmbPlayType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(386, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 98;
            this.label1.Text = "PlayBack by:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(605, 354);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 99;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // textBoxSaveFilePath
            // 
            this.textBoxSaveFilePath.Location = new System.Drawing.Point(464, 352);
            this.textBoxSaveFilePath.Name = "textBoxSaveFilePath";
            this.textBoxSaveFilePath.Size = new System.Drawing.Size(135, 21);
            this.textBoxSaveFilePath.TabIndex = 100;
            this.textBoxSaveFilePath.Text = "C:\\";
            // 
            // PlayBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textBoxSaveFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cmbPlayType);
            this.Controls.Add(this.checkBoxDownload);
            this.Controls.Add(this.RecordFileEndTime2);
            this.Controls.Add(this.RecordFileStartTime2);
            this.Controls.Add(this.listViewRecordFile);
            this.Controls.Add(this.buttonStopPlay);
            this.Controls.Add(this.buttonStartPlay);
            this.Controls.Add(this.m_PlayBackPanel);
            this.Controls.Add(this.RecordFileEndTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RecordFileStartTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonSearch);
            this.Name = "PlayBack";
            this.Size = new System.Drawing.Size(687, 441);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DateTimePicker RecordFileEndTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker RecordFileStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonStartPlay;
        private System.Windows.Forms.Button buttonStopPlay;
        private System.Windows.Forms.Panel m_PlayBackPanel;
        private System.Windows.Forms.ListView listViewRecordFile;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.DateTimePicker RecordFileStartTime2;
        private System.Windows.Forms.DateTimePicker RecordFileEndTime2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBoxDownload;
        private System.Windows.Forms.ComboBox m_cmbPlayType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textBoxSaveFilePath;
    }
}