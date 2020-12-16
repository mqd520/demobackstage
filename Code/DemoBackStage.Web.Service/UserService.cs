using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using SqlSugar;

using Common;
using AutoFacUtils;
using DemoBackStage.Entity;
using DemoBackStage.View;
using DemoBackStage.Web.IService;
using DemoBackStage.IRepository;
using DemoBackStage.Def;
using DemoBackStage.Redis;
using DemoBackStage.DAL;

using DemoBackStage.Web.Service._01_Config;
using DemoBackStage.Web.Service._02_Common;

namespace DemoBackStage.Web.Service
{
    public class UserService : Service<UserInfoEntity>, IUserService
    {
        #region Property
        public virtual IUserInfoRepository GetUserInfoRepository() { return AutoFacHelper.Get<IUserInfoRepository>(); }

        public virtual IUserLoginLogRepository GetUserLoginLogRepository() { return AutoFacHelper.Get<IUserLoginLogRepository>(); }
        #endregion


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public virtual EUserLoginResult Login(string username, string pwd)
        {
            EUserLoginResult result = EUserLoginResult.Fail;

            string pwd1 = MyCommonTool.Encrypt(pwd);
            try
            {
                var entity = GetUserInfoRepository().QueryByUserName(username);
                if (entity != null)
                {
                    if (entity.Pwd.Equals(pwd1, StringComparison.OrdinalIgnoreCase))
                    {
                        string sessionId = HttpContext.Current.Session.SessionID;
                        HttpContext.Current.Session[DefConsts.RedisKey_UserInfo] = new UserInfo
                        {
                            Id = entity.Id,
                            NickName = entity.NickName,
                            RegIp = entity.RegIp,
                            RegTime = entity.RegTime,
                            UserName = entity.UserName
                        };

                        GetUserLoginLogRepository().Add(new UserLoginLogEntity
                        {
                            Agent = HttpContext.Current.Request.UserAgent,
                            Ip = CommonTool.GetClientIp(),
                            Time = DateTime.Now,
                            UserName = username
                        });

                        result = EUserLoginResult.Success;

                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                var redis = AutoFacHelper.Get<IUserInfoRedisService>();
                                var ls = redis.GetAllKeysByUserName(username);
                                var ls1 = ls.Where(x => !x.Contains(sessionId)).ToList();
                                redis.RemoveKeys(ls1);
                            }
                            catch (Exception e)
                            {
                                string param = string.Format("username: {0}, pwd: {1}, SessionId: {2}", username, pwd, sessionId);
                                CommonLogger.WriteLog(
                                    ELogCategory.Error,
                                    string.Format("UserService.Login Task.Factory.StartNew Exception: {0}{1}{2}", e.Message, Environment.NewLine, param),
                                    e
                                );
                            }
                        });
                    }
                    else
                    {
                        result = EUserLoginResult.NotMatch;
                    }
                }
                else
                {
                    result = EUserLoginResult.NotMatch;
                }
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("UserService.Login Exception: {0}", e.Message),
                    e
                );
            }

            return result;
        }

        /// <summary>
        /// Logout
        /// </summary>
        public virtual void Logout()
        {

        }

        /// <summary>
        /// Get Login User
        /// </summary>
        /// <returns></returns>
        public virtual UserInfo GetLoginUser()
        {
            UserInfo ui = null;

            try
            {
                ui = HttpContext.Current.Session[DefConsts.RedisKey_UserInfo] as UserInfo;
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("UserService.GetLoginUser Exception: {0}", e.Message),
                    e
                );
            }

            return ui;
        }

        /// <summary>
        /// Is Login
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLogin()
        {
            var ui = GetLoginUser();

            return ui != null;
        }

        /// <summary>
        /// Is Administrator
        /// </summary>
        /// <returns></returns>
        public bool IsAdministrator()
        {
            var user = GetLoginUser();
            if (user != null)
            {
                return user.UserName.Equals(MyConfig.Administrator);
            }

            return false;
        }

        /// <summary>
        /// Get User Navs
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual IList<MenuEntity> GetUserNavs(string username)
        {
            IList<MenuEntity> ls = new List<MenuEntity>();

            try
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
            catch (Exception e)
            {
                string param = string.Format("username: {0}", username);
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("UserService.GetUserNavs Exception: {0}{1}{2}", e.Message, Environment.NewLine, param),
                    e
                );
            }

            return ls;
        }

        /// <summary>
        /// Get Login User Navs
        /// </summary>
        /// <returns></returns>
        public virtual IList<MenuEntity> GetLoginUserNavs()
        {
            var user = GetLoginUser();
            if (user != null)
            {
                return GetUserNavs(user.UserName);
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
                    string.Format("UserService.GetUserPermissions Exception: {0}", e.Message),
                    e
                );
            }

            return ls;
        }

        /// <summary>
        /// Is Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool IsPermission(string url, int userid, EPermissionType type)
        {
            var p = ((int)type).ToString();
            var ls = GetUserPermissions(userid);

            return ls.Count(x => x.Permissions.Contains(p)) > 0;
        }

        /// <summary>
        /// Is Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public bool IsPermission(string url, int userid, IEnumerable<EPermissionType> types)
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
        public bool IsLoginUserPermission(string url, EPermissionType type)
        {
            var user = GetLoginUser();
            if (user != null)
            {
                return IsPermission(url, user.Id, type);
            }

            return false;
        }

        /// <summary>
        /// Is LoginUser Permission
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsLoginUserPermission(string url, IEnumerable<EPermissionType> types)
        {
            var user = GetLoginUser();
            if (user != null)
            {
                return IsPermission(url, user.Id, types);
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
