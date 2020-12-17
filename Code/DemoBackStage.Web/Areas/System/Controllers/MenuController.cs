using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using Common;
using AutoFacUtils;
using DemoBackStage.Def;
using DemoBackStage.Entity;
using DemoBackStage.IRepository;

using DemoBackStage.Web.ViewData;
using DemoBackStage.Web.Common;
using DemoBackStage.Web.Filter;

namespace DemoBackStage.Web.Areas.System.Controllers
{
    public class MenuController : Controller
    {
        #region Property
        IMenuRepository GetMenuRepository() { return AutoFacHelper.Get<IMenuRepository>(); }
        #endregion


        [PermissionFilter(EPermissionType.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        public ActionResult List()
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
                    string.Format("MenuController.List Exception: {0}", e.Message),
                    e
                );

                b = false;
                msg = "查询数据失败, 请稍后再试或联系管理员";
            }

            return new MyJsonResult
            {
                Data = new { Success = b, Msg = msg, Data = ls },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}