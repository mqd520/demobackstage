using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// Enum Tool
    /// </summary>
    public static class EnumTool
    {
        /// <summary>
        /// Get MyEnumInfo
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static MyEnumInfo<TEnumValue> GetMyEnumInfo<TEnum, TEnumValue>(TEnumValue val)
        {
            MyEnumInfo<TEnumValue> info = new MyEnumInfo<TEnumValue>();

            Type t = typeof(TEnum);
            FieldInfo[] pis = t.GetFields();
            foreach (var item in pis)
            {
                if (item.FieldType.Name == t.Name)
                {
                    TEnumValue val1 = (TEnumValue)item.GetValue(t);
                    if (val1.Equals(val))
                    {
                        info.Value = val;
                        info.Name = item.Name;
                        MyEnumAttrAttribute attr = item.GetCustomAttribute<MyEnumAttrAttribute>();
                        if (attr != null)
                        {
                            info.Description = attr.Description;
                        }
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// Get MyEnumInfo
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static MyEnumInfo<TEnumValue> GetMyEnumInfo<TEnum, TEnumValue>(TEnum val)
        {
            MyEnumInfo<TEnumValue> info = new MyEnumInfo<TEnumValue>();

            Type t = typeof(TEnum);
            FieldInfo[] pis = t.GetFields();
            foreach (var item in pis)
            {
                if (item.FieldType.Name == t.Name)
                {
                    if (item.Name == val.ToString())
                    {
                        TEnumValue val1 = (TEnumValue)item.GetValue(t);
                        info.Value = val1;
                        info.Name = item.Name;
                        MyEnumAttrAttribute attr = item.GetCustomAttribute<MyEnumAttrAttribute>();
                        if (attr != null)
                        {
                            info.Description = attr.Description;
                        }
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// Get MyEnumInfo
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static MyEnumInfo<TEnumValue> GetMyEnumInfo<TEnum, TEnumValue>(string key)
        {
            MyEnumInfo<TEnumValue> info = new MyEnumInfo<TEnumValue>();

            Type t = typeof(TEnum);
            FieldInfo[] pis = t.GetFields();
            foreach (var item in pis)
            {
                if (item.FieldType.Name == t.Name)
                {
                    if (item.Name == key)
                    {
                        TEnumValue val1 = (TEnumValue)item.GetValue(t);
                        info.Value = val1;
                        info.Name = item.Name;
                        MyEnumAttrAttribute attr = item.GetCustomAttribute<MyEnumAttrAttribute>();
                        if (attr != null)
                        {
                            info.Description = attr.Description;
                        }
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// Get MyEnumInfo List
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <returns></returns>
        public static IList<MyEnumInfo<TEnumValue>> GetMyEnumInfoList<TEnum, TEnumValue>()
        {
            IList<MyEnumInfo<TEnumValue>> ls = new List<MyEnumInfo<TEnumValue>>();

            Type t = typeof(TEnum);
            FieldInfo[] pis = t.GetFields();
            foreach (var item in pis)
            {
                if (item.FieldType.Name == t.Name)
                {
                    MyEnumInfo<TEnumValue> info = new MyEnumInfo<TEnumValue>();
                    info.Name = item.Name;
                    info.Value = (TEnumValue)item.GetValue(t);
                    MyEnumAttrAttribute attr = item.GetCustomAttribute<MyEnumAttrAttribute>();
                    if (attr != null)
                    {
                        info.Description = attr.Description;
                    }

                    ls.Add(info);
                }
            }

            return ls;
        }

        /// <summary>
        /// Get Description
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetDescription<TEnum, TEnumValue>(TEnumValue val)
        {
            return GetMyEnumInfo<TEnum, TEnumValue>(val).Description;
        }

        /// <summary>
        /// Get Description
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetDescription<TEnum, TEnumValue>(TEnum val)
        {
            return GetMyEnumInfo<TEnum, TEnumValue>(val).Description;
        }

        /// <summary>
        /// Get Enum Name List
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IList<string> GetEnumNameList<TEnum>()
        {
            IList<string> ls = new List<string>();

            Type t = typeof(TEnum);
            FieldInfo[] pis = t.GetFields();
            foreach (var item in pis)
            {
                if (item.FieldType.Name == t.Name)
                {
                    ls.Add(item.Name);
                }
            }

            return ls;
        }

        /// <summary>
        /// Get Enum Value List
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <returns></returns>
        public static IList<TEnumValue> GetEnumValueList<TEnum, TEnumValue>()
        {
            IList<TEnumValue> ls = new List<TEnumValue>();

            Type t = typeof(TEnum);
            FieldInfo[] pis = t.GetFields();
            foreach (var item in pis)
            {
                if (item.FieldType.Name == t.Name)
                {
                    TEnumValue value = (TEnumValue)item.GetValue(t);
                    ls.Add(value);
                }
            }

            return ls;
        }

        /// <summary>
        /// To Js Object
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string ToJsObject<TEnum, TEnumValue>(string varName)
        {
            var val = "";
            IList<MyEnumInfo<TEnumValue>> ls = GetMyEnumInfoList<TEnum, TEnumValue>();
            foreach (var item in ls)
            {
                Type t = typeof(TEnumValue);
                if (t.Name.ToLower().Contains("string"))
                {
                    val += string.Format("{0}: \"{1}\", ", item.Name, item.Value);
                }
                else
                {
                    val += string.Format("{0}: {1}, ", item.Name, item.Value);
                }
            }

            if (!string.IsNullOrEmpty(val))
            {
                val = val.TrimEnd(", ".ToCharArray());
            }

            string str = string.Format("var {0} = {2}{1}{3};", varName, val, "{", "}");

            return str;
        }

        /// <summary>
        /// Get Enum List
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IList<TEnum> GetEnumList<TEnum>()
        {
            IList<TEnum> ls = new List<TEnum>();

            Type t = typeof(TEnum);
            FieldInfo[] pis = t.GetFields();
            foreach (var item in pis)
            {
                if (item.FieldType.Name == t.Name)
                {
                    ls.Add((TEnum)item.GetValue(t));
                }
            }

            return ls;
        }

        /// <summary>
        /// Is Contain
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsContain<TEnum>(TEnum value)
        {
            return GetEnumList<TEnum>().Contains(value);
        }

        /// <summary>
        /// Is Contain
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static bool IsContain<TEnum>(TEnum value, TEnum exception)
        {
            return GetEnumList<TEnum>().Where(x => !x.Equals(exception)).Contains(value);
        }

        /// <summary>
        /// Is Contain
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsContain<TEnum>(string key)
        {
            var ls = GetEnumNameList<TEnum>();

            return ls.Count(x => x == key) > 0;
        }

        /// <summary>
        /// Is Contain
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsContain<TEnum, TValue>(TValue val)
        {
            var ls = GetEnumValueList<TEnum, TValue>();

            return ls.Count(x => x.ToString() == val.ToString()) > 0;
        }

        /// <summary>
        /// To Enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TEnumValue"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TEnumValue ToEnum<TEnum, TEnumValue>(string key) where TEnumValue : struct
        {
            var info = GetMyEnumInfo<TEnum, TEnumValue>(key);

            return info.Value;
        }
    }
}
