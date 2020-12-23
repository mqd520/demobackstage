using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using Common;
using AutoFacUtils;
using DemoBackStage.Def;
using DemoBackStage.Entity;
using DemoBackStage.View;
using DemoBackStage.IRepository;
using DemoBackStage.Web.IService;

using DemoBackStage.Web.Filter;
using DemoBackStage.Web.Validator.System;
using DemoBackStage.Web.Areas.System.Models;
using DemoBackStage.Web.Common;
using DemoBackStage.Web.ViewData;

namespace DemoBackStage.Web.Areas.System.Controllers
{
    public class RoleController : Controller
    {
        #region Property
        IRoleRepository GetRoleRepository() { return AutoFacHelper.Get<IRoleRepository>(); }

        IRoleMenuRepository GetRoleMenuRepository() { return AutoFacHelper.Get<IRoleMenuRepository>(); }

        IMenuRepository GetMenuRepository() { return AutoFacHelper.Get<IMenuRepository>(); }

        IPermissionService GetPermissionService() { return AutoFacHelper.Get<IPermissionService>(); }
        #endregion


        [PermissionFilter(EPermissionType.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        [ValidatorFilter(typeof(QueryRoleModelValidator), typeof(QueryRoleModel))]
        public ActionResult List(QueryRoleModel p)
        {
            int count = 0;
            IList<RoleEntity> ls = new List<RoleEntity>();

            try
            {
                var srv = GetRoleRepository();
                ls = srv.QueryPaging(p.pageIndex + 1, p.pageSize, out count, p.Name);
            }
            catch (Exception e)
            {
                string paramStr = JsonConvert.SerializeObject(p);
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("RoleController.List Exception: {0}{1}{2}", e.Message, Environment.NewLine, paramStr),
                    e
                );
            }

            return new MyJsonResult
            {
                ContentType = "application/json",
                Data = new MiniUIDataGridVD<RoleEntity>
                {
                    data = ls,
                    total = count
                },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        public ActionResult RoleMenuList(int roleId)
        {
            bool b = false;
            string msg = "";
            IList<RoleMenuView> ls = new List<RoleMenuView>();

            try
            {
                var srv = GetPermissionService();
                ls = srv.QueryRoleMenus(roleId);

                b = true;
            }
            catch (Exception e)
            {
                string paramStr = string.Format("roleId: {0}", roleId);
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("RoleController.RoleMenuList Exception: {0}{1}{2}", e.Message, Environment.NewLine, paramStr),
                    e
                );

                b = false;
                msg = "系统异常, 请稍后再试或联系管理员!";
            }

            return new MyJsonResult
            {
                ContentType = "application/json",
                Data = new { Success = b, Msg = msg, Data = ls },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        public ActionResult MenuList()
        {
            bool b = false;
            string msg = "";
            IList<MenuVD> ls = new List<MenuVD>();

            try
            {
                var srv = GetMenuRepository();
                var ls1 = srv.QueryAll();
                ls = ls1.Select(x => WebCommonTool.MenuEntity2MenuVD(x)).ToList();

                b = true;
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("RoleController.MenuList Exception: {0}", e.Message),
                    e
                );

                b = false;
                msg = "系统异常, 请稍后再试或联系管理员!";
            }

            return new MyJsonResult
            {
                ContentType = "application/json",
                Data = new { Success = b, Msg = msg, Data = ls },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.Update)]
        [ValidatorFilter(typeof(ResetPermissionModelValidator), typeof(ResetPermissionModel))]
        public ActionResult Update(ResetPermissionModel p)
        {
            bool b = false;
            string msg = "";

            try
            {
                var srv = GetPermissionService();
                var dict = new Dictionary<int, string>();
                foreach (var item in p.Items)
                {
                    if (!string.IsNullOrEmpty(item.Permissions))
                    {
                        dict.Add(item.MenuId, item.Permissions);
                    }
                }

                b = srv.ResetPermission(p.RoleId, dict);
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("RoleController.Update Exception: {0}", e.Message),
                    e
                );

                b = false;
                msg = "系统异常, 请稍后再试或联系管理员";
            }

            return new MyJsonResult
            {
                ContentType = "application/json",
                Data = new { Success = b, Msg = msg },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}