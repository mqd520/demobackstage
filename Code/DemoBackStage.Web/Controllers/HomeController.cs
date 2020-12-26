using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;
using AutoFacUtils;
using DemoBackStage.Web.IService;
using DemoBackStage.IRepository;

using DemoBackStage.Web.Common;
using DemoBackStage.Web.Models;
using DemoBackStage.Web.Validator;
using DemoBackStage.Web.Filter;

namespace DemoBackStage.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Property
        private IUserService GetUserService() { return AutoFacHelper.Get<IUserService>(); }

        private IPermissionService GetPermissionService() { return AutoFacHelper.Get<IPermissionService>(); }

        private IUserInfoRepository GetUserInfoRepository() { return AutoFacHelper.Get<IUserInfoRepository>(); }
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

        [HttpPost]
        [ValidatorFilter(typeof(ResetPwdModelValidator), typeof(ResetPwdModel))]
        public ActionResult ResetPwd(ResetPwdModel p)
        {
            bool b = false;
            string msg = "";

            try
            {
                var srv = GetUserService();
                var user = srv.GetLoginUser();
                if (user != null)
                {
                    var srv1 = GetUserInfoRepository();
                    b = srv1.ResetPwd(user.UserName, p.OldPwd, p.NewPwd);
                }

                if (!b)
                {
                    msg = "用户名或密码不正确!";
                }
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("HomeController.ResetPwd Exception: {0}", e.Message),
                    e
                );

                b = false;
                msg = "系统异常, 请稍后再试或联系管理员!";
            }

            return new MyJsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = new { Success = b, Msg = msg }
            };
        }

        [HttpPost]
        public ActionResult UserInfo()
        {
            var srv = GetUserService();
            var entity = srv.GetLoginUser();

            return new MyJsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = entity
            };
        }
    }
}