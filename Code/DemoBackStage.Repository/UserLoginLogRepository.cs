﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

using DemoBackStage.Repository._01_Config;
using DemoBackStage.Repository._02_Common;

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
        /// <param name="orderBy"></param>
        /// <param name="asc"></param>
        /// <param name="IsContainAdmin"></param>
        /// <returns></returns>
        public IList<UserLoginLogEntity> QueryPaging(int page, int size, out int count,
            string username,
            string ip = null,
            DateTime? startTime = null, DateTime? endTime = null,
            string orderBy = "",
            bool asc = true,
            bool IsContainAdmin = false
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
                if (!IsContainAdmin)
                {
                    query = query.Where(x => x.UserName != MyConfig.Administrator);
                }

                count = query.Count();
                if (count > 0)
                {
                    if (!string.IsNullOrEmpty(orderBy))
                    {
                        query = MyCommonTool.AddOrderBy<UserLoginLogEntity>(db, query, orderBy, asc);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.Id, OrderByType.Desc);
                    }

                    query = query.Skip((page - 1) * size).Take(size);

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
