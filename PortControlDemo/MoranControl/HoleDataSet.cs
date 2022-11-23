using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 先实现默认从上到下,从左到右
    /// </summary>
    [Serializable]
    public class PlateLayout
    {
        public PlateLayout()
        {
            ListHoleData = new List<HoleData>();
        }
        /// <summary>
        /// 初始化孔信息列表ListHoleData
        /// </summary>
        public void InItDataSet()
        {
            ListHoleData.Clear();
            for (int j = 0; j < ColNum; j++)
                for (int i = 0; i < RowNum; i++)
                {
                    HoleData holeData = new HoleData(i + 1, j + 1);
                    ListHoleData.Add(holeData);
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">从0开始第几行0..7</param>
        /// <param name="j">从0开始第几列0..11</param>
        /// <returns></returns>
        public HoleData getHoleData(int i,int j)
        {
            return ListHoleData[(j) * RowNum + i];
        }
        
        private int _colNum = 12;
        public int ColNum
        {
            get { return _colNum; }
            set { _colNum = value; }
        }

        private int _rowNum = 8;
        public int RowNum
        {
            get { return _rowNum; }
            set { _rowNum = value; }
        }

        public List<HoleData> ListHoleData { get; set; }
        public bool ExitByHoleType(HoleType ty)
        {
            return ListHoleData.Any(data => data.ThisHoleType == ty);
        }
    }
    /// <summary>
    /// 孔类型
    /// </summary>
    [Serializable]
    public enum HoleType   //lxm
    {
        /// <summary>
        /// 空
        /// </summary>
        Null=9,
        /// <summary>
        /// 空白对照
        /// </summary>
        BL=6,
        /// <summary>
        /// 样本
        /// </summary>
        Sample=0,
        /// <summary>
        /// 标准品
        /// </summary>
        Std=4,
        /// <summary>
        /// 对照品P
        /// </summary>
        P=2,
        /// <summary>
        /// 对照品P2
        /// </summary>
        P2=3,
        /// <summary>
        /// 对照品N
        /// </summary>
        N=1,
        /// <summary>
        /// 质控品
        /// </summary>
        Qc=5,
        /// <summary>
        /// 用户自定义
        /// </summary>
        User=7,
        /// <summary>
        /// 样本倍数稀释
        /// </summary>
        SamBsxs = 8,
        /// <summary>
        /// 标准品倍比稀释
        /// </summary>
        StdBsxs = 10
    }
    /// <summary>
    ///孔信息
    /// </summary>
    [Serializable]
    public class HoleData
    {
        public HoleData()
        {

        }
        /// <summary>
        /// 纵向第几个1...12 从1开始
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 横向第几个A...H 从1开始
        /// </summary>
        public int X { get; set; }
        [XmlIgnore]
        public string GroupName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        private string _unit = string.Empty;//"单位"
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// 样本值/标准的值
        /// </summary>
        public double SampleValue { get; set; }

        /// <summary>
        /// 稀释度
        /// </summary>
        private string _concentrationDilution = string.Empty;//"1/1"
        /// <summary>
        /// 稀释度
        /// </summary>
        public string ConcentrationDilution
        {
            get { return _concentrationDilution; }
            set { _concentrationDilution = value; }
        }
        /// <summary>
        /// 孔类型
        /// </summary>
        private HoleType _thisHoleType = HoleType.Null;
        /// <summary>
        /// 孔类型
        /// </summary>
        public HoleType ThisHoleType
        {
            get { return _thisHoleType; }
            set { _thisHoleType = value; }
        }
        [XmlIgnore]
        public string OtherValue { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 样品主键
        /// </summary>
        [XmlIgnore]
        public string Num { get; set; }
        [XmlIgnore]
        public string Name { get; set; }
        /// <summary>
        /// 液体名称//样本编号
        /// </summary>
        public string LiquidName { get; set; }
        public string AfterxishiName { get; set; }
        /// <summary>
        /// 原始吸光度值
        /// </summary>
        public double OD { get; set; }
        /// <summary>
        /// 主波长
        /// </summary>
        public double MainOD { get; set; }
        /// <summary>
        /// 辅波长
        /// </summary>
        public double LessOD { get; set; }
        /// <summary>
        /// 孔位置
        /// </summary>
        /// <param name="x">A-H</param>
        /// <param name="y">1-12</param>
        public HoleData(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            if (ThisHoleType == HoleType.Null) return "";
            return "坐标值:" + ItemName;
        }


        public string ItemName { get; set; }
        public string ItemColor { get; set; }
        public string ItemAttribute { get; set; }
        public string SampleID { get; set; }
        public string SampleColor { get; set; }

        public string SampleName { get; set; }

    }
}
