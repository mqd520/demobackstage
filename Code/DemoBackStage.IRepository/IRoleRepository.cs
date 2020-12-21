using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;

namespace DemoBackStage.IRepository
{
    public interface IRoleRepository : IRepository<RoleEntity>
    {
        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<RoleEntity> QueryPaging(int page, int size, out int count, string name);
    }
}
