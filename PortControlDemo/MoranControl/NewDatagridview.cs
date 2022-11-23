using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo.MoranControl
{
    class NewDatagridview : DataGridView
    {
        private CRYSTAL.numTextBox numTextBox1;

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
            }
            catch
            {

                Invalidate();
            }
        }

        private void InitializeComponent()
        {
            this.numTextBox1 = new CRYSTAL.numTextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // numTextBox1
            // 
            // 
            // 
            // 
            this.numTextBox1.Border.Class = "TextBoxBorder";
            this.numTextBox1.Dec = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTextBox1.Dou = 0D;
            this.numTextBox1.Int = 0;
            this.numTextBox1.IsCorrection = true;
            this.numTextBox1.IsFuShu = true;
            this.numTextBox1.IsNull = false;
            this.numTextBox1.IsXiaoShu = true;
            this.numTextBox1.Location = new System.Drawing.Point(0, 0);
            this.numTextBox1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTextBox1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTextBox1.Name = "numTextBox1";
            this.numTextBox1.OneXiaoShu = false;
            this.numTextBox1.Size = new System.Drawing.Size(100, 21);
            this.numTextBox1.TabIndex = 0;
            this.numTextBox1.valueDec = null;
            this.numTextBox1.valueDouble = 0D;
            this.numTextBox1.valueInt = null;
            // 
            // NewDatagridview
            // 
            this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
