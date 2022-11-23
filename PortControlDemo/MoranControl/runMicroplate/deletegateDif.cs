using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ComponentModel;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 按钮点击委托，用于左键点击
    /// </summary>
    public delegate void CellClick(object sender, EventArgs e);
    /// <summary>
    /// 按钮点击委托，用于右键点击
    /// </summary>
    public delegate double CellRClick(double dValue);

    /// <summary>
    /// 继承自List《KeyValue》
    /// </summary>
    [Serializable]
    public class KeyValueList : List<KeyValue>
    {

    }
    [Serializable]
    public class KeyValue //: ISerializable
    {
        public KeyValue():this(0,string.Empty,string.Empty,0,0,0, string.Empty, string.Empty,0)
        {

        }
        /// <summary>
        /// KeyValue构造函数
        /// </summary>
        /// <param name="vKey">板位标识</param>
        /// <param name="vValue">试剂名</param>
        /// <param name="vDiameter">直径</param>
        /// <param name="vRow">行</param>
        /// <param name="vCol">列</param>
        public KeyValue(int vNo,string vKey, string vValue, double vDiameter,int vRow,int vCol,string vLiqName,string vPlateType, int vSiteNum)//指令、试剂名、直径
        {
            idCode = vNo;
            Key = vKey;
            Value = vValue;
            Diameter = vDiameter;            
            Row = vRow;
            Col = vCol;
            LiqName = vLiqName;
            PlateType = vPlateType;
            siteNum = vSiteNum;
        }
        //[NotifyParentProperty(true)]
        /// <summary>
        /// 板架标识：Qc,Reagent,ReagentManger试剂槽.Deep深孔板
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 试剂名称
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 试剂瓶直径
        /// </summary>
        public double Diameter { get; set; }
        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public int Col { get; set; }
        /// <summary>
        /// 第几个
        /// </summary>
        public int idCode { get; set; }//第几个
        /// <summary>
        /// 该孔的液体名称
        /// </summary>
        public string LiqName { get; set; }//该孔的液体名称
        /// <summary>
        /// 板架类型QQC Reagent等
        /// </summary>
        public string PlateType { get; set; }//板架类型
        public int siteNum { get; set; }  //位置编号 底部圆点
    }
}
