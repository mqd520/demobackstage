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
    public interface IUserService : IService<UserInfoEntity>
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        EUserLoginResult Login(string username, string pwd);

        /// <summary>
        /// Logout
        /// </summary>
        void Logout();

        /// <summary>
        /// Get Login User
        /// </summary>
        /// <returns></returns>
        UserInfo GetLoginUser();

        /// <summary>
        /// Is Login
        /// </summary>
        /// <returns></returns>
        bool IsLogin();

        /// <summary>
        /// Is Administrator
        /// </summary>
        /// <returns></returns>
        bool IsAdministrator();

        /// <summary>
        /// Get User Navs
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        IList<MenuEntity> GetUserNavs(string username);

        /// <summary>
        /// Get Login User Navs
        /// </summary>
        /// <returns></returns>
        IList<MenuEntity> GetLoginUserNavs();

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
    }
}
