using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
