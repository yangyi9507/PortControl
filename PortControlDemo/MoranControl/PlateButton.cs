using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 微孔板按钮
    /// </summary>
    public partial class PlateButton : System.Windows.Forms.Button
    {

        /// <summary>
        /// 微孔板按钮的状态
        /// </summary>
        public enum PlateColState
        {
            /// <summary>
            /// LightGray  White 白
            /// </summary>
            None,
            /// <summary>
            /// Blue  DodgerBlue 蓝
            /// </summary>
            Placed,
            /// <summary>
            /// Firebrick  IndianRed 红
            /// </summary>
            Running,
            /// <summary>
            /// Green  LimeGreen 绿
            /// </summary>
            Finish
        }
        /// <summary>
        /// 孔状态集合
        /// </summary>
        public List<PlateColState> ColsState { get; set; }
        /// <summary>
        /// 绘制微孔板按钮 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;//消除锯齿
            g.Clear(this.BackColor);
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            g.DrawRectangle(Pens.Black, rect);

            int L = (this.Width - 1 - (NeiPad.Left + NeiPad.Right)) / 12;
            int H = (this.Height - 1 - (NeiPad.Top + NeiPad.Bottom)) / 8;
            int ti = 4, tj = 4;
            for (int i = 0; i < 8; i++)
            {
                tj = i * H + NeiPad.Top;
                for (int j = 0; j < 12; j++)
                {
                    ti = j * L + NeiPad.Left;
                    Rectangle tcc = new Rectangle(ti, tj, L - CellPad.Width, H - CellPad.Height);
                    LinearGradientBrush myBrush = null;
                    if (ColsState != null)
                        switch (ColsState[j])
                        {
                            case PlateColState.None:
                                myBrush = new LinearGradientBrush(tcc, Color.DimGray, Color.LightGray, LinearGradientMode.Vertical);
                                break;
                            case PlateColState.Placed:
                                myBrush = new LinearGradientBrush(tcc, Color.Blue, Color.DodgerBlue, LinearGradientMode.Vertical);
                                break;
                            case PlateColState.Running:
                                myBrush = new LinearGradientBrush(tcc, Color.Firebrick, Color.IndianRed, LinearGradientMode.Vertical);
                                break;
                            case PlateColState.Finish:
                                myBrush = new LinearGradientBrush(tcc, Color.Green, Color.LimeGreen, LinearGradientMode.Vertical);
                                break;
                        }
                    else
                        myBrush = new LinearGradientBrush(tcc, Color.Blue, Color.DodgerBlue, LinearGradientMode.Vertical);
                    g.FillRectangle(myBrush, tcc);
                    myBrush.Dispose();
                    g.DrawRectangle(Pens.Black, tcc);
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
        }
        /// <summary>
        /// 间隔
        /// </summary>
        public Padding NeiPad { get; set; }

        [Bindable(true), DefaultValue(typeof(Size), "3,3"), Description("控件對應數據庫中字段的名稱。")]
        public Size CellPad { get; set; }
        public PlateButton()
        {
            CellPad = new Size(4, 4);
            NeiPad = new Padding(4);
        }
    }
}
