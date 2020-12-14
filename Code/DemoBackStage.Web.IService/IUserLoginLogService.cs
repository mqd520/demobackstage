using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Web.IService
{
    public interface IUserLoginLogService : IService<UserLoginLogEntity>
    {
        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="username"></param>
        /// <param name="ip"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="orderBy"></param>
        /// <param name="sort"></param>
        /// <param name="IsContainAdmin"></param>
        /// <returns></returns>
        IList<UserLoginLogEntity> QueryPaging(int page, int size, out int count,
            string username,
            string ip = null,
            DateTime? startTime = null, DateTime? endTime = null,
            string orderBy = "",
            string sort = ""
        );
    }
}
