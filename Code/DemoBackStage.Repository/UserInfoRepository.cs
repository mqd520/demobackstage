using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

using DemoBackStage.Repository._01_Config;

namespace DemoBackStage.Repository
{
    /// <summary>
    /// UserInfo Repository
    /// </summary>
    public class UserInfoRepository : Repository<UserInfoEntity>, IUserInfoRepository
    {
        /// <summary>
        /// Get or Set IsAdministrator
        /// </summary>
        public virtual bool IsAdministrator { get; set; } = false;


        /// <summary>
        /// Query By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual UserInfoEntity QueryByUserName(string username)
        {
            return QuerySingle(x => x.UserName == username);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual IList<UserInfoEntity> QueryPaging(int page, int size, out int count,
            string username)
        {
            var ls = new List<Expression<Func<UserInfoEntity, bool>>>();

            if (!string.IsNullOrEmpty(username))
            {
                ls.Add(x => x.UserName.Contains(username));
            }
            if (!IsAdministrator)
            {
                ls.Add(x => x.UserName != MyConfig.Administrator);
            }

            return QueryPaging(page, size, out count, ls);
        }
    }
}
