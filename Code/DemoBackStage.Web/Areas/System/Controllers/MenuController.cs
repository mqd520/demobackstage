using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using Common;
using AutoFacUtils;
using DemoBackStage.Def;
using DemoBackStage.Entity;
using DemoBackStage.IRepository;

using DemoBackStage.Web.ViewData;
using DemoBackStage.Web.Common;
using DemoBackStage.Web.Filter;
using DemoBackStage.Web.Areas.System.Models;
using DemoBackStage.Web.Validator.System;

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

        [HttpPost]
        [PermissionFilter(EPermissionType.Add)]
        [ValidatorFilter(typeof(AddMenuModelValidator), typeof(AddMenuModel))]
        public ActionResult Add(AddMenuModel p)
        {
            bool b = false;
            string msg = "";

            try
            {
                var srv = GetMenuRepository();
                int n = srv.Add(new MenuEntity
                {
                    isdir = p.IsDir ? 1 : 0,
                    Level = p.Level,
                    Name = p.Name,
                    ParentId = p.ParentId,
                    Rank = p.Rank,
                    Remark = p.Remark,
                    Url = p.Url
                });

                b = n > 0;
                if (!b)
                {
                    msg = "新增菜单数据失败, 请稍后再试或联系管理员!";
                }
            }
            catch (Exception e)
            {
                string str = JsonConvert.SerializeObject(p);
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("MenuController.Add Exception: {0}{1}{2}", e.Message, Environment.NewLine, str),
                    e
                );

                b = false;
                msg = "新增菜单数据失败, 请稍后再试或联系管理员!";
            }

            return new MyJsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = new { Success = b, Msg = msg }
            };
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.Delete)]
        public ActionResult Delete(int id)
        {
            bool b = false;
            string msg = "";

            try
            {
                var srv = GetMenuRepository();
                int n = srv.DeleteAllById(id);
                b = n > 0;
                if (!b)
                {
                    msg = "删除菜单数据失败, 请稍后再试或联系管理员";
                }
            }
            catch (Exception e)
            {
                string str = string.Format("id: {0}", id);
                CommonLogger.WriteLog(
                     ELogCategory.Fatal,
                     string.Format("MenuController.Delete Exception: {0}{1}{2}", e.Message, Environment.NewLine, str),
                     e
                );

                b = false;
                msg = "系统异常, 请稍后再试或联系管理员!";
            }

            return new MyJsonResult
            {
                Data = new { Success = b, Msg = msg },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}