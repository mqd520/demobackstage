using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.View;
using DemoBackStage.Def;

namespace DemoBackStage.Web.IService
{
    public interface IPermissionService
    {
        /// <summary>
        /// Get User Menu List
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        IList<MenuEntity> GetUserMenus(string username);

        /// <summary>
        /// Get Login User Menu List
        /// </summary>
        /// <returns></returns>
        IList<MenuEntity> GetLoginUserMenus();

        /// <summary>
        /// Get User Permissions
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        IList<UserPermissionView> GetUserPermissions(int userid);

        /// <summary>
        /// Get User Permissions
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        IList<UserPermissionView> GetUserPermissions(int userid, string url);

        /// <summary>
        /// Get Login User Permissions
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IList<UserPermissionView> GetLoginUserPermissions(string url);

        /// <summary>
        /// Is Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool IsPermission(string url, int userid, EPermissionType type);

        /// <summary>
        /// Is Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        bool IsPermission(string url, int userid, IEnumerable<EPermissionType> types);

        /// <summary>
        /// Is LoginUser Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsLoginUserPermission(string url, EPermissionType type);

        /// <summary>
        /// Is LoginUser Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsLoginUserPermission(string url, IEnumerable<EPermissionType> types);

        /// <summary>
        /// Query Role Menu List
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<RoleMenuView> QueryRoleMenus(int roleId);

        /// <summary>
        /// Reset Permission
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="dict"></param>
        bool ResetPermission(int roleId, IDictionary<int, string> dict);
    }
}
