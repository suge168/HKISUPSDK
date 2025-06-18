namespace ISUPDemo
{
    partial class PT_PROXY
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
            this.btnStartListen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStopListen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxListeningIP = new System.Windows.Forms.TextBox();
            this.textBoxListeningPort = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartListen
            // 
            this.btnStartListen.Location = new System.Drawing.Point(83, 130);
            this.btnStartListen.Name = "btnStartListen";
            this.btnStartListen.Size = new System.Drawing.Size(103, 23);
            this.btnStartListen.TabIndex = 0;
            this.btnStartListen.Text = "StartListen";
            this.btnStartListen.UseVisualStyleBackColor = true;
            this.btnStartListen.Click += new System.EventHandler(this.btnStartListen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxListeningPort);
            this.groupBox1.Controls.Add(this.textBoxListeningIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 99);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "private SDK protocol proxy";
            // 
            // btnStopListen
            // 
            this.btnStopListen.Location = new System.Drawing.Point(371, 130);
            this.btnStopListen.Name = "btnStopListen";
            this.btnStopListen.Size = new System.Drawing.Size(103, 23);
            this.btnStopListen.TabIndex = 2;
            this.btnStopListen.Text = "StopListen";
            this.btnStopListen.UseVisualStyleBackColor = true;
            this.btnStopListen.Click += new System.EventHandler(this.btnStopListen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Listenling IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Listening Port:";
            // 
            // textBoxListeningIP
            // 
            this.textBoxListeningIP.Location = new System.Drawing.Point(132, 43);
            this.textBoxListeningIP.Name = "textBoxListeningIP";
            this.textBoxListeningIP.Size = new System.Drawing.Size(123, 22);
            this.textBoxListeningIP.TabIndex = 2;
            // 
            // textBoxListeningPort
            // 
            this.textBoxListeningPort.Location = new System.Drawing.Point(400, 43);
            this.textBoxListeningPort.Name = "textBoxListeningPort";
            this.textBoxListeningPort.Size = new System.Drawing.Size(130, 22);
            this.textBoxListeningPort.TabIndex = 3;
            // 
            // PT_PROXY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 175);
            this.Controls.Add(this.btnStopListen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStartListen);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PT_PROXY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PT_PROXY";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartListen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStopListen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxListeningIP;
        private System.Windows.Forms.TextBox textBoxListeningPort;
    }
}