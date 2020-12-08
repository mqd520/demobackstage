using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    /// <summary>
    /// UserInfo Repository
    /// </summary>
    public class UserInfoRepository : Repository<UserInfoEntity>, IUserInfoRepository
    {
        /// <summary>
        /// Query By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfoEntity QueryByUserName(string username)
        {
            return QuerySingle(x => x.UserName == username);
        }
    }
}
