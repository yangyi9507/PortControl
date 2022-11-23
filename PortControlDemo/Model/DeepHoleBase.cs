using System;
using System.Text;
using System.Collections.Generic;
namespace PortControlDemo.Model
{
    public class DeepHoleBase
    {

        /// <summary>
        /// 孔位ID
        /// </summary>		
        private int _holeid;
        public int HOLEID
        {
            get { return _holeid; }
            set { _holeid = value; }
        }
        /// <summary>
        /// 孔位名称
        /// </summary>		
        private string _holename;
        public string HOLENAME
        {
            get { return _holename; }
            set { _holename = value; }
        }
        /// <summary>
        /// 空位坐标
        /// </summary>		
        private string _holexy;
        public string HOLEXY
        {
            get { return _holexy; }
            set { _holexy = value; }
        }
        /// <summary>
        /// 荧光坐标点
        /// </summary>		
        private string _deepxy;
        public string DEEPXY
        {
            get { return _deepxy; }
            set { _deepxy = value; }
        }

    }
}

