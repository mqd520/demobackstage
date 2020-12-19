using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DemoBackStage.Web.Def;
using DemoBackStage.Web.Common;
using DemoBackStage.Web.Filter;

namespace DemoBackStage.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyCustomErrorFilterAttribute());
            if (MyConfig.EnableWebSecurity)
            {
                filters.Add(new MySecurityFilterAttribute(), (int)EFilterOrder.Security);
            }
            filters.Add(new LoginAuthorizeFilterAttribute(), (int)EFilterOrder.Login);
        }
    }
}