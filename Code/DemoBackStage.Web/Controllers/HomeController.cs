using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoFacUtils;
using DemoBackStage.Web.IService;

using DemoBackStage.Web.Common;

namespace DemoBackStage.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Property
        private IUserService GetUserService()
        {
            return AutoFacHelper.Get<IUserService>();
        }
        #endregion


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return Redirect("/User");
        }

        [HttpPost]
        public ActionResult Nav()
        {
            var srv = GetUserService();
            var ls = srv.GetLoginUserNavs();
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

        public ActionResult Index1()
        {
            Session["key1"] = Guid.NewGuid().ToString();

            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index3()
        {
            string key = Session["key1"] as string;

            return View();
        }
    }
}