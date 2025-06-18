namespace ISUPDemo.UI
{
    partial class FaceManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceManage));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Face_EmpNoBox = new System.Windows.Forms.TextBox();
            this.Face_DataBaseNo = new System.Windows.Forms.TextBox();
            this.Face_LibType = new System.Windows.Forms.ComboBox();
            this.FaceCaptureBtn = new System.Windows.Forms.Button();
            this.FaceSetBtn = new System.Windows.Forms.Button();
            this.FaceGetBtn = new System.Windows.Forms.Button();
            this.FaceDeleteBtn = new System.Windows.Forms.Button();
            this.FacepictureBox = new System.Windows.Forms.PictureBox();
            this.FaceClearBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.FaceText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FacepictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(264, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "EmployeeNo：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(264, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Face DataBase No：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(264, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Face Lib Type：";
            // 
            // Face_EmpNoBox
            // 
            this.Face_EmpNoBox.Location = new System.Drawing.Point(425, 132);
            this.Face_EmpNoBox.Name = "Face_EmpNoBox";
            this.Face_EmpNoBox.Size = new System.Drawing.Size(129, 21);
            this.Face_EmpNoBox.TabIndex = 4;
            // 
            // Face_DataBaseNo
            // 
            this.Face_DataBaseNo.Location = new System.Drawing.Point(425, 187);
            this.Face_DataBaseNo.Name = "Face_DataBaseNo";
            this.Face_DataBaseNo.Size = new System.Drawing.Size(129, 21);
            this.Face_DataBaseNo.TabIndex = 6;
            this.Face_DataBaseNo.Text = "1";
            // 
            // Face_LibType
            // 
            this.Face_LibType.FormattingEnabled = true;
            this.Face_LibType.Items.AddRange(new object[] {
            "blackFD",
            "staticFD"});
            this.Face_LibType.Location = new System.Drawing.Point(425, 242);
            this.Face_LibType.Name = "Face_LibType";
            this.Face_LibType.Size = new System.Drawing.Size(129, 20);
            this.Face_LibType.TabIndex = 7;
            this.Face_LibType.Text = "blackFD";
            // 
            // FaceCaptureBtn
            // 
            this.FaceCaptureBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FaceCaptureBtn.Location = new System.Drawing.Point(259, 400);
            this.FaceCaptureBtn.Name = "FaceCaptureBtn";
            this.FaceCaptureBtn.Size = new System.Drawing.Size(75, 23);
            this.FaceCaptureBtn.TabIndex = 8;
            this.FaceCaptureBtn.Text = "Capture";
            this.FaceCaptureBtn.UseVisualStyleBackColor = true;
            this.FaceCaptureBtn.Click += new System.EventHandler(this.FaceCaptureBtn_Click);
            // 
            // FaceSetBtn
            // 
            this.FaceSetBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FaceSetBtn.Location = new System.Drawing.Point(370, 400);
            this.FaceSetBtn.Name = "FaceSetBtn";
            this.FaceSetBtn.Size = new System.Drawing.Size(75, 23);
            this.FaceSetBtn.TabIndex = 10;
            this.FaceSetBtn.Text = "Set";
            this.FaceSetBtn.UseVisualStyleBackColor = true;
            this.FaceSetBtn.Click += new System.EventHandler(this.FaceSetBtn_Click);
            // 
            // FaceGetBtn
            // 
            this.FaceGetBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FaceGetBtn.Location = new System.Drawing.Point(470, 400);
            this.FaceGetBtn.Name = "FaceGetBtn";
            this.FaceGetBtn.Size = new System.Drawing.Size(75, 23);
            this.FaceGetBtn.TabIndex = 11;
            this.FaceGetBtn.Text = "Get";
            this.FaceGetBtn.UseVisualStyleBackColor = true;
            this.FaceGetBtn.Click += new System.EventHandler(this.FaceGetBtn_Click);
            // 
            // FaceDeleteBtn
            // 
            this.FaceDeleteBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FaceDeleteBtn.Location = new System.Drawing.Point(571, 400);
            this.FaceDeleteBtn.Name = "FaceDeleteBtn";
            this.FaceDeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.FaceDeleteBtn.TabIndex = 12;
            this.FaceDeleteBtn.Text = "Delete";
            this.FaceDeleteBtn.UseVisualStyleBackColor = true;
            this.FaceDeleteBtn.Click += new System.EventHandler(this.FaceDeleteBtn_Click);
            // 
            // FacepictureBox
            // 
            this.FacepictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FacepictureBox.Location = new System.Drawing.Point(12, 133);
            this.FacepictureBox.Name = "FacepictureBox";
            this.FacepictureBox.Size = new System.Drawing.Size(229, 239);
            this.FacepictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FacepictureBox.TabIndex = 13;
            this.FacepictureBox.TabStop = false;
            // 
            // FaceClearBtn
            // 
            this.FaceClearBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FaceClearBtn.Location = new System.Drawing.Point(161, 400);
            this.FaceClearBtn.Name = "FaceClearBtn";
            this.FaceClearBtn.Size = new System.Drawing.Size(75, 23);
            this.FaceClearBtn.TabIndex = 14;
            this.FaceClearBtn.Text = "Clear";
            this.FaceClearBtn.UseVisualStyleBackColor = true;
            this.FaceClearBtn.Click += new System.EventHandler(this.FaceClearBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 123);
            this.panel1.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(207, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(303, 41);
            this.label5.TabIndex = 1;
            this.label5.Text = "Face Management";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(560, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 14);
            this.label6.TabIndex = 59;
            this.label6.Text = "Set/Get/Delete Need";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(260, 504);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 14);
            this.label13.TabIndex = 83;
            this.label13.Text = "and more than 4k!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(261, 486);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(378, 14);
            this.label7.TabIndex = 82;
            this.label7.Text = "But Remember,the face picture should less than 200k! ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(260, 469);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(238, 14);
            this.label8.TabIndex = 81;
            this.label8.Text = "capture or browse a face picture.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(260, 453);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(266, 14);
            this.label9.TabIndex = 80;
            this.label9.Text = "Before set face to device,you should ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(7, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 84;
            this.button1.Text = "Show";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FaceText
            // 
            this.FaceText.Location = new System.Drawing.Point(425, 351);
            this.FaceText.Name = "FaceText";
            this.FaceText.Size = new System.Drawing.Size(129, 21);
            this.FaceText.TabIndex = 85;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(264, 355);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 14);
            this.label10.TabIndex = 86;
            this.label10.Text = "Face Url:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(264, 301);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 14);
            this.label11.TabIndex = 87;
            this.label11.Text = "Name:";
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(425, 296);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(129, 21);
            this.TextName.TabIndex = 88;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(560, 189);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 89;
            this.label12.Text = "1 Or 2";
            // 
            // FaceManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(706, 537);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.FaceText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.FaceClearBtn);
            this.Controls.Add(this.FacepictureBox);
            this.Controls.Add(this.FaceDeleteBtn);
            this.Controls.Add(this.FaceGetBtn);
            this.Controls.Add(this.FaceSetBtn);
            this.Controls.Add(this.FaceCaptureBtn);
            this.Controls.Add(this.Face_LibType);
            this.Controls.Add(this.Face_DataBaseNo);
            this.Controls.Add(this.Face_EmpNoBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "FaceManage";
            this.Text = "FaceManage";
            ((System.ComponentModel.ISupportInitialize)(this.FacepictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Face_EmpNoBox;
        private System.Windows.Forms.TextBox Face_DataBaseNo;
        private System.Windows.Forms.ComboBox Face_LibType;
        private System.Windows.Forms.Button FaceCaptureBtn;
        private System.Windows.Forms.Button FaceSetBtn;
        private System.Windows.Forms.Button FaceGetBtn;
        private System.Windows.Forms.Button FaceDeleteBtn;
        private System.Windows.Forms.PictureBox FacepictureBox;
        private System.Windows.Forms.Button FaceClearBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox FaceText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.Label label12;
    }
}