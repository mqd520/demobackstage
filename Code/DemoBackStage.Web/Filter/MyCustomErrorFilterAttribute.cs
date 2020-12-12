using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using Common;

namespace DemoBackStage.Web.Filter
{
    public class MyCustomErrorFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string path = filterContext.HttpContext.Request.Url.AbsolutePath;
            string body = "";
            var stream = filterContext.HttpContext.Request.InputStream;
            if (stream.CanRead)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(stream))
                {
                    body = sr.ReadToEnd();
                }
            }

            string str = string.Format("Url: {0}", path);
            if (!string.IsNullOrEmpty(body))
            {
                str += string.Format("{0}Body: {1}", Environment.NewLine, body);
            }

            CommonLogger.WriteLog(
                ELogCategory.Fatal,
                string.Format("MyCustomErrorFilterAttribute.OnException, Msg: {0}{1}{2}",
                    filterContext.Exception.Message,
                    Environment.NewLine,
                    str
                ),
                filterContext.Exception
            );

            if (!filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Server.ClearError();
                filterContext.HttpContext.Response.StatusCode = 500;
            }
        }
    }
}