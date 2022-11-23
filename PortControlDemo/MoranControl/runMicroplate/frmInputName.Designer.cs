namespace PortControlDemo.MoranControl.runMicroplate
{
    partial class frmInputName
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
            this.cmbMethods = new ElContros.CRYCombobox();
            this.cmbLiqName = new ElContros.CRYCombobox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new Sunny.UI.UIButton();
            this.btnCancel = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // cmbMethods
            // 
            this.cmbMethods.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbMethods.FormattingEnabled = true;
            this.cmbMethods.Location = new System.Drawing.Point(139, 27);
            this.cmbMethods.MaxDropDownItems = 10;
            this.cmbMethods.Name = "cmbMethods";
            this.cmbMethods.ReadOnly = false;
            this.cmbMethods.Size = new System.Drawing.Size(137, 24);
            this.cmbMethods.TabIndex = 8;
            this.cmbMethods.SelectedIndexChanged += new System.EventHandler(this.CmbMethods_SelectedIndexChanged);
            // 
            // cmbLiqName
            // 
            this.cmbLiqName.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbLiqName.FormattingEnabled = true;
            this.cmbLiqName.Location = new System.Drawing.Point(139, 72);
            this.cmbLiqName.MaxDropDownItems = 10;
            this.cmbLiqName.Name = "cmbLiqName";
            this.cmbLiqName.ReadOnly = false;
            this.cmbLiqName.Size = new System.Drawing.Size(137, 24);
            this.cmbLiqName.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(0, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 49);
            this.label1.TabIndex = 4;
            this.label1.Text = "液体名称:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 43);
            this.label2.TabIndex = 5;
            this.label2.Text = "项目名称:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnOK.Location = new System.Drawing.Point(55, 115);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 30);
            this.btnOK.StyleCustomMode = true;
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnCancel.Location = new System.Drawing.Point(187, 115);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.StyleCustomMode = true;
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // frmInputName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(333, 172);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbMethods);
            this.Controls.Add(this.cmbLiqName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmInputName";
            this.Opacity = 0.9D;
            this.Text = "试剂录入";
            this.Load += new System.EventHandler(this.FrmInputName_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public ElContros.CRYCombobox cmbMethods;
        public ElContros.CRYCombobox cmbLiqName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UIButton btnOK;
        private Sunny.UI.UIButton btnCancel;
    }
}