using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    public class RoleRepository : Repository<RoleEntity>, IRoleRepository
    {
        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<RoleEntity> QueryPaging(int page, int size, out int count,
            string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return QueryPaging(page, size, out count, x => x.Name.Contains(name));
            }
            else
            {
                return QueryPaging(page, size, out count);
            }
        }
    }
}
