using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;

namespace DemoBackStage.IRepository
{
    public interface IUserLoginLogRepository : IRepository<UserLoginLogEntity>
    {
        /// <summary>
        /// QueryPaging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="username"></param>
        /// <param name="ip"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IList<UserLoginLogEntity> QueryPaging(int page, int size, out int count,
            string username,
            string ip,
            DateTime? startTime, DateTime? endTime
        );
    }
}
