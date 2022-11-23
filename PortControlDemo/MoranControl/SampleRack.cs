using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace PortControlDemo.MoranControl
{
    public partial class SampleRack : UserControl
    {
        //默认值
        private int width = 140;
        private int height = 340;
        public SampleRack()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 控件的大小-宽度
        /// </summary>
        public int ImgWidth
        {
            get
            {
                //TODO
                return this.width;
            }
            set
            {
                //TODO
                this.width = value;
                paint();

            }
        }
        /// <summary>
        /// 控件的大小-高度
        /// </summary>
        public int ImgHigh
        {
            get
            {
                //TODO
                return this.height;
            }
            set
            {
                //TODO
                this.height = value;
                paint();
            }
        }




        private void paint()
        {

            //创建画笔
            Graphics g = this.CreateGraphics();
            //g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            //g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            //g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿
            //创建灰色画笔
            Pen rec_pen = new Pen(Color.FromArgb(188, 181, 181));
            Point rec_p = new Point(0, 0);
            Size rec_size = new Size(100, 240);
            Brush rec_brush = new SolidBrush(Color.FromArgb(223, 34, 34));
            //Rectangle rec_R = new Rectangle(rec_p, rec_size);
            //g.DrawRectangle(rec_pen, rec_R);
            new MoranControl.common.common().drawRoundedRect(g, rec_pen, 0, 0, rec_size.Width, rec_size.Height, 10);
            
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    //创建黑色画笔
                    Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                    //
                    Point p = new Point(10 + 14 * j, 10 + 14 * i);
                    Size size = new Size(12, 12);
                    Rectangle R = new Rectangle(p, size);
                    Brush brush = new SolidBrush(Color.FromArgb(116,177,37));
                    g.DrawEllipse(pen, R);
                    g.FillEllipse(brush,R);
                }
            }
        }



        private void SampleRack_Paint(object sender, PaintEventArgs e)
        {
            paint();
        }
    }

}
