using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using ElContros;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 微孔板网格控件
    /// </summary>
    public class PlateGrid:DataGridView
    {
        /// <summary>
        /// 横向孔个数
        /// </summary>
        int colNum = 12;
        private System.ComponentModel.IContainer components;
    
        public int ColNum
        {
            get { return colNum; }
            set
            {
                colNum = value;
                init();
            }
        }
        int rowNum = 8;
        public int RowNum
        {
            get { return rowNum; }
            set { rowNum = value;
                init();

            }
        }
        /// <summary>
        /// 初始化横向与竖向孔宽度和高度
        /// </summary>
        void init()
        {
            if (colNum <= 0) return;
            Columns.Clear();
            for (int i = 0; i < colNum; i++)
            {
                DataGridViewColumn NewCol = new DataGridViewTextBoxColumn();
                Columns.Add(NewCol);
            }
            if (rowNum <= 0) return;
            Rows.Clear();
            Rows.Add(rowNum);
            Ajust();
        }
        /// <summary>
        /// 孔数据列表
        /// </summary>
        public List<HoleData> DataSour
        {
            get { return _dataSour; }
            set
            {
                _dataSour = value;
                if (DataSour != null)
                    for (int j = 0; j < colNum; j++)
                    {
                        for (int i = 0; i < rowNum; i++)
                        {
                            Rows[i].Cells[j].Value = DataSour[j * RowNum + i];
                        }
                    }
            }
        }

        /// <summary>
        /// 设置每孔的宽和高，采用均分方法
        /// </summary>
        public void Ajust()
        {
            int colWidth = (Width - RowHeadersWidth) / ColNum;
            int rowHeght = (Height - ColumnHeadersHeight) / RowNum;
            foreach (DataGridViewColumn col in Columns)
            {
                col.Width = colWidth;
            }
            foreach (DataGridViewRow row in Rows)
            {
                row.Height = rowHeght;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public PlateGrid()
        {
            this.ReadOnly = true;
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);   //   禁止擦除背景.   
            SetStyle(ControlStyles.DoubleBuffer, true);   //   双缓冲  
            RowHeadersWidth = 20;
            ReadOnly = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            
        }
        /// <summary>
        /// 孔绘制方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            if ((e.ColumnIndex >= 0) && (e.RowIndex >= 0))
            {
                e.PaintBackground(e.ClipBounds, false);
                HoleData dt = (HoleData)e.Value;
                if (dt != null)
                    this.DrawBackground(e, getColorBySampleColor(dt.SampleColor));
                if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                {
                    this.DrawSelection(e, e.RowIndex, e.ColumnIndex);
                }
                if ((dt != null) && (dt.ThisHoleType != HoleType.Null))
                    this.DrawTextAndBoundaries(e, e.RowIndex, e.ColumnIndex, dt);
                
            }
            else
                DrawColumnHeader(this, e);
            
        }

        /// <summary>
        /// 获取孔颜色
        /// </summary>
        /// <param name="tp">孔类型</param>
        /// <returns>颜色</returns>
        Color getColorBySampleColor(string sampleColor)
        {
            try
            {
                if (!string.IsNullOrEmpty(sampleColor))
                {
                    return ColorTranslator.FromHtml(sampleColor);
                }
                else {
                    return Color.Beige;
                }
                
            }
            catch (Exception)
            {

                return Color.Beige;

            }


        }

        /// <summary>
        /// 获取孔颜色
        /// </summary>
        /// <param name="tp">孔类型</param>
        /// <returns>颜色</returns>
        Color getColorByDataType(HoleType tp)
        {
            try
            {
                return Color.FromArgb(ElisaConfig.Instance.HoleTypeColors[(int)tp]);
            }
            catch (Exception)
            {
                switch (tp)
                {
                    case HoleType.Sample:
                        return Color.DarkGreen;
                    case HoleType.Std:
                        return Color.Yellow;
                    case HoleType.Qc:
                        return Color.Blue;
                    case HoleType.BL:
                        return Color.Gainsboro;
                    case HoleType.N:
                        return Color.LimeGreen;
                    case HoleType.P:
                        return Color.Red;
                    case HoleType.User:
                        return Color.DarkOrchid;
                    //case HoleType.None:
                    //    return Color.LightGray;
                    default:
                        return Color.Beige;
                }
            }
            
            
        }
        /// <summary>
        /// 控件大小调整
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Ajust();
        }
        /// <summary>
        /// 绘制孔背景
        /// </summary>
        /// <param name="e"></param>
        /// <param name="colorSample"></param>
        public void DrawBackground(DataGridViewCellPaintingEventArgs e, Color colorSample)
        {
            Brush brush = new LinearGradientBrush(cellRc(e.CellBounds), colorSample, Color.FromArgb(((colorSample.R * 0x84) / 0xff) + 0x7b, ((colorSample.G * 0x84) / 0xff) + 0x7b, ((colorSample.B * 0x84) / 0xff) + 0x7b), 40f, false);
            e.Graphics.FillRectangle(brush, cellRc(e.CellBounds));
            e.Handled = true;
        }
        /// <summary>
        /// 缩小所传递矩形对象的大小（缩小程度4）
        /// </summary>
        /// <param name="te">矩形对象</param>
        /// <returns></returns>
        Rectangle cellRc(Rectangle te)
        {
            return new Rectangle(te.X + 1, te.Y + 1, te.Width - 3, te.Height - 3);
        }
        /// <summary>
        /// 绘制被选中状态
        /// </summary>
        /// <param name="e"></param>
        private void DrawSelection(DataGridViewCellPaintingEventArgs e, int x, int y)
        {
            //if ((this.m_plateLayoutDefinition.Plates.CurrentPlate.Index == this.SelectedCells.SelectionPlate) && this.SelectedCells.Selected(x, y))
            //{
                this.DrawSelection(e);
            //}
        }
        /// <summary>
        /// 绘制被选中状态
        /// </summary>
        /// <param name="e"></param>
        public void DrawSelection(DataGridViewCellPaintingEventArgs e)
        {
            Brush brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.FromArgb(0x7f, Color.Black), Color.FromArgb(0x1f, Color.Black));
            e.Graphics.FillRectangle(brush, cellRc(e.CellBounds));
            e.Handled = true;
        }
        /// <summary>
        /// 绘制文本与边界
        /// </summary>
        /// <param name="e"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="replicate"></param>
        private void DrawTextAndBoundaries(DataGridViewCellPaintingEventArgs e, int x, int y, object replicate)
        {
            if (replicate != null)
            {
                this.DrawSampleBoundaries(e.Graphics, x, y, e.CellBounds, Color.CornflowerBlue);
                StringFormat tty = new StringFormat();
                tty.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(replicate.ToString(), this.DefaultCellStyle.Font, SystemBrushes.ControlText, cellRc(e.CellBounds), tty);
            }
            e.Handled = true;
        }
        /// <summary>
        /// 绘制样本边界
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bounds"></param>
        /// <param name="color"></param>
        private void DrawSampleBoundaries(Graphics graphics, int x, int y, Rectangle bounds, Color color)
        {
            Pen pen = new Pen(color, 2f);
            Rectangle tt = cellRc(bounds);
            graphics.DrawLine(pen, tt.Left+1, tt.Top, tt.Left+1, tt.Bottom);
            graphics.DrawLine(pen, tt.Left, tt.Top+1, tt.Right, tt.Top+1);
            graphics.DrawLine(pen, tt.Right, tt.Bottom-1, tt.Left, tt.Bottom-1);
            graphics.DrawLine(pen, tt.Right-1, tt.Bottom, tt.Right-1, tt.Top);
            
        }
        /// <summary>
        /// 绘制列指示标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawColumnHeader(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Brush brush = new SolidBrush(SystemColors.Control);
            Rectangle bounds = e.CellBounds;
            bounds.Inflate(-1, -1);
            e.Graphics.FillRectangle(brush, bounds);
            bounds = e.CellBounds;
            ControlPaint.DrawBorder3D(e.Graphics, bounds, Border3DStyle.RaisedInner);
            brush = new SolidBrush(SystemColors.ControlText);
            if(e.RowIndex==-1)
                if (e.ColumnIndex > -1)
                {
                    SizeF ef = e.Graphics.MeasureString((e.ColumnIndex+1).ToString(), Control.DefaultFont);
                    e.Graphics.DrawString((e.ColumnIndex + 1).ToString(), Control.DefaultFont, brush, (float)((bounds.Left + (bounds.Width / 2)) - (ef.Width / 2f)), (float)((bounds.Top + (bounds.Height / 2)) - (ef.Height / 2f)));
                }
            int rowA = 65;
            if(e.ColumnIndex==-1)
                if(e.RowIndex>-1)
                {
                    SizeF eff = e.Graphics.MeasureString(Convert.ToChar(rowA+e.RowIndex).ToString(), Control.DefaultFont);
                    e.Graphics.DrawString(Convert.ToChar(rowA + e.RowIndex).ToString(), Control.DefaultFont, brush, (float)((bounds.Left + (bounds.Width / 2)) - (eff.Width / 2f)), (float)((bounds.Top + (bounds.Height / 2)) - (eff.Height / 2f)));
                }
            e.Handled = true;
        }
        /// <summary>
        /// 绘制行指示标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawRowIndicator(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Brush brush = new SolidBrush(SystemColors.Control);
            Rectangle bounds = cellRc(e.CellBounds);
            bounds.Inflate(-1, -1);
            e.Graphics.FillRectangle(brush, bounds);
            bounds = cellRc(e.CellBounds);
            ControlPaint.DrawBorder3D(e.Graphics, bounds, Border3DStyle.RaisedInner);
            brush = new SolidBrush(SystemColors.ControlText);
            //if (e.Info.IsRowIndicator)
            //{
                string text = Numerals.IndexAsLetters(e.RowIndex);
                SizeF ef = e.Graphics.MeasureString(text, Control.DefaultFont);
                e.Graphics.DrawString(text, Control.DefaultFont, brush, (float)((bounds.Left + (bounds.Width / 2)) - (ef.Width / 2f)), (float)((bounds.Top + (bounds.Height / 2)) - (ef.Height / 2f)));
            //}
            e.Handled = true;
        }

        private List<HoleData> _dataSour;

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PlateGrid
            // 
            this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
        }
    }
    public class Numerals
    {
        public static string IndexAsLetters(int index)
        {
            bool flag = true;
            string str = string.Empty;
            int num2 = index;
            int num3 = 0x1a;
            do
            {
                int num;
                int num4 = num2 / num3;
                if (0 >= num4)
                {
                    num = 1;
                }
                else
                {
                    num = 0;
                }
                if (flag)
                {
                    num = 0;
                }
                char ch = (char)(((num2 % num3) - num) + 0x41);
                str = ch + str;
                num2 = num4;
                flag = false;
            }
            while (0 < num2);
            return str;
        }
    }
}
