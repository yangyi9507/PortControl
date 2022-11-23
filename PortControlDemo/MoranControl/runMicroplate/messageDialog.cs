using System.Windows.Forms;

namespace PortControlDemo.MoranControl
{
    public class messageDialog
    {
        public static string applicationName = "";
        public DialogResult Confirm(string str)
        {
            return MessageBox.Show(str, applicationName, MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
        }
        public void warning(string str)
        {
            MessageBox.Show(str, applicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
        public void show(string str)
        {
            MessageBox.Show(str, applicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
        #region 添加了弹框内容为是否按钮的弹框。
        /// <summary>
        /// 选择（是否）弹窗
        /// </summary>
        /// <param name="str">提示信息</param>
        /// <returns>DialogResult.Yes或DialogResult.No</returns>
        public DialogResult Shifou(string str)
        {
            return MessageBox.Show(str, applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion
        /// <summary>
        /// 错误弹窗
        /// </summary>
        /// <param name="str">错误提示信息</param>
        public void erroring(string str)
        {
            MessageBox.Show(str, applicationName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
      
    }

}
