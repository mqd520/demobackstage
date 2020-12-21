using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using Common;
using AutoFacUtils;
using DemoBackStage.Def;
using DemoBackStage.Web.IService;

using DemoBackStage.Web.Def;
using DemoBackStage.Web.Common;

namespace DemoBackStage.Web.Filter
{
    public class PermissionFilterAttribute : AuthorizeAttribute
    {
        public EPermissionType PermissionType { get; private set; }


        public PermissionFilterAttribute(EPermissionType type)
        {
            Order = (int)EFilterOrder.Permission;
            PermissionType = type;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool bSuccess = false;

            string path = httpContext.Request.Url.AbsolutePath;

            try
            {
                var srv = AutoFacHelper.Get<IPermissionService>();
                var srv1 = AutoFacHelper.Get<IUserService>();

                // 判断当前请求是否为请求一个Html页面
                if (PermissionType == EPermissionType.View &&
                    httpContext.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) &&
                    !httpContext.Request.Headers.AllKeys.Contains("X-Requested-With") &&
                    (httpContext.Request.Headers["Accept"].Contains("text/html") || httpContext.Request.ContentType.Contains("text/html")))
                {
                    IList<EPermissionType> ls = new List<EPermissionType>();

                    if (srv1.IsAdministrator())
                    {
                        ls = EnumTool.GetEnumList<EPermissionType>();
                        bSuccess = true;
                    }
                    else
                    {
                        var ls1 = srv.GetLoginUserPermissions(path);
                        var p = ((int)PermissionType).ToString();
                        bSuccess = ls1.Count(x => x.Permissions.Contains(p)) > 0;
                        if (bSuccess)
                        {
                            foreach (var item in ls1)
                            {
                                string[] arr = item.Permissions.Split(',');
                                if (arr != null && arr.Length > 0)
                                {
                                    foreach (var item1 in arr)
                                    {
                                        int n = 0;
                                        bool b = int.TryParse(item1, out n);
                                        if (b)
                                        {
                                            ls.Add((EPermissionType)n);
                                        }
                                    }
                                }
                            }
                            ls = ls.Distinct().ToList();
                        }
                    }

                    if (bSuccess)
                    {
                        string json = string.Format("[{0}]", ls.ConcatElement(", ", x => ((int)x).ToString()));
                        string str1 = string.Format("<script type=\"text/javascript\">var {0} = {1};</script>", MyConfig.PermissionVar, json);
                        httpContext.Response.Write(str1);
                    }
                }
                else
                {
                    if (srv.IsLoginUserPermission(path, PermissionType))
                    {
                        bSuccess = true;
                    }
                    else
                    {
                        bSuccess = false;
                    }
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