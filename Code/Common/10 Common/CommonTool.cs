using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;
using System.Reflection;

using Newtonsoft.Json;

namespace Common
{
    /// <summary>
    /// CommonTool
    /// </summary>
    public static class CommonTool
    {
        /// <summary>  
        /// GetTimeStamp
        /// </summary>  
        /// <returns></returns>  
        public static Int32 GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// Get Misco TimeStamp
        /// </summary>
        /// <returns></returns>
        public static Int64 GetMiscoTimeStamp()
        {
            return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000);
        }

        /// <summary>
        /// From TimeStamp to DataTime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="isMill"></param>
        /// <returns></returns>
        public static DateTime FromTimeStamp(Int64 timestamp, bool isMill = false)
        {
            var date = new DateTime(1970, 1, 1, 8, 0, 0).AddMilliseconds(isMill ? timestamp : timestamp * 1000);

            return date;
        }

        /// <summary>
        /// Get DateTime From TimeStamp
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime GetBJDateTimeFromTimeStamp(int timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 8, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// Get String EndChar Byte Length
        /// </summary>
        /// <param name="encode">encode</param>
        /// <returns>int</returns>
        public static int GetStringEndCharByteLength(Encoding encode)
        {
            if (encode == Encoding.UTF8)
            {
                return 1;
            }

            if (encode == Encoding.UTF32)
            {
                return 4;
            }

            if (encode == Encoding.GetEncoding("GB2312"))
            {
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// Get String Byte Count
        /// </summary>
        /// <param name="str">str</param>
        /// <param name="encode">encode</param>
        /// <param name="prefix">prefix</param>
        /// <param name="hasEndChar">has EndChar</param>
        /// <returns></returns>
        public static int GetStringByteCount(string str, Encoding encode,
            EStringPrefixLen prefix = EStringPrefixLen.Int32, bool hasEndChar = true)
        {
            int count = (int)prefix;

            if (!string.IsNullOrEmpty(str))
            {
                count += encode.GetByteCount(str);
            }

            if (hasEndChar)
            {
                count += GetStringEndCharByteLength(encode);
            }

            return count;
        }

        /// <summary>
        /// Get String Byte Count
        /// </summary>
        /// <param name="str">str</param>
        /// <param name="prefix">prefix</param>
        /// <param name="hasEndChar">has EndChar</param>
        /// <returns></returns>
        public static int GetStringByteCount(string str,
            EStringPrefixLen prefix = EStringPrefixLen.Int32, bool hasEndChar = true)
        {
            return GetStringByteCount(str, Encoding.UTF8, prefix, hasEndChar);
        }

        /// <summary>
        /// Parse Cookies
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IList<HttpCookie> ParseCookies(string str)
        {
            IList<HttpCookie> ls = new List<HttpCookie>();

            if (!string.IsNullOrEmpty(str))
            {
                string str1 = str.Replace(", ", "####");
                string[] arr = str1.Split(',');
                foreach (var item in arr)
                {
                    ls.Add(ParseCookie(item.Trim().Replace("####", ", ")));
                }
            }

            return ls;
        }

        /// <summary>
        /// Parse Cookie
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static HttpCookie ParseCookie(string str)
        {
            HttpCookie cookie = new HttpCookie("");

            string[] arr1 = str.Split(';');
            foreach (var item in arr1)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (item.Contains("="))
                    {
                        int index = item.Trim().IndexOf("=");
                        string name = item.Trim().Substring(0, index);
                        string value = item.Trim().Substring(index + 1);

                        if (name.Equals("path", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Path = value;
                        }
                        else if (name.Equals("expires", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Expires = Convert.ToDateTime(value);
                        }
                        else if (name.Equals("SameSite", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Shareable = value.Equals("None", StringComparison.OrdinalIgnoreCase) ? true : false;
                        }
                        else if (name.Equals("Domain", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Domain = value;
                        }
                        else if (name.Equals("Max-Age", StringComparison.OrdinalIgnoreCase))
                        {
                            int age = Convert.ToInt32(value);
                            if (age == 0)
                            {
                                cookie.Expires = DateTime.Now.AddDays(-1);
                            }
                            else
                            {
                                cookie.Expires = DateTime.Now.AddSeconds(age);
                            }
                        }
                        else
                        {
                            cookie.Name = name;
                            cookie.Value = value;
                        }
                    }
                    else
                    {
                        if (item.Trim().Equals("secure", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Secure = true;
                        }
                        else if (item.Trim().Equals("HttpOnly", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.HttpOnly = true;
                        }
                    }
                }
            }

            if (cookie.Expires == DateTime.MinValue)
            {
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            return cookie;
        }

        /// <summary>
        /// Data To Dict
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static Dictionary<string, string> DataToDict<T>(T data)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            foreach (var item in pis)
            {
                var obj = item.GetValue(data);
                string val = obj != null ? obj.ToString() : "";
                dict.Add(item.Name, val);
            }

            return dict;
        }

        /// <summary>
        /// Dict To Data
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="dict">dict</param>
        /// <returns></returns>
        public static T DictToData<T>(Dictionary<string, string> dict)
        {
            string json = JsonConvert.SerializeObject(dict);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Get Client Ip
        /// </summary>
        /// <param name="bIsIgnoreAgent">Is Ignore Agent</param>
        /// <returns></returns>
        public static string GetClientIp(bool bIsIgnoreAgent = true)
        {
            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (bIsIgnoreAgent)
            {
                string str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(str))
                {
                    str = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                }

                if (!string.IsNullOrEmpty(str))
                {
                    ip = str;
                }
            }

            return ip;
        }
    }
}
