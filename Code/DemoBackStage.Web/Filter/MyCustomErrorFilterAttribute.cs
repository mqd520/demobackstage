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
            string str = MyWebTool.GetHttpRequestInfo(filterContext.HttpContext);

            CommonLogger.WriteLog(
                ELogCategory.Fatal,
                string.Format("MyCustomErrorFilterAttribute.OnException, Msg: {0}{1}",
                    filterContext.Exception.Message,
                    str
                ),
                filterContext.Exception
            );

            if (!filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Server.ClearError();
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new ContentResult
                {
                    Content = "500"
                };
            }
        }
    }
}