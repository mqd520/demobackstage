using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

using Common;
using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    public class MenuRepository : Repository<MenuEntity>, IMenuRepository
    {
        protected void QueryChildrenMenu(ISqlSugarClient db, int id, IList<MenuEntity> ls)
        {
            var ls1 = db.Queryable<MenuEntity>().Where(x => x.ParentId == id).ToList();
            foreach (var item1 in ls1)
            {
                if (item1 != null && item1.Id > 0)
                {
                    ls.Add(item1);
                }
            }

            foreach (var item1 in ls1)
            {
                if (item1 != null && item1.Id > 0 && item1.isdir == 1)
                {
                    QueryChildrenMenu(db, item1.Id, ls);
                }
            }
        }

        /// <summary>
        /// Query Menus By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IList<MenuEntity> QueryMenusById(int id)
        {
            IList<MenuEntity> ls = new List<MenuEntity>();

            using (var db = GetDb())
            {
                MenuEntity self = db.Queryable<MenuEntity>().Where(x => x.Id == id).First();
                if (self != null && self.Id > 0)
                {
                    ls.Add(self);
                    if (self.isdir == 1)
                    {
                        QueryChildrenMenu(db, self.Id, ls);
                    }
                }
            }

            return ls;
        }

        protected void QueryChildrenMenuId(ISqlSugarClient db, int id, IList<int> ls)
        {
            var ls1 = db.Queryable<MenuEntity>().Where(x => x.ParentId == id).Select(x => new { Id = x.Id, IsDir = x.isdir }).ToList();
            foreach (var item1 in ls1)
            {
                if (item1 != null && item1.Id > 0)
                {
                    ls.Add(item1.Id);
                }
            }

            foreach (var item1 in ls1)
            {
                if (item1 != null && item1.Id > 0 && item1.IsDir == 1)
                {
                    QueryChildrenMenuId(db, item1.Id, ls);
                }
            }
        }

        /// <summary>
        /// Delete All By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAllById(int id)
        {
            int n = 0;

            using (var db = GetDb())
            {
                string tableName = db.EntityMaintenance.GetTableName(typeof(MenuEntity));
                string menuId = db.EntityMaintenance.GetDbColumnName(CommonTool.GetPropertyName<MenuEntity, int>(x => x.Id), typeof(MenuEntity));
                string tableName1 = db.EntityMaintenance.GetTableName(typeof(RoleMenuEntity));
                string menuId1 = db.EntityMaintenance.GetDbColumnName(CommonTool.GetPropertyName<RoleMenuEntity, int>(x => x.MenuId), typeof(RoleMenuEntity));


                IList<int> ls = new List<int>();

                MenuEntity self = db.Queryable<MenuEntity>().Where(x => x.Id == id).First();
                if (self != null)
                {
                    ls.Add(self.Id);

                    QueryChildrenMenuId(db, self.Id, ls);
                }

                if (ls.Count > 0)
                {
                    string ids = ls.ConcatElement();

                    string sql = string.Format("delete from {0} where {1} in ({2})", tableName, menuId, ids);
                    string sql1 = string.Format("delete from {0} where {1} in ({2})", tableName1, menuId1, ids);
                    string sqls = string.Format("{0};{1}", sql, sql1);

                    try
                    {
                        db.BeginTran();
                        n = db.Ado.ExecuteCommand(sqls);
                        db.CommitTran();
                    }
                    catch (Exception e)
                    {
                        db.RollbackTran();
                        CommonLogger.WriteLog(
                            ELogCategory.Error,
                            string.Format("MenuRepository.DeleteAllById Transiaction: {0}{1}{2}", e.Message, Environment.NewLine, sqls),
                            e
                        );
                    }
                }
            }

            return n;
        }
    }
}
