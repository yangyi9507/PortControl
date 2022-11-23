
using PortControlDemo.MoranControl.runMicroplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PortControlDemo.MoranControl
{
    public class InstrumentLayout
    {
        /// <summary>
        /// 微孔板
        /// </summary>
        public List<MicroplateSeting> listMic { get; set; }
        /// <summary>
        /// 深孔板
        /// </summary>
        public List<DeepHole> listDeepHole { get; set; }
        /// <summary>
        /// 样本架
        /// </summary>
        public List<SampleRackSeting> listSampleRack { get; set; }
        /// <summary>
        /// 质控架
        /// </summary>
        public List<QcRackSeting> listQc { get; set; }
        /// <summary>
        /// 试剂架
        /// </summary>
        public List<ReagentSeting> listReagent { get; set; }
        /// <summary>
        /// 试剂槽
        /// </summary>
        public List<Reagent1Seting> listReaggentC { get; set; }
        /// <summary>
        /// 洗板液
        /// </summary>
        public List<KeyValue> listWasherFluid { get; set; }

        /// <summary>
        /// 布局
        /// </summary>
        public InstrumentLayout()
        {
            listMic = new List<MicroplateSeting>();
            listDeepHole = new List<DeepHole>();
            listSampleRack = new List<SampleRackSeting>();
            listReagent = new List<ReagentSeting>();
            listQc = new List<QcRackSeting>();
            listReaggentC = new List<Reagent1Seting>();
            listWasherFluid = new List<KeyValue>();
        }
        public void NewRs()
        {
            listMic = new List<MicroplateSeting>();
     //       listDeepHole = new List<MicroplateSeting>();
            MicroplateSeting MicSeting;
            for (int i = 1; i < 13; i++)
            {
                MicSeting = new MicroplateSeting();
                MicSeting.BetweenDistance = 9;
                MicSeting.Bottom = 0;
                MicSeting.Cell_Height = 11.9;
                MicSeting.Cell_Radius = 6.3;
                MicSeting.CenterDistanceH = 9;
                MicSeting.CenterDistanceV = 9;
                MicSeting.DBID = Guid.NewGuid().ToString();
                MicSeting.Height = 300;
                MicSeting.HorizontalNum = 12;
                MicSeting.Index = 1;
                MicSeting.LeftPad = 5;
                MicSeting.Lenght = 100;
                MicSeting.Name = "AloneMic";
                MicSeting.PlateOrderType = OrderType.TDLR;
                //MicSeting.ProName = "ProMic";
                MicSeting.SampleStartNum = 1;
                MicSeting.SampleUsedNum = 0;
                MicSeting.Span = 12;
                MicSeting.TopPad = 5;
                MicSeting.VerticalNum = 8;
                MicSeting.Width = 100;
                MicSeting.XNum = 12;
                MicSeting.YNum = 8;
               // MicSeting.isMic = false;
                if (MicSeting.MLayout == null)
                {
                    MicSeting.MLayout = new PlateLayout();
                    MicSeting.MLayout.InItDataSet();
                }
                listMic.Add(MicSeting);
            }
            for (int i = 1; i < 13; i++)
            {
                DeepHole DeepSeting = new DeepHole() { IndexPostion = i };
                DeepSeting.Cells = DeepSeting.NewCells();
                listDeepHole.Add(DeepSeting);
            }
            for (int i = 0; i < 1; i++)
            {
                SampleRackSeting Samplest = new SampleRackSeting() { IndexPostion = i };
                Samplest.Cells = Samplest.NewCells();
                listSampleRack.Add(Samplest);
            }
            for (int i = 0; i < 1; i++)
            {
                QcRackSeting st = new QcRackSeting() { IndexPostion = i };
                st.Cells = st.NewCells();
                listQc.Add(st);
            }
            for (int i = 0; i < 1; i++)
            {
                ReagentSeting st = new ReagentSeting() { IndexPostion = i };
                st.Cells = st.NewCells();
                listReagent.Add(st);
            }
            for (int i = 0; i < 4; i++)
            {
                Reagent1Seting ReagCSeting = new Reagent1Seting() { IndexPostion = 1 };
                ReagCSeting.Cells = ReagCSeting.NewCells();
                listReaggentC.Add(ReagCSeting);
            }

                listWasherFluid = new List<KeyValue>();
            listWasherFluid.Add(new KeyValue() { Key = "0", Value = string.Empty });
            listWasherFluid.Add(new KeyValue() { Key = "1", Value = string.Empty });
            listWasherFluid.Add(new KeyValue() { Key = "2", Value = string.Empty });
            listWasherFluid.Add(new KeyValue() { Key = "3", Value = string.Empty });
            listWasherFluid.Add(new KeyValue() { Key = "4", Value = string.Empty });
        }
        public class RunPos
        {
            public int ChanelNo { get; set; }
            public string strPOs { get; set; }
            public Pos Pos { get; set; }
            public List<Pos> AllPos { get; set; }
            public double Vol { get; set; }
            public int InsPart { get; set; }
            public bool Adjustmented { get; set; }
            public RunPos()
            {
                strPOs = string.Empty;
                Adjustmented = false;
                ChanelNo = -1;
                Pos = new Pos();
                AllPos = new List<Pos>();
            }
            public void AdjustmentPosByChanelNo(int ChanelNo)
            {
                if (Pos.X == -1) return;
                if (strPOs.IndexOf("gh") != -1 || strPOs.IndexOf("gr") != -1)//20161014 lxm 原来为 gd 改为 gh
                    if (!Adjustmented)
                    {
                        Pos.Y += ChanelNo * 13.5;//20161014 lxm 原来为9改为13.5
                    }
            }
        }

        public InstrumentLayout CloneThis()
        {
            BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream();
            Formatter.Serialize(stream, this);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return (InstrumentLayout)clonedObj;
        }
        public int ChannelCanMoves(KeyValue  kv)
        {
            for (int i = 0; i < 4; i++)
            {
                if (CanGo(kv.Row,kv.Col,kv.PlateType, i))
                    return 4 - i;
            }
            return 0;
        }
        public bool CanGo(int row,int col,string plateType, int ChannelID)
        {            
            switch (plateType)//lxm
            {
                case "Qc":
                    row = (row - 1) % 6 + 1;
                    if (row <= 5) return true;
                    switch (row)
                    {
                        case 6:
                            return ChannelID >= 3;                        
                    }
                    break;                
            }
            return true;
        }
    }
}
