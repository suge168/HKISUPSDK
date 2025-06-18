namespace ISUPDemo.UI
{
    partial class CardManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardManager));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EmployeeNoBox = new System.Windows.Forms.TextBox();
            this.CardNoBox = new System.Windows.Forms.TextBox();
            this.CardTypeBox = new System.Windows.Forms.ComboBox();
            this.CardSetBtn = new System.Windows.Forms.Button();
            this.CardDeleteBtn = new System.Windows.Forms.Button();
            this.CardSearchBtn = new System.Windows.Forms.Button();
            this.CardCountBtn = new System.Windows.Forms.Button();
            this.MsgLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(85, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "EmployeeNo：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(85, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "CardNo：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(85, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "CardType：";
            // 
            // EmployeeNoBox
            // 
            this.EmployeeNoBox.Location = new System.Drawing.Point(226, 135);
            this.EmployeeNoBox.Name = "EmployeeNoBox";
            this.EmployeeNoBox.Size = new System.Drawing.Size(128, 21);
            this.EmployeeNoBox.TabIndex = 3;
            // 
            // CardNoBox
            // 
            this.CardNoBox.Location = new System.Drawing.Point(226, 173);
            this.CardNoBox.Name = "CardNoBox";
            this.CardNoBox.Size = new System.Drawing.Size(128, 21);
            this.CardNoBox.TabIndex = 4;
            // 
            // CardTypeBox
            // 
            this.CardTypeBox.FormattingEnabled = true;
            this.CardTypeBox.Items.AddRange(new object[] {
            "normalCard",
            "patrolCard",
            "hijackCard",
            "superCard",
            "dismissingCard",
            "emergencyCard"});
            this.CardTypeBox.Location = new System.Drawing.Point(226, 213);
            this.CardTypeBox.Name = "CardTypeBox";
            this.CardTypeBox.Size = new System.Drawing.Size(128, 20);
            this.CardTypeBox.TabIndex = 5;
            // 
            // CardSetBtn
            // 
            this.CardSetBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardSetBtn.Location = new System.Drawing.Point(192, 262);
            this.CardSetBtn.Name = "CardSetBtn";
            this.CardSetBtn.Size = new System.Drawing.Size(75, 23);
            this.CardSetBtn.TabIndex = 6;
            this.CardSetBtn.Text = "Set";
            this.CardSetBtn.UseVisualStyleBackColor = true;
            this.CardSetBtn.Click += new System.EventHandler(this.CardSetBtn_Click);
            // 
            // CardDeleteBtn
            // 
            this.CardDeleteBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardDeleteBtn.Location = new System.Drawing.Point(305, 262);
            this.CardDeleteBtn.Name = "CardDeleteBtn";
            this.CardDeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.CardDeleteBtn.TabIndex = 8;
            this.CardDeleteBtn.Text = "Delete";
            this.CardDeleteBtn.UseVisualStyleBackColor = true;
            this.CardDeleteBtn.Click += new System.EventHandler(this.CardDeleteBtn_Click);
            // 
            // CardSearchBtn
            // 
            this.CardSearchBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardSearchBtn.Location = new System.Drawing.Point(79, 262);
            this.CardSearchBtn.Name = "CardSearchBtn";
            this.CardSearchBtn.Size = new System.Drawing.Size(75, 23);
            this.CardSearchBtn.TabIndex = 9;
            this.CardSearchBtn.Text = "Get";
            this.CardSearchBtn.UseVisualStyleBackColor = true;
            this.CardSearchBtn.Click += new System.EventHandler(this.CardSearchBtn_Click);
            // 
            // CardCountBtn
            // 
            this.CardCountBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardCountBtn.Location = new System.Drawing.Point(418, 262);
            this.CardCountBtn.Name = "CardCountBtn";
            this.CardCountBtn.Size = new System.Drawing.Size(75, 23);
            this.CardCountBtn.TabIndex = 10;
            this.CardCountBtn.Text = "Count";
            this.CardCountBtn.UseVisualStyleBackColor = true;
            this.CardCountBtn.Click += new System.EventHandler(this.CardCountBtn_Click);
            // 
            // MsgLabel
            // 
            this.MsgLabel.AutoSize = true;
            this.MsgLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MsgLabel.ForeColor = System.Drawing.Color.Red;
            this.MsgLabel.Location = new System.Drawing.Point(280, 306);
            this.MsgLabel.Name = "MsgLabel";
            this.MsgLabel.Size = new System.Drawing.Size(0, 14);
            this.MsgLabel.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 118);
            this.panel1.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(142, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(303, 41);
            this.label4.TabIndex = 1;
            this.label4.Text = "Card Management";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(378, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 14);
            this.label8.TabIndex = 58;
            this.label8.Text = "Set Card Need";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(378, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 14);
            this.label6.TabIndex = 59;
            this.label6.Text = "Set/Get/Delete Card Need";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(378, 218);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 14);
            this.label7.TabIndex = 60;
            this.label7.Text = "Set Card Need";
            // 
            // CardManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(620, 345);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MsgLabel);
            this.Controls.Add(this.CardCountBtn);
            this.Controls.Add(this.CardSearchBtn);
            this.Controls.Add(this.CardDeleteBtn);
            this.Controls.Add(this.CardSetBtn);
            this.Controls.Add(this.CardTypeBox);
            this.Controls.Add(this.CardNoBox);
            this.Controls.Add(this.EmployeeNoBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CardManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CardManager";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox EmployeeNoBox;
        private System.Windows.Forms.TextBox CardNoBox;
        private System.Windows.Forms.ComboBox CardTypeBox;
        private System.Windows.Forms.Button CardSetBtn;
        private System.Windows.Forms.Button CardDeleteBtn;
        private System.Windows.Forms.Button CardSearchBtn;
        private System.Windows.Forms.Button CardCountBtn;
        private System.Windows.Forms.Label MsgLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}