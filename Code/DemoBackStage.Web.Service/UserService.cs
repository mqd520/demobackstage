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
    }
}
