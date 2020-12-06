using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using Autofac.Integration.Mvc;

using AutoFacUtils;

namespace DemoBackStage.Web.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            AutoFacHelper.Init(new string[] { "DemoBackStage.IRepository", "DemoBackStage.Repository", "DemoBackStage.Web.IService", "DemoBackStage.Web.Service" });
            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutoFacHelper.Container));
        }
    }
}