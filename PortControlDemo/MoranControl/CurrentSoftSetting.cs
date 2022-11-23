using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using log4net.Plugin;

namespace PortControlDemo.MoranControl
{
   
    public class CurrentSoftSetting
    {
        public static string SampleModerator = "";
    }
    public class AppPar
    {
        public static Form mainFrm;
        public static IPlugin IPuginer;
        public static IApplication IApplicationer;
        //public static string MachineType { get; set; }
#if fengbitime
        public static EliRemoteObject.EliClock SoftClock = null;
#endif
        public static bool isLegality = true;
        public static DateTime Now
        {
            get
            {
#if fengbitime
                return SoftClock.getTime;
#else
                return DateTime.Now;
#endif

            }
        }
        public static string CodeBarBataBasePath;
    }
    /// <summary>
    /// SoftConfig读取设置类
    /// </summary>
    [Serializable]
    public class AppSet
    {
        public bool IsNeedOnline;
        /// <summary>
        /// 是否从数据库中取数据
        /// </summary>
        public bool IsMinute;
        /// <summary>
        /// 是否探测
        /// </summary>
        public bool IsProbe = false;
        /// <summary>
        /// 样本试剂从不从库中取
        /// </summary>
        public bool IsSampleReagentMinute = false;
        public bool IsMoot = false;
        public bool NeedRecord = false;
        [NonSerialized]
        [XmlIgnore]
        public string ConfigurationPath;
        private static AppSet _instance;
        public static AppSet Instance
        {
            get
            {
                if (_instance == null)
                {
                    XmlReader reader = new XmlTextReader(Application.StartupPath + @"\SoftConfig.xml");
                    reader.ReadToFollowing("AppSet");
                    if (reader.NodeType == XmlNodeType.None)
                        _instance = new AppSet();
                    else
                    {
                        XmlSerializer keySerializer = new XmlSerializer(typeof(AppSet));
                        AppSet ini = (AppSet)keySerializer.Deserialize(reader);
                        _instance = ini;
                        //XmlReader reader = new XmlTextReader(System.Windows.Forms.Application.StartupPath + @"\SoftConfig.xml");
                        //reader.ReadStartElement();
                        //_instance = (AppSet)Elisa.SerializerHelper.XMLDeserialize(typeof(AppSet), reader);
                        ////SoftConfiger.Instance.appset = _instance = SoftConfiger.Instance.appset;
                        ////if (_instance == null) ;
                        ////_instance = new AppSet();
                    }
                    reader.Close();
                }
                return _instance;
            }
        }
        public AppSet()
        {

        }
        public void Save()
        {
            using (MemoryStream mem = new MemoryStream())
            {
                System.Xml.XmlWriterSettings set = new XmlWriterSettings();
                set.Indent = true;
                set.IndentChars = "  ";
                set.Encoding = System.Text.Encoding.UTF8;
                XmlWriter doc = XmlWriter.Create(mem, set);
                doc.WriteStartDocument();
                SerializerHelper.XMLSerializeToWriter(this, doc);
                doc.Flush();
                doc.Close();
                XmlDocument xmlDocc = new XmlDocument();
                mem.Seek(0, SeekOrigin.Begin);
                xmlDocc.Load(mem);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Application.StartupPath + @"\SoftConfig.xml");
                XmlNode node = xmlDoc.SelectSingleNode(@"/SoftConfiger/AppSet");
                if (node == null)
                    xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDocc.DocumentElement.CloneNode(true), true));
                else
                    node.InnerXml = xmlDocc.DocumentElement.InnerXml;
                xmlDoc.Save(Application.StartupPath + @"\SoftConfig.xml");
            }
        }
    }
    [Serializable]
    public class PrintSetting
    {
        public string HospitalName { get; set; }
        public string InOrder { get; set; }
        public bool IsExport { get; set; }
        public string PageSize { get; set; }

        public PrintSetting()
        {
            IsExport = true;
        }
        private static PrintSetting _instance;
        public static PrintSetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    XmlReader reader = new XmlTextReader(Application.StartupPath + @"\SoftConfig.xml");
                    reader.ReadToFollowing("PrintSetting");
                    if (reader.NodeType == XmlNodeType.None)
                        _instance = new PrintSetting();
                    else
                    {
                        XmlSerializer keySerializer = new XmlSerializer(typeof(PrintSetting));
                        PrintSetting ini = (PrintSetting)keySerializer.Deserialize(reader);
                        _instance = ini;
                     }
                    reader.Close();
                    ////_instance = SoftConfiger.Instance.printset;
                    ////if (_instance == null)
                    ////    SoftConfiger.Instance.printset = _instance = new PrintSetting();
                }
                return _instance;
            }
        }
        public void Save()
        {
            using (MemoryStream mem = new MemoryStream())
            {
                System.Xml.XmlWriterSettings set = new XmlWriterSettings();
                set.Indent = true;
                set.IndentChars = "  ";
                set.Encoding = System.Text.Encoding.UTF8;
                XmlWriter doc = XmlWriter.Create(mem, set);
                doc.WriteStartDocument();
                SerializerHelper.XMLSerializeToWriter(this, doc);
                doc.Flush();
                doc.Close();
                XmlDocument xmlDocc = new XmlDocument();
                mem.Seek(0, SeekOrigin.Begin);
                xmlDocc.Load(mem);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"/SoftConfig.xml");
                XmlNode node = xmlDoc.SelectSingleNode(@"/SoftConfiger/PrintSetting");
                if (node == null)
                    xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDocc.DocumentElement.CloneNode(true), true));
                else
                    node.InnerXml = xmlDocc.DocumentElement.InnerXml;
                xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"/SoftConfig.xml");
            }
        }
    }
    [Serializable]
    public class ElisaConfig
    {
        private static ElisaConfig _instance;
        public static ElisaConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    XmlReader reader = new XmlTextReader(System.Windows.Forms.Application.StartupPath + @"/SoftConfig.xml");
                    reader.ReadToFollowing("ElisaConfig");
                    if (reader.NodeType == XmlNodeType.None)
                        _instance = new ElisaConfig();
                    else
                    {
                        XmlSerializer keySerializer = new XmlSerializer(typeof(ElisaConfig));
                        ElisaConfig ini = (ElisaConfig)keySerializer.Deserialize(reader);
                        _instance = ini;
                    }
                    reader.Close();
                    ////_instance = SoftConfiger.Instance.printset;
                    ////if (_instance == null)
                    ////    SoftConfiger.Instance.printset = _instance = new PrintSetting();
                }
                return _instance;
            }
        }
        public ElisaConfig()
        {
        }
        public void Save()
        {
            using (MemoryStream mem = new MemoryStream())
            {
                System.Xml.XmlWriterSettings set = new XmlWriterSettings();
                set.Indent = true;
                set.IndentChars = "  ";
                set.Encoding = System.Text.Encoding.UTF8;
                XmlWriter doc = XmlWriter.Create(mem, set);
                doc.WriteStartDocument();
                SerializerHelper.XMLSerializeToWriter(this, doc);
                doc.Flush();
                doc.Close();
                XmlDocument xmlDocc = new XmlDocument();
                mem.Seek(0, SeekOrigin.Begin);
                xmlDocc.Load(mem);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"/SoftConfig.xml");
                XmlNode node = xmlDoc.SelectSingleNode(@"/SoftConfiger/ElisaConfig");
                if (node == null)
                    xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDocc.DocumentElement.CloneNode(true), true));
                else
                    node.InnerXml = xmlDocc.DocumentElement.InnerXml;
                xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"/SoftConfig.xml");
            }
        }
        public int BeginWaitDelay { get; set; }
        public bool AbsorbanceAbs { get; set; }
        public bool IsRegent { get; set; }
        public int CodeNumber { get; set; }
        public string HExper { get; set; }
        public string LExper { get; set; }
        public string DiameterPassWord { get; set; }
        public List<int> HoleTypeColors { get; set; }
    }
    //[Serializable]
    //public class SoftConfiger
    //{
    //    private static SoftConfiger _instance;
    //    public static SoftConfiger Instance
    //    {
    //        get
    //        {
    //            if (_instance == null)
    //            {
    //                _instance = (SoftConfiger)SerializerHelper.XMLDeserialize(typeof(SoftConfiger), System.Windows.Forms.Application.StartupPath + @"\SoftConfig.xml");
    //            }
    //            return _instance;
    //        }
    //    }
    //    public AppSet appset { get; set; }
    //    public PrintSetting printset { get; set; }
    //    //public SoftConfiger()
    //    //{
    //    //    appset = AppSet.Instance;
    //    //    printset =PrintSetting.Instance;
    //    //}
    //    public void Save()
    //    {
    //        SerializerHelper.XMLSerialize(Instance, System.Windows.Forms.Application.StartupPath + @"\SoftConfig.xml");
    //    }
    //}
    //public class SoftInier
    //{
        
    //    public void Save2()
    //    {
    //        SoftConfiger st = new SoftConfiger();
    //        Elisa.SerializerHelper.XMLSerialize(st, System.Windows.Forms.Application.StartupPath + @"\SoftConfig2.xml");
    //    }
    //    public void Save()
    //    {
    //        XmlDocument xmlDoc = new XmlDocument();
    //        xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
    //        System.Xml.XmlWriterSettings set = new XmlWriterSettings();
    //        XmlNode rootNode = xmlDoc.CreateElement("configuration");
    //        rootNode.InnerXml += Elisa.SerializerHelper.XMLSerialize(AppSet.Instance).Substring(38);
    //        rootNode.InnerXml += Elisa.SerializerHelper.XMLSerialize(PrintSetting.Instance).Substring(38);
    //        xmlDoc.AppendChild(rootNode);
    //        xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\SoftConfig.xml");
    //        //SerializerHelper
    //        //set.Indent = true;
    //        //set.IndentChars = "  ";
    //        //set.Encoding = System.Text.Encoding.UTF8;
    //        //XmlElement mt = new XmlElement();

    //        //ment.AppendChild(mt);
    //        //XmlWriter doc = XmlWriter.Create("SoftConfig.xml", set);
    //        //doc.WriteStartDocument();
    //        //doc.WriteStartElement("configuration");
    //        //AppSet.Instance.WriteXmlWriter(doc);
    //        //PrintSetting.Instance.WriteXmlWriter(doc);
    //        //doc.Flush();
    //        //doc.Close();
    //    }
    //}
    public class HashEncrypt
    {
        //private string strIN;
        private bool isReturnNum;
        private bool isCaseSensitive;

        public HashEncrypt(bool IsCaseSensitive, bool IsReturnNum)
        {
            this.isReturnNum = IsReturnNum;
            this.isCaseSensitive = IsCaseSensitive;
        }
        public HashEncrypt()
        {
            this.isReturnNum = true; ;
            this.isCaseSensitive = true; ;
        }

        private string getstrIN(string strIN)
        {
            //string strIN = strIN;
            if (strIN.Length == 0)
            {
                strIN = "~NULL~";
            }
            if (isCaseSensitive == false)
            {
                strIN = strIN.ToUpper();
            }
            return strIN;
        }
        public string MD5Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(getstrIN(strIN)));
            md5.Clear();

            return GetStringValue(tmpByte);

        }

        public string SHA1Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash(GetKeyByteArray(strIN));
            sha1.Clear();

            return GetStringValue(tmpByte);

        }

        public string SHA256Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));
            sha256.Clear();

            return GetStringValue(tmpByte);

        }

        public string SHA512Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash(GetKeyByteArray(strIN));
            sha512.Clear();

            return GetStringValue(tmpByte);

        }

        /// <summary>
        /// 使用DES加密（Added by niehl 2005-4-6）
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public string DESEncrypt(string originalValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(originalValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());

        }

        public string DESEncrypt(string originalValue, string key)
        {
            return DESEncrypt(originalValue, key, key);
        }

        /// <summary>
        /// 使用DES解密（Added by niehl 2005-4-6）
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">m初始化向量(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public string DESDecrypt(string encryptedValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateDecryptor();

            byt = Convert.FromBase64String(encryptedValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());

        }

        public string DESDecrypt(string encryptedValue, string key)
        {
            return DESDecrypt(encryptedValue, key, key);
        }

        private string GetStringValue(byte[] Byte)
        {
            string tmpString = "";

            if (this.isReturnNum == false)
            {
                ASCIIEncoding Asc = new ASCIIEncoding();
                tmpString = Asc.GetString(Byte);
            }
            else
            {
                int iCounter;

                for (iCounter = 0; iCounter < Byte.Length; iCounter++)
                {
                    tmpString = tmpString + Byte[iCounter].ToString();
                }

            }

            return tmpString;
        }

        private byte[] GetKeyByteArray(string strKey)
        {

            ASCIIEncoding Asc = new ASCIIEncoding();

            int tmpStrLen = strKey.Length;
            byte[] tmpByte = new byte[tmpStrLen - 1];

            tmpByte = Asc.GetBytes(strKey);

            return tmpByte;

        }
        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = "64";
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i], 16);
            }
            return result;
        }
        public static string HexStringToString(string hs, Encoding encode)
        {
            //以%分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length - 1];
            //逐个字符变为16进制字节数据
            for (int i = 1; i < chars.Length; i++)
            {
                b[i - 1] = Convert.ToByte(chars[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }
        public static string StringToOxString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = "32";
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i], 8);
            }
            return result;
        }
        public static string OxStringToString(string hs, Encoding encode)
        {
            //以%分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length - 1];
            //逐个字符变为16进制字节数据
            for (int i = 1; i < chars.Length; i++)
            {
                b[i - 1] = Convert.ToByte(chars[i], 8);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }
        public static bool SelectHAN(string s)
        {
            Regex reg = new Regex(@"[\u4e00-\u9fa5]");//正则表达式                       
            if (reg.IsMatch(s))            
            {
                return true;           
            }           
            else            
            {
                return false;            
            }
 
        }

    }
   
}
