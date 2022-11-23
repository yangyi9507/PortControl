using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo.MoranControl
{
    public partial class MyLabelX : SkinLabel
    {
        public MyLabelX()
        {
            InitializeComponent();
        }
        public int _RowNumber = 1;//行号
        public int _ColumnNumber = 1;//列号
        private const uint WM_LBUTTONDBLCLK = 0x203;//鼠标左键双击消息

        [DefaultValue(typeof(int), "1"), Description("行号")]
        public int RowNumber
        {
            get { return _RowNumber; }
            set
            {
                if (value < 0)
                {
                    value = 1;
                }
                _RowNumber = value;
                //base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), "1"), Description("列号")]
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

        //protected override void WndProc(ref Message m)
        //{
        //    switch ((uint)m.Msg)
        //    {
        //        case WM_LBUTTONDBLCLK: return;//忽略掉
        //    }
        //    base.WndProc(ref m);
        //}
    }
}
