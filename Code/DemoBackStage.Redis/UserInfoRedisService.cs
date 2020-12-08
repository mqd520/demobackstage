using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServiceStack.Redis.Utils;
using DemoBackStage.Def;

namespace DemoBackStage.Redis
{
    public class UserInfoRedisService : ItemRedisService<UserInfo>, IUserInfoRedisService
    {
        private static string _redisSessionName;

        public static string RedisSessionName
        {
            get
            {
                if (string.IsNullOrEmpty(_redisSessionName))
                {
                    _redisSessionName = "DemoBackStage.Web.RedisSession";
                }

                return _redisSessionName;
            }
        }


        /// <summary>
        /// Get UserInfo
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string username)
        {
            string key = "{" + RedisSessionName + "_" + username + "}_Data";

            return GetItem(key);
        }

        /// <summary>
        /// Get All Keys By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAllKeysByUserName(string username)
        {
            IList<string> keys = new List<string>();

            string pattern = string.Format("*{0}*", RedisSessionName);
            try
            {
                using (var client = GetReadOnlyClient())
                {
                    var keys1 = client.GetKeysByPattern(pattern);
                    foreach (var item in keys1)
                    {
                        UserInfo ui = client.Get<UserInfo>(item);
                        if (ui != null && ui.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                        {
                            keys.Add(item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("UserInfoRedisService.GetAllKeysByUserName Exception: {0}", e.Message),
                    e
                );
            }

            return keys;
        }
    }
}
