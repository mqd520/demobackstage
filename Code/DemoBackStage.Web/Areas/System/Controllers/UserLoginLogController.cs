using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using Common;
using AutoFacUtils;
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


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidatorFilter(typeof(UserLoginLogQueryModelValidator), typeof(UserLoginLogQueryModel))]
        public ActionResult List(UserLoginLogQueryModel param)
        {
            int count = 0;
            IList<UserLoginLogEntity> ls = new List<UserLoginLogEntity>();

            try
            {
                var srv = GetUserLoginLogService();
                ls = srv.QueryPaging(param.pageIndex + 1, param.pageSize, out count,
                    param.Ip, param.StartTime, param.EndTime, param.sortField, param.sortOrder);
            }
            catch (Exception e)
            {
                string paramStr = JsonConvert.SerializeObject(param);
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