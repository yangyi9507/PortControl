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
    public partial class Tip : UserControl
    {
        public Tip()
        {
            InitializeComponent();
        }

        //类型：1000，200，50
        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private void Tip_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen rec_pen = new Pen(Color.FromArgb(188, 181, 181));
            Point rec_p = new Point(0, 0);
            Size rec_size = new Size(175, 120);
            Brush rec_brush = new SolidBrush(Color.FromArgb(223, 34, 34));
            new MoranControl.common.common().drawRoundedRect(g, rec_pen, 0, 0, rec_size.Width, rec_size.Height, 10);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                    //
                    Point p = new Point(4 + 14 * j, 4 + 14 * i);
                    Size size = new Size(12, 12);
                    Rectangle R = new Rectangle(p, size);
                    Brush brush = new SolidBrush(Color.FromArgb(255, 165, 0));
                    switch (type)
                    {
                        case 1:
                            brush = new SolidBrush(Color.FromArgb(32, 178, 170));
                            break;
                        case 2:
                            brush = new SolidBrush(Color.FromArgb(154, 205, 50));
                            break;
                        default:
                            break;
                    }
                    g.DrawEllipse(pen, R);
                    g.FillEllipse(brush, R);
                    //Brush brush = new SolidBrush(Color.FromArgb(255, 165, 0));
                    //g.DrawEllipse(pen, R);
                    //g.FillEllipse(brush, R);
                }
            }
        }
    }
}
