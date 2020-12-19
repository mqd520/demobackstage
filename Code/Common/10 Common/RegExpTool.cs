using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    public static class RegExpTool
    {
        /// <summary>
        /// Is Ip
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIp(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string pattern = "((25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))";
                Regex reg = new Regex(pattern);

                return reg.IsMatch(str);
            }

            return false;
        }

        /// <summary>
        /// Is Name
        /// </summary>
        /// <param name="str"></param>
        /// <param name="minLen"></param>
        /// <param name="maxLen"></param>
        /// <returns></returns>
        public static bool IsName(string str, int minLen = 1, int maxLen = 20)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string pattern = "^[\\u4e00-\\u9fa5a-zA-Z0-9]{" + minLen + "," + maxLen + "}$";
                Regex reg = new Regex(pattern);

                return reg.IsMatch(str);
            }

            return false;
        }
    }
}
