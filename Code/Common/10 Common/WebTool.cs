using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace Common
{
    public static class WebTool
    {
        public static string GetHttpRequestInfo(HttpContext context)
        {
            string str = "";

            str += string.Format("{0}Url: {1}", Environment.NewLine, context.Request.Url.AbsolutePath);
            str += string.Format("{0}Remote: {1}", Environment.NewLine, CommonTool.GetClientIp());

            string body = "";
            var stream = context.Request.InputStream;
            if (stream.CanRead)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(stream))
                {
                    body = sr.ReadToEnd();
                }
            }

            if (!string.IsNullOrEmpty(body))
            {
                str += string.Format("{0}Body: {1}", Environment.NewLine, body);
            }

            return str;
        }

        public static string GetHttpRequestInfo(HttpContextBase context)
        {
            string str = "";

            str += string.Format("{0}Url: {1}", Environment.NewLine, context.Request.Url.AbsolutePath);
            str += string.Format("{0}Remote: {1}", Environment.NewLine, CommonTool.GetClientIp());

            string body = "";
            var stream = context.Request.InputStream;
            if (stream.CanRead)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(stream))
                {
                    body = sr.ReadToEnd();
                }
            }

            if (!string.IsNullOrEmpty(body))
            {
                str += string.Format("{0}Body: {1}", Environment.NewLine, body);
            }

            return str;
        }
    }
}
