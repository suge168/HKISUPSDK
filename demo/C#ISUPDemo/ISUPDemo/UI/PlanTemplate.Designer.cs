namespace ISUPDemo.UI
{
    partial class PlanTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanTemplate));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.cbPlanTemplateType = new System.Windows.Forms.ComboBox();
            this.textBoxTemplateNo = new System.Windows.Forms.TextBox();
            this.labelTemplate = new System.Windows.Forms.Label();
            this.labelHoildayGroupNo = new System.Windows.Forms.Label();
            this.textBoxHoildayGroupNo = new System.Windows.Forms.TextBox();
            this.checkEnable = new System.Windows.Forms.CheckBox();
            this.labelWeekPlanNo = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textName = new System.Windows.Forms.TextBox();
            this.textBoxWeekPlan = new System.Windows.Forms.TextBox();
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
            this.label9.Size = new System.Drawing.Size(265, 41);
            this.label9.TabIndex = 1;
            this.label9.Text = "Plan Template";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelType.Location = new System.Drawing.Point(35, 129);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(41, 12);
            this.labelType.TabIndex = 15;
            this.labelType.Text = "Type：";
            // 
            // cbPlanTemplateType
            // 
            this.cbPlanTemplateType.FormattingEnabled = true;
            this.cbPlanTemplateType.Items.AddRange(new object[] {
            "User",
            "CardReader",
            "Door"});
            this.cbPlanTemplateType.Location = new System.Drawing.Point(139, 126);
            this.cbPlanTemplateType.Name = "cbPlanTemplateType";
            this.cbPlanTemplateType.Size = new System.Drawing.Size(85, 20);
            this.cbPlanTemplateType.TabIndex = 16;
            // 
            // textBoxTemplateNo
            // 
            this.textBoxTemplateNo.Location = new System.Drawing.Point(397, 126);
            this.textBoxTemplateNo.Name = "textBoxTemplateNo";
            this.textBoxTemplateNo.Size = new System.Drawing.Size(100, 21);
            this.textBoxTemplateNo.TabIndex = 40;
            this.textBoxTemplateNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxTemplateNo_KeyPress);
            // 
            // labelTemplate
            // 
            this.labelTemplate.AutoSize = true;
            this.labelTemplate.Location = new System.Drawing.Point(293, 129);
            this.labelTemplate.Name = "labelTemplate";
            this.labelTemplate.Size = new System.Drawing.Size(77, 12);
            this.labelTemplate.TabIndex = 39;
            this.labelTemplate.Text = "TemplateNo.:";
            // 
            // labelHoildayGroupNo
            // 
            this.labelHoildayGroupNo.AutoSize = true;
            this.labelHoildayGroupNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelHoildayGroupNo.Location = new System.Drawing.Point(293, 227);
            this.labelHoildayGroupNo.Name = "labelHoildayGroupNo";
            this.labelHoildayGroupNo.Size = new System.Drawing.Size(101, 12);
            this.labelHoildayGroupNo.TabIndex = 17;
            this.labelHoildayGroupNo.Text = "HolidayGroupNo.:";
            // 
            // textBoxHoildayGroupNo
            // 
            this.textBoxHoildayGroupNo.Location = new System.Drawing.Point(397, 224);
            this.textBoxHoildayGroupNo.Name = "textBoxHoildayGroupNo";
            this.textBoxHoildayGroupNo.Size = new System.Drawing.Size(100, 21);
            this.textBoxHoildayGroupNo.TabIndex = 18;
            // 
            // checkEnable
            // 
            this.checkEnable.AutoSize = true;
            this.checkEnable.Location = new System.Drawing.Point(37, 175);
            this.checkEnable.Name = "checkEnable";
            this.checkEnable.Size = new System.Drawing.Size(60, 16);
            this.checkEnable.TabIndex = 19;
            this.checkEnable.Text = "Enable";
            this.checkEnable.UseVisualStyleBackColor = true;
            // 
            // labelWeekPlanNo
            // 
            this.labelWeekPlanNo.AutoSize = true;
            this.labelWeekPlanNo.Location = new System.Drawing.Point(35, 227);
            this.labelWeekPlanNo.Name = "labelWeekPlanNo";
            this.labelWeekPlanNo.Size = new System.Drawing.Size(77, 12);
            this.labelWeekPlanNo.TabIndex = 31;
            this.labelWeekPlanNo.Text = "WeekPlanNo.:";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(534, 124);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 34;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(295, 278);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 35;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelName.Location = new System.Drawing.Point(293, 176);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(89, 12);
            this.labelName.TabIndex = 36;
            this.labelName.Text = "Template Name:";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(397, 173);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(100, 21);
            this.textName.TabIndex = 37;
            // 
            // textBoxWeekPlan
            // 
            this.textBoxWeekPlan.Location = new System.Drawing.Point(139, 224);
            this.textBoxWeekPlan.Name = "textBoxWeekPlan";
            this.textBoxWeekPlan.Size = new System.Drawing.Size(100, 21);
            this.textBoxWeekPlan.TabIndex = 38;
            // 
            // PlanTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 331);
            this.Controls.Add(this.textBoxTemplateNo);
            this.Controls.Add(this.labelTemplate);
            this.Controls.Add(this.textBoxWeekPlan);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.labelWeekPlanNo);
            this.Controls.Add(this.checkEnable);
            this.Controls.Add(this.textBoxHoildayGroupNo);
            this.Controls.Add(this.labelHoildayGroupNo);
            this.Controls.Add(this.cbPlanTemplateType);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.panel1);
            this.Name = "PlanTemplate";
            this.Text = "PlanTemplate";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox cbPlanTemplateType;
        private System.Windows.Forms.TextBox textBoxTemplateNo;
        private System.Windows.Forms.Label labelTemplate;
        private System.Windows.Forms.Label labelHoildayGroupNo;
        private System.Windows.Forms.TextBox textBoxHoildayGroupNo;
        private System.Windows.Forms.CheckBox checkEnable;
        private System.Windows.Forms.Label labelWeekPlanNo;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textBoxWeekPlan;
    }
}