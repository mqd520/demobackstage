using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    public class UserLoginLogRepository : Repository<UserLoginLogEntity>, IUserLoginLogRepository
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
        /// <returns></returns>
        public IList<UserLoginLogEntity> QueryPaging(int page, int size, out int count,
            string username,
            string ip,
            DateTime? startTime, DateTime? endTime
        )
        {
            using (var db = GetDb())
            {
                var query = db.Queryable<UserLoginLogEntity>();

                if (!string.IsNullOrEmpty(username))
                {
                    query = query.Where(x => x.UserName.Contains(username));
                }
                if (!string.IsNullOrEmpty(ip))
                {
                    query = query.Where(x => x.Ip.Contains(ip));
                }
                if (startTime.HasValue)
                {
                    query = query.Where(x => x.Time >= startTime);
                }
                if (endTime.HasValue)
                {
                    query = query.Where(x => x.Time <= endTime);
                }

                count = query.Count();
                if (count > 0)
                {
                    query = query.Skip(page * size).Take(size);

                    return query.ToList();
                }
                else
                {
                    return new List<UserLoginLogEntity>();
                }
            }
        }
    }
}
