using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoranControl
{
    class AchieveFourRackSetting
    {
    }
    [Serializable]
    public class RackSeting
    {
        public string IDName { get; set; }
        public int Sum { get; set; }
        public int IndexPostion { get; set; }
        public int ModeType { get; set; }
        public KeyValueList Cells { get; set; }
        public virtual KeyValueList NewCells()
        {
            return null;
        }
    }
    [Serializable]
    public class KeyValueList : List<KeyValue>
    {

    }

    public class KeyValue
    {
    }
}
