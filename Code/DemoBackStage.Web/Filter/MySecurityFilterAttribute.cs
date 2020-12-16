using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;

namespace DemoBackStage.Web.Filter
{
    public class MySecurityFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool intercept = true;
            var error = MySecurity.ProcessContext(filterContext.HttpContext, out intercept);
            if (intercept)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Security,
                    string.Format("Intercept Web Danger Data: {0}", error)
                );

                filterContext.HttpContext.Response.ClearContent();
                filterContext.HttpContext.Response.ClearHeaders();
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new ContentResult
                {
                    Content = "403"
                };

                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}