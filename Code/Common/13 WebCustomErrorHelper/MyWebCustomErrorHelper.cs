using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Net;
using System.IO;

namespace Common
{
    /// <summary>
    /// MyWebCustomErrorHelper
    /// </summary>
    public static class MyWebCustomErrorHelper
    {
        public static string DefaultErrorHtml { get; set; }


        static MyWebCustomErrorHelper()
        {
            DefaultErrorHtml = "Internal Server Error";
        }


        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        public static void Process(HttpContext context)
        {
            int code = context.Response.StatusCode;

            if (code == 200 || code == 301 || code == 302)
            {
                return;
            }

            CustomErrorsSection customErrors = ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
            if (customErrors.Mode == CustomErrorsMode.On ||
                (customErrors.Mode == CustomErrorsMode.RemoteOnly && IsRemote(context)))
            {
                string html = "";

                string path = "";
                foreach (CustomError item in customErrors.Errors)
                {
                    if (item.StatusCode == context.Response.StatusCode)
                    {
                        path = item.Redirect;

                        break;
                    }
                }

                if (string.IsNullOrEmpty(path))
                {
                    path = customErrors.DefaultRedirect;
                }

                string path1 = context.Server.MapPath(path);
                if (File.Exists(path1))
                {
                    using (FileStream fs = File.Open(path1, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        StreamReader sr = new StreamReader(fs);
                        html = sr.ReadToEnd();
                    }
                }
                else
                {
                    html = DefaultErrorHtml;
                }

                context.Response.ClearContent();
                context.Response.Clear();
                context.Response.StatusCode = code;
                context.Response.Write(html);
                context.Response.End();
            }
        }

        /// <summary>
        /// Is Remote
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsRemote(HttpContext context)
        {
            bool bRemote = true;

            string ip = context.Request.ServerVariables["REMOTE_ADDR"];
            if (ip.Equals("::1") ||
                ip.Equals("127.0.0.1"))
            {
                bRemote = false;
            }
            else
            {
                string hostname = Dns.GetHostName();
                IPAddress[] ips = Dns.GetHostEntry(hostname).AddressList;
                foreach (var item in ips)
                {
                    if (item.ToString().Equals(ip))
                    {
                        bRemote = false;

                        break;
                    }
                }
            }

            return bRemote;
        }
    }
}
