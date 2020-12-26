using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    public class UserRoleRepository : Repository<UserRoleEntity>, IUserRoleRepository
    {
        /// <summary>
        /// Query By User Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<UserRoleEntity> QueryByUserId(int Id)
        {
            return QueryAll(x => x.UserId == Id);
        }
    }
}
