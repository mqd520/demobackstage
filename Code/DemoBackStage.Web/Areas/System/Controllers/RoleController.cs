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
            IList<RoleMenuView> ls = new List<RoleMenuView>();

            try
            {
                var srv = GetPermissionService();
                ls = srv.QueryRoleMenus(roleId);
            }
            catch (Exception e)
            {
                string paramStr = string.Format("roleId: {0}", roleId);
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("RoleController.RoleMenuList Exception: {0}{1}{2}", e.Message, Environment.NewLine, paramStr),
                    e
                );
            }

            return new MyJsonResult
            {
                ContentType = "application/json",
                Data = ls,
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}