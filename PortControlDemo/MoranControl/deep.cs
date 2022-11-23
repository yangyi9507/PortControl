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
    public partial class deep : UserControl
    {
        public deep()
        {
            InitializeComponent();
        }

        private void 深孔板_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Deep_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen rec_pen = new Pen(Color.FromArgb(188, 181, 181));
            Point rec_p = new Point(0, 0);
            Size rec_size = new Size(150, 105);
            Brush rec_brush = new SolidBrush(Color.FromArgb(223, 34, 34));
            new MoranControl.common.common().drawRoundedRect(g, rec_pen, 0, 0, rec_size.Width, rec_size.Height, 10);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                    //
                    Point p = new Point(5 + 12 * j, 5 + 12 * i);
                    Size size = new Size(9, 9);
                    Rectangle R = new Rectangle(p, size);
                    g.DrawEllipse(pen, R);
                }
            }
        }
    }
}
