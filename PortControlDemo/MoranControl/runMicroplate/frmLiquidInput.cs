
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRYSTAL;
using CCWin.SkinControl;
using System;

namespace PortControlDemo.MoranControl.runMicroplate
{
    public partial class frmLiquidInput : Form
    {
        public HoleData hlData;
        public  HoleType HoleTy;
        internal string LiquidName;
        internal string ProName;
        ProMethod program;
        private messageDialog msd = new messageDialog();
        public frmLiquidInput()
        {
            InitializeComponent();
            SetLanguage();
        }
        private void SetLanguage()
        {
            label2.Text = MoranControl.MoranLanguage.frmInputName_label2;
            lbLiqName.Text = MoranControl.MoranLanguage.frmInputName_label1;
            lbSample.Text = MoranControl.MoranLanguage.frmLiquidInput_lbSample;
            btnOk.Text = MoranControl.MoranLanguage.frmInputName_btnOK;
            btnCancel.Text = MoranControl.MoranLanguage.frmInputName_btnCancel;
        }
        private void FrmLiquidInput_Load(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Programs");
            foreach (FileInfo fi in di.GetFiles("*.xml"))
                cmbProgram.Items.Add(fi.Name.Substring(0, fi.Name.LastIndexOf('.')));
            cmbProgram.SelectedItem = hlData.ProName;
            lbLiqName.Visible = true;
            lbSample.Visible = false;

            switch (HoleTy)
            {
                case HoleType.N:
                case HoleType.User:
                case HoleType.P:
                case HoleType.P2:
                    this.cmbLiqName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    cmbLiqName.Text = hlData.LiquidName;
                    break;

                case HoleType.Std:
                    this.cmbLiqName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    foreach ( ListItemT li in cmbLiqName.Items)
                    {
                        if (li.ThisText == hlData.LiquidName)
                        {
                            cmbLiqName.SelectedItem = li;
                            break;
                        }
                    }
                    break;
                case HoleType.Qc:
                    this.cmbLiqName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    if (hlData.OtherValue != null && ! string.IsNullOrEmpty(hlData.OtherValue))
                        cmbLiqName.SelectedValue = new Guid(hlData.OtherValue);
                    break;
                case HoleType.BL:
                    this.cmbLiqName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    cmbLiqName.Text = hlData.LiquidName;
                    break;
                case HoleType.Sample:
                    this.cmbLiqName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
                    cmbLiqName.Text = hlData.LiquidName;
                    lbLiqName.Visible = false;
                    lbSample.Visible = true;
                    lbSample.Text = MoranControl.MoranLanguage.frmLiquidInput_Tips1 + ":";
                    break;
            }
        }

        private void CmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProgram.SelectedItem == null) return;
            program = ProMethod.getProMethod(cmbProgram.SelectedItem.ToString());
            switch (HoleTy)
            {
                case  HoleType.N:
                    cmbLiqName.Items.Clear();
                    if (program.CProInfo.NNum > 0)
                    {
                        cmbLiqName.Items.Add(program.CProInfo.NName);//20150601 lxm
                        cmbLiqName.SelectedIndex = 0;
                    }
                    break;
                case  HoleType.P:
                    cmbLiqName.Items.Clear();
                    if (program.CProInfo.PNum > 0)
                    {
                        cmbLiqName.Items.Add(program.CProInfo.PName);
                        cmbLiqName.SelectedIndex = 0;
                    }
                    break;
                case  HoleType.P2://lxm
                    cmbLiqName.Items.Clear();
                    if (program.CProInfo.P2Num > 0)
                    {
                        cmbLiqName.Items.Add(program.CProInfo.P2Name);
                        cmbLiqName.SelectedIndex = 0;
                    }
                    break;
                case  HoleType.User:
                    cmbLiqName.Items.Clear();
                    foreach (PorNInfo p in program.CProInfo.ListPN)//kcl 修改右键自定义的液体显示名称
                        cmbLiqName.Items.Add(p.PorNname);
                    if (cmbLiqName.Items.Count > 0)//lyn add 避免若item没有值时软件报错
                        cmbLiqName.SelectedIndex = 0;
                    break;
                case  HoleType.Std:
                    cmbLiqName.Text = string.Empty;
                    cmbLiqName.Items.Clear();
                    if (program.CProInfo.ResponseProperties == "3" || program.CProInfo.ResponseProperties == "2")
                    {
                        foreach (StandardInfo inf in program.CProInfo.ListStds)
                            cmbLiqName.Items.Add(new ListItemT(inf.LiqName, inf.LiqValue));
                    }
                    if (cmbLiqName.Items.Count > 0)
                        cmbLiqName.SelectedIndex = 0;
                    break;
                case  HoleType.Qc:
                    BindCmbNoNewPer(cmbLiqName, "SELECT QCBatchID, proName + '-' + QCBatchNo AS Expr1 FROM QCBatch where proName='" + cmbProgram.SelectedItem.ToString() + "'");
                    if (cmbLiqName.Items.Count > 0)
                        cmbLiqName.SelectedIndex = 0;
                    break;
                case HoleType.BL://空白
                    cmbLiqName.Items.Clear();
                    AddSample adds = null;
                    foreach (Steps stp in program.CMethodSteps.ListMethod)
                        if (stp.StepType == ExperimentStep.AddSample)
                        {
                            adds = (AddSample)stp.ObjStep;
                            break;
                        }
                    if (adds != null)
                        if (!string.IsNullOrEmpty(adds.BlankLiquidName))
                            cmbLiqName.Items.Add(adds.BlankLiquidName);
                    if (cmbLiqName.Items.Count > 0)
                        cmbLiqName.SelectedIndex = 0;
                    break;
                case HoleType.Sample:
                    cmbLiqName.Items.Clear();
                    break;
            }
        }
        /// <summary>
        /// 绑定信息到控件
        /// </summary>
        /// <param name="cmb">ComboBox控件</param>
        /// <param name="sql">数据库查询语句</param>
        private void BindCmbNoNewPer(ComboBox cmb, string sql)
        {
            //DataTable dt = DBUtility.DbHelperSQL.Query(sql).Tables[0];
            //cmb.DisplayMember = dt.Columns[1].ColumnName;
            //cmb.ValueMember = dt.Columns[0].ColumnName;
            //cmb.DataSource = dt;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {

            {
                if (HoleTy ==  HoleType.Sample)
                {
                    if (string.IsNullOrEmpty(cmbLiqName.Text.Trim()))
                    {
                        msd.show(MoranControl.MoranLanguage.frmLiquidInput_Tips2);
                        return;
                    }
                    else
                    {
                        int ty = 0;
                        if (!int.TryParse(cmbLiqName.Text.Trim(), out ty))
                        {
                           msd.show(MoranControl.MoranLanguage.frmLiquidInput_Tips3);
                            cmbLiqName.SelectAll();
                            cmbLiqName.Focus();
                            return;
                        }
                        else
                        {
                            //if (ty < 1 || ty > DeviceConfig.DeviceMaxSampleNum)
                            if (ty < 1 || ty >96)
                            {
                                msd.show(string.Format(MoranControl.MoranLanguage.frmLiquidInput_Tips4, 96));
                                //msd.show(getLocaltionStr("Input180Number"));
                                cmbLiqName.SelectAll();
                                cmbLiqName.Focus();
                                return;
                            }
                        }
                    }
                }
                else if (HoleTy !=  HoleType.BL)
                {
                    if (string.IsNullOrEmpty(cmbLiqName.Text.Trim()))
                    {
                        msd.show(MoranControl.MoranLanguage.frmLiquidInput_Tips5);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(cmbProgram.Text.Trim()))
                {
                    msd.show(MoranControl.MoranLanguage.frmLiquidInput_Tips6);
                    return;
                }
                switch (HoleTy)
                {
                    case HoleType.Std:
                        double du = double.MaxValue;
                        foreach (StandardInfo stdInfo in program.CProInfo.ListStds)
                        {
                            if (stdInfo.LiqName == cmbLiqName.Text)
                                du = double.Parse(stdInfo.LiqValue);
                        }
                        if (du == double.MaxValue)
                        {
                            msd.show(MoranControl.MoranLanguage.frmLiquidInput_Tips7);
                            return;
                        }
                        break;
                    case HoleType.BL:
                        AddSample adds = null;
                        foreach (Steps stp in program.CMethodSteps.ListMethod)
                            if (stp.StepType == ExperimentStep.AddSample)
                            {
                                adds = (AddSample)stp.ObjStep;
                                break;
                            }
                        if (adds == null)
                        {
                            msd.show(MoranControl.MoranLanguage.frmLiquidInput_Tips8);
                            return;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(adds.BlankLiquidName))
                                if (!string.IsNullOrEmpty(cmbLiqName.Text) && cmbLiqName.Text != adds.BlankLiquidName)
                                {
                                    msd.show(MoranControl.MoranLanguage.frmLiquidInput_Tips9 + cmbLiqName.Text);
                                    return;
                                }
                        }
                        break;
                }
                hlData = new HoleData();
                switch (HoleTy)
                {
                    case HoleType.N:
                        hlData.ProName = cmbProgram.SelectedItem.ToString();
                        hlData.LiquidName = cmbLiqName.Text;
                        hlData.ThisHoleType = HoleType.N;
                        break;
                    case HoleType.P:
                        hlData.ProName = cmbProgram.SelectedItem.ToString();
                        hlData.LiquidName = cmbLiqName.Text;
                        hlData.ThisHoleType = HoleType.P;
                        break;
                    case HoleType.P2:
                        hlData.ProName = cmbProgram.SelectedItem.ToString();
                        hlData.LiquidName = cmbLiqName.Text;
                        hlData.ThisHoleType = HoleType.P2;
                        break;
                    case HoleType.User:
                        hlData.ProName = cmbProgram.SelectedItem.ToString();
                        hlData.LiquidName = cmbLiqName.SelectedItem.ToString();//cmbLiqName.Text;
                        hlData.ThisHoleType = HoleType.User;
                        break;
                    case HoleType.Std:
                        hlData.ProName = cmbProgram.SelectedItem.ToString();
                        ListItemT li = (ListItemT)cmbLiqName.SelectedItem;
                        hlData.LiquidName = li.ThisText;
                        foreach (StandardInfo stdInfo in program.CProInfo.ListStds)
                        {
                            if (stdInfo.LiqName == cmbLiqName.Text)
                                hlData.SampleValue = double.Parse(stdInfo.LiqValue);
                        }
                        hlData.ThisHoleType = HoleType.Std;
                        break;
                    case HoleType.BL:
                        hlData.ProName = cmbProgram.SelectedItem.ToString();
                        hlData.LiquidName = cmbLiqName.Text.Trim();
                        hlData.ThisHoleType = HoleType.BL;
                        break;
                    case HoleType.Sample:
                        hlData.ProName = cmbProgram.SelectedItem.ToString();
                        hlData.LiquidName = cmbLiqName.Text.Trim();
                        hlData.ThisHoleType = HoleType.Sample;
                        break;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
