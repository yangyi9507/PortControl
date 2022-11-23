using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

    public static class Extensions
    {
    /// <summary>
    /// 判定字符串是否为空字符串或为空
    /// </summary>
    /// <param name="str">待判定字符串</param>
    /// <returns>字符串为null或""，返回true；否则，返回false</returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
    /// <summary>
    /// 取整到秒
    /// </summary>
    /// <param name="dt">时间</param>
    /// <returns>取整到秒的时间值</returns>
    public static DateTime RoundSecond(this DateTime dt)
        {
            return dt.AddMilliseconds(-dt.Millisecond);
        }
        /// <summary>
        /// 判定某变量是否在lowerBound和upperBound之间
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t"></param>
        /// <param name="lowerBound">下极限</param>
        /// <param name="upperBound">上极限</param>
        /// <param name="includeLowerBound">是否包含下极限值</param>
        /// <param name="includeUpperBound">是否包含上极限值</param>
        /// <returns>在范围内，返回true；否则返回false</returns>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, bool includeLowerBound = false, bool includeUpperBound = false) where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
        /// <summary>
        /// 克隆类实例
        /// </summary>
        /// <param name="obj">类实例</param>
        /// <returns>克隆的类实例</returns>
        public static object ExClone(this object obj)
        {
            BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream();
            Formatter.Serialize(stream, obj);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }
    }
    /// <summary>
    /// 自定义实体类，内含三个类成员，
    /// 分别为索引（或相关值）、名称（或描述）、状态，
    /// 均为string类型
    /// </summary>
    public class ListItemT
{
        private string _thisValue = "";
        private string _thisText = "";
        private string _thisState = "";

        /// <summary>
        /// 构造函数（两参）
        /// </summary>
        /// <param name="text">名称或描述</param>
        /// <param name="value">索引或相关值</param>
        public ListItemT(string text, string value)
        {
            this.ThisText = text;
            this.ThisValue = value;
        }
        /// <summary>
        /// 构造函数（三参）
        /// </summary>
        /// <param name="text">名称或描述</param>
        /// <param name="value">索引或相关值</param>
        /// <param name="state">状态</param>
        public ListItemT(string text, string value, string state)
        {
            this.ThisText = text;
            this.ThisValue = value;
            this.ThisState = state;
        }

        public string ThisText
        {
            get { return _thisText; }
            set { _thisText = value; }
        }

        public string ThisValue
        {
            get { return _thisValue; }
            set { _thisValue = value; }
        }

        public string ThisState
        {
            get { return _thisState; }
            set { _thisState = value; }
        }

        public override string ToString()
        {
            return this.ThisText.ToString();
        }
    }

    public static class LINQHelper
    {

        public static T FirstOrNullObject<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T nullObject)
        {
            var val = enumerable.FirstOrDefault<T>(func);
            if (val == null)
            {
                val = nullObject;
            }

            return val;
        }

        public static T FirstOrNullObject<T>(this IEnumerable<T> enumerable, T nullObject)
        {
            var val = enumerable.FirstOrDefault<T>();
            if (val == null)
            {
                val = nullObject;
            }
            return val;
        }

       

        public static T LastOrNullObject<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T nullObject)
        {
            var val = enumerable.LastOrDefault<T>(func);
            if (val == null)
            {
                val = nullObject;
            }

            return val;
        }

        public static T LastOrNullObject<T>(this IEnumerable<T> enumerable, T nullObject)
        {
            var val = enumerable.LastOrDefault<T>();
            if (val == null)
            {
                val = nullObject;
            }
            return val;
        }

     
    }


