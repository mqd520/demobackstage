using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;

namespace DemoBackStage.Web.Filter
{
    public class MySecurityFilterAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool intercept = true;
            var error = MySecurity.ProcessContext(httpContext, out intercept);
            if (intercept)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Security,
                    string.Format("Intercept Web Danger Data: {0}", error)
                );
            }

            if (intercept)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.ClearContent();
            filterContext.HttpContext.Response.ClearHeaders();
            filterContext.HttpContext.Response.StatusCode = 403;
            filterContext.Result = new ContentResult
            {
                Content = "403"
            };
        }
    }
}