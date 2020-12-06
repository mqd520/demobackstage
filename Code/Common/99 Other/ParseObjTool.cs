using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// ParseObjTool
    /// </summary>
    public static class ParseObjTool
    {
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>Dictionary(string, object)</returns>
        public static Dictionary<string, object> Parse(object obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (obj != null)
            {
                string str = obj.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    Regex reg1 = new Regex("^{", RegexOptions.IgnoreCase);
                    Regex reg2 = new Regex("}$", RegexOptions.IgnoreCase);
                    string str2 = reg1.Replace(str, "", 1);
                    string str3 = reg2.Replace(str2, "", 1).Trim();

                    string[] arr = str3.Split(',');
                    foreach (var item in arr)
                    {
                        if (item.Contains("="))
                        {
                            string[] arr1 = item.Split('=');
                            dict.Add(arr1[0].Trim(), arr1[1].TrimStart());
                        }
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>List(object)</returns>
        public static List<object> ParseAnonymouseObj(object obj)
        {
            List<object> ls = new List<object>();

            if (obj != null)
            {
                string str = obj.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    Regex reg1 = new Regex("^{", RegexOptions.IgnoreCase);
                    Regex reg2 = new Regex("}$", RegexOptions.IgnoreCase);
                    string str2 = reg1.Replace(str, "", 1);
                    string str3 = reg2.Replace(str2, "", 1).Trim();

                    string[] arr = str3.Split(',');
                    foreach (var item in arr)
                    {
                        if (item.Contains("="))
                        {
                            string[] arr1 = item.Split('=');
                            ls.Add(arr1[1].TrimStart());
                        }
                    }
                }
            }

            return ls;
        }
    }
}
