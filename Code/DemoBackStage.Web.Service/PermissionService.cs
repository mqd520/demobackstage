using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using SqlSugar;

using Common;
using AutoFacUtils;
using DemoBackStage.Entity;
using DemoBackStage.View;
using DemoBackStage.IRepository;
using DemoBackStage.DAL;
using DemoBackStage.Web.IService;
using DemoBackStage.Def;

using DemoBackStage.Web.Service._01_Config;

namespace DemoBackStage.Web.Service
{
    public class PermissionService : IPermissionService
    {
        #region Property
        public virtual IMenuRepository GetMenuRepository() { return AutoFacHelper.Get<IMenuRepository>(); }

        public virtual IUserService GetUserService() { return AutoFacHelper.Get<IUserService>(); }
        #endregion


        /// <summary>
        /// Get User Menu List
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual IList<MenuEntity> GetUserMenus(string username)
        {
            IList<MenuEntity> ls = new List<MenuEntity>();

            try
            {
                if (username.Equals(MyConfig.Administrator))
                {
                    var srv = GetMenuRepository();
                    ls = srv.QueryAll();
                }
                else
                {
                    using (var db = SqlSugarHelper.GetDb())
                    {
                        var query = db.Queryable<UserInfoEntity, UserRoleEntity, RoleEntity, RoleMenuEntity, MenuEntity>((ui, ur, r, rm, m) =>
                            new object[] {
                            JoinType.Inner, ui.Id == ur.UserId,
                            JoinType.Inner, ur.RoleId == r.Id,
                            JoinType.Inner, r.Id == rm.RoleId,
                            JoinType.Inner, rm.MenuId == m.Id
                            }
                        ).Where((ui, ur, r, rm, m) => ui.UserName == username).Select((ui, ur, r, rm, m) => m).Distinct();

                        ls = query.ToList();
                    }
                }
            }
            catch (Exception e)
            {
                string param = string.Format("username: {0}", username);
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("PermissionService.GetUserMenus Exception: {0}{1}{2}", e.Message, Environment.NewLine, param),
                    e
                );
            }

            return ls;
        }

        /// <summary>
        /// Get Login User Menu List
        /// </summary>
        /// <returns></returns>
        public IList<MenuEntity> GetLoginUserMenus()
        {
            var srv = GetUserService();
            var user = srv.GetLoginUser();
            if (user != null)
            {
                return GetUserMenus(user.UserName);
            }

            return new List<MenuEntity>();
        }

        /// <summary>
        /// Get User Permissions
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IList<UserPermissionView> GetUserPermissions(int userid)
        {
            var ls = new List<UserPermissionView>();

            try
            {
                using (var db = SqlSugarHelper.GetDb())
                {
                    var query = db.Queryable<MenuEntity, RoleMenuEntity, RoleEntity, UserRoleEntity>((m, rm, r, ur) =>
                        new object[] {
                            JoinType.Inner, m.Id == rm.MenuId,
                            JoinType.Inner, rm.RoleId == r.Id,
                            JoinType.Inner, r.Id == ur.RoleId
                        }
                    ).Where((m, rm, r, ur) => ur.UserId == userid).Select((m, rm, r, ur) => new UserPermissionView
                    {
                        UserId = ur.UserId,
                        MenuName = m.Name,
                        MenuUrl = m.Url,
                        Permissions = rm.Permissions
                    });

                    ls = query.ToList();
                }
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Fatal,
                    string.Format("PermissionService.GetUserPermissions Exception: {0}", e.Message),
                    e
                );
            }

            return ls;
        }

        /// <summary>
        /// Get User Permissions
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual IList<UserPermissionView> GetUserPermissions(int userid, string url)
        {
            var ls = GetUserPermissions(userid);

            return ls.Where(x => url.StartsWith(x.MenuUrl ?? "")).ToList();
        }

        /// <summary>
        /// Get Login User Permissions
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual IList<UserPermissionView> GetLoginUserPermissions(string url)
        {
            var srv = GetUserService();
            var user = srv.GetLoginUser();
            if (user != null)
            {
                return GetUserPermissions(user.Id, url);
            }

            return new List<UserPermissionView>();
        }

        /// <summary>
        /// Is Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public virtual bool IsPermission(string url, int userid, EPermissionType type)
        {
            var p = ((int)type).ToString();
            var ls = GetUserPermissions(userid);

            return ls.Count(x => url.StartsWith(x.MenuUrl ?? "") && x.Permissions.Contains(p)) > 0;
        }

        /// <summary>
        /// Is Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public virtual bool IsPermission(string url, int userid, IEnumerable<EPermissionType> types)
        {
            bool b = false;

            var ls = GetUserPermissions(userid);
            foreach (var item in types)
            {
                var p = ((int)item).ToString();
                b = ls.Count(x => x.Permissions.Contains(p)) > 0;
                if (!b)
                {
                    break;
                }
            }

            return b;
        }

        /// <summary>
        /// Is LoginUser Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool IsLoginUserPermission(string url, EPermissionType type)
        {
            var srv = GetUserService();
            var user = srv.GetLoginUser();
            if (user != null)
            {
                if (user.UserName.Equals(MyConfig.Administrator))
                {
                    return true;
                }
                else
                {
                    return IsPermission(url, user.Id, type);
                }
            }

            return false;
        }

        /// <summary>
        /// Is LoginUser Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool IsLoginUserPermission(string url, IEnumerable<EPermissionType> types)
        {
            var srv = GetUserService();
            var user = srv.GetLoginUser();
            if (user != null)
            {
                if (user.UserName.Equals(MyConfig.Administrator))
                {
                    return true;
                }
                else
                {
                    return IsPermission(url, user.Id, types);
                }
            }

            return false;
        }

        /// <summary>
        /// Query Role Menu List
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<RoleMenuView> QueryRoleMenus(int roleId)
        {
            using (var db = SqlSugarHelper.GetDb())
            {
                var query = db.Queryable<RoleMenuEntity, MenuEntity>((rm, m) => new object[] { JoinType.Inner, rm.MenuId == m.Id })
                    .Where((rm, m) => rm.RoleId == roleId)
                    .Select((rm, m) => new RoleMenuView
                    {
                        RoleId = rm.RoleId,
                        Permissions = rm.Permissions,
                        MenuId = m.Id,
                        MenuIsDir = m.isdir,
                        MenuLevel = m.Level,
                        MenuName = m.Name,
                        MenuParentId = m.ParentId,
                        MenuRank = m.Rank,
                        MenuRemark = m.Remark,
                        MenuUrl = m.Url
                    });

                return query.ToList();
            }
        }

        /// <summary>
        /// Reset Permission
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="dict"></param>
        public bool ResetPermission(int roleId, IDictionary<int, string> dict)
        {
            bool b = false;

            using (var db = SqlSugarHelper.GetDb())
            {
                var table = db.EntityMaintenance.GetTableName<RoleMenuEntity>();
                string properName = CommonTool.GetPropertyName<RoleMenuEntity, int>(x => x.RoleId);
                string fieldName = db.EntityMaintenance.GetDbColumnName<RoleMenuEntity>(properName);
                string properName2 = CommonTool.GetPropertyName<RoleMenuEntity, int>(x => x.MenuId);
                string fieldName2 = db.EntityMaintenance.GetDbColumnName<RoleMenuEntity>(properName2);
                string properName3 = CommonTool.GetPropertyName<RoleMenuEntity, string>(x => x.Permissions);
                string fieldName3 = db.EntityMaintenance.GetDbColumnName<RoleMenuEntity>(properName3);

                string sql1 = string.Format("delete from {0} where {1} = {2};", table, fieldName, roleId);

                StringBuilder sb = new StringBuilder();
                SugarParameter[] paramArr = new SugarParameter[dict.Count];
                int index = 0;
                foreach (var item in dict)
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        sb.Append(string.Format("insert into {0}({1}, {2}, {3}) values({4}, {5}, @permissons_{6});",
                            table, fieldName, fieldName2, fieldName3, roleId, item.Key, index + 1));
                        paramArr[index] = new SugarParameter(string.Format("@permissons_{0}", index + 1), item.Value, System.Data.DbType.String, ParameterDirection.Input, 125);
                        index++;
                    }
                }

                string sqls = sql1 + sb.ToString();

                db.BeginTran();
                try
                {
                    db.Ado.ExecuteCommand(sqls, paramArr);
                    db.CommitTran();

                    b = true;
                }
                catch (Exception e)
                {
                    db.RollbackTran();
                    CommonLogger.WriteLog(
                        ELogCategory.Error,
                        string.Format("PermissionService.ResetPermission Trans Exception: {0}", e.Message),
                        e
                    );

                    b = false;
                }
            }

            return b;
        }

        /// <summary>
        /// Query By UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<RoleEntity> QueryByUserId(int userId)
        {
            using (var db = SqlSugarHelper.GetDb())
            {
                var query = db.Queryable<UserRoleEntity, RoleEntity>((ur, r) =>
                    new object[] {
                        JoinType.Inner, ur.RoleId == r.Id
                    }
                ).Where((ur, r) => ur.UserId == userId).Select((ur, r) => r);

                return query.ToList();
            }
        }

        /// <summary>
        /// Reset UserRole
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lsRoleId"></param>
        /// <returns></returns>
        public bool ResetUserRole(int userId, IEnumerable<int> lsRoleId)
        {
            bool b = false;

            using (var db = SqlSugarHelper.GetDb())
            {
                string table = db.EntityMaintenance.GetTableName<UserRoleEntity>();
                string property1 = CommonTool.GetPropertyName<UserRoleEntity, int>(x => x.UserId);
                string field1 = db.EntityMaintenance.GetDbColumnName<UserRoleEntity>(property1);
                string property2 = CommonTool.GetPropertyName<UserRoleEntity, int>(x => x.RoleId);
                string field2 = db.EntityMaintenance.GetDbColumnName<UserRoleEntity>(property2);

                string sql1 = string.Format("delete from {0} where {1} = {2};", table, field1, userId);
                StringBuilder sb = new StringBuilder();
                if (lsRoleId != null)
                {
                    foreach (var item in lsRoleId)
                    {
                        sb.Append(string.Format("insert into {0}({1}, {2}) values({3}, {4});", table, field1, field2, userId, item));
                    }
                }

                string sqls = sql1 + sb.ToString();

                try
                {
                    db.BeginTran();
                    int n = db.Ado.ExecuteCommand(sqls);
                    db.CommitTran();
                    b = n > 0;
                }
                catch (Exception e)
                {
                    db.RollbackTran();
                    CommonLogger.WriteLog(
                        ELogCategory.Error,
                        string.Format("PermissionService.ResetUserRole Sql Trans Exception: {0}", e.Message),
                        e
                    );
                }
            }

            return b;
        }
    }

    internal class MenuCompare : IEqualityComparer<MenuEntity>
    {
        public virtual bool Equals(MenuEntity x, MenuEntity y)
        {
            return x.Id == y.Id;
        }

        public virtual int GetHashCode(MenuEntity obj)
        {
            return obj.Id * 10 + obj.Level * 20;
        }
    }
}
