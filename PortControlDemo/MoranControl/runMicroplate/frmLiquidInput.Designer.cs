namespace PortControlDemo.MoranControl.runMicroplate
{
    partial class frmLiquidInput
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
            this.cmbProgram = new ElContros.CRYCombobox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbLiqName = new System.Windows.Forms.Label();
            this.lbSample = new System.Windows.Forms.Label();
            this.cmbLiqName = new ElContros.CRYCombobox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbProgram
            // 
            this.cmbProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProgram.FormattingEnabled = true;
            this.cmbProgram.Location = new System.Drawing.Point(146, 41);
            this.cmbProgram.Name = "cmbProgram";
            this.cmbProgram.ReadOnly = false;
            this.cmbProgram.Size = new System.Drawing.Size(124, 20);
            this.cmbProgram.TabIndex = 3;
            this.cmbProgram.SelectedIndexChanged += new System.EventHandler(this.CmbProgram_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(10, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 48);
            this.label2.TabIndex = 0;
            this.label2.Text = "项目名称:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOk
            // 
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point(71, 185);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 30);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // lbLiqName
            // 
            this.lbLiqName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbLiqName.Location = new System.Drawing.Point(6, 86);
            this.lbLiqName.Name = "lbLiqName";
            this.lbLiqName.Size = new System.Drawing.Size(132, 44);
            this.lbLiqName.TabIndex = 0;
            this.lbLiqName.Text = "液体名称:";
            this.lbLiqName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbSample
            // 
            this.lbSample.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbSample.Location = new System.Drawing.Point(8, 86);
            this.lbSample.Name = "lbSample";
            this.lbSample.Size = new System.Drawing.Size(132, 44);
            this.lbSample.TabIndex = 5;
            this.lbSample.Text = "液体位置：";
            this.lbSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLiqName
            // 
            this.cmbLiqName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLiqName.FormattingEnabled = true;
            this.cmbLiqName.Location = new System.Drawing.Point(146, 99);
            this.cmbLiqName.Name = "cmbLiqName";
            this.cmbLiqName.ReadOnly = false;
            this.cmbLiqName.Size = new System.Drawing.Size(124, 20);
            this.cmbLiqName.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(189, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbProgram);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbLiqName);
            this.groupBox1.Controls.Add(this.lbSample);
            this.groupBox1.Controls.Add(this.cmbLiqName);
            this.groupBox1.Location = new System.Drawing.Point(22, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 153);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "液体信息";
            // 
            // frmLiquidInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 229);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmLiquidInput";
            this.Text = "编辑液体";
            this.Load += new System.EventHandler(this.FrmLiquidInput_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ElContros.CRYCombobox cmbProgram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbLiqName;
        private System.Windows.Forms.Label lbSample;
        private ElContros.CRYCombobox cmbLiqName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}