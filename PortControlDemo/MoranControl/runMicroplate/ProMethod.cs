using CCWin.SkinControl;
using ElContros;
using PortControlDemo.MoranControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 项目参数类
    /// </summary>
    [Serializable]
    public class ProMethod
    {
        /// <summary>
        /// 打印顺序
        /// </summary>
        public int PrintIndex { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProType { get; set; }
        /// <summary>
        /// 项目基本信息
        /// </summary>
        public ProInfo CProInfo;
        /// <summary>
        /// 微孔板布局顺序
        /// </summary>
        public PlateLayout CPlateLayout;
        /// <summary>
        /// 项目步骤
        /// </summary>
        public MethodSteps CMethodSteps;
        /// <summary>
        /// 读数规则
        /// </summary>
        public ReadRule CReadRule;
        /// <summary>
        /// CutOff判定
        /// </summary>
        public CUTFFQC CCUTFFQC;
        public ProMethod()
        {
        }
        /// <summary>
        /// 实例化并new
        /// </summary>
        /// <param name="flag"></param>
        public ProMethod(bool flag)
        {
            if (!flag) return;
            CProInfo = new ProInfo();
            CPlateLayout = new PlateLayout();
            CPlateLayout.InItDataSet();

            CMethodSteps = new MethodSteps(true);
            CReadRule = new ReadRule();
            CCUTFFQC = new CUTFFQC();
        }
        /// <summary>
        /// 保存项目
        /// </summary>
        public void SaveToXml()
        {
            switch (this.ProType)
            {
                case 1:
                    SerializerHelper.XMLSerialize(this, Application.StartupPath + @"\Programs\" + this.CProInfo.ProName + ".xml");
                    break;
                case 2:
                    SerializerHelper.XMLSerialize(this, Application.StartupPath + @"\Programs\" + this.CProInfo.ProName + ".xm");
                    break;
            }

        }

        ////======================================Editzx20180607=====================================================////
        #region 通过项目名称获取全部实验步骤
        /// <summary>
        /// 通过项目名称获取全部实验步骤 Editzx20180607
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static ProMethod getAllProMethod(string MethodName)
        {
            foreach (ProMethod me in proCatch)
            {
                if (me.CProInfo.ProName == MethodName)
                    return me;
            }
            if (System.IO.File.Exists(Application.StartupPath + @"\Programs\" + MethodName + ".xml"))
                return (ProMethod)SerializerHelper.XMLDeserialize(typeof(ProMethod), Application.StartupPath + @"\Programs\" + MethodName + ".xml");
            if (System.IO.File.Exists(Application.StartupPath + @"\Programs\" + MethodName + ".xm"))
                return (ProMethod)SerializerHelper.XMLDeserialize(typeof(ProMethod), Application.StartupPath + @"\Programs\" + MethodName + ".xm");
            throw new Exception(string.Format(MoranControl.MoranLanguage.ProMethod_Tips1, MethodName));
        }
        #endregion

        #region 根据项目名称获取项目要执行的实验步骤ProMethod
        /// <summary>
        /// 根据项目名称获取项目要执行的实验步骤 Editzx20180607
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static ProMethod getProMethod(string MethodName)
        {
            ProMethod pm = getAllProMethod(MethodName);
            ProMethod newPm = new ProMethod();
            newPm.CCUTFFQC = pm.CCUTFFQC;
            newPm.CPlateLayout = pm.CPlateLayout;
            newPm.CProInfo = pm.CProInfo;
            newPm.CReadRule = pm.CReadRule;
            newPm.PrintIndex = pm.PrintIndex;//20180820 lxm 以下两行修改因Editzx20180607导致的打印顺序无法保存问题
            newPm.ProType = pm.ProType;//20180820
            newPm.CMethodSteps = new MethodSteps();
            if (pm.CMethodSteps != null)
            {
                foreach (Steps sp in pm.CMethodSteps.ListMethod)
                {
                    if (sp.IsExecute != true)
                    {
                        continue;
                    }

                    newPm.CMethodSteps.ListMethod.Add(sp);
                }
            }
            return newPm;
        }
        #endregion
        ////======================================Endzx20180607====================================================////

        private static List<ProMethod> proCatch = new List<ProMethod>();
        /// <summary>
        /// 获取参照类溶液名称
        /// </summary>
        /// <returns>以“|”间隔的液体名称字符串</returns>
        public string getLiquids()
        {
            StringBuilder strbud = new StringBuilder();
            //if (!CProInfo.NName.IsNullOrEmpty()) //20161128lxm注释掉，将其替换为下面的foreach语句
            //    strbud.Append(CProInfo.NName + "|");
            //if (!CProInfo.PName.IsNullOrEmpty())
            //    strbud.Append(CProInfo.PName + "|");
            foreach (PorNInfo p in CProInfo.ListPN) //20161128 替换上面的4条语句
            {
                if (p.PorNname != "" && p.PorNname != null)
                {
                    strbud.Append(p.PorNname + "|");
                }
            }
            foreach (StandardInfo lt in CProInfo.ListStds)
                strbud.Append(lt.LiqName + "|");
            //foreach (UserLiq lq in CProInfo.ListUserLiqS)
            //    strbud.Append(lq.LiqName + "/" + lq.DirectDilutionValue + "/" + lq.DilutionBei + "|");
            if (strbud.Length > 0)
            {
                strbud.Length -= 1;
                return strbud.ToString();
            }
            else
                return string.Empty;
        }
    }
    /// <summary>
    /// 判定步骤信息是否相同，主要用于判定可否拼板
    /// </summary>
    public class ProMethodEquals
    {
        /// <summary>
        /// 判定是否可以拼板
        /// </summary>
        /// <param name="m1">项目1</param>
        /// <param name="m2">项目2</param>
        /// <returns>返回true，则可以拼板，否则不可</returns>
        public bool AtOnePlate(ProMethod m1, ProMethod m2)
        {
            List<Steps> s1 = m1.CMethodSteps.ListMethod;
            List<Steps> s2 = m2.CMethodSteps.ListMethod;
            if (s1.Count != s2.Count) return false;
            for (int i = 0; i < s1.Count; i++)
            {
                if (s1[i].StepType != s2[i].StepType) return false;
                switch (s1[i].StepType)
                {//以温育步骤为对象进行比对
                    case ExperimentStep.Incubation:
                        Incubation in1 = (Incubation)s1[i].ObjStep;
                        Incubation in2 = (Incubation)s2[i].ObjStep;
                        if (in1 == null || in2 == null) return false;
                        if (!(in1.IncubationTemperature == in2.IncubationTemperature && in1.IncubationTime == in2.IncubationTime && in1.shockTime == in2.shockTime))
                            return false;
                        break;
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 项目基本信息类
    /// </summary>
    [Serializable]
    public class ProInfo
    {
        public string XiShiName { get; set; }
        public string OriginLiquid { get; set; }//使用倍比稀释的时候自动生成的母液的名称。thhAdd 2017.12.05//originLiquid
        private string _methodName = "";
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }
        public bool StdStepDil { get; set; }//是否选择标准品分步稀释
        public double StdStep1 { get; set; }
        public double StdStep2 { get; set; }
        public DateTime Period { get; set; }
        public string Manufacturer { get; set; }
        public string BatchNo { get; set; }
        /// <summary>
        /// 项目性质，1为定性，2为半定量，3为定量
        /// </summary>
        public string ResponseProperties { get; set; }

        public ResultFormat ResultFormat { get; set; }

        public string ProName { get; set; }

        public string ProEName { get; set; }

        public double LowValue { get; set; }

        public double HiValue { get; set; }

        public string Reference { get; set; }

        public string Unit { get; set; }

        public string LowExpre { get; set; }

        public string HiExpre { get; set; }

        public string NormalExpre { get; set; }

        public string ClinicalExpre { get; set; }

        public string NName { get; set; }
        public string PName { get; set; }
        public int NNum { get; set; }
        public int PNum { get; set; }
        public int NVol { get; set; }
        public int PVol { get; set; }
        public string P2Name { get; set; }
        public int P2Num { get; set; }
        public int P2Vol { get; set; }
        public int BlankNum { get; set; }

        public double NPDirectDilutionValue { get; set; }
        public int NPDilutionBei { get; set; }
        public double StdDirectDilutionValue { get; set; }//标准品直接稀释值
        public int StdDilutionBei { get; set; }//标准品倍数稀释倍数//此处dilution的值现在在软件中代表母液浓度 kcl 2019.4.24
        public int DilutionBei { get; set; }//倍比稀释倍数  thhAdd 2017.12.04
        public double QcDirectDilutionValue { get; set; }
        public int QcDilutionBei { get; set; }
        public bool StdBlank { get; set; }
        public bool cleanzhen { get; set; }

        public bool cleanzhenNTimes { get; set; }//洗针液多次洗针防交叉污染
        public bool xishi { get; set; }//是否倍比稀释勾选框  thhAdd 2017.12.04
        public bool AbsorbanceDivisionS0 { get; set; }
        public bool MultiplySampleDilution { get; set; }
        public string dgPorNName { get; set; }//参照品名称
        public string dgStdsName { get; set; }//标准品名称
        /// <summary>
        /// 复孔个数
        /// </summary>
        public int RepeatNumber { get; set; }
        public bool Compete { get; set; }//是否为竞争法 默认false
        public bool ParallelAdd { get; set; }//是否可平行加样，与竞争法相反
        public List<StandardInfo> ListStds = new List<StandardInfo>();
        public List<OriginLiquidInfo> ListOrLiq = new List<OriginLiquidInfo>();//addkcl 2019.4.4
        //public List<UserLiq> ListUserLiqS = new List<UserLiq>();
        public List<PorNInfo> ListPN = new List<PorNInfo>();
    }
    //[Serializable]
    //public class UserLiq
    //{
    //    public string LiqName { get; set; }
    //    public double DirectDilutionValue { get; set; }
    //    public int DilutionBei { get; set; }
    //    public int SamplePos { get; set; }
    //}
    /// <summary>
    /// 标准品信息类
    /// </summary>
    [Serializable]
    public class StandardInfo
    {
        public string LiqName { get; set; }
        public string LiqValue { get; set; }
        public int Num { get; set; }
        public int Vol { get; set; }
    }
    /// <summary>
    /// 参照品信息类
    /// </summary>
    [Serializable]
    public class PorNInfo
    {
        public string PorNname { get; set; }
        public int PorNnum { get; set; }
        public int PorNvol { get; set; }
    }
    /// <summary>
    /// 母液信息类 addkcl 2019.4.4
    /// </summary>
    [Serializable]
    public class OriginLiquidInfo
    {
        public string OrLiqName { get; set; }
        public int OrLiqNum { get; set; }
        public int OrLiqVol { get; set; }
    }
    /// <summary>
    /// 项目性质枚举
    /// </summary>
    public enum ReactionType
    {
        /// <summary>
        /// 定量
        /// </summary>
        Quantitative,
        /// <summary>
        /// 定性
        /// </summary>
        qualitative,
        /// <summary>
        /// 半定量
        /// </summary>
        Semi_quantitative
    }
    public enum ResultFormat
    {
        General,
        G1,
        G2,
        G3
    }
    /// <summary>
    /// 项目步骤类
    /// </summary>
    [Serializable]
    public class MethodSteps
    {
        /// <summary>
        /// 步骤列表
        /// </summary>
        List<Steps> listMethod = new List<Steps>();
        //private Hashtable ht = new Hashtable();
        /// <summary>
        /// 步骤列表
        /// </summary>
        public List<Steps> ListMethod
        {
            get { return listMethod; }
            set { listMethod = value; }
        }
        public MethodSteps()
        {

        }
        /// <summary>
        /// 构造函数，实例化对象
        /// </summary>
        /// <param name="flag"></param>
        public MethodSteps(bool flag)
        {
            if (!flag) return;
            //listMethod.Add(new Steps("1", LanguageManager.Instance.getLocaltionStr("Word-AddSample"), null, true) { StepType = ExperimentStep.AddSample });//加样
            //listMethod.Add(new Steps("2", LanguageManager.Instance.getLocaltionStr("Word-Incubate"), null, true) { StepType = ExperimentStep.Incubation });
            //listMethod.Add(new Steps("3", LanguageManager.Instance.getLocaltionStr("Word-CleanPlate"), null, true) { StepType = ExperimentStep.ClearnPlate });
            //listMethod.Add(new Steps("4", LanguageManager.Instance.getLocaltionStr("Word-AddEnzyme"), null, true) { StepType = ExperimentStep.AddEnzyme });//加酶
            //listMethod.Add(new Steps("5", LanguageManager.Instance.getLocaltionStr("Word-Incubate"), null, true) { StepType = ExperimentStep.Incubation });
            //listMethod.Add(new Steps("6", LanguageManager.Instance.getLocaltionStr("Word-CleanPlate"), null, true) { StepType = ExperimentStep.ClearnPlate });
            //listMethod.Add(new Steps("7", LanguageManager.Instance.getLocaltionStr("Word-Substrate"), null, true) { StepType = ExperimentStep.AddSubstrate });//加底物
            //listMethod.Add(new Steps("8", LanguageManager.Instance.getLocaltionStr("Word-Incubate"), null, true) { StepType = ExperimentStep.Incubation });
            //listMethod.Add(new Steps("9", LanguageManager.Instance.getLocaltionStr("Word-StopSolution"), null, true) { StepType = ExperimentStep.AddStopping });//终止液
            //listMethod.Add(new Steps("10", LanguageManager.Instance.getLocaltionStr("Word-Read"), null, true) { StepType = ExperimentStep.Read });
        }
        /// <summary>
        /// 无用，保留
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Next(int i)
        {
            for (int j = i; j < ListMethod.Count; j++)
                if (((StepBase)ListMethod[j].ObjStep).IsNeedTreat)
                    return j;
                else
                    continue;
            return 11;
        }
    }
    /// <summary>
    /// 步骤类型枚举
    /// </summary>
    public enum ExperimentStep
    {
        AddSample,
        Incubation,
        AddEnzyme,
        ClearnPlate,
        SendClean,
        BackClean,
        Read,
        SendRead,
        BackRead,
        MovePlate,
        Shock,
        AddSubstrate,
        AddStopping,
        AddDilution,
        AddOriginLiquid,//add kcl
        ClampingJaw
    }
    /// <summary>
    /// 步骤信息类
    /// </summary>
    [Serializable]
    public class Steps : IXmlSerializable
    {
        public string Num { get; set; }
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepsName { get; set; }
        /// <summary>
        /// 步骤类型
        /// </summary>
        public ExperimentStep StepType { get; set; }
        public StepBase ObjStep { get; set; }
        /// <summary>
        /// 是否执行该命令 zx20180606 Add
        /// </summary>
        public bool IsExecute { get; set; }

        public Steps()
        {

        }

        public Steps(string strNum, string strName, StepBase obj, bool boolExecute)
        {
            Num = strNum;
            StepsName = strName;
            ObjStep = obj;
            IsExecute = boolExecute;  //是否执行该命令 zx20180606 Add
        }
        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            Num = reader.ReadElementString();
            StepsName = reader.ReadElementString();
            //判断该步骤是否执行 Add zx20180606
            IsExecute = true;
            string boolisExecute = reader.ReadElementString();
            if (boolisExecute == "False")
                IsExecute = false;
            //修改前的xml文件可能没有IsExecute属性，没有的话就跳过默认为true
            string objstepType = boolisExecute;
            if (boolisExecute == "True" || boolisExecute == "False")
            {
                objstepType = reader.ReadElementString();
            }
            //endAdd zx20180606
            StepType = (ExperimentStep)Enum.Parse(typeof(ExperimentStep), objstepType);
            objstepType = reader.ReadElementString();
            XmlSerializer keySerializer = null;
            switch (objstepType.Substring(objstepType.LastIndexOf('.') + 1))
            {
                case "AddSample":
                    keySerializer = new XmlSerializer(typeof(AddSample));
                    ObjStep = (AddSample)keySerializer.Deserialize(reader);
                    break;
                case "Suction_Distribution":
                    keySerializer = new XmlSerializer(typeof(Suction_Distribution));
                    ObjStep = (Suction_Distribution)keySerializer.Deserialize(reader);
                    break;
                case "Incubation":
                    keySerializer = new XmlSerializer(typeof(Incubation));
                    ObjStep = (Incubation)keySerializer.Deserialize(reader);
                    break;
                case "ReadWave":
                    keySerializer = new XmlSerializer(typeof(ReadWave));
                    ObjStep = (ReadWave)keySerializer.Deserialize(reader);
                    break;
                case "ClearnPlate":
                    //ObjStep = getClearnPlateByName(reader.ReadElementString());
                    keySerializer = new XmlSerializer(typeof(ClearnPlate));
                    ObjStep = (ClearnPlate)keySerializer.Deserialize(reader);
                    break;
            }
            reader.ReadEndElement();
        }
        /// <summary>
        /// 获取洗板步骤（无用，保留）
        /// </summary>
        /// <param name="clearnName"></param>
        /// <returns></returns>
        private ClearnPlate getClearnPlateByName(string clearnName)
        {
            if (string.IsNullOrEmpty(clearnName)) return null;
            //BLL.WashInfo bllWasher = new BLL.WashInfo();
            //Model.WashInfo mdWasher = bllWasher.GetModel(new Guid(DBUtility.DbHelperSQL.GetSingle("select washid from WashInfo where washProName='" + clearnName + "'").ToString()));
            ClearnPlate cp = new ClearnPlate();
            //cp.ClearnPlateName = mdWasher.washProName;
            ////cp.Rows = mdWasher.Rows;
            //cp.startCol = (int)mdWasher.startCol;
            //cp.endCol = (int)mdWasher.endCol;
            //cp.ClearnTimes = (int)mdWasher.CleanTimes;
            //cp.WasherVolume = (int)mdWasher.Injectvol;
            //cp.SoakTime = (int)mdWasher.Soaktime;
            //cp.ClearnType = (int)mdWasher.WashType;
            //cp.VibrationWay = mdWasher.VibrationWay;
            //cp.SprayHeigth = mdWasher.SprayHeight;
            //cp.SuctionHeigth = mdWasher.SuctionHeight;
            //cp.AdjustmentPos = mdWasher.AdjustmentPos;
            //cp.MidPos = mdWasher.MidPos;
            //cp.LastSuctionTime = mdWasher.LastSuctionTime;
            //cp.Spacing = mdWasher.Spacing;

            return cp;
        }
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Num", Num);
            writer.WriteElementString("StepsName", StepsName);

            //写入该步骤是否执行 Add zx20180606
            writer.WriteElementString("IsExecute", IsExecute.ToString());
            //endAdd zx20180606

            writer.WriteElementString("StepType", StepType.ToString());
            if (ObjStep == null)
                writer.WriteElementString("ObjStep_ObjStep", "null");
            else
            {
                writer.WriteElementString("ObjStep_ObjStep", ObjStep.GetType().ToString());
                XmlSerializer keySerializer = null;
                XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
                xmlns.Add(string.Empty, string.Empty);
                //if (ObjStep is ClearnPlate)
                //{
                //    //keySerializer = new XmlSerializer(typeof(ClearnPlate));
                //    writer.WriteElementString("ClearnPlate_Name", ((ClearnPlate)ObjStep).ClearnPlateName);
                //}
                //else
                keySerializer = new XmlSerializer(ObjStep.GetType());
                if (keySerializer != null)
                    keySerializer.Serialize(writer, ObjStep, xmlns);
            }
        }
    }
    /// <summary>
    /// 步骤基类
    /// </summary>
    [Serializable]
    public abstract class StepBase
    {
        /// <summary>
        /// 是否需要处理
        /// </summary>
        public virtual bool IsNeedTreat
        {
            get { return NeedTreat(); }
        }

        public virtual bool NeedTreat()
        {
            return false;
        }
    }
    /// <summary>
    /// 孵育
    /// </summary>
    [Serializable]
    public class Incubation : StepBase
    {
        /// <summary>
        /// 孵育温度
        /// </summary>
        public double IncubationTemperature { get; set; }
        /// <summary>
        /// 孵育时间
        /// </summary>
        public double IncubationTime { get; set; }
        /// <summary>
        /// 震荡等级
        /// </summary>
        public int shockLevel { get; set; }
        /// <summary>
        /// 震荡时间
        /// </summary>
        public double shockTime { get; set; }
        /// <summary>
        /// 是否盖盖
        /// </summary>
        public bool isgai { get; set; }
        /// <summary>
        /// 操作顺序0先孵育在震荡1先震荡在孵育2先开孵育再开震荡同时
        /// </summary>
        public int sort { get; set; }
        public int runModel { get; set; }
        public int runTime { get; set; }
        public override string ToString()
        {
            return MoranControl.MoranLanguage.ProMethod_Tips2 + IncubationTime.ToString();
        }
        /// <summary>
        /// 是否需要处理
        /// </summary>
        /// <returns></returns>
        public override bool NeedTreat()
        {
            if ((IncubationTemperature == IncubationTime) && (IncubationTime == shockTime) && (shockTime == 0))
                return false;
            return true;
        }
    }
    /// <summary>
    /// 读数
    /// </summary>
    [Serializable]
    public class ReadWave : StepBase
    {
        /// <summary>
        /// 主波长
        /// </summary>
        public int MainWave { get; set; }
        /// <summary>
        /// 辅波长
        /// </summary>
        public int LessWave { get; set; }
        public int runTime { get; set; }
        public int runModel { get; set; }
        public ReadWave()
        {
            MainWave = 1;
        }
       
        /// <summary>
        /// 是否需要处理
        /// </summary>
        /// <returns></returns>
        public override bool NeedTreat()
        {
            if ((LessWave == MainWave) && (MainWave == 0))
                return false;
            return true;
        }
    }
    /// <summary>
    /// 加样步骤类
    /// </summary>
    [Serializable]
    public class AddSample : StepBase
    {
        public double Volumn { get; set; }
        public string BlankLiquidName { get; set; }
        public double DirectDilutionValue { get; set; }
        public int DilutionBei { get; set; }//是否选择样本分步稀释 kcl add 2019.11.5
        public bool SamStepDil { get; set; }
        public double SampleStep1 { get; set; }
        public double SampleStep2 { get; set; }
        public bool BlankNoSample { get; set; }
        public bool CleanZhen { get; set; }

        public bool CleanZhenNTimes { get; set; }//2021.9.14 增加HIV类型需多次洗针的项目
        public int runModel { get; set; }
        public int runTime { get; set; }
        public LiqParam samLiq { get; set; }
        public string SamCupType { get; set; }//样本管类型，真空采血管/1.5ml离心管
        public override bool NeedTreat()
        {
            return true;
        }
        public override string ToString()
        {
            return MoranControl.MoranLanguage.ProMethod_Tips3;
        }
        public double DilutionCal
        {
            get
            {
                if (DilutionBei > 1)
                {
                    return DilutionBei;
                }
                return 1;
            }
        }

        public bool AddToBlank { get; set; }//空白孔是否添加稀释液
    }
    public class LiqParam
    {
        public int id { get; set; }
        public int liquidindex { get; set; }
        public string liquidname { get; set; }
        public DateTime liquidaddtime { get; set; }
        public int tiptype { get; set; }
        public int xydelaytime { get; set; }
        public int xystartspeed { get; set; }
        public int xyendspeed { get; set; }
        public int xysuckspeed { get; set; }
        public int xyacceleration { get; set; }
        public int xymaxspeed { get; set; }
        public int xyneedletipair { get; set; }
        public int xyneedtailair { get; set; }
        public int xyfollowspeed { get; set; }
        public int xyspitvolume { get; set; }
        public int xyvolume { get; set; }
        public int xysurplusvolume { get; set; }
        public int zyacceleration { get; set; }
        public int zymaxspeed { get; set; }
        public int zyendspeed { get; set; }
        public int zyfollowspeed { get; set; }
        public int zysuckvolume { get; set; }
        public int zydelaytime { get; set; }
        public int zyvolume { get; set; }
    }
    /// <summary>
    /// 分配液体类
    /// </summary>
    [Serializable]
    public class Suction_Distribution : StepBase
    {
        private List<SuctionInfo> listSuctionInfo = new List<SuctionInfo>();
        /// <summary>
        /// 是否回吐
        /// </summary>
        public bool ToTak { get; set; }
        /// <summary>
        /// 是否分配到空白孔
        /// </summary>
        public bool AddToBlank { get; set; }
        /// <summary>
        /// 分配速度
        /// </summary>
        public int DisSpeed { get; set; }
        /// <summary>
        /// 试剂类型
        /// </summary>
        public LiqParam LiqParam { get; set; }
        /// <summary>
        /// 分配孔位选择,0:全部  1：仅样本  2 仅参照品（N、P） 3 仅STD  4 仅QC
        /// </summary>
        public int DisHole { get; set; }
        public int runModel { get; set; }
        public int runTime { get; set; }
        public Suction_Distribution()
        {

        }

        public List<SuctionInfo> ListSuctionInfo
        {
            get { return listSuctionInfo; }
            set { listSuctionInfo = value; }
        }

        

        public override string ToString()
        {
            StringBuilder strbud = new StringBuilder();
            foreach (SuctionInfo suc in ListSuctionInfo)
            {
                strbud.Append(MoranControl.MoranLanguage.ProMethod_Tips4 + suc.LiquidName + " ");// "到:" + suc.HolePosition +
            }
            return strbud.ToString();
        }
        public override bool NeedTreat()
        {
            return ListSuctionInfo.Count != 0;
        }
    }
    /// <summary>
    /// 液体信息类
    /// </summary>
    [Serializable]
    public class SuctionInfo
    {
        public string LiquidName { get; set; }

        public string HolePosition { get; set; }

        public int DilutionRatio { get; set; }

        public bool IsDilution { get; set; }

        public bool Probe { get; set; }

        public int DistributionHeight { get; set; }

        public int SuctionHeight { get; set; }

        public int Volume { get; set; }
        public int Site { get; set; }
    }
    /// <summary>
    /// 洗板
    /// </summary>
    [Serializable]
    public class ClearnPlate : StepBase
    {
        public int washid { get; set; }
        public int startCol { get; set; }
        public int endCol { get; set; }
        public string ClearnPlateName { get; set; }
        public int Rows { get; set; }
        public int ClearnTimes { get; set; }
        public int WasherVolume { get; set; }//吸液量
        public int SoakTime { get; set; }
        public int ClearnType { get; set; }
        public int VibrationWay { get; set; }
        public int SprayHeigth { get; set; }
        public int SuctionHeigth { get; set; }
        public int AdjustmentPos { get; set; }
        public int MidPos { get; set; }
        public int Spacing { get; set; }
        public int LastSuctionTime { get; set; }
        public int WashBottle { get; set; }
        public string WasherFluid { get; set; }
        public int Strength { get; set; }
        public bool BottomWash { get; set; }
        public int DryTimes { get; set; }//add kcl 吸干次数
        public int PumpTime { get; set; }//抽液时间
        public int runTime { get; set; }
        public int runModel { get; set; }
        public override bool NeedTreat()
        {
            return ClearnTimes > 0;
            //return !string.IsNullOrEmpty(ClearnPlateName);
        }
    }
    /// <summary>
    /// 读数规则
    /// </summary>
    [Serializable]
    public class ReadRule
    {
        int _blanckCalType;

        /// <summary>
        /// 计算方式
        /// </summary>
        public int BlanckCalType
        {
            get { return _blanckCalType; }
            set { _blanckCalType = value; }
        }
        List<DelSetting> listDel = new List<DelSetting>();

        public List<DelSetting> ListDel
        {
            get { return listDel; }
            set { listDel = value; }
        }
        List<ConditionsSetting> listConditions = new List<ConditionsSetting>();

        public List<ConditionsSetting> ListConditions
        {
            get { return listConditions; }
            set { listConditions = value; }
        }
    }
    [Serializable]
    public class ConditionsSetting
    {
        string _valueStr;

        public string ValueStr
        {
            get { return _valueStr; }
            set { _valueStr = value; }
        }
        public override string ToString()
        {
            return string.IsNullOrEmpty(ValueStr) ? "" : ValueStr;
        }
    }
    [Serializable]
    public class DelSetting
    {
        string _HoleName;

        public string HoleName
        {
            get { return _HoleName; }
            set { _HoleName = value; }
        }
        int _CalIndex;

        public int CalIndex
        {
            get { return _CalIndex; }
            set { _CalIndex = value; }
        }
        decimal _par1;

        public decimal Par1
        {
            get { return _par1; }
            set { _par1 = value; }
        }
        decimal _par2;

        public decimal Par2
        {
            get { return _par2; }
            set { _par2 = value; }
        }
        int _AtLeastNum;

        public int AtLeastNum
        {
            get { return _AtLeastNum; }
            set { _AtLeastNum = value; }
        }
     
        public string Formula
        {
            get
            {
                switch (CalIndex)
                {
                    case 0:
                        return "(" + Par1 + "<[" + HoleName + "])&&([" + HoleName + "]<" + Par2 + ")";
                    case 1:
                        return "((Avg(" + HoleName + ")-" + Par1 + ")<[" + HoleName + "])&&([" + HoleName + "]<(Avg(" + HoleName + ")+" + Par2 + "))";
                    case 2:
                        return "((Avg(" + HoleName + ")*" + Par1 + ")<[" + HoleName + "])&&([" + HoleName + "]<(Avg(" + HoleName + ")*" + Par2 + "))";
                    case 3:
                    case 4:
                        return "(Max(" + HoleName + ")-Min(" + HoleName + "))<" + Par1;
                }
                return string.Empty;
            }
        }
    }
    /// <summary>
    /// Cutoff值设定
    /// </summary>
    [Serializable]
    public class CUTFFQC
    {
        public double Negative { get; set; }
        public double LowCutOff { get; set; }
        public bool ValueAbsorbance { get; set; }

        public string CutOff { get; set; }
        public string NegativeExpre { get; set; }
        public string PositiveExpre { get; set; }
        public string PositiveExpreL { get; set; }
        public string PositiveExpreH { get; set; }
        public string GrayZone { get; set; }
        public double ULimit { get; set; }
        public double DLimit { get; set; }
        public string QCValue1 { get; set; }
        public string QCValue2 { get; set; }
        public string QCValue3 { get; set; }
        public double PBei { get; set; }
        public double PBei2 { get; set; }
        //public bool QcXiShi { get; set; }
        public List<string> listQc = new List<string>();
    }
}
