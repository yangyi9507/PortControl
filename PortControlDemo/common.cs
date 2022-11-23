using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortControlDemo.MoranControl.common
{
    public class common
    {
        /// <summary>
        /// 带圆角矩形
        /// </summary>
        /// <param name="radius">圆角半径</param>
        public void drawRoundedRect(Graphics g, System.Drawing.Pen pen, int x, int y, int width, int height, int radius)
        {
            //去掉圆角的锯齿
            g.SmoothingMode = SmoothingMode.HighQuality;
            //上
            g.DrawLine(pen, new PointF(x + radius, y), new PointF(x + width - radius, y));
            //下
            g.DrawLine(pen, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            //左
            g.DrawLine(pen, new PointF(x, y + radius), new PointF(x, y + height - radius));
            //右
            g.DrawLine(pen, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));
            //左上角
            g.DrawArc(pen, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            //右上角
            g.DrawArc(pen, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            //左下角
            g.DrawArc(pen, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            //右下角
            g.DrawArc(pen, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);

        }
    }
}
