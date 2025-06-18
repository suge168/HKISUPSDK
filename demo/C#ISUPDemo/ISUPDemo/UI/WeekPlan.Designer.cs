namespace ISUPDemo.UI
{
    partial class WeekPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeekPlan));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDeviceType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxWPNumber = new System.Windows.Forms.TextBox();
            this.checkBoxEnableWP = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLCID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDate = new System.Windows.Forms.ComboBox();
            this.listViewTimeSegment = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.dTPStartTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dTPEndTime = new System.Windows.Forms.DateTimePicker();
            this.checkBoxEnableTime = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbDoorStateMode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVerifyMode = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(-2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 110);
            this.panel1.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(234, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(189, 41);
            this.label9.TabIndex = 1;
            this.label9.Text = "Week Plan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(35, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "UserType：";
            // 
            // cbDeviceType
            // 
            this.cbDeviceType.FormattingEnabled = true;
            this.cbDeviceType.Items.AddRange(new object[] {
            "User",
            "CardReader",
            "Door"});
            this.cbDeviceType.Location = new System.Drawing.Point(106, 128);
            this.cbDeviceType.Name = "cbDeviceType";
            this.cbDeviceType.Size = new System.Drawing.Size(85, 20);
            this.cbDeviceType.TabIndex = 16;
            this.cbDeviceType.SelectedIndexChanged += new System.EventHandler(this.cbDeviceType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(410, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "WeekPlan Number:";
            // 
            // textBoxWPNumber
            // 
            this.textBoxWPNumber.Location = new System.Drawing.Point(539, 128);
            this.textBoxWPNumber.Name = "textBoxWPNumber";
            this.textBoxWPNumber.Size = new System.Drawing.Size(100, 21);
            this.textBoxWPNumber.TabIndex = 18;
            // 
            // checkBoxEnableWP
            // 
            this.checkBoxEnableWP.AutoSize = true;
            this.checkBoxEnableWP.Location = new System.Drawing.Point(35, 167);
            this.checkBoxEnableWP.Name = "checkBoxEnableWP";
            this.checkBoxEnableWP.Size = new System.Drawing.Size(60, 16);
            this.checkBoxEnableWP.TabIndex = 19;
            this.checkBoxEnableWP.Text = "Enable";
            this.checkBoxEnableWP.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(410, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "LocalControllerID:";
            // 
            // textBoxLCID
            // 
            this.textBoxLCID.Location = new System.Drawing.Point(539, 162);
            this.textBoxLCID.Name = "textBoxLCID";
            this.textBoxLCID.Size = new System.Drawing.Size(100, 21);
            this.textBoxLCID.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(218, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "Date：";
            // 
            // cbDate
            // 
            this.cbDate.FormattingEnabled = true;
            this.cbDate.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.cbDate.Location = new System.Drawing.Point(259, 209);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(91, 20);
            this.cbDate.TabIndex = 23;
            this.cbDate.Text = "Monday";
            this.cbDate.SelectedIndexChanged += new System.EventHandler(this.cbDate_SelectedIndexChanged);
            // 
            // listViewTimeSegment
            // 
            this.listViewTimeSegment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTimeSegment.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewTimeSegment.FullRowSelect = true;
            this.listViewTimeSegment.GridLines = true;
            this.listViewTimeSegment.HideSelection = false;
            this.listViewTimeSegment.Location = new System.Drawing.Point(32, 259);
            this.listViewTimeSegment.Name = "listViewTimeSegment";
            this.listViewTimeSegment.Size = new System.Drawing.Size(607, 161);
            this.listViewTimeSegment.TabIndex = 2;
            this.listViewTimeSegment.UseCompatibleStateImageBehavior = false;
            this.listViewTimeSegment.View = System.Windows.Forms.View.Details;
            this.listViewTimeSegment.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewTimeSegment_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time Segment";
            this.columnHeader1.Width = 123;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Enable";
            this.columnHeader2.Width = 101;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Start Time";
            this.columnHeader3.Width = 132;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "End Time";
            this.columnHeader4.Width = 132;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "VerifyMode";
            this.columnHeader5.Width = 110;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "DoorStatusMode";
            this.columnHeader6.Width = 110;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 446);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "Start Time:";
            // 
            // dTPStartTime
            // 
            this.dTPStartTime.CalendarFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTPStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dTPStartTime.Location = new System.Drawing.Point(129, 440);
            this.dTPStartTime.Name = "dTPStartTime";
            this.dTPStartTime.ShowUpDown = true;
            this.dTPStartTime.Size = new System.Drawing.Size(98, 21);
            this.dTPStartTime.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 446);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "End Time:";
            // 
            // dTPEndTime
            // 
            this.dTPEndTime.CalendarFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTPEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dTPEndTime.Location = new System.Drawing.Point(475, 440);
            this.dTPEndTime.Name = "dTPEndTime";
            this.dTPEndTime.ShowUpDown = true;
            this.dTPEndTime.Size = new System.Drawing.Size(106, 21);
            this.dTPEndTime.TabIndex = 6;
            // 
            // checkBoxEnableTime
            // 
            this.checkBoxEnableTime.AutoSize = true;
            this.checkBoxEnableTime.Location = new System.Drawing.Point(52, 484);
            this.checkBoxEnableTime.Name = "checkBoxEnableTime";
            this.checkBoxEnableTime.Size = new System.Drawing.Size(60, 16);
            this.checkBoxEnableTime.TabIndex = 28;
            this.checkBoxEnableTime.Text = "Enable";
            this.checkBoxEnableTime.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(410, 488);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "DoorStateMode:";
            // 
            // cbDoorStateMode
            // 
            this.cbDoorStateMode.FormattingEnabled = true;
            this.cbDoorStateMode.Items.AddRange(new object[] {
            "Invalid",
            "Sleep",
            "RemainOpen",
            "RemainClosed",
            "Normal"});
            this.cbDoorStateMode.Location = new System.Drawing.Point(523, 485);
            this.cbDoorStateMode.Name = "cbDoorStateMode";
            this.cbDoorStateMode.Size = new System.Drawing.Size(121, 20);
            this.cbDoorStateMode.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(410, 509);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "VerifyMode:";
            // 
            // cbVerifyMode
            // 
            this.cbVerifyMode.FormattingEnabled = true;
            this.cbVerifyMode.Items.AddRange(new object[] {
            "0-Invalid",
            "1-sleep",
            "2-cardOrfaceOrPw",
            "3-fpOrface",
            "4-faceOrfaceAndCard",
            "5-employeeNoAndFace",
            "6-faceAndPwAndFp",
            "7-faceAndFpAndCard",
            "8-employeeNoAndFpAndPw",
            "9-employeeNoAndFp",
            "10-fpOrPw",
            "11-employeeNoAndPw",
            "12-face",
            "13-faceAndCard",
            "14-faceAndPw",
            "15-faceAndFp",
            "16-faceOrFpOrCardOrPw",
            "17-fpAndCardAndPw",
            "18-fpAndCard",
            "19-fpOrCard",
            "20-fpAndPw",
            "21-fp",
            "22-cardOrPw",
            "23-card",
            "24-cardAndPw",
            "25-cardOrFace",
            "26-cardOrFaceOrFp"});
            this.cbVerifyMode.Location = new System.Drawing.Point(523, 506);
            this.cbVerifyMode.Name = "cbVerifyMode";
            this.cbVerifyMode.Size = new System.Drawing.Size(121, 20);
            this.cbVerifyMode.TabIndex = 32;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(54, 526);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 33;
            this.btnAdd.Text = "update";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(54, 576);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 34;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(412, 576);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 35;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // WeekPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 620);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbVerifyMode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbDoorStateMode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBoxEnableTime);
            this.Controls.Add(this.dTPEndTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dTPStartTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listViewTimeSegment);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxLCID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxEnableWP);
            this.Controls.Add(this.textBoxWPNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDeviceType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "WeekPlan";
            this.Text = "WeekPlan";
            this.Load += new System.EventHandler(this.WeekPlan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDeviceType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxWPNumber;
        private System.Windows.Forms.CheckBox checkBoxEnableWP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLCID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDate;
        private System.Windows.Forms.ListView listViewTimeSegment;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dTPStartTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dTPEndTime;
        private System.Windows.Forms.CheckBox checkBoxEnableTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbDoorStateMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbVerifyMode;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnSet;
    }
}