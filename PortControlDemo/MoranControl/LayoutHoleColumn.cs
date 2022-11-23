using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo.MoranControl
{
    public partial class LayoutHoleColumn : UserControl
    {
        public LayoutHoleColumn()
        {
            InitializeComponent();
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var parms = base.CreateParams;
        //        parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN
        //        return parms;
        //    }
        //}

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
                return cp;
            }

        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public int _ColumnNumber = 1;//板列


        [DefaultValue(typeof(int), "1"), Description("板列")]
        public int ColumnNumber
        {
            get { return _ColumnNumber; }
            set
            {
                if (value < 0)
                {
                    value = 1;
                }
                _ColumnNumber = value;
                //base.Invalidate();
            }
        }
    }
}
