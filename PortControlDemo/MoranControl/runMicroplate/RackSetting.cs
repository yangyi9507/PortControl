using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 孔架信息设置类
    /// </summary>
    [Serializable]
    public class RackSetting : DevicesComponentParent
    {
        /// <summary>
        /// 跨度，微孔板纵向跨度，12
        /// </summary>
        public int Span { get; set; }

        /// <summary>
        /// 两点间距，间距一般默认为9
        /// </summary>
        public double BetweenDistance { get; set; }
        /// <summary>
        /// 数据库ID
        /// </summary>
        public string DBID { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public RackSetting()
        {
            XNum = 1;
        }

        /// <summary>
        /// X向数量，一般默认为12
        /// </summary>
        public int XNum { get; set; }
        /// <summary>
        /// Y向数量，一般默认为8
        /// </summary>
        public int YNum { get; set; }
    }

    public class rackLayout
    {
        public string Key { get; set; }
        public string Name{ get; set; }
        //public string btnKey { get; set; }
    }
}
