using ElContros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 微孔板信息类
    /// </summary>
    [Serializable]
    public class MicroplateSeting : RackSetting, ICloneable, IComparable
    {
        public PlateLayout _Layout;

        public PlateLayout MLayout//板布局
        {
            get { return _Layout; }
            set { _Layout = value; }
        }
        public MicroplateRackSeting RackSetting;
        public int SampleStartNum { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProName { get; set; }
        public int SampleUsedNum { get; set; }
        /// <summary>
        /// 孔间横间距
        /// </summary>
        public double CenterDistanceH { get; set; }
        /// <summary>
        /// 孔间纵间距
        /// </summary>
        public double CenterDistanceV { get; set; }
        /// <summary>
        /// X轴上横向数
        /// </summary>
        public int HorizontalNum { get; set; }
        /// <summary>
        /// Y轴上纵向数
        /// </summary>
        public int VerticalNum { get; set; }
        /// <summary>
        /// 是否为微孔板
        /// </summary>
        public bool isMic { get; set; }

        OrderType _plateOrderType = OrderType.TDLR;
        /// <summary>
        /// 微孔板号
        /// </summary>
        public int TestPlateNo { get; set; }

        public MicroplateSeting()
        {
            VerticalNum = 8;
            HorizontalNum = 12;

            TopPad = LeftPad = CenterDistanceH = CenterDistanceV = 5;
            Width = 100;
            Height = 50;
        }

        public OrderType PlateOrderType
        {
            get { return _plateOrderType; }
            set { _plateOrderType = value; }
        }



        #region ICloneable 成员

        public object Clone()
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bf.Serialize(ms, this);
            ms.Position = 0;
            return (bf.Deserialize(ms));
        }

        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            MicroplateSeting c1 = this;
            MicroplateSeting c2 = (MicroplateSeting)obj;

            if (c1.Index > c2.Index)
                return 1;
            if (c1.Index < c2.Index)
                return -1;
            return 0;
        }

        #endregion

        #region 孔参数
        public double Cell_Height { get; set; }
        public double Cell_Radius { get; set; }
        #endregion
    }

    public enum OrderType
    {
        LRTD,
        LRDT,
        TDLR,
        DTLR
    }
}
