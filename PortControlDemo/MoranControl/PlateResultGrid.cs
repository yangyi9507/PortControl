
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 微孔板结果组件
    /// </summary>
    public class PlateResultGrid : DataGridView
    {
        public PlateResultGrid()
        {
            this.ReadOnly = true;
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);   //   禁止擦除背景.   
            SetStyle(ControlStyles.DoubleBuffer, true);   //   双缓冲  
            RowHeadersWidth = 20;
            ReadOnly = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            DrawgroundLevel = true;
        }
        /// <summary>
        /// 列数
        /// </summary>
        int colNum = 12;
        /// <summary>
        /// 微孔板列数
        /// </summary>
        public int ColNum
        {
            get { return colNum; }
            set
            {
                colNum = value;
                init();
            }
        }

        /// <summary>
        /// 行数
        /// </summary>
        int rowNum = 8;
        /// <summary>
        /// 微孔板行数
        /// </summary>
        public int RowNum
        {
            get { return rowNum; }
            set
            {
                rowNum = value;
                init();

            }
        }
        /// <summary>
        /// 按照设置的行列数生成微孔板模型
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
        /// 微孔板长宽调整
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
        /// 绘制微孔类型颜色
        /// </summary>
        public bool DrawgroundLevel { get; set; }
        /// <summary>
        /// 调整大小
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Ajust();
        }
        /// <summary>
        /// 绘制整个微孔板孔
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            if ((e.ColumnIndex >= 0) && (e.RowIndex >= 0))
            {
                e.PaintBackground(e.ClipBounds, false);//绘制背景

                PlateCellResult CellValue = (PlateCellResult)e.Value;

                if (CellValue == null) return;
                this.DrawBackground(e, getColorByDataType(CellValue.ThisHoleType));
                if (CellValue != null)
                    if (DrawgroundLevel)
                        this.DrawBackgroundLevel(e, getColor(CellValue.Level));
                if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                {
                    this.DrawSelection(e);
                }
                if (CellValue != null)
                    DrawText(e, CellValue);

            }
            else
                DrawColumnHeader(this, e);

        }
        /// <summary>
        /// 选中微孔时的状态
        /// </summary>
        /// <param name="e"></param>
        public void DrawSelection(DataGridViewCellPaintingEventArgs e)
        {
            //绘制阴性斜线
            Brush brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.FromArgb(0x7f, Color.Black), Color.FromArgb(0x1f, Color.Black));
            e.Graphics.FillRectangle(brush, cellRc(e.CellBounds));
            e.Handled = true;
        }
        /// <summary>
        /// 根据孔类型赋颜色
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        Color getColorByDataType(HoleType tp)
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
                case HoleType.P2:
                    return Color.DarkRed;
                case HoleType.User:
                    return Color.Fuchsia;
                default:
                    return Color.Beige;
            }
        }
        /// <summary>
        /// 绘制微孔信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="Value">微孔的结果信息</param>
        void DrawText(DataGridViewCellPaintingEventArgs e, PlateCellResult Value)
        {
            if ((Value.Title != "Null") && !string.IsNullOrEmpty(Value.Title))
            {
                #region 对样本编号、结果、OD、Sco的值依次排序绘制
                StringFormat tty = new StringFormat();
                //样本编号
                tty.Alignment = StringAlignment.Center;
                tty.LineAlignment = StringAlignment.Near;
                tty.FormatFlags = StringFormatFlags.NoWrap;
                Font t = new System.Drawing.Font(this.DefaultCellStyle.Font, FontStyle.Bold);
                Font tSCO = new System.Drawing.Font(t.FontFamily, t.Size-1);
                //标题 如 样本编号、阴阳对照N P，标准品S1、S2等
                e.Graphics.DrawString(Value.Title, this.DefaultCellStyle.Font, SystemBrushes.ControlText, cellRc(e.CellBounds), tty);
                //结果
                tty.Alignment = StringAlignment.Center;
                tty.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(Value.Result, t, SystemBrushes.ControlText, cellRc(e.CellBounds), tty);
                //OD
                e.Graphics.DrawString(Value.OD, this.DefaultCellStyle.Font, SystemBrushes.ControlText, cellRc2(e.CellBounds), tty);
                //SCO
                tty.Alignment = StringAlignment.Center;
                tty.LineAlignment = StringAlignment.Far;
                e.Graphics.DrawString(Value.SCO, tSCO, SystemBrushes.ControlText, cellRcsco(e.CellBounds), tty);
                #endregion
            }
            e.Handled = true;
        }
        /// <summary>
        /// 绘制结果位置
        /// </summary>
        /// <param name="te"></param>
        /// <returns></returns>
        Rectangle cellRc(Rectangle te)
        {
            return new Rectangle(te.X + 1, te.Y + 1, te.Width - 3, te.Height - 3);
        }
        /// <summary>
        ///绘制sco的位置
        /// </summary>
        /// <param name="te"></param>
        /// <returns></returns>
        Rectangle cellRcsco(Rectangle te)
        {
            return new Rectangle(te.X + 1, te.Y + 1, te.Width - 3, te.Height);
        }
        /// <summary>
        /// 绘制OD的位置
        /// </summary>
        /// <param name="te"></param>
        /// <returns></returns>
        Rectangle cellRc2(Rectangle te)
        {
            return new Rectangle(te.X + 1, te.Y + 1 + (te.Height - 10) / 2, te.Width - 3, te.Height / 2);
        }
        Rectangle cellRc3(Rectangle te)
        {
            return new Rectangle(te.X + 1, te.Y + 1 + (te.Height - 3) / 2, te.Width - 3, te.Height / 2);
        }
        public void DrawBackground(DataGridViewCellPaintingEventArgs e, Color colorHoleType)
        {
            Brush brushh = new LinearGradientBrush(cellRc(e.CellBounds), colorHoleType, Color.FromArgb(((colorHoleType.R * 0x84) / 0xff) + 0x7b, ((colorHoleType.G * 0x84) / 0xff) + 0x7b, ((colorHoleType.B * 0x84) / 0xff) + 0x7b), 40f, false);
            e.Graphics.FillRectangle(brushh, cellRc(e.CellBounds));
            e.Handled = true;
        }
        /// <summary>
        /// 绘制有孔类型的微孔板
        /// </summary>
        /// <param name="e"></param>
        /// <param name="colorSample"></param>
        public void DrawBackgroundLevel(DataGridViewCellPaintingEventArgs e, Color colorSample)
        {
            Brush brush = new LinearGradientBrush(cellRc(e.CellBounds), colorSample, Color.FromArgb(((colorSample.R * 0x84) / 0xff) + 0x7b, ((colorSample.G * 0x84) / 0xff) + 0x7b, ((colorSample.B * 0x84) / 0xff) + 0x7b), 30f, false);
            e.Graphics.FillRectangle(brush, cellRc3(e.CellBounds));
            e.Handled = true;
        }
        /// <summary>
        /// 列标题绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawColumnHeader(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region 绘制边框
            Brush brush = new SolidBrush(SystemColors.Control);
            Rectangle bounds = e.CellBounds;
            bounds.Inflate(-1, -1);
            e.Graphics.FillRectangle(brush, bounds);
            bounds = e.CellBounds;
            ControlPaint.DrawBorder3D(e.Graphics, bounds, Border3DStyle.RaisedInner);
            #endregion
            #region 绘制标号
            brush = new SolidBrush(SystemColors.ControlText);
            if (e.RowIndex == -1)
                if (e.ColumnIndex > -1)
                {
                    SizeF ef = e.Graphics.MeasureString((e.ColumnIndex + 1).ToString(), Control.DefaultFont);
                    e.Graphics.DrawString((e.ColumnIndex + 1).ToString(), Control.DefaultFont, brush, (float)((bounds.Left + (bounds.Width / 2)) - (ef.Width / 2f)), (float)((bounds.Top + (bounds.Height / 2)) - (ef.Height / 2f)));
                }
            int rowA = 65;
            if (e.ColumnIndex == -1)
                if (e.RowIndex > -1)
                {
                    SizeF eff = e.Graphics.MeasureString(Convert.ToChar(rowA + e.RowIndex).ToString(), Control.DefaultFont);
                    e.Graphics.DrawString(Convert.ToChar(rowA + e.RowIndex).ToString(), Control.DefaultFont, brush, (float)((bounds.Left + (bounds.Width / 2)) - (eff.Width / 2f)), (float)((bounds.Top + (bounds.Height / 2)) - (eff.Height / 2f)));
                }
            e.Handled = true;
            #endregion
        }
        /// <summary>
        /// 微孔板单个微孔结果信息
        /// </summary>
        private List<PlateCellResult> _dataSour;
        /// <summary>
        /// 微孔板单个微孔结果信息
        /// </summary>
        public List<PlateCellResult> DataSour
        {
            get { return _dataSour; }
            set
            {
                _dataSour = value;
                if (DataSour != null)
                    foreach (PlateCellResult pcr in _dataSour)
                        Rows[pcr.X - 1].Cells[pcr.Y - 1].Value = pcr;
            }
        }


        /// <summary>
        /// 根据微孔板结果给其赋颜色
        /// </summary>
        /// <param name="tp">结果等级（0-白；1-灰；2-红）</param>
        /// <returns></returns>
        Color getColor(int tp)
        {
            if (tp == 0)
                return Color.White;
            else if (tp == 1)
                return Color.DimGray;
            else
                return Color.Red;
        }
        /// <summary>
        /// 把微孔板的值都赋为null
        /// </summary>
        public void ClearnCellsValue()
        {
            foreach (DataGridViewRow row in Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                    cell.Value = null;
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PlateResultGrid
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
    /// <summary>
    /// 微孔结果信息
    /// </summary>
    public class PlateCellResult
    {
        /// <summary>
        /// 样本号
        /// </summary>
        public string Title { get; set; }
        public string Result { get; set; }
        public string OD { get; set; }
        public string SCO { get; set; }
        /// <summary>
        /// A..H
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// 1..12
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 0阴1灰-2阳
        /// </summary>
        public int Level { get; set; }
        public string ProName { get; set; }
        public string ResultID { get; set; }
        private HoleType _thisHoleType = HoleType.Null;
        public HoleType ThisHoleType
        {
            get { return _thisHoleType; }
            set { _thisHoleType = value; }
        }
    }
}
