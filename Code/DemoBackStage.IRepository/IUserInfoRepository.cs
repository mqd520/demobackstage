using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;

namespace DemoBackStage.IRepository
{
    public interface IUserInfoRepository : IRepository<UserInfoEntity>
    {
        /// <summary>
        /// Get or Set IsAdministrator
        /// </summary>
        bool IsAdministrator { get; set; }

        /// <summary>
        /// Query By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        UserInfoEntity QueryByUserName(string username);

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        IList<UserInfoEntity> QueryPaging(int page, int size, out int count,
            string username);
    }
}
