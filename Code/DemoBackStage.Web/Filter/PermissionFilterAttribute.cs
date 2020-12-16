using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;
using AutoFacUtils;
using DemoBackStage.Def;
using DemoBackStage.Web.IService;

namespace DemoBackStage.Web.Filter
{
    public class PermissionFilterAttribute : AuthorizeAttribute
    {
        public EPermissionType PermissionType { get; private set; }


        public PermissionFilterAttribute(EPermissionType type)
        {
            PermissionType = type;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool bSuccess = false;

            string path = httpContext.Request.Url.AbsolutePath;

            try
            {
                var srv = AutoFacHelper.Get<IUserService>();
                if (srv.IsLoginUserPermission(path, PermissionType))
                {
                    bSuccess = true;
                }
                else
                {
                    bSuccess = false;
                }
            }
            catch (Exception e)
            {
                var str = MyWebTool.GetHttpRequestInfo(httpContext);
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("PermissionFilterAttribute.AuthorizeCore Exception: {0}{1}", e.Message, str),
                    e
                );
            }

            return bSuccess;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var str = MyWebTool.GetHttpRequestInfo(filterContext.HttpContext);
            CommonLogger.WriteLog(
                ELogCategory.Fatal,
                string.Format("No Permission: {0}{1}", PermissionType, str)
            );

            filterContext.HttpContext.Response.ClearContent();
            filterContext.HttpContext.Response.StatusCode = 403;
            filterContext.Result = new ContentResult
            {
                Content = "403"
            };
        }
    }
}