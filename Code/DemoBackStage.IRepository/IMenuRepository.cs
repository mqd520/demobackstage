using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;

namespace DemoBackStage.IRepository
{
    public interface IMenuRepository : IRepository<MenuEntity>
    {
        /// <summary>
        /// Query Menus By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<MenuEntity> QueryMenusById(int id);

        /// <summary>
        /// Delete All By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteAllById(int id);
    }
}
