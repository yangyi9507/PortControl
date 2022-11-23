using PortControlDemo.MoranControl;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortControlDemo
{
    public partial class Main : UIForm
    {
        #region 字段/属性
        int[] BaudRateArr = new int[] { 9600, 4800, 115200 };
        int[] DataBitArr = new int[] { 8 };
        int[] StopBitArr = new int[] { 1, 2, 3 };
        int[] TimeoutArr = new int[] { 500, 1000, 2000, 5000, 10000 };
        object[] CheckBitArr = new object[] { "None" };
        private bool ReceiveState = false;
        private PortControlHelper pchSend;
        #endregion

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitView()
        {
            uiComboBox3.DataSource = pchSend.PortNameArr;
            uiComboBox4.DataSource = BaudRateArr;
            uiComboBox5.DataSource = DataBitArr;
            uiComboBox6.DataSource = StopBitArr;
            uiComboBox7.DataSource = CheckBitArr;
            uiComboBox8.DataSource = TimeoutArr;
        }


        public Main()
        {
            InitializeComponent();
            pchSend = new PortControlHelper();
            InitView();
            ComboBoxCameras = new List<ComboBoxCamera>();
            fmm = new FastModeManager();
            fastCallback = OnFastImage;
            temp = new Thread(UpdateTemperature);
            temp.Start();

        }
        public static string ReportID = "";
        /// <summary>
        /// 孔布局
        /// </summary>
        public MoranControl.PlateLayout Playout;
        /// <summary>
        /// 微孔板信息
        /// </summary>
        public MoranControl.MicroplateSeting MicSeting;
        /// <summary>
        /// 用于界面传值的自定义委托
        /// </summary>
        /// <param name="hd"></param>
        public delegate void BackHoleData(MoranControl.HoleData hd);
        /// <summary>
        /// 用于传值的事件，将右边栏添加的孔信息传递给微孔板
        /// </summary>
        public event BackHoleData BackHole;



        /// <summary>
        /// 根据串口信息打开相应串口
        /// </summary>
        private void open_serial()
        {

            if (pchSend.PortState)
            {
                pchSend.ClosePort();
                uiButton50.Text = "打开";
                UIMessageTip.Show(AppCode.Close_success);
            }
            else
            {
                pchSend.OpenPort(uiComboBox3.Text, int.Parse(uiComboBox4.Text),
                    int.Parse(uiComboBox5.Text), int.Parse(uiComboBox6.Text),
                    int.Parse(uiComboBox8.Text));
                uiButton50.Text = "关闭";
                UIMessageTip.Show(AppCode.Open_success);
            }
            pchSend.OnComReceiveDataHandler = new PortControlHelper.ComReceiveDataHandler(ComReceiveData);

            ReceiveState = true;
        }


        /// <summary>
        /// 按键状态变化显示
        /// </summary>
        void btn_state(bool state) {
            foreach (Control control in uiTitlePanel1.Controls)
            {
                if (control is Button || control is UIButton)
                {
                    control.Enabled = state;
                }
            }
            foreach (Control control in uiTitlePanel2.Controls)
            {
                if (control is Button || control is UIButton)
                {
                    control.Enabled = state;
                }
            }
            foreach (Control control in uiTitlePanel3.Controls)
            {
                if (control is Button || control is UIButton)
                {
                    control.Enabled = state;
                }
            }
            foreach (Control control in uiTitlePanel4.Controls)
            {
                if (control is Button || control is UIButton)
                {
                    control.Enabled = state;
                }
            }
            foreach (Control control in uiTitlePanel5.Controls)
            {
                if (control is Button || control is UIButton)
                {
                    control.Enabled = state;
                }
            }
            foreach (Control control in uiTitlePanel6.Controls)
            {
                if (control is Button || control is UIButton)
                {
                    control.Enabled = state;
                }
            }
        }

        /// <summary>
        /// 接收到的数据，写入文本框内
        /// </summary>
        /// <param name="data"></param>
        private void ComReceiveData(string data)
        {
            this.Invoke(new Action(() =>
            {
                Console.WriteLine("接收" + data);
                log4netHelper.Info(string.Format("接收数据：" + data));
                if (data.Substring(0, 4).Equals("A064"))
                {
                    switch (data.Substring(4, 10))
                    {
                        #region 初始化
                        case "3000000000":
                            btn_state(true);
                            break;
                        #endregion

                        #region 激发滤光片复位
                        case "3101000000":
                            btn_state(true);
                            break;
                        #endregion

                        #region 激发滤光片123456移动
                        case "3104000000":
                            btn_state(true);
                            break;
                        #endregion

                        #region 荧光滤光片复位
                        case "3201000000":
                            btn_state(true);
                            break;
                        #endregion

                        #region 荧光滤光片123456移动
                        case "3204000000":
                            btn_state(true);
                            break;
                        #endregion

                        #region 开关风扇
                        case "3600000000":
                            if (flag == 1)
                            {
                                //开风扇返回
                                UIMessageTip.Show(AppCode.Open_success);
                            }
                            else {
                                //关风扇返回
                                UIMessageTip.Show(AppCode.Close_success);
                            }
                            break;
                        #endregion

                        #region 灯泡模式
                        case "3400000000":
                            btn_state(true);
                            if (flag == 1)
                            {
                                //热机模式
                                UIMessageTip.Show(AppCode.Open_success);
                            }
                            else
                            {
                                //工作模式
                                UIMessageTip.Show(AppCode.Open_success);
                            }
                            break;
                        #endregion

                        #region 蜂鸣器配置
                        case "3701000000":
                            btn_state(true);
                            UIMessageTip.Show(AppCode.Configure_success);
                            break;
                        #endregion

                        #region PCR不在板
                        case "3801000000":
                            btn_state(true);
                            uiTextBox1.Text = AppCode.No_board;
                            break;
                        #endregion

                        #region PCR在板
                        case "3801000001":
                            btn_state(true);
                            uiTextBox1.Text = AppCode.board;
                            break;
                        #endregion

                        #region 加热温控打开关闭
                        case "6001000000":
                            btn_state(true);
                            if (flag == 1)
                            {
                                //打开
                                UIMessageTip.Show(AppCode.Open_success);
                            }
                            else
                            {
                                //关闭
                                UIMessageTip.Show(AppCode.Close_success);
                            }
                            break;
                        #endregion

                        #region Y轴开关舱
                        case "3300000001":
                            btn_state(true);
                            if (flag == 1)
                            {
                                //打开
                                UIMessageTip.Show(AppCode.Open_success);
                            }
                            else
                            {
                                //关闭
                                UIMessageTip.Show(AppCode.Close_success);
                            }
                            break;
                        #endregion

                        #region Z轴释放压紧
                        case "3301000000":
                            btn_state(true);
                            if (flag == 1)
                            {
                                //释放
                                UIMessageTip.Show(AppCode.Release_success);
                            }
                            else
                            {
                                //压紧
                                UIMessageTip.Show(AppCode.Compression_success);
                            }
                            break;
                        #endregion

                        #region 翻盖电机开光盖
                        case "3302000000":
                            btn_state(true);
                            if (flag == 1)
                            {
                                //开盖
                                UIMessageTip.Show(AppCode.Release_success);
                            }
                            else
                            {
                                //关盖
                                UIMessageTip.Show(AppCode.Compression_success);
                            }
                            break;
                            #endregion
                    }
                }

            }));
        }

        private int flag = 1;

        #region 打开串口
        private void uiButton50_Click(object sender, EventArgs e)
        {
            if (uiComboBox3.Text == "")
            {
                MessageBox.Show("未识别端口");
                return;
            }
            open_serial();
        }
        #endregion

        #region 初始化
        private void uiButton35_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3000000000000000");
        }
        #endregion

        #region 激发滤光片复位
        private void uiButton34_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3101010000000000");
        }
        #endregion

        #region 激发滤光片1
        private void uiButton28_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3104010000000000");
        }
        #endregion

        #region 激发滤光片2
        private void uiButton29_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3104020000000000");
        }
        #endregion

        #region 激发滤光片3
        private void uiButton30_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3104030000000000");
        }
        #endregion

        #region 激发滤光片4
        private void uiButton31_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3104040000000000");
        }
        #endregion

        #region 激发滤光片5
        private void uiButton33_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3104050000000000");
        }
        #endregion

        #region 激发滤光片6
        private void uiButton32_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3104060000000000");
        }
        #endregion

        #region 荧光滤光片复位
        private void uiButton41_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3201010000000000");
        }
        #endregion

        #region 荧光滤光片1
        private void uiButton36_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3204010000000000");
        }
        #endregion

        #region 荧光滤光片2
        private void uiButton37_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3204020000000000");
        }
        #endregion

        #region 荧光滤光片3
        private void uiButton38_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3204030000000000");
        }
        #endregion

        #region 荧光滤光片4
        private void uiButton39_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3204040000000000");
        }
        #endregion

        #region 荧光滤光片5
        private void uiButton42_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3204050000000000");
        }
        #endregion

        #region 荧光滤光片6
        private void uiButton40_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3204060000000000");
        }
        #endregion

        #region 风扇控制
        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            if (uiSwitch1.Active)
            {
                //开风扇
                flag = 1;
                pchSend.SendData("3600010000000000");
            }
            else {
                //关风扇
                flag = 2;
                pchSend.SendData("3600000000000000");
            }
        }
        #endregion

        #region 热机模式
        private void uiButton45_Click(object sender, EventArgs e)
        {
            flag = 1;
            btn_state(false);
            pchSend.SendData("3400000000000000");
        }
        #endregion

        #region 工作模式
        private void uiButton46_Click(object sender, EventArgs e)
        {
            flag = 2;
            btn_state(false);
            pchSend.SendData("3400010000000000");
        }
        #endregion

        #region 蜂鸣器配置
        private void uiButton48_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiComboBox1.Text) || string.IsNullOrEmpty(uiComboBox2.Text))
            {
                UIMessageTip.Show(AppCode.Null_value);
                return;
            }
            if (int.Parse(uiComboBox1.Text) >= 65535 || int.Parse(uiComboBox2.Text) >= 65535)
            {
                UIMessageTip.Show(AppCode.Value_large);
                return;
            }
            btn_state(false);
            pchSend.SendData("3701" + Convert.ToString(int.Parse(uiComboBox1.Text), 16).PadLeft(2, '0')
                + Convert.ToString(int.Parse(uiComboBox2.Text), 16).PadLeft(4, '0') + "000000");
        }
        #endregion

        #region PCR在板监测
        private void uiButton49_Click(object sender, EventArgs e)
        {
            btn_state(false);
            pchSend.SendData("3801000000000000");
        }
        #endregion

        #region RGB指示灯颜色配置
        private void uiButton47_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiDoubleUpDown1.Value.ToString()) || string.IsNullOrEmpty(uiDoubleUpDown2.Value.ToString())
                || string.IsNullOrEmpty(uiDoubleUpDown3.Value.ToString()))
            {
                UIMessageTip.Show(AppCode.Null_value);
                return;
            }
            if (uiDoubleUpDown1.Value > 256 || uiDoubleUpDown2.Value > 256 || uiDoubleUpDown3.Value > 256)
            {
                UIMessageTip.Show(AppCode.ValueColor_large);
                return;
            }
            btn_state(false);
            pchSend.SendData("3700" + Convert.ToString(int.Parse(uiDoubleUpDown1.Value.ToString()), 16).PadLeft(2, '0')
                + Convert.ToString(int.Parse(uiDoubleUpDown2.Value.ToString()), 16).PadLeft(2, '0')
                 + Convert.ToString(int.Parse(uiDoubleUpDown3.Value.ToString()), 16).PadLeft(2, '0') + "000000");
        }
        #endregion

        #region 查询
        private void uiButton51_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 加热温控打开
        private void uiButton55_Click(object sender, EventArgs e)
        {
            flag = 1;
            btn_state(false);
            pchSend.SendData("600101" + Convert.ToString(uiRadioButton2.Checked == true ? 1 : 2, 16).PadLeft(2, '0')
                + Convert.ToString(int.Parse(uiDoubleUpDown11.Text), 16).PadLeft(2, '0')
                 + Convert.ToString(int.Parse(uiDoubleUpDown10.Text), 16).PadLeft(2, '0') + "0000");
        }
        #endregion

        #region 加热温控关闭
        private void uiButton54_Click(object sender, EventArgs e)
        {
            flag = 2;
            btn_state(false);
            pchSend.SendData("600100" + Convert.ToString(uiRadioButton2.Checked == true ? 1 : 2, 16).PadLeft(2, '0')
                + Convert.ToString(int.Parse(uiDoubleUpDown11.Text), 16).PadLeft(2, '0')
                 + Convert.ToString(int.Parse(uiDoubleUpDown10.Text), 16).PadLeft(2, '0') + "0000");
        }
        #endregion

        #region 获取温度查询
        private void uiButton57_Click(object sender, EventArgs e)
        {
            int i = uiRadioButton_tec1.Checked == true ? 1 : uiRadioButton_tec2.Checked == true ? 2 : uiRadioButton_tec3.Checked == true ? 3 :
                uiRadioButton_tec4.Checked == true ? 4 : uiRadioButton_tec5.Checked == true ? 5 : uiRadioButton_tec6.Checked == true ? 6 :
                uiRadioButton3.Checked == true ? 7 : uiRadioButton4.Checked == true ? 8 : 9;//获取radiobutton选中项
            btn_state(false);
            pchSend.SendData("FF01" + Convert.ToString(i, 16).PadLeft(2, '0') + "0000000000");
        }
        #endregion

        #region Y电机开舱
        private void uiButton23_Click(object sender, EventArgs e)
        {
            flag = 1;
            btn_state(false);
            pchSend.SendData("3300000000000000");
        }
        #endregion

        #region 关舱
        private void uiButton22_Click(object sender, EventArgs e)
        {
            flag = 2;
            btn_state(false);
            pchSend.SendData("3300010000000000");
        }
        #endregion

        #region z释放
        private void uiButton44_Click(object sender, EventArgs e)
        {
            flag = 1;
            btn_state(false);
            pchSend.SendData("3301000000000000");
        }
        #endregion

        #region z压紧
        private void uiButton43_Click(object sender, EventArgs e)
        {
            flag = 2;
            btn_state(false);
            pchSend.SendData("3301010000000000");
        }
        #endregion

        #region 翻盖电机开盖
        private void uiButton25_Click(object sender, EventArgs e)
        {
            flag = 1;
            btn_state(false);
            pchSend.SendData("3302000000000000");
        }
        #endregion

        #region 翻盖电机关盖
        private void uiButton24_Click(object sender, EventArgs e)
        {
            flag = 2;
            btn_state(false);
            pchSend.SendData("3302010000000000");
        }
        #endregion

        #region 相机部分

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            checkTemperature = false;
            appRunning = false;
            base.OnFormClosed(e);
            AtikPInvoke.ArtemisShutdown();
        }

        void UpdateTemperature()
        {
            while (appRunning)
            {
                while (checkTemperature)
                {
                    int t = 0;
                    AtikPInvoke.ArtemisTemperatureSensorInfo(handle, 1, ref t);
                    double tD = t * 0.01;
                    Temperature.BeginInvoke(new Action(() => Temperature.Text = tD.ToString()));
                    Thread.Sleep(1000);
                }
            }
        }

        void OnFastImage(IntPtr handle, int x, int y, int w, int h, int binx, int biny, IntPtr imageBuffer)
        {
            if (this.handle != handle)
                return;

            if (fastModeNmrImage == 0)
                fastModeSW = Stopwatch.StartNew();

            ++fastModeNmrImage;

            if (fastModeNmrImage > 1)
            {
                double fps = (fastModeNmrImage - 1) / (0.001 * fastModeSW.ElapsedMilliseconds);
                fpsValueLabel.BeginInvoke(new Action(() =>
                    fpsValueLabel.Text = Math.Round(fps, 2).ToString()));
            }

            fmw = w;
            fmh = h;
            fmm.Update(x, y, w, h, binx, biny, imageBuffer);
        }

        void DisplayFastModeImages()
        {
            while (appRunning)
            {
                if (fastMode)
                {
                    byte[] rawImg = fmm.GetImage();

                    if (rawImg != null)
                    {
                        // create enough space for a 24 bit per pixel bitmap of the image
                        byte[] bmpBytes = new byte[fmw * fmh * 3];
                        for (int i = 0, j = 0; i < fmw * fmh; ++i, j += 2)
                        {
                            if (stopProcessing)
                            {
                                stopProcessing = false;
                                return;
                            }

                            var val = rawImg[j];
                            // we have a mono image so we place the same value into each bitmap byte
                            bmpBytes[i] = val;
                            bmpBytes[++i] = val;
                            bmpBytes[++i] = val;
                        }

                        var pic = new Bitmap(fmw, fmh);
                        var rect = new Rectangle(0, 0, fmw, fmh);
                        var picData = pic.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                        Marshal.Copy(bmpBytes, 0, picData.Scan0, fmw * 3 * fmh);
                        pic.UnlockBits(picData);

                        PictureBox.BeginInvoke(new Action(() => PictureBox.Image = pic));
                    }
                }
            }
        }

        private void Find_Click(object sender, EventArgs e)
        {
            ComboBoxCameras.Clear();

            AtikPInvoke.ArtemisRefreshDevicesCount();
            for (var i = 0; i < 16; ++i)
            {
                if (AtikPInvoke.ArtemisDevicePresent(i))
                {
                    StringBuilder sbName = new StringBuilder();
                    StringBuilder sbSerial = new StringBuilder();
                    AtikPInvoke.ArtemisDeviceName(i, sbName);
                    AtikPInvoke.ArtemisDeviceSerial(i, sbSerial);

                    ComboBoxCameras.Add(new ComboBoxCamera(i, $"{sbName} {sbSerial}"));
                }
            }

            CameraComboBox.Items.Clear();
            foreach (var camera in ComboBoxCameras)
                CameraComboBox.Items.Add(camera);
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            var selected = (ComboBoxCamera)CameraComboBox.SelectedItem;
            if (selected != null)
            {
                handle = AtikPInvoke.ArtemisConnect(selected.id);
                if (handle.ToInt32() != 0)
                {
                    ConnectedLabel.Text = "已联机";
                    Disconnect.Enabled = true;
                    Connect.Enabled = false;
                    StartExposureButton.Enabled = true;

                    // Using 0 for the sensor checks the number of sensors
                    // rather than checking for a temperature
                    int num = 0;
                    AtikPInvoke.ArtemisTemperatureSensorInfo(handle, 0, ref num);
                    if (num != 0)
                    {
                        StartCoolingButton.Enabled = true;
                        checkTemperature = true;
                    }
                    else
                    {
                        Temperature.Text = "无冷却";
                    }
                }

                if (AtikPInvoke.ArtemisHasCameraSpecificOption(handle, (ushort)AtikCameraSpecificOptions.ID_GOCustomGain) &&
                   AtikPInvoke.ArtemisHasCameraSpecificOption(handle, (ushort)AtikCameraSpecificOptions.ID_GOCustomOffset) &&
                   AtikPInvoke.ArtemisHasCameraSpecificOption(handle, (ushort)AtikCameraSpecificOptions.ID_PadData) &&
                   AtikPInvoke.ArtemisHasCameraSpecificOption(handle, (ushort)AtikCameraSpecificOptions.ID_EvenIllumination))
                {
                    CMOSOptionsBox.Visible = true;

                    int length = 0;

                    byte[] go = new byte[2];
                    AtikPInvoke.ArtemisCameraSpecificOptionGetData(handle, (ushort)AtikCameraSpecificOptions.ID_GOPresetMode, go, 2, ref length);
                    GOModeComboBox.SelectedIndex = go[0];

                    if (go[0] == 0)
                    {
                        GainBox.Visible = true;
                        byte[] gain = new byte[6];
                        AtikPInvoke.ArtemisCameraSpecificOptionGetData(handle, (ushort)AtikCameraSpecificOptions.ID_GOCustomGain, gain, 6, ref length);
                        var gainMax = (gain[3] << 8) + gain[2];
                        var gainVal = (gain[5] << 8) + gain[4];
                        GainUpDown.Minimum = 0;
                        GainUpDown.Maximum = gainMax;
                        GainUpDown.Value = gainVal;

                        OffsetBox.Visible = true;
                        byte[] offset = new byte[6];
                        AtikPInvoke.ArtemisCameraSpecificOptionGetData(handle, (ushort)AtikCameraSpecificOptions.ID_GOCustomOffset, offset, 6, ref length);
                        var offsetMax = (offset[3] << 8) + offset[2];
                        var offsetVal = (offset[5] << 8) + offset[4];
                        OffsetUpDown.Minimum = 0;
                        OffsetUpDown.Maximum = offsetMax;
                        OffsetUpDown.Value = offsetVal;
                    }

                    byte[] padData = new byte[1];
                    AtikPInvoke.ArtemisCameraSpecificOptionGetData(handle, (ushort)AtikCameraSpecificOptions.ID_PadData, padData, 1, ref length);
                    PadDataCheckBox.Checked = Convert.ToBoolean(padData[0]);

                    byte[] evenIllu = new byte[1];
                    AtikPInvoke.ArtemisCameraSpecificOptionGetData(handle, (ushort)AtikCameraSpecificOptions.ID_EvenIllumination, evenIllu, 1, ref length);
                    EvenIlluminationCheckBox.Checked = Convert.ToBoolean(evenIllu[0]);
                }

                if (AtikPInvoke.ArtemisHasFastMode(handle))
                {
                    int length = 0;
                    FastModeBox.Visible = true;

                    byte[] expSpeed = new byte[2];
                    AtikPInvoke.ArtemisCameraSpecificOptionGetData(handle, (ushort)AtikCameraSpecificOptions.ID_ExposureSpeed, expSpeed, 2, ref length);
                    ExposureSpeedBox.SelectedIndex = expSpeed[0];

                    byte[] bitSend = new byte[2];
                    AtikPInvoke.ArtemisCameraSpecificOptionGetData(handle, (ushort)AtikCameraSpecificOptions.ID_BitSendMode, bitSend, 2, ref length);
                    BitSendModeBox.SelectedIndex = bitSend[0];

                    AtikPInvoke.ArtemisSetFastCallback(handle, fastCallback);
                    fastMode = true;
                    Thread fastModeReceive = new Thread(DisplayFastModeImages);
                    fastModeReceive.Start();
                }
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            checkTemperature = false;
            // Stop cooling otherwise camera will continue 
            // to cool when disconnected
            if (StopCooling.Enabled)
            {
                AtikPInvoke.ArtemisCoolerWarmUp(handle);
                StopCooling.Enabled = false;
            }

            AtikPInvoke.ArtemisStopExposure(handle);
            AtikPInvoke.ArtemisDisconnect(handle);
            ConnectedLabel.Text = "未联机";
            Temperature.Text = "无摄像头";

            Disconnect.Enabled = false;
            Connect.Enabled = true;
            StartCoolingButton.Enabled = false;
            StartExposureButton.Enabled = false;
            CMOSOptionsBox.Visible = false;
            FastModeBox.Visible = false;
            fastModeNmrImage = 0;
        }

        private void StartExposureButton_Click(object sender, EventArgs e)
        {
            if (handle.ToInt32() != 0)
            {
                if (AtikPInvoke.ArtemisStartExposure(handle, 2) == (int)ArtemisError.OK)
                {
                    ExposureStatus.Text = "开始曝光";
                    Thread t1 = new Thread(WaitForImage);
                    t1.Start();

                    StopExposure.Enabled = true;
                    StartExposureButton.Enabled = false;
                }
                else
                {
                    ExposureStatus.Text = "未能开始曝光";
                }
            }
        }

        private void WaitForImage()
        {
            ExposureStatus.BeginInvoke(new Action(() => ExposureStatus.Text = "正在等待图像"));
            while (!AtikPInvoke.ArtemisImageReady(handle))
            {
                if (stopProcessing)
                {
                    stopProcessing = false;
                    return;
                }

                Thread.Sleep(100);
            }

            int x = 0, y = 0, w = 0, h = 0, binX = 0, binY = 0;
            AtikPInvoke.ArtemisGetImageData(handle, ref x, ref y,
                                                    ref w, ref h,
                                                    ref binX, ref binY);

            // Create memory to copy pixels into
            byte[] pix = new byte[w * h * 2];

            var intPtr = AtikPInvoke.ArtemisImageBuffer(handle);
            ExposureStatus.BeginInvoke(new Action(() => ExposureStatus.Text = "正在转换"));

            Marshal.Copy(intPtr, pix, 0, w * h * 2);

            // create enough space for a 24 bit per pixel bitmap of the image
            byte[] bmpBytes = new byte[w * h * 3];
            for (int i = 0, j = 0; i < w * h; ++i, j += 2)
            {
                if (stopProcessing)
                {
                    stopProcessing = false;
                    return;
                }

                var val = pix[j];
                // we have a mono image so we place the same value into each bitmap byte
                bmpBytes[i] = val;
                bmpBytes[++i] = val;
                bmpBytes[++i] = val;
            }

            var pic = new Bitmap(w, h);
            var rect = new Rectangle(0, 0, w, h);
            var picData = pic.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(bmpBytes, 0, picData.Scan0, w * 3 * h);
            pic.UnlockBits(picData);

            PictureBox.BeginInvoke(new Action(() => PictureBox.Image = pic));
            ExposureStatus.BeginInvoke(new Action(() => ExposureStatus.Text = "Idle"));

            StartExposureButton.BeginInvoke(new Action(() => StartExposureButton.Enabled = true));
            StopExposure.BeginInvoke(new Action(() => StopExposure.Enabled = false));
        }

        private void StopExposure_Click(object sender, EventArgs e)
        {
            AtikPInvoke.ArtemisStopExposure(handle);
            stopProcessing = true;

            StopExposure.Enabled = false;
            StartExposureButton.Enabled = true;
        }

        private void StartCoolingButton_Click(object sender, EventArgs e)
        {
            if (handle != null)
            {
                AtikPInvoke.ArtemisSetCooling(handle, -10);

                StopCooling.Enabled = true;
                StartCoolingButton.Enabled = false;
            }
        }

        private void StopCooling_Click(object sender, EventArgs e)
        {
            AtikPInvoke.ArtemisCoolerWarmUp(handle);

            StopCooling.Enabled = false;
            StartCoolingButton.Enabled = true;
        }

        private void CameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connect.Enabled = true;
        }

        private void GOModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] go = new byte[2];
            go[0] = (byte)GOModeComboBox.SelectedIndex;
            AtikPInvoke.ArtemisCameraSpecificOptionSetData(handle, (ushort)AtikCameraSpecificOptions.ID_GOPresetMode, go, 2);

            if (GOModeComboBox.SelectedIndex == 0)
            {
                GainBox.Visible = true;
                OffsetBox.Visible = true;
            }
            else
            {
                GainBox.Visible = false;
                OffsetBox.Visible = false;
            }
        }

        private void GainUpDown_ValueChanged(object sender, EventArgs e)
        {
            byte[] gain = new byte[2];
            gain[0] = (byte)GainUpDown.Value;
            AtikPInvoke.ArtemisCameraSpecificOptionSetData(handle, (ushort)AtikCameraSpecificOptions.ID_GOCustomGain, gain, 2);
        }

        private void OffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            byte[] offset = new byte[2];
            offset[0] = (byte)((int)OffsetUpDown.Value & 0x00FF);
            offset[1] = (byte)((int)OffsetUpDown.Value >> 8);
            AtikPInvoke.ArtemisCameraSpecificOptionSetData(handle, (ushort)AtikCameraSpecificOptions.ID_GOCustomOffset, offset, 2);
        }

        private void PadDataCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            byte[] padData = new byte[1];
            padData[0] = Convert.ToByte(PadDataCheckBox.Checked);
            AtikPInvoke.ArtemisCameraSpecificOptionSetData(handle, (ushort)AtikCameraSpecificOptions.ID_PadData, padData, 1);
        }

        private void EvenIlluminationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            byte[] evenIllu = new byte[1];
            evenIllu[0] = Convert.ToByte(EvenIlluminationCheckBox.Checked);
            AtikPInvoke.ArtemisCameraSpecificOptionSetData(handle, (ushort)AtikCameraSpecificOptions.ID_EvenIllumination, evenIllu, 1);
        }

        private void ExposureSpeedBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] expSpeed = new byte[2];
            expSpeed[0] = (byte)ExposureSpeedBox.SelectedIndex;
            AtikPInvoke.ArtemisCameraSpecificOptionSetData(handle, (ushort)AtikCameraSpecificOptions.ID_ExposureSpeed, expSpeed, 2);
        }

        private void BitSendModeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] bitSend = new byte[2];
            bitSend[0] = (byte)BitSendModeBox.SelectedIndex;
            AtikPInvoke.ArtemisCameraSpecificOptionSetData(handle, (ushort)AtikCameraSpecificOptions.ID_BitSendMode, bitSend, 2);
        }

        private void StartFastModeButton_Click(object sender, EventArgs e)
        {
            AtikPInvoke.ArtemisStartFastExposure(handle, 1);
            fastMode = true;
            StartFastModeButton.Enabled = false;
            StopFastModeButton.Enabled = true;
        }

        private void StopFastModeButton_Click(object sender, EventArgs e)
        {
            AtikPInvoke.ArtemisStopExposure(handle);
            StartFastModeButton.Enabled = true;
            StopFastModeButton.Enabled = false;
            fastMode = false;
            fastModeNmrImage = 0;
        }

        IntPtr handle;

        List<ComboBoxCamera> ComboBoxCameras;

        bool stopProcessing;
        bool checkTemperature;
        bool appRunning = true;
        Thread temp;

        ArtemisSetFastCallback fastCallback;
        bool fastMode;
        FastModeManager fmm;
        int fmw, fmh;
        int fastModeNmrImage;

        private void PictureBox_Click(object sender, EventArgs e)
        {
            //点击获取picturebox内的X，Y坐标
            int x = Control.MousePosition.X;
            int y = Control.MousePosition.Y;

            Bitmap bm = (Bitmap)PictureBox.Image;
            MessageBox.Show(string.Format("当前坐标点：（{0}，{1}） 灰度值：{2}", x, y, bm.GetPixel(x, y)));

            MessageBox.Show(x.ToString() + ":" + y.ToString() + "    灰度值：" + (0.299 * int.Parse(bm.GetPixel(x, y).R.ToString()) + 0.587 * int.Parse(bm.GetPixel(x, y).G.ToString()) + 0.144 * int.Parse(bm.GetPixel(x, y).B.ToString())));
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.X.ToString() + ":" + e.Y.ToString());
        }

        private void PictureBox_Click_1(object sender, EventArgs e)
        {
            // 点击获取picturebox内的X，Y坐标
            int x = Control.MousePosition.X;
            int y = Control.MousePosition.Y;

            Bitmap bm = (Bitmap)PictureBox.Image;
          /*  MessageBox.Show(string.Format("当前坐标点：（{0}，{1}） 灰度值：{2}", x, y, bm.GetPixel(x, y)));

            MessageBox.Show(x.ToString() + ":" + y.ToString() + "    灰度值：" + (0.299 * int.Parse(bm.GetPixel(x, y).R.ToString()) + 0.587 * int.Parse(bm.GetPixel(x, y).G.ToString()) + 0.144 * int.Parse(bm.GetPixel(x, y).B.ToString())));*/
            Coordinate_Dialog frmLoading = new Coordinate_Dialog(x.ToString() + ":" + y.ToString());
            frmLoading.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void uiTabControlMenu1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.uiTabControlMenu1.SelectedIndex)
            {
                case 5:
                    #region 初始化深孔板画布
                    BLL.DeepHoleBase gain = new BLL.DeepHoleBase();
                    DataTable data = gain.GetAllList().Tables[0];
                    int id = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            HoleData hlData = new HoleData();
                            hlData.ThisHoleType = HoleType.Sample;
                            if (data.Rows[id]["DEEPXY"].ToString().Equals(""))
                            {
                                hlData.ItemName = "";
                                hlData.SampleColor = "white";
                            }
                            else
                            {
                                hlData.ItemName = data.Rows[id]["DEEPXY"].ToString();
                                hlData.SampleColor = "yellow";

                            }
                            uMPlate.Rows[i].Cells[j].Value = hlData;
                            id++;
                        }
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }

        Stopwatch fastModeSW;
    }

    public class ComboBoxCamera
    {
        public ComboBoxCamera(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        public int id;
        public string name;
    }
    #endregion
}
