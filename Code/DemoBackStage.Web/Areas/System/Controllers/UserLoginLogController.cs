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
using DemoBackStage.Web.IService;

using DemoBackStage.Web.Common;
using DemoBackStage.Web.Filter;
using DemoBackStage.Web.ViewData;
using DemoBackStage.Web.Areas.System.Models;
using DemoBackStage.Web.Validator.System;

namespace DemoBackStage.Web.Areas.System.Controllers
{
    public class UserLoginLogController : Controller
    {
        #region Property
        private IUserLoginLogRepository GetUserLoginLogRepository() { return AutoFacHelper.Get<IUserLoginLogRepository>(); }

        private IUserService GetUserService() { return AutoFacHelper.Get<IUserService>(); }

        private IUserLoginLogService GetUserLoginLogService() { return AutoFacHelper.Get<IUserLoginLogService>(); }
        #endregion


        [PermissionFilter(EPermissionType.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [PermissionFilter(EPermissionType.View)]
        [ValidatorFilter(typeof(UserLoginLogQueryModelValidator), typeof(UserLoginLogQueryModel))]
        public ActionResult List(UserLoginLogQueryModel p)
        {
            int count = 0;
            IList<UserLoginLogEntity> ls = new List<UserLoginLogEntity>();

            try
            {
                var srv = GetUserLoginLogService();
                ls = srv.QueryPaging(p.pageIndex + 1, p.pageSize, out count,
                    p.UserName, p.Ip, p.StartTime, p.EndTime, p.sortField, p.sortOrder);
            }
            catch (Exception e)
            {
                string paramStr = JsonConvert.SerializeObject(p);
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("UserLoginLogController.List Exception: {0}{1}{2}", e.Message, Environment.NewLine, paramStr),
                    e
                );
            }

            return new MyJsonResult
            {
                ContentType = "application/json",
                Data = new MiniUIDataGridVD<UserLoginLogEntity>
                {
                    data = ls,
                    total = count
                },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}