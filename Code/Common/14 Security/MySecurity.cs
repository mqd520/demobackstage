using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;


namespace Common
{
    public static class MySecurity
    {
        static MySecurity()
        {

        }

        public static string ProcessContext(HttpContextBase context, out bool intercept)
        {
            var ls = new List<string>();

            var query = context.Request.Url.Query;
            if (!string.IsNullOrEmpty(query))
            {
                ls.Add(query);
            }

            var headers = context.Request.Headers;
            foreach (var item in headers.AllKeys)
            {
                ls.Add(headers[item]);
            }

            var s = context.Request.InputStream;
            if (s != null && s.CanSeek)
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    ls.Add(sr.ReadToEnd());
                }
            }

            var str1 = MySQLSecurity.ProcessSQL(context, ls, out intercept);

            return str1;
        }
    }
}
