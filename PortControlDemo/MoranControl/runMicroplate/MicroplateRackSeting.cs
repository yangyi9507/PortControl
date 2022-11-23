using System;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 微孔板孔架信息设置类
    /// </summary>
    [Serializable]
    public class MicroplateRackSeting : RackSetting, ICloneable, IComparable
    {
        /// <summary>
        /// 构造函数，初始化参数值
        /// </summary>
        public MicroplateRackSeting()
        {
            TopPad = 20;
            LeftPad = 10;
            BetweenDistance = 15;
        }

        #region ICloneable 成员
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>返回克隆对象</returns>
        public object Clone()
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bf.Serialize(ms, this);
            ms.Position = 0;
            return (bf.Deserialize(ms));
        }

        #endregion
        /// <summary>
        /// MicroplateRackSeting对象比较方法
        /// </summary>
        /// <param name="obj">MicroplateRackSeting对象</param>
        /// <returns>索引Index大于obj.Index,则返回1，小于则返回-1，相等返回0</returns>
        int IComparable.CompareTo(object obj)
        {
            MicroplateRackSeting c1 = this;
            MicroplateRackSeting c2 = (MicroplateRackSeting)obj;

            if (c1.Index > c2.Index)
                return 1;
            if (c1.Index < c2.Index)
                return -1;
            return 0;
        }
    }
}