namespace ISUPDemo.UI
{
    partial class FingerManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FingerManager));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEmployeeNo = new System.Windows.Forms.TextBox();
            this.textBoxCardReadNo = new System.Windows.Forms.TextBox();
            this.textBoxFingerPrintNo = new System.Windows.Forms.TextBox();
            this.textBoxFingerData = new System.Windows.Forms.TextBox();
            this.btnFingerGet = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.LabelProcess = new System.Windows.Forms.Label();
            this.BtnCapture = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.BtnScan = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Employee NO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(60, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Card Read No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(60, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Finger Print ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(60, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Finger Data:";
            // 
            // textBoxEmployeeNo
            // 
            this.textBoxEmployeeNo.Location = new System.Drawing.Point(210, 132);
            this.textBoxEmployeeNo.Name = "textBoxEmployeeNo";
            this.textBoxEmployeeNo.Size = new System.Drawing.Size(140, 21);
            this.textBoxEmployeeNo.TabIndex = 4;
            // 
            // textBoxCardReadNo
            // 
            this.textBoxCardReadNo.Location = new System.Drawing.Point(210, 165);
            this.textBoxCardReadNo.Name = "textBoxCardReadNo";
            this.textBoxCardReadNo.Size = new System.Drawing.Size(140, 21);
            this.textBoxCardReadNo.TabIndex = 5;
            // 
            // textBoxFingerPrintNo
            // 
            this.textBoxFingerPrintNo.Location = new System.Drawing.Point(210, 198);
            this.textBoxFingerPrintNo.Name = "textBoxFingerPrintNo";
            this.textBoxFingerPrintNo.Size = new System.Drawing.Size(140, 21);
            this.textBoxFingerPrintNo.TabIndex = 6;
            // 
            // textBoxFingerData
            // 
            this.textBoxFingerData.Location = new System.Drawing.Point(210, 231);
            this.textBoxFingerData.Name = "textBoxFingerData";
            this.textBoxFingerData.Size = new System.Drawing.Size(140, 21);
            this.textBoxFingerData.TabIndex = 7;
            // 
            // btnFingerGet
            // 
            this.btnFingerGet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFingerGet.Location = new System.Drawing.Point(168, 303);
            this.btnFingerGet.Name = "btnFingerGet";
            this.btnFingerGet.Size = new System.Drawing.Size(83, 22);
            this.btnFingerGet.TabIndex = 9;
            this.btnFingerGet.Text = "Get";
            this.btnFingerGet.UseVisualStyleBackColor = true;
            this.btnFingerGet.Click += new System.EventHandler(this.btnFingerGet_Click);
            // 
            // btnSet
            // 
            this.btnSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSet.Location = new System.Drawing.Point(277, 303);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(83, 22);
            this.btnSet.TabIndex = 10;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Location = new System.Drawing.Point(380, 303);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 22);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // LabelProcess
            // 
            this.LabelProcess.AutoSize = true;
            this.LabelProcess.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelProcess.Location = new System.Drawing.Point(254, 270);
            this.LabelProcess.Name = "LabelProcess";
            this.LabelProcess.Size = new System.Drawing.Size(0, 14);
            this.LabelProcess.TabIndex = 12;
            // 
            // BtnCapture
            // 
            this.BtnCapture.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCapture.Location = new System.Drawing.Point(73, 302);
            this.BtnCapture.Name = "BtnCapture";
            this.BtnCapture.Size = new System.Drawing.Size(75, 23);
            this.BtnCapture.TabIndex = 13;
            this.BtnCapture.Text = "Capture";
            this.BtnCapture.UseVisualStyleBackColor = true;
            this.BtnCapture.Click += new System.EventHandler(this.BtnCapture_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(377, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(231, 14);
            this.label6.TabIndex = 59;
            this.label6.Text = "Set/Get/Delete Finger Print Need";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(377, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(231, 14);
            this.label5.TabIndex = 62;
            this.label5.Text = "Set/Get/Delete Finger Print Need";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(380, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(231, 14);
            this.label7.TabIndex = 65;
            this.label7.Text = "Set/Get/Delete Finger Print Need";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(86, 351);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(357, 14);
            this.label8.TabIndex = 70;
            this.label8.Text = "If Device doesn\'t has FP,you must capture FP First";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 109);
            this.panel1.TabIndex = 71;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(81, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(455, 41);
            this.label10.TabIndex = 1;
            this.label10.Text = "Finger Print Management";
            // 
            // BtnScan
            // 
            this.BtnScan.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnScan.Location = new System.Drawing.Point(380, 231);
            this.BtnScan.Name = "BtnScan";
            this.BtnScan.Size = new System.Drawing.Size(75, 23);
            this.BtnScan.TabIndex = 72;
            this.BtnScan.Text = "Scan";
            this.BtnScan.UseVisualStyleBackColor = true;
            this.BtnScan.Click += new System.EventHandler(this.BtnScan_Click);
            // 
            // FingerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(650, 406);
            this.Controls.Add(this.BtnScan);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BtnCapture);
            this.Controls.Add(this.LabelProcess);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnFingerGet);
            this.Controls.Add(this.textBoxFingerData);
            this.Controls.Add(this.textBoxFingerPrintNo);
            this.Controls.Add(this.textBoxCardReadNo);
            this.Controls.Add(this.textBoxEmployeeNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FingerManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FingerManager";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEmployeeNo;
        private System.Windows.Forms.TextBox textBoxCardReadNo;
        private System.Windows.Forms.TextBox textBoxFingerPrintNo;
        private System.Windows.Forms.TextBox textBoxFingerData;
        private System.Windows.Forms.Button btnFingerGet;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label LabelProcess;
        private System.Windows.Forms.Button BtnCapture;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BtnScan;
    }
}