
using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo.MoranControl.runMicroplate
{
    public partial class frmInputName : Form
    {
        /// <summary>
        /// 是否是洗板液的选择
        /// </summary>
        public bool washerFluid = false;

        public frmInputName()
        {
            InitializeComponent();
            SetLanguage();
        }

        public string LiqName { get; set; }
        public string ProName { get; set; }

        private void FrmInputName_Load(object sender, EventArgs e)
        {
            cmbMethods.Items.Add("");
            if (ProName == "")
                cmbMethods.SelectedIndex = 0;
            else
                cmbMethods.Text = ProName;
            cmbLiqName.Text = LiqName;
        }

        private void CmbMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLiqName.Items.Clear();
            cmbLiqName.Text = null;
            List<string> listPro = new List<string>();

            List<string> prosNames = new List<string>();
            if (string.IsNullOrEmpty(cmbMethods.Text))
            {
                for (int i = 0; i < cmbMethods.Items.Count - 1; i++) //cmbMethods.Items的最后一个项目名称为"",所以不循环
                {
                    prosNames.Add(cmbMethods.Items[i].ToString());
                }
                cmbLiqName.Items.Add("");
            }
            else
                prosNames.Add(cmbMethods.Text);
            foreach (string proName in prosNames)
            {
                ProMethod program = ProMethod.getProMethod(proName);
                if (!washerFluid)
                {
                    #region 样本
                    var addSample = (from t in program.CMethodSteps.ListMethod where t.StepType == ExperimentStep.AddSample select t);
                    if (addSample.Count() > 0)
                    {
                        AddSample addSpe = (AddSample)addSample.First().ObjStep;
                        if (addSpe.DilutionBei > 1 || addSpe.DirectDilutionValue > 0)
                        {
                            if (!listPro.Any(ty => ty == program.CProInfo.XiShiName))
                            {
                                listPro.Add(program.CProInfo.XiShiName);
                                cmbLiqName.Items.Add(program.CProInfo.XiShiName);
                            }
                            if (!string.IsNullOrEmpty(addSpe.BlankLiquidName) && program.CProInfo.XiShiName != addSpe.BlankLiquidName)
                                if (!listPro.Any(ty => ty == addSpe.BlankLiquidName))
                                {
                                    listPro.Add(addSpe.BlankLiquidName);
                                    cmbLiqName.Items.Add(addSpe.BlankLiquidName);
                                }
                        }
                    }
                    #endregion
                    #region 当勾选了倍比稀释功能的时候，在液体名称的下拉列框中添加实验的母液名称选项，以便于可手动添加相应的实验的母液名称。thhAdd 2017.12.06
                    //if ((SoftConfig.Doubledilution == true) && (program.CProInfo.xishi == true))
                    //{
                    //    listPro.Add(program.CProInfo.OriginLiquid);
                    //    cmbLiqName.Items.Add(program.CProInfo.OriginLiquid);
                    //}
                    #endregion
                    #region 稀释液
                    if ((program.CProInfo.NPDilutionBei + program.CProInfo.NPDirectDilutionValue + program.CProInfo.StdDilutionBei + program.CProInfo.StdDirectDilutionValue + program.CProInfo.QcDilutionBei + program.CProInfo.QcDirectDilutionValue + program.CProInfo.DilutionBei) > 0)
                    {
                        if (!listPro.Any(ty => ty == program.CProInfo.XiShiName))
                        {
                            listPro.Add(program.CProInfo.XiShiName);
                            cmbLiqName.Items.Add(program.CProInfo.XiShiName);
                        }
                    }
                    #endregion
                    #region PN
                    foreach (PorNInfo pninfo in program.CProInfo.ListPN)
                    {
                        if (pninfo.PorNnum > 0)
                        {
                            if (!listPro.Any(ty => ty == pninfo.PorNname))
                            {
                                listPro.Add(pninfo.PorNname);
                                cmbLiqName.Items.Add(pninfo.PorNname);
                            }
                        }
                    }
                    #endregion
                    #region 定量实验std
                    if (program.CProInfo.ResponseProperties == "2" || program.CProInfo.ResponseProperties == "3")
                    {
                        //if (program.CProInfo.ListStds.Any(ty => ty.XiShiVol > 0))
                        foreach (StandardInfo info in program.CProInfo.ListStds)
                        {
                            if (info.Num > 0)
                                if (!listPro.Any(ty => ty == info.LiqName))
                                {
                                    listPro.Add(info.LiqName);
                                    cmbLiqName.Items.Add(info.LiqName);
                                }
                        }
                    }
                    #endregion
                    #region 酶 终止液 底物 Editzx 洗板液跟其他分开(如果是单针，进行洗板液的添加)
                    foreach (Steps stp in program.CMethodSteps.ListMethod)
                    {
                        switch (stp.StepType)
                        {
                            case ExperimentStep.AddEnzyme:
                            case ExperimentStep.AddStopping:
                            case ExperimentStep.AddSubstrate:
                                if (stp.ObjStep != null)
                                    foreach (SuctionInfo sd in ((Suction_Distribution)stp.ObjStep).ListSuctionInfo)
                                    {
                                        if (!listPro.Any(ty => ty == sd.LiquidName))
                                        {
                                            listPro.Add(sd.LiquidName);
                                            cmbLiqName.Items.Add(sd.LiquidName);
                                        }
                                    }
                                break;
                            case ExperimentStep.ClearnPlate:
                                if (stp.ObjStep != null)
                                {
                                    string str = ((ClearnPlate)stp.ObjStep).WasherFluid;
                                    if (!string.IsNullOrEmpty(str))
                                        if (!listPro.Any(ty => ty == str))
                                        {
                                            listPro.Add(str);
                                            cmbLiqName.Items.Add(str);
                                        }
                                }
                                break;
                        }
                    }
                    #endregion
                }

            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            LiqName = cmbLiqName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetLanguage() {
            label2.Text = MoranControl.MoranLanguage.frmInputName_label2;
            label1.Text = MoranControl.MoranLanguage.frmInputName_label1;
            btnOK.Text = MoranControl.MoranLanguage.frmInputName_btnOK;
            btnCancel.Text = MoranControl.MoranLanguage.frmInputName_btnCancel;
        }
    }
}
