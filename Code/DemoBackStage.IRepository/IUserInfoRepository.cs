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
        /// Query By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        UserInfoEntity QueryByUserName(string username);
    }
}
