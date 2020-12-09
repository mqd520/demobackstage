using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Common;
using ServiceStack.Redis.Utils;
using DemoBackStage.Def;

using DemoBackStage.Redis._02_Common;

namespace DemoBackStage.Redis
{
    public class UserInfoRedisService : ItemRedisService<UserInfo>, IUserInfoRedisService
    {
        public UserInfoRedisService()
        {
            Db = MyCommonTool.GetRedisSessionDb();
        }

        /// <summary>
        /// Get UserInfo
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string username)
        {
            string key = "{" + MyCommonTool.GetRedisSessionName() + "_" + username + "}_Data";

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

            string pattern = "{" + MyCommonTool.GetRedisSessionName() + "_*";
            try
            {
                using (var client = GetReadOnlyClient())
                {
                    var keys1 = client.GetKeysByPattern(pattern);
                    foreach (var item in keys1)
                    {
                        if (item.EndsWith("_Data"))
                        {
                            string json = client.GetValueFromHash(item, DefConsts.RedisKey_UserInfo);
                            UserInfo ui = JsonConvert.DeserializeObject<UserInfo>(json);
                            if (ui != null && ui.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                            {
                                int index = item.IndexOf("_Data");
                                string str = item.Substring(0, index);
                                keys.Add(str + "_Data");
                                keys.Add(str + "_Internal");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string param = string.Format("username: {0}", username);
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("UserInfoRedisService.GetAllKeysByUserName Exception: {0}{1}{2}", e.Message, Environment.NewLine, param),
                    e
                );
            }

            return keys;
        }
    }
}
