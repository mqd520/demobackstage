using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;
using AutoFacUtils;
using DemoBackStage.Def;
using DemoBackStage.Entity;
using DemoBackStage.IRepository;
using DemoBackStage.Web.IService;

using DemoBackStage.Web.Common;
using DemoBackStage.Web.Filter;
using DemoBackStage.Web.ViewData;
using DemoBackStage.Web.Models;
using DemoBackStage.Web.Areas.System.Models;
using DemoBackStage.Web.Validator;
using DemoBackStage.Web.Validator.System;

namespace DemoBackStage.Web.Areas.System.Controllers
{
    public class UserRoleController : Controller
    {
        #region Property
        IUserInfoRepository GetUserInfoRepository() { return AutoFacHelper.Get<IUserInfoRepository>(); }

        IUserService GetUserService() { return AutoFacHelper.Get<IUserService>(); }

        IUserRoleRepository GetUserRoleRepository() { return AutoFacHelper.Get<IUserRoleRepository>(); }

        IPermissionService GetPermissionService() { return AutoFacHelper.Get<IPermissionService>(); }

        IRoleRepository GetRoleRepository() { return AutoFacHelper.Get<IRoleRepository>(); }
        #endregion


        [PermissionFilter(EPermissionType.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        [ValidatorFilter(typeof(QueryUserInfoModelValidator), typeof(QueryUserInfoModel))]
        public ActionResult List(QueryUserInfoModel p)
        {
            int count = 0;
            IList<UserInfoEntity> ls = new List<UserInfoEntity>();

            try
            {
                var srv1 = GetUserInfoRepository();
                ls = srv1.QueryPaging(p.pageIndex + 1, p.pageSize, out count, p.UserName);
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("UserRoleController.List Exception: {0}", e.Message),
                    e
                );

                Response.StatusCode = 500;
                return new ContentResult
                {
                    Content = "系统异常, 请稍后再试或联系管理员!"
                };
            }

            return new MyJsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = new MiniUIDataGridVD<UserInfoEntity>
                {
                    data = ls,
                    total = count
                }
            };
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        [ValidatorFilter(typeof(QueryUserRoleModelValidator), typeof(QueryUserRoleModel))]
        public ActionResult UserRoleList(QueryUserRoleModel p)
        {
            IList<RoleEntity> ls = new List<RoleEntity>();

            try
            {
                var srv = GetPermissionService();
                ls = srv.QueryByUserId(p.UserId);
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("UserRoleController.UserRoleList Exception: {0}", e.Message),
                    e
                );

                Response.StatusCode = 500;
                return new ContentResult
                {
                    Content = "系统异常, 请稍后再试或联系管理员!"
                };
            }

            return new MyJsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = ls
            };
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        [ValidatorFilter(typeof(MiniUIQueryValidator<MiniUIQueryModel>), typeof(MiniUIQueryModel))]
        public ActionResult RoleList(MiniUIQueryModel p)
        {
            IList<RoleEntity> ls = new List<RoleEntity>();

            try
            {
                var srv = GetRoleRepository();
                ls = srv.QueryAll();
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("UserRoleController.RoleList Exception: {0}", e.Message),
                    e
                );

                Response.StatusCode = 500;
                return new ContentResult
                {
                    Content = "系统异常, 请稍后再试或联系管理员!"
                };
            }

            return new MyJsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = ls
            };
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.Update)]
        [ValidatorFilter(typeof(ResetUserRoleModelValidator), typeof(ResetUserRoleModel))]
        public ActionResult Update(ResetUserRoleModel p)
        {
            bool b = false;
            string msg = "";

            try
            {
                var srv = GetPermissionService();
                b = srv.ResetUserRole(p.UserId, p.RoleIds);
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("UserRoleController.Update Exception: {0}", e.Message),
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
    }
}