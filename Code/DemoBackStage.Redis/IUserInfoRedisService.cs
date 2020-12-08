using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Redis.Utils;
using DemoBackStage.Def;

namespace DemoBackStage.Redis
{
    public interface IUserInfoRedisService : IItemRedisService<UserInfo>
    {
        /// <summary>
        /// Get UserInfo
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        UserInfo GetUserInfo(string username);

        /// <summary>
        /// Get All Keys By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        IEnumerable<string> GetAllKeysByUserName(string username);
    }
}
