using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
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
    }
}
