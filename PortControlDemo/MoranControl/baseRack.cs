using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo.MoranControl
{
    public partial class baseRack : UserControl
    {
        public baseRack()
        {
            InitializeComponent();
        }

        private void BaseRack_Paint(object sender, PaintEventArgs e)
        {
            //创建画笔
            Graphics g = this.CreateGraphics();
            //g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            //g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            //g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿
            //创建灰色画笔
            Pen rec_pen = new Pen(Color.FromArgb(188, 181, 181));
            Point rec_p = new Point(0, 0);
            Size rec_size = new Size(110, 300);
            Brush rec_brush = new SolidBrush(Color.FromArgb(223, 34, 34));
            //Rectangle rec_R = new Rectangle(rec_p, rec_size);
            //g.DrawRectangle(rec_pen, rec_R);
            new MoranControl.common.common().drawRoundedRect(g, rec_pen, 0, 0, rec_size.Width, rec_size.Height, 7);
            Point linepoint1 = new Point(0, 50);
            Point linepoint2 = new Point(110, 50);
            g.DrawLine(rec_pen, linepoint1, linepoint2);
            Point Elllipse_p = new Point(50, 25);
            Size Elllipse_size = new Size(5, 5);
            Rectangle Elllipse_R = new Rectangle(Elllipse_p, Elllipse_size);
            g.DrawEllipse(rec_pen, Elllipse_R);
        }
    }
}
