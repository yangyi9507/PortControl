using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PortControlDemo.MoranControl
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlugin
    {
        IApplication application { get; set; }
        void Loaditem();
        string SerialPortForms { get; }
        //ReaderCommandBase GetReader();
        //ElisaInstrumentsSet GetElisaInstrumentsSet();
        bool isExecutiveBody { get; }
    }
    /// <summary>
    /// 工具栏菜单接口
    /// </summary>
    public interface IApplication
    {
        ToolStripMenuItem tlSeting { get;}
        ToolStripMenuItem tlLayOut { get;}
        ToolStrip MainToolBar { get; }
    }
}
