using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;

namespace Common
{
    public static class MySQLSecurity
    {
        public static string Sql { get; private set; }


        static MySQLSecurity()
        {

        }

        public static string IsContainSQL(HttpContextBase context, IList<string> ls, out bool intercept)
        {
            intercept = false;
            string str = "";

            var sqls = MySecurityConfig.Sqls;
            foreach (var item in sqls)
            {
                string pattern = string.Format("{0}\\s+", item);
                var reg = new Regex(pattern);

                foreach (var item1 in ls)
                {
                    if (reg.IsMatch(item1))
                    {
                        str = item1;
                        intercept = true;

                        break;
                    }
                }

                if (intercept)
                {
                    break;
                }
            }

            return str;
        }

        public static string ProcessSQL(HttpContextBase context, IList<string> ls, out bool intercept)
        {
            return IsContainSQL(context, ls, out intercept);
        }
    }
}
