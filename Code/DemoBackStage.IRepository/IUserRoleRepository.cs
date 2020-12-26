using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;

namespace DemoBackStage.IRepository
{
    public interface IUserRoleRepository : IRepository<UserRoleEntity>
    {
        /// <summary>
        /// Query By User Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IList<UserRoleEntity> QueryByUserId(int Id);
    }
}
