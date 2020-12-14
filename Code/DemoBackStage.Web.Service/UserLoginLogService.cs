using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFacUtils;
using DemoBackStage.Entity;
using DemoBackStage.IRepository;
using DemoBackStage.Web.IService;

namespace DemoBackStage.Web.Service
{
    public class UserLoginLogService : Service<UserLoginLogEntity>, IUserLoginLogService
    {
        #region Property
        public IUserLoginLogRepository GetUserLoginLogRepository() { return AutoFacHelper.Get<IUserLoginLogRepository>(); }

        public IUserService GetUserService() { return AutoFacHelper.Get<IUserService>(); }
        #endregion


        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="ip"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="orderBy"></param>
        /// <param name="sort"></param>
        /// <param name="IsContainAdmin"></param>
        /// <returns></returns>
        public IList<UserLoginLogEntity> QueryPaging(int page, int size, out int count,
            string ip = null,
            DateTime? startTime = null, DateTime? endTime = null,
            string orderBy = "",
            string sort = ""
        )
        {
            var srv = GetUserService();
            var repos = GetUserLoginLogRepository();
            var b = srv.IsAdministrator();
            bool sort1 = true;
            if (!string.IsNullOrEmpty(sort) && sort.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                sort1 = false;
            }

            return repos.QueryPaging(page, size, out count, ip, startTime, endTime, orderBy, sort1, b);
        }
    }
}
