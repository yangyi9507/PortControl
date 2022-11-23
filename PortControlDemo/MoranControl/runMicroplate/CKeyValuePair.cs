using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 泛型类,实例化时必须传递具体的类型替换TKey和TValue
/// </summary>
/// <typeparam name="TKey">自定义类型1,其为键</typeparam>
/// <typeparam name="TValue">自定义类型2，其为值</typeparam>
[Serializable]
public class CKeyValuePair<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}
/// <summary>
/// 泛型类,实例化时必须传递具体的类型替换TKey、TValue和TValue2
/// </summary>
/// <typeparam name="TKey">自定义类型1,其为键</typeparam>
/// <typeparam name="TValue">自定义类型2,其为值</typeparam>
/// <typeparam name="TValue2">自定义类型3,其为值2</typeparam>
[Serializable]
public class CKeyValuePair<TKey, TValue, TValue2>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public TValue2 Value2 { get; set; }
}
