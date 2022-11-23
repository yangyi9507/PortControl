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

namespace PortControlDemo.MoranControl
{
    public partial class LayoutSample : UserControl
    {
        public LayoutSample()
        {
            InitializeComponent();
        }
        private Color _containerColor = Color.White;//容器填充颜色
        private int _radius = 10;//圆角大小
        private const int WM_PAINT = 0xF;//更新绘制通知
        private int _radiusCorrection = 1;//边框宽度
        private int _number = 0;//个数

        [DefaultValue(typeof(int), "0"), Description("个数")]
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "223, 34, 34"), Description("容器填充颜色")]
        public Color ContainerColor
        {
            get { return _containerColor; }
            set
            {
                _containerColor = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), "10"), Description("圆角大小")]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (value < 0)
                {
                    value = 1;
                }
                _radius = value;
                base.Invalidate();
            }
        }


        [DefaultValue(typeof(int), "1"), Description("边框宽度")]
        public int RadiusCorrection
        {
            get { return _radiusCorrection; }
            set
            {
                _radiusCorrection = value;
                base.Invalidate();
            }
        }

        private void LayoutSample_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is MyButtonX)
                {
                    control.BackColor = this._containerColor;
                }
            }
            if (this.Radius > 0)
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle r = new Rectangle(3, 3, this.Width - 6, this.Height - 6);
                    Pen rec_pen = new Pen(Color.FromArgb(188, 181, 181), this.RadiusCorrection);
                    Brush b = new SolidBrush(this.ContainerColor);
                    g.FillRectangle(b, r);
                    DrawBorder(g, r, this.Radius);
                    g.Dispose();
                    b.Dispose();
                    rec_pen.Dispose();
                }
            }
        }

        private void DrawBorder(Graphics g, Rectangle rect, int radius)
        {
            using (GraphicsPath path = CreatePath(rect, radius))
            {
                using (Pen pen = new Pen(this.ContainerColor))
                {
                    g.DrawPath(pen, path);
                }
            }
        }
        /// <summary>
        /// 建立带有圆角样式的路径。
        /// </summary>
        /// <param name="rect">用来建立路径的矩形。</param>
        /// <param name="radius">圆角的大小。</param>
        /// <param name="boardStyle">圆角的样式。</param>
        /// <param name="correction">是否把矩形长宽减 1,以便画出边框。</param>
        /// <returns>建立的路径。</returns>
        private GraphicsPath CreatePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            path.CloseFigure(); //这句很关键，缺少会没有左边线。
            return path;
        }
    }
}
