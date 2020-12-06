using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    public static class StringEntension
    {
        public static string ReplaceRegularKeyChar(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char[] ch = new char[] { '$', '^', '[', ']' };
                foreach (var item in ch)
                {
                    str = str.Replace(item.ToString(), string.Format("\\{0}", item));
                }
            }

            return str;
        }

        /// <summary>
        /// SubString
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="bContainStart"></param>
        /// <param name="bContainEnd"></param>
        /// <param name="isAutoParaphrase"></param>
        /// <returns></returns>
        public static IList<string> SubString(this string str, string start, string end,
            bool bContainStart = false, bool bContainEnd = false, bool isAutoParaphrase = true)
        {
            IList<string> ls = new List<string>();

            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                if (isAutoParaphrase)
                {
                    start = ReplaceRegularKeyChar(start);
                    end = ReplaceRegularKeyChar(end);
                }

                string pattern = string.Format("{0}.*{1}", start, end);
                Regex reg = new Regex(pattern);
                MatchCollection mc = reg.Matches(str);
                foreach (Match item in mc)
                {
                    string val = item.Value;
                    if (!string.IsNullOrEmpty(val))
                    {
                        if (!bContainStart)
                        {
                            Regex reg1 = new Regex(string.Format("^{0}", start));
                            val = reg1.Replace(val, "");
                        }
                        if (!bContainEnd)
                        {
                            Regex reg2 = new Regex(string.Format("{0}$", end));
                            val = reg2.Replace(val, "");
                        }

                        ls.Add(val);
                    }
                }
            }

            return ls;
        }
    }
}
