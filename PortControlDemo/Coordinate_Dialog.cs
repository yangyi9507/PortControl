using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo
{
    public partial class Coordinate_Dialog : UIForm
    {
        public Coordinate_Dialog(string strMag)
        {
            InitializeComponent();
            uiTextBox1.Text = strMag;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        BLL.DeepHoleBase bll_DeepHoleBase = new BLL.DeepHoleBase();
        private void uiButton1_Click(object sender, EventArgs e)
        {
            PortControlDemo.Model.DeepHoleBase DeepHoleBase = new PortControlDemo.Model.DeepHoleBase();
            DeepHoleBase.HOLEID = (int.Parse(uiComboBox1.Text) - 1) * 12 + int.Parse(uiComboBox2.Text);
            DeepHoleBase.DEEPXY = uiTextBox1.Text;
            bool flg = bll_DeepHoleBase.Update(DeepHoleBase);
            if (flg)
            {
                UIMessageTip.Show("保存成功");
            }
            else
            {
                UIMessageTip.Show("保存失败");
            }
        }
    }
}
