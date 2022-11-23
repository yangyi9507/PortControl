using DevComponents.DotNetBar.Controls;
using System;
using System.Windows.Forms;
namespace CRYSTAL
{
    public class numTextBox : TextBoxX
    {
        private bool _isXiaoShu = true;
        private bool _isFuShu = true;
        private decimal _minValue = 0;
        private decimal _maxValue = 100;
        private bool _isNull = false;
        private bool _isCorrection = true;
        private bool _oneXiaoShu = false;

        /// <summary>
        /// 是否允许Null值
        /// </summary>
        public bool IsNull
        {
            get { return _isNull; }
            set { _isNull = value; zz(); }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public decimal MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                zz();
            }
        }
        public decimal MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value;
                zz();
            }
        }
        /// <summary>
        /// 负数
        /// </summary>
        public bool IsFuShu
        {
            get { return _isFuShu; }
            set { _isFuShu = value; zz(); }
        }
        /// <summary>
        /// 小数
        /// </summary>
        public bool IsXiaoShu
        {
            get { return _isXiaoShu; }
            set { _isXiaoShu = value; zz(); }
        }

        public bool IsCorrection
        {
            get { return _isCorrection; }
            set { _isCorrection = value; zz(); }
        }
        public bool OneXiaoShu
        {
            get { return _oneXiaoShu; }
            set { _oneXiaoShu = value; }
        }
        void zz()
        {
            if (!IsNull)
            {
                if (Text.Trim() == "")
                    Text = MinValue.ToString();
            }
            if (IsCorrection)
            {
                if (string.IsNullOrEmpty(Text.Trim()))
                    Text = MinValue.ToString();
                if ((Dec < MinValue) || (Dec > MaxValue))
                {
                    if (IsCorrection)
                    {
                        Text = MinValue.ToString();
                    }
                }
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!IsXiaoShu)//如果不是小数
            {
                if (e.KeyChar == '.')
                {
                    e.KeyChar = (char)Keys.None;
                }
            }
            else
            {
                if (OneXiaoShu)
                {
                    int result = Text.Length - Text.IndexOf('.') - 1;
                    if (result == 1)
                    {
                        e.KeyChar = (char)Keys.None;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }

            }
            if (!IsFuShu)
                if (e.KeyChar == '-')
                    e.KeyChar = (char)Keys.None;
            if (SelectionStart > 0)
            {
                if (e.KeyChar == '.')
                {
                    if (Text.Substring((SelectionStart - 1), 1) == "-")
                    {
                        e.Handled = true;
                        return;
                    }
                    if (Text.IndexOf('.') != -1)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                if (e.KeyChar == '-')
                    e.Handled = true;
                //return;
            }
            else
            {
                if (SelectionStart == 0)
                    if (e.KeyChar == '.')
                    {
                        e.Handled = true;
                        return;
                    }
            }
            if ((e.KeyChar > 65280) && (e.KeyChar < 65375))
                e.KeyChar = (char)(e.KeyChar - 65248);
            string temp = "-0123456789.";
            if (temp.IndexOf(e.KeyChar) == -1 && Convert.ToInt32(e.KeyChar) != 8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
            base.OnKeyPress(e);
        }
        public int? valueInt
        {
            get
            {
                if (Text.IndexOf('.') != -1)
                {
                    string str = Text.Trim();
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str.Substring(0, str.IndexOf('.'));
                        if (string.IsNullOrEmpty(str))
                        {
                            return 0;
                        }
                        return int.Parse(str);
                    }

                }
                if (Text == "" || Text.IndexOf('.') != -1)
                    return null;
                return int.Parse(this.Text);
            }
            set
            {
                if (value == null)
                    Clear();
                else
                    Text = value.ToString();
            }
        }
        public double valueDouble
        {
            get
            {
                if (Text.IndexOf('.') != -1)
                {
                    string str = Text.Trim();
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str.Substring(0, str.IndexOf('.'));
                        if (string.IsNullOrEmpty(str))
                        {
                            return 0;
                        }
                        return double.Parse(str);
                    }

                }
                if (Text == "" || Text.IndexOf('.') != -1)
                    return 0;
                return double.Parse(this.Text);
            }
            set
            {
                Text = value.ToString();
            }
        }

        public int Int
        {
            get
            {
                if (Text.IndexOf('.') != -1)
                {
                    string str = Text.Trim();
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str.Substring(0, str.IndexOf('.'));
                        if (string.IsNullOrEmpty(str))
                        {
                            return 0;
                        }
                        return int.Parse(str);
                    }

                }
                if (Text == "" || Text.IndexOf('.') != -1)
                    return 0;
                return int.Parse(this.Text);
            }
            set
            {
                Text = value.ToString();
            }
        }
        public decimal? valueDec
        {
            get
            {
                if (Text == "")
                    return null;
                return (decimal)double.Parse(this.Text);
            }
            set
            {
                if (value != null)
                    Text = value.ToString();
            }
        }
        public decimal Dec
        {
            get
            {
                if (Text == "")
                    return 0;
                return (decimal)double.Parse(this.Text);
            }
            set
            {
                Text = value.ToString();
            }
        }

        public double Dou
        {
            get
            {
                if (Text == "")
                    return 0;
                return double.Parse(this.Text);
            }
            set
            {
                Text = value.ToString();
            }
        }
        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {

            if (Text.Trim() == "")
                Text = MinValue.ToString();              
            base.OnValidating(e);
        }
    }
}
