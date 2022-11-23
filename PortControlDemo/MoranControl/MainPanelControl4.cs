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
    public partial class MainPanelControl4 : UserControl
    {
        public MainPanelControl4()
        {
            InitializeComponent();
        }

        private Color _containerColor = Color.LightBlue;//容器填充颜色
        private Color _fillColor = Color.Orange;//控件边框颜色
        private int _radius = 10;//圆角大小
        private BoardStyle _board_type;//图形类型
        private int _size_width =10;//区域宽度
        private int _size_height = 10;//区域高度
        private int _rows_count = 6;//行个数
        private int _columns_count = 16;//列个数
        private int _spacing = 8;//间隔
        private const int WM_PAINT = 0xF;//更新绘制通知
        private bool _correction = true;//是否边框减去边框宽度
        private int _radiusCorrection = 1;//边框宽度
        private string _containerText = "";//容器文本值
        private int _containerFontSize = 20; //容器文本字号
        private int _type = 1; //容器文本字号
        private int _Slocation = 5; //容器文本字号
        private TextDirection _textDirection; //容器文本排列方向
        private int _UseNumber = 0; //更新圆的个数
        private Color _UpdateColor = Color.SpringGreen; //更新圆的颜色
        private int _AllTestNum = 0;//测试总个数
        private Color _AllTestColor = Color.Red; //更新圆的颜色
        private int _StartNumber = 0; //保留圆的开始圆

        [DefaultValue(typeof(int), "0"), Description("第一个开始圆")]
        public int StartNumber
        {
            get { return _StartNumber; }
            set
            {
                _StartNumber = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), "0"), Description("总更新圆的个数")]
        public int AllTestNum
        {
            get { return _AllTestNum; }
            set
            {
                _AllTestNum = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "Red"), Description("总更新圆的颜色")]
        public Color AllTestColor
        {
            get { return _AllTestColor; }
            set
            {
                _AllTestColor = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), ""), Description("第一个控件位置")]
        public int SLocation
        {
            get { return _Slocation; }
            set
            {
                _Slocation = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), ""), Description("板架类型")]
        public int Type
        {
            get { return _type; }
            set
            {
                _type = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 建立图形类型的样式
        /// </summary>
        public enum BoardStyle
        {
            /// <summary>
            /// 圆形。
            /// </summary>
            Circle = 0,
            /// <summary>
            /// 矩形。
            /// </summary>
           Rectangle = 1,
        }

        /// <summary>
        ///文本方向
        /// </summary>
        public enum TextDirection
        {
            /// <summary>
            /// 水平
            /// </summary>
            Horizontal = 0,
            /// <summary>
            /// 垂直
            /// </summary>
            Vertical = 1,
        }

        [DefaultValue(typeof(Color), "23, 169, 254"), Description("控件填充颜色")]
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
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

        [DefaultValue(BoardStyle.Circle), Description("图形类型")]
        public BoardStyle BoardwithStyle
        {
            get { return _board_type; }
            set
            {
                _board_type = value;
                base.Invalidate();
            }
        }

        [DefaultValue(TextDirection.Horizontal), Description("文本方向")]
        public TextDirection TextDirectionStyle
        {
            get { return _textDirection; }
            set
            {
                _textDirection = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(double), "10"), Description("区域宽度")]
        public int SizeWidth
        {
            get { return _size_width; }
            set
            {
                _size_width = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(double), "10"), Description("区域高度")]
        public int SizeHeight
        {
            get { return _size_height; }
            set
            {
                _size_height = value;
                base.Invalidate();
            }
        }


        [DefaultValue(typeof(int), "10"), Description("行个数")]
        public int RowsCount
        {
            get { return _rows_count; }
            set
            {
                _rows_count = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), "10"), Description("列个数")]
        public int ColumnsCount
        {
            get { return _columns_count; }
            set
            {
                _columns_count = value;
                base.Invalidate();
            }
        }


        [DefaultValue(typeof(int), "10"), Description("间隔")]
        public int Spacing
        {
            get { return _spacing; }
            set
            {
                _spacing = value;
                base.Invalidate();
            }
        }
        [DefaultValue(typeof(bool), "true"), Description("是否设置边框宽度")]
        public bool Correction
        {
            get { return _correction; }
            set
            {
                _correction = value;
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


        [DefaultValue(typeof(string), ""), Description("容器文本值")]
        public string ContainerText
        {
            get { return _containerText; }
            set
            {
                _containerText = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), "20"), Description("容器文本字号")]
        public int ContainerFontSize
        {
            get { return _containerFontSize; }
            set
            {
                _containerFontSize = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), "0"), Description("需要更新圆的个数")]
        public int UseNumber
        {
            get { return _UseNumber; }
            set
            {
                _UseNumber = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "SpringGreen"), Description("更新圆的颜色")]
        public Color UpdateColor
        {
            get { return _UpdateColor; }
            set
            {
                _UpdateColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 更新绘制
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
                if (m.Msg == WM_PAINT)
                {
                    if (this.Radius > 0)
                    {
                        using (Graphics g = Graphics.FromHwnd(this.Handle))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            Rectangle r = new Rectangle(3, 3, this.Width - 6, this.Height - 6);
                            Pen rec_pen = new Pen(Color.FromArgb(188, 181, 181), this.RadiusCorrection);
                            Brush b = new SolidBrush(this.ContainerColor);
                            g.FillRectangle(b, r);
                            //创建容器字体大小
                            Font font = new Font("微软雅黑", this.ContainerFontSize); // 定义字体
                            //容器字体颜色
                            Brush whiteBrush = new SolidBrush(Color.FromArgb(220, Color.FromArgb(64, 64, 64))); // 画文字用
                            //计算容器文字尺寸
                            Size textSize = TextRenderer.MeasureText(this.ContainerText, font);
                            //创建矩形文字区域
                            RectangleF textArea= new RectangleF();
                            StringFormat drawFormat = new StringFormat();
                            //文本方向
                            switch (this.TextDirectionStyle)
                            {
                                case TextDirection.Horizontal:
                                    textArea = new RectangleF(6 + r.Width / 2 - textSize.Width / 2, 3 + r.Height / 2 - textSize.Height / 2, textSize.Width, textSize.Height);
                                    drawFormat.Alignment = StringAlignment.Near;
                                    drawFormat.LineAlignment = StringAlignment.Near;
                                    break;
                                case TextDirection.Vertical:
                                    textArea = new RectangleF(2 + r.Width / 2 - textSize.Height / 2, 3 + r.Height / 2 - textSize.Width / 2, textSize.Height, textSize.Width);
                                    drawFormat.FormatFlags = StringFormatFlags.DirectionVertical | StringFormatFlags.DirectionRightToLeft;
                                    break;
                                default:
                                    break;
                            }
                            DrawBorder(g, r, this.BoardwithStyle, this.Radius);
                            g.Dispose();
                            font.Dispose();
                            b.Dispose();
                            
                            rec_pen.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DrawBorder(Graphics g, Rectangle rect, BoardStyle boardStyle, int radius)
        {      
            using (GraphicsPath path = CreatePath(rect, radius, boardStyle))
            {
                using (Pen pen = new Pen(this.ContainerColor))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
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
        GraphicsPath CreatePath(Rectangle rect, int radius, BoardStyle boardStyle)
        {
            GraphicsPath path = new GraphicsPath();
            int radiusCorrection = this.Correction ? this.RadiusCorrection : 0;
            switch (boardStyle)
            {
                case BoardStyle.Rectangle:
                    path.AddRectangle(rect);
                    break;
                case BoardStyle.Circle:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Bottom - radius - radiusCorrection, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius - radiusCorrection, radius, radius, 90, 90);
                    break;
            }

            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                int Allcount = 0;//总个数
                for (int i = 0; i < this.ColumnsCount; i++)//列
                {
                    for (int j = 0; j < this.RowsCount; j++)//行
                    {
                        if (i == 0)
                        {
                            //Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                            Pen pen = new Pen(Color.Black);
                            //Point p = new Point(this.SLocation + (this.Spacing + this.SizeWidth) * j, this.SLocation + (this.Spacing + this.SizeHeight) * i);
                            Point p = new Point(this.SLocation + (this.Spacing + this.SizeWidth) * i, this.SLocation + (this.Spacing + this.SizeHeight) * j);
                            Size size = new Size(this.SizeWidth, this.SizeHeight);
                            Brush brush;
                            if (Allcount < AllTestNum + StartNumber && Allcount >= StartNumber)
                            {
                                if (Allcount < UseNumber)
                                    brush = new SolidBrush(this.UpdateColor);
                                else
                                    brush = new SolidBrush(this.AllTestColor);
                            }
                            else
                            {
                                brush = new SolidBrush(this.FillColor);
                            }
                            Allcount++;
                            Rectangle R = new Rectangle(p, size);
                            switch (boardStyle)
                            {
                                case BoardStyle.Rectangle:
                                    new MoranControl.common.common().drawRoundedRect(g, pen, this.SLocation + (this.Spacing + this.SizeWidth) * i, this.SLocation + (this.Spacing + this.SizeHeight) * j, R.Width, R.Height, 2);
                                    g.FillRectangle(brush, R);
                                    break;
                                case BoardStyle.Circle:
                                    g.DrawEllipse(pen, R);
                                    g.FillEllipse(brush, R);
                                    break;
                            }
                            pen.Dispose();
                        }
                        else
                        {
                            //Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                            Pen pen = new Pen(Color.Black);
                            //Point p = new Point(this.SLocation + (this.Spacing + this.SizeWidth) * j, this.SLocation + (this.Spacing + this.SizeHeight) * i);
                            Point p = new Point(this.SLocation + (this.Spacing + this.SizeWidth) * (i*2-1), this.SLocation + (this.Spacing + this.SizeHeight) * j);
                            Size size = new Size(this.SizeWidth*2+ this.Spacing, this.SizeHeight);
                            Brush brush;
                            if (Allcount < AllTestNum + StartNumber && Allcount >= StartNumber)
                            {
                                if (Allcount < UseNumber)
                                    brush = new SolidBrush(this.UpdateColor);
                                else
                                    brush = new SolidBrush(this.AllTestColor);
                            }
                            else
                            {
                                brush = new SolidBrush(this.FillColor);
                            }
                            Allcount++;
                            Rectangle R = new Rectangle(p, size);
                            switch (boardStyle)
                            {
                                case BoardStyle.Rectangle:
                                    new MoranControl.common.common().drawRoundedRect(g, pen, this.SLocation + (this.Spacing + this.SizeWidth) * (i * 2 - 1), this.SLocation + (this.Spacing + this.SizeHeight) * j, R.Width, R.Height, 2);
                                    g.FillRectangle(brush, R);
                                    break;
                                case BoardStyle.Circle:
                                    g.DrawEllipse(pen, R);
                                    g.FillEllipse(brush, R);
                                    break;
                            }
                            pen.Dispose();
                        }
                       
                    }
                }
                g.Dispose();
               
            }
            path.CloseFigure(); //这句很关键，缺少会没有左边线。
            return path;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

    }
}
