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
            string pattern = "((25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))";
            Regex reg = new Regex(pattern);

            return reg.IsMatch(str);
        }
    }
}
