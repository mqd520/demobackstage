using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;
using AutoFacUtils;
using DemoBackStage.Web.IService;

using DemoBackStage.Web.Common;

namespace DemoBackStage.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Property
        private IUserService GetUserService() { return AutoFacHelper.Get<IUserService>(); }

        private IPermissionService GetPermissionService() { return AutoFacHelper.Get<IPermissionService>(); }
        #endregion


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                Session.Abandon();
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("HomeController.Logout Exception: {0}", e.Message),
                    e
                );
            }

            return View();
        }

        [HttpPost]
        public ActionResult Nav()
        {
            var srv = GetPermissionService();
            var ls = srv.GetLoginUserMenus();
            var ls1 = ls.Select(x => WebCommonTool.MenuEntity2MenuVD(x)).ToList();

            return new JsonResult
            {
                ContentType = "application/json",
                Data = ls1,
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        [HttpPost]
        public ActionResult IsLogin()
        {
            return new JsonResult
            {
                ContentType = "application/json",
                Data = new { Success = true },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}