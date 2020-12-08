using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;
using AutoFacUtils;
using DemoBackStage.Web.IService;

namespace DemoBackStage.Web.Filter
{
    public class LoginAuthorizeFilterAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IUserService service = null;

            try
            {
                service = AutoFacHelper.Get<IUserService>();
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("LoginAuthorizeFilterAttribute.AuthorizeCore Exception: {0}", e.Message),
                    e
                );
            }

            if (service != null)
            {
                if (service.IsLogin())
                {
                    return true;
                }
            }

            return false;
        }
    }
}