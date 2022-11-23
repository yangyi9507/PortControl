
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PortControlDemo.MoranControl.runMicroplate
{
    class ReagentSetting
    {
    }
    [Serializable]
    public class DeepHole : RackSeting
    {       
        public DeepHole()
        {
            Sum = 12;
            Holes = 96;
        }
        public override KeyValueList NewCells()
        {
           // XmlDocument objXmlDoc = new XmlDocument();
           // objXmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\InstrumentFourConfig.xml");
           // XmlNode objRootNode = objXmlDoc.SelectSingleNode("//Run/Diameter/SampleDia");
           // double gqc = double.Parse(objRootNode.InnerText);//试剂直径值
           // objXmlDoc = null;
            KeyValueList cells = new KeyValueList();
            for (int i = 0; i < Holes; i++)
            {
                cells.Add(new KeyValue(i, "Deep", string.Empty, 9,i%8,i/8, string.Empty, "Deep", 0));
            }
            return cells;
        }
    }
    [Serializable]
    public class SampleRackSeting : RackSeting
    {       
        public SampleRackSeting()
        {
            Sum = 1;
            Holes = 192;
        }
        public override KeyValueList NewCells()
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\InstrumentFourConfig.xml");
            XmlNode objRootNode = objXmlDoc.SelectSingleNode("//Run/Diameter/SampleDia");
            double gqc = double.Parse(objRootNode.InnerText);//试剂直径值
            objXmlDoc = null;
            KeyValueList cells = new KeyValueList();
            for (int i = 0; i < Holes; i++)
            {
                cells.Add(new KeyValue(i, "Sample", string.Empty, gqc, i % 16, i / 16, string.Empty, "Sample", 0));
            }
            return cells;
        }
    }
    [Serializable]
    public class ReagentSeting : RackSeting
    {
        public ReagentSeting()
        {
            Sum = 1;
            Holes = 12;
        }
        public override KeyValueList NewCells()
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\InstrumentFourConfig.xml");
            XmlNode objRootNode = objXmlDoc.SelectSingleNode("//Run/Diameter/ReagDia");
            double gqc = double.Parse(objRootNode.InnerText);           
            objXmlDoc = null;
            KeyValueList cells = new KeyValueList();
            for (int i = 0; i < Holes; i++)
            {
                cells.Add(new KeyValue(i, "Reagent", string.Empty, gqc, i%3, i/3, string.Empty, "Reagent" ,0));
            }
            return cells;
        }
    }
    [Serializable]
    public class QcRackSeting : RackSeting
    {
        public QcRackSeting()
        {
            ModeType = 0;
            Sum = 1;
            Holes = 40;
        }
        public override KeyValueList NewCells()
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\InstrumentFourConfig.xml");
            XmlNode objRootNode = objXmlDoc.SelectSingleNode("//Run/Diameter/QCDia");
            double gqc = double.Parse(objRootNode.InnerText);
            objXmlDoc = null;
            KeyValueList cells = new KeyValueList();
            for (int i = 0; i < Holes; i++)
            {
                cells.Add(new KeyValue(i,"Qc" , string.Empty, gqc,i%5,i/5, string.Empty, "Qc",0));
            }
            return cells;
        }
    }
    [Serializable]
    public class Reagent1Seting : RackSeting
    {
        public Reagent1Seting()
        {
            Sum = 1;
            Holes = 7;
        }
        public override KeyValueList NewCells()
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\InstrumentFourConfig.xml");
            XmlNode objRootNode = objXmlDoc.SelectSingleNode("//Run/Diameter/ReagentDia");
            double gqc = double.Parse(objRootNode.InnerText.Split('|')[0]) * double.Parse(objRootNode.InnerText.Split('|')[1]);
            objXmlDoc = null;
            KeyValueList cells = new KeyValueList();
            for (int i = 0; i < Holes; i++)
            {
                cells.Add(new KeyValue(i, "ReagentManger", string.Empty, gqc, 0, i, string.Empty, "ReagentManger",0));
            }
            return cells;
        }
    }
    [Serializable]
    public class RackSeting
    {
        public string IDName { get; set; }
        public int Sum { get; set; }
        public int Holes { get; set; }
        public int IndexPostion { get; set; }
        public int ModeType { get; set; }
       
        public KeyValueList Cells { get; set; }
        public int idCode { get; set; }//第几个
        public int Row { get; set; }//行
        public int Col { get; set; }//列
        public string LiqName { get; set; }//该孔的液体名称
        public string PlateType { get; set; }//板架类型
        public virtual KeyValueList NewCells()
        {
            return null;
        }

    }

}
