using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRYSTAL;
using PortControlDemo.MoranControl.runMicroplate;

namespace PortControlDemo.MoranControl
{
    public partial class UMicroplateDesign : UserControl
    {
        public UMicroplateDesign()
        {
            InitializeComponent();
            SetLanguage();
        }
        private void SetLanguage()
        {
            itemEdit.Text = MoranControl.MoranLanguage.UMicroplateDesign_Edit;
            itemSample.Text = MoranControl.MoranLanguage.UMicroplateDesign_Sample;
            itemNegative.Text = MoranControl.MoranLanguage.UMicroplateDesign_N;
            itemPositive.Text = MoranControl.MoranLanguage.UMicroplateDesign_P;
            itemPositive2.Text = MoranControl.MoranLanguage.UMicroplateDesign_P2;
            itemStander.Text = MoranControl.MoranLanguage.UMicroplateDesign_Std;
            itemQc1.Text = MoranControl.MoranLanguage.UMicroplateDesign_Qc;
            itemBlanck.Text = MoranControl.MoranLanguage.UMicroplateDesign_Blank;
            itemUser.Text = MoranControl.MoranLanguage.UMicroplateDesign_User;
            itemNull.Text = MoranControl.MoranLanguage.UMicroplateDesign_Delete;
            CutMic.Text = MoranControl.MoranLanguage.UMicroplateDesign_Shear;
            PasteMic.Text = MoranControl.MoranLanguage.UMicroplateDesign_Paste;
        }
        private MicroplateSeting _MicSeting;
        /// <summary>
        /// 微孔板信息
        /// </summary>
        public MicroplateSeting MicSeting
        {
            get { return _MicSeting; }
            set
            {
                if (value == null) return;
                _MicSeting = value;
                Playout = value.MLayout;
                dgPlate.DataSour = value.MLayout.ListHoleData;
            }
        }
        private PlateLayout Playout;
        private messageDialog msd = new messageDialog();
        /// <summary>
        /// 两个微孔板之间的信息进行剪切粘贴信息移动时的中间静态变量
        /// </summary>
        public static List<HoleData> tempCellDates;

        private void ItemSample_Click(object sender, EventArgs e)
        {
        }

        
        private void DgPlate_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
                if (dgPlate.SelectedCells.Count == 1)
                    dgPlate.CurrentCell = dgPlate.Rows[e.RowIndex].Cells[e.ColumnIndex];
                menuDgPlate.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void DgPlate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                foreach (DataGridViewCell cell in dgPlate.SelectedCells)
                {
                    HoleData hd = (HoleData)cell.Value;
                    hd.ProName = hd.LiquidName = string.Empty;
                    hd.ThisHoleType = HoleType.Null;
                }
            dgPlate.Refresh();
        }

        private void ItemSample_Click_1(object sender, EventArgs e)
        {
            frmLiquidInput frm = new frmLiquidInput();
            //frmSampleInput frm = new frmSampleInput();
            frm.HoleTy = HoleType.Sample;
            frm.hlData = (HoleData)dgPlate.SelectedCells.OfType<DataGridViewCell>().OrderBy(ty => MicSeting.PlateOrderType == OrderType.LRTD ? ty.ColumnIndex + ty.RowIndex * dgPlate.Columns.Count : ty.ColumnIndex * dgPlate.Rows.Count + ty.RowIndex).First().Value;
            frm.LiquidName = frm.hlData.LiquidName;
            frm.ProName = frm.hlData.ProName;

            if (frm.ShowDialog() != DialogResult.OK) return;
            int orderNum = int.Parse(frm.hlData.LiquidName);
            foreach (DataGridViewCell cell in dgPlate.SelectedCells.OfType<DataGridViewCell>().OrderBy(ty => MicSeting.PlateOrderType == OrderType.LRTD ? ty.ColumnIndex + ty.RowIndex * dgPlate.Columns.Count : ty.ColumnIndex * dgPlate.Rows.Count + ty.RowIndex))
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = (orderNum++).ToString();
                hd.ProName = frm.hlData.ProName;
                hd.ThisHoleType = HoleType.Sample;
                hd.Unit = string.Empty;
            }
            dgPlate.Invalidate();
        }

        private void ItemNegative_Click(object sender, EventArgs e)
        {
            if (dgPlate.SelectedCells.Count < 1) return;
            frmLiquidInput frm = new frmLiquidInput();
            frm.HoleTy = HoleType.N;
            frm.hlData = (HoleData)dgPlate.SelectedCells[0].Value;
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = frm.hlData.LiquidName;
                hd.ProName = frm.hlData.ProName;
                hd.ThisHoleType = frm.hlData.ThisHoleType;
            }
            dgPlate.Invalidate();
        }

        private void ItemPositive_Click(object sender, EventArgs e)
        {
            if (dgPlate.SelectedCells.Count < 1) return;
            frmLiquidInput frm = new frmLiquidInput();
            frm.HoleTy = HoleType.P;
            frm.hlData = (HoleData)dgPlate.SelectedCells[0].Value;
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = frm.hlData.LiquidName;
                hd.ProName = frm.hlData.ProName;
                hd.ThisHoleType = frm.hlData.ThisHoleType;
            }
            dgPlate.Invalidate();
        }

        private void ItemPositive2_Click(object sender, EventArgs e)
        {
            if (dgPlate.SelectedCells.Count < 1) return;
            frmLiquidInput frm = new frmLiquidInput();
            frm.HoleTy = HoleType.P2;
            frm.hlData = (HoleData)dgPlate.SelectedCells[0].Value;
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = frm.hlData.LiquidName;
                hd.ProName = frm.hlData.ProName;
                hd.ThisHoleType = frm.hlData.ThisHoleType;
            }
            dgPlate.Invalidate();
        }

        private void ItemStander_Click(object sender, EventArgs e)
        {
            if (dgPlate.SelectedCells.Count < 1) return;
            frmLiquidInput frm = new frmLiquidInput();
            frm.HoleTy = HoleType.Std;
            frm.hlData = (HoleData)dgPlate.SelectedCells[0].Value;
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = frm.hlData.LiquidName;
                hd.ProName = frm.hlData.ProName;
                hd.SampleValue = frm.hlData.SampleValue;
                hd.ThisHoleType = frm.hlData.ThisHoleType;
            }
            dgPlate.Invalidate();
        }

        private void ItemQc1_Click(object sender, EventArgs e)
        {
            if (dgPlate.SelectedCells.Count < 1) return;
            frmLiquidInput frm = new frmLiquidInput();
            frm.HoleTy = HoleType.Qc;
            frm.hlData = (HoleData)dgPlate.SelectedCells[0].Value;
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = frm.hlData.LiquidName;
                hd.ProName = frm.hlData.ProName;
                hd.OtherValue = frm.hlData.OtherValue;
                hd.ThisHoleType = frm.hlData.ThisHoleType;
            }
            dgPlate.Invalidate();
        }

        private void ItemBlanck_Click(object sender, EventArgs e)
        {
            if (dgPlate.SelectedCells.Count < 1) return;
            frmLiquidInput frm = new frmLiquidInput();
            frm.HoleTy = HoleType.BL;
            frm.hlData = (HoleData)dgPlate.SelectedCells[0].Value;
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = frm.hlData.LiquidName;
                hd.ProName = frm.hlData.ProName;
                hd.OtherValue = frm.hlData.OtherValue;
                hd.ThisHoleType = frm.hlData.ThisHoleType;
            }
            dgPlate.Invalidate();
        }

        private void ItemUser_Click(object sender, EventArgs e)
        {
            if (dgPlate.SelectedCells.Count < 1) return;
            frmLiquidInput frm = new frmLiquidInput();
            frm.HoleTy = HoleType.User;
            frm.hlData = (HoleData)dgPlate.SelectedCells[0].Value;
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.LiquidName = frm.hlData.LiquidName;
                hd.ProName = frm.hlData.ProName;
                hd.OtherValue = frm.hlData.OtherValue;
                hd.ThisHoleType = frm.hlData.ThisHoleType;
            }
            dgPlate.Invalidate();
        }

        private void ItemNull_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                hd.ThisHoleType = HoleType.Null;
                hd.LiquidName = string.Empty;
                hd.Name = string.Empty;
                hd.Num = string.Empty;
                hd.OtherValue = string.Empty;
                hd.ProName = string.Empty;
            }
            dgPlate.Invalidate();
        }
        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutMic_Click(object sender, EventArgs e)
        {
            bool cutAllowed = true;
            foreach (DataGridViewCell cell in dgPlate.SelectedCells)
            {
                HoleData hd = (HoleData)cell.Value;
                if (hd.ThisHoleType == HoleType.Null)//防止剪切空白孔导致已剪切的数据因覆盖而丢失
                {
                    cutAllowed = false;
                }
            }
            if (cutAllowed == true)
            {
                SortingtempCellDates();
            }
            else
            {
                msd.show("请勿选中未分配的孔位");
            }
            dgPlate.Invalidate();
        }
        /// <summary>
        /// 给剪切后的数据排序
        /// </summary>
        private void SortingtempCellDates()
        {
            List<int> hangs = new List<int>();//李文洁2017.11.02
            List<int> lies = new List<int>();//所选孔的横、纵坐标
            tempCellDates = new List<HoleData>();
            foreach (DataGridViewCell item in dgPlate.SelectedCells)
            {
                hangs.Add(item.RowIndex);
                lies.Add(item.ColumnIndex);//所选孔的坐标添加
            }
            for (int i = 0; i < lies.Count - 1; i++)//对随机选择的孔位坐标进行重新排序
            {
                for (int j = 0; j < lies.Count - i - 1; j++)
                {
                    if (lies[j] > lies[j + 1])//列数大的靠后
                    {
                        int a = lies[j];
                        lies[j] = lies[j + 1];
                        lies[j + 1] = a;
                        a = hangs[j];
                        hangs[j] = hangs[j + 1];
                        hangs[j + 1] = a;
                    }
                    else if (lies[j] == lies[j + 1])//列数相同比较行数
                    {
                        if (hangs[j] > hangs[j + 1])
                        {
                            int a = hangs[j];
                            hangs[j] = hangs[j + 1];
                            hangs[j + 1] = a;
                        }
                    }
                    else { }
                }
            }
            for (int i = 0; i < dgPlate.SelectedCells.Count; i++)
            {
                DataGridViewCell cell = dgPlate[lies[i], hangs[i]];
                HoleData hd = (HoleData)cell.Value;
                HoleData exp = new HoleData();
                exp.ThisHoleType = hd.ThisHoleType;
                exp.LiquidName = hd.LiquidName;
                exp.Name = hd.Name;
                exp.Num = hd.Num;
                exp.OtherValue = hd.OtherValue;
                exp.ProName = hd.ProName;
                exp.GroupName = hd.GroupName;
                exp.Num = hd.Num;
                exp.ConcentrationDilution = hd.ConcentrationDilution;
                exp.LessOD = hd.LessOD;
                exp.MainOD = hd.MainOD;
                exp.OD = hd.OD;
                exp.SampleValue = hd.SampleValue;
                exp.Unit = hd.Unit;

                tempCellDates.Add(exp);
                //删除旧孔上的数据
                hd.ThisHoleType = HoleType.Null;
                hd.LiquidName = string.Empty;
                hd.Name = string.Empty;
                hd.Num = string.Empty;
                hd.OtherValue = string.Empty;
                hd.ProName = string.Empty;
                hd.GroupName = string.Empty;
                hd.Num = string.Empty;
                hd.ConcentrationDilution = string.Empty;
                hd.LessOD = 0;
                hd.MainOD = 0;
                hd.OD = 0;
                hd.SampleValue = 0;
                hd.Unit = string.Empty;
            }

        }
        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteMic_Click(object sender, EventArgs e)
        {
            if (tempCellDates == null)
            {
                return;
            }
            #region 对选择的孔进行整理排序使其排列顺序和选择顺序无关
            List<int> hangs = new List<int>();//李文洁2017.11.02
            List<int> lies = new List<int>();//所选孔的横、纵坐标
            foreach (DataGridViewCell item in dgPlate.SelectedCells)
            {
                hangs.Add(item.RowIndex);
                lies.Add(item.ColumnIndex);//所选孔的坐标添加
            }
            for (int i = 0; i < lies.Count - 1; i++)//对随机选择的孔位坐标进行重新排序
            {
                for (int j = 0; j < lies.Count - i - 1; j++)
                {
                    if (lies[j] > lies[j + 1])//列数大的靠后
                    {
                        int a = lies[j];
                        lies[j] = lies[j + 1];
                        lies[j + 1] = a;
                        a = hangs[j];
                        hangs[j] = hangs[j + 1];
                        hangs[j + 1] = a;
                    }
                    else if (lies[j] == lies[j + 1])//列数相同比较行数
                    {
                        if (hangs[j] > hangs[j + 1])
                        {
                            int a = hangs[j];
                            hangs[j] = hangs[j + 1];
                            hangs[j + 1] = a;
                        }
                    }
                    else { }
                }
            }
            #endregion
            #region 当选择的孔位较少时，自动添加所选孔的后面至接收数据的孔
            if (tempCellDates.Count >= dgPlate.SelectedCells.Count)
            {
                for (int i = 0; i < tempCellDates.Count - dgPlate.SelectedCells.Count; i++)
                {
                    if (hangs[hangs.Count - 1] < 7)//最后一位行号大于7自动转为下一列
                    {
                        hangs.Add(hangs[hangs.Count - 1] + 1);
                        lies.Add(lies[lies.Count - 1]);
                    }
                    else
                    {
                        hangs.Add(0);
                        lies.Add(lies[lies.Count - 1] + 1);
                    }
                }
            }
            #endregion
            for (int i = 0; i < tempCellDates.Count; i++)
            {
                DataGridViewCell cell = dgPlate[lies[i], hangs[i]];
                HoleData hd = (HoleData)cell.Value;
                hd.ThisHoleType =  tempCellDates[i].ThisHoleType;
                hd.LiquidName =  tempCellDates[i].LiquidName;
                hd.Name =  tempCellDates[i].Name;
                hd.Num =  tempCellDates[i].Num;
                hd.OtherValue =  tempCellDates[i].OtherValue;
                hd.ProName =  tempCellDates[i].ProName;
                hd.GroupName =  tempCellDates[i].GroupName;
                hd.Num =  tempCellDates[i].Num;
                hd.ConcentrationDilution =  tempCellDates[i].ConcentrationDilution;
                hd.Unit =  tempCellDates[i].Unit;
                hd.LessOD =  tempCellDates[i].LessOD;
                hd.MainOD =  tempCellDates[i].MainOD;
                hd.OD =  tempCellDates[i].OD;
                hd.SampleValue =  tempCellDates[i].SampleValue;


                hd = null;
            }
            dgPlate.Invalidate();

        }
        /// <summary>
        /// MicSeting赋初始值，即MicSeting初始化
        /// </summary>
        public void NewOne()
        {
            MicroplateSeting micSeting = new MicroplateSeting();
            micSeting.BetweenDistance = 9;
            micSeting.Bottom = 0;
            micSeting.Cell_Height = 11.9;
            micSeting.Cell_Radius = 6.3;
            micSeting.CenterDistanceH = 9;
            micSeting.CenterDistanceV = 9;
            micSeting.DBID = Guid.NewGuid().ToString();
            micSeting.Height = 300;
            micSeting.HorizontalNum = 12;
            micSeting.Index = 0;
            micSeting.LeftPad = 5;
            micSeting.Lenght = 100;
            micSeting.Name = "AloneMic";
            micSeting.PlateOrderType = OrderType.TDLR;
            micSeting.ProName = "ProMic";
            micSeting.SampleStartNum = 1;
            micSeting.SampleUsedNum = 0;
            micSeting.Span = 12;
            micSeting.TopPad = 5;
            micSeting.VerticalNum = 8;
            micSeting.Width = 100;
            micSeting.XNum = 12;
            micSeting.YNum = 8;
            if (micSeting.MLayout == null)
            {
                micSeting.MLayout = new PlateLayout();
                micSeting.MLayout.InItDataSet();
            }
            MicSeting = micSeting;
        }

        public void isMenuDgPlate() {
            menuDgPlate.Enabled = false;
        }
    }

}
