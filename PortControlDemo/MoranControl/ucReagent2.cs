using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo.MoranControl
{
    public partial class ucReagent2 : UserControl
    {
        public ucReagent2()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 点击板
        /// </summary>
        public event Action<string> btnDiagnostician;
        private void myButtonX1_Click(object sender, EventArgs e)
        {
            if (btnDiagnostician != null)
            {
                btnDiagnostician(((Button)sender).Name);
            }
        }
    }
}
