using System;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 自定义控件基类
    /// </summary>
    [Serializable]
    public class DevicesComponentParent
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 索引从0开始//使用时从1开始了
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 上边距中心位置
        /// </summary>
        public double TopPad { get; set; }
        /// <summary>
        /// 左边距中心位置
        /// </summary>
        public double LeftPad { get; set; }
        /// <summary>
        /// 宽度 长方形竖方向
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public double Lenght { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 底厚度
        /// </summary>
        public double Bottom { get; set; }
    }
}