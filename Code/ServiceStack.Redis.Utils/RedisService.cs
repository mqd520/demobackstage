using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServiceStack.Redis.Utils
{
    public class RedisService<T> : IRedisService<T>
    {
        #region Property
        public string Prefix { get; set; } = "";

        public int? Db { get; set; } = null;

        public TimeSpan? ExpireTs { get; set; } = null;
        #endregion


        /// <summary>
        /// Get Type Name
        /// </summary>
        /// <returns></returns>
        public string GetTypeName()
        {
            Type t = typeof(T);

            return t.Name;
        }

        /// <summary>
        /// Get Client
        /// </summary>
        /// <returns></returns>
        public IRedisClient GetClient()
        {
            var client = ServiceStackRedisUtils.GetClient();
            if (Db.HasValue)
            {
                client.Db = Db.Value;
            }

            return client;
        }

        /// <summary>
        /// Get ReadOnly Client
        /// </summary>
        /// <returns></returns>
        public IRedisClient GetReadOnlyClient()
        {
            var client = ServiceStackRedisUtils.GetReadOnlyClient();
            if (Db.HasValue)
            {
                client.Db = Db.Value;
            }

            return client;
        }

        /// <summary>
        /// Get All Keys
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllKeys()
        {
            IEnumerable<string> ls = new List<string>();

            string pattern = string.Format("{0}*");

            try
            {
                using (var client = GetReadOnlyClient())
                {
                    ls = client.GetKeysByPattern(pattern);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.WriteLine(
                    ELogCategory.Error,
                    string.Format("RedisService<{0}>.GetAllKeys Exception: {1}", GetTypeName(), e.Message),
                    true,
                    e: e
                );
            }

            return ls;
        }

        /// <summary>
        /// Reset Expire Time
        /// </summary>
        /// <param name="key"></param>
        public void ResetExpireTime(string key)
        {
            if (ExpireTs.HasValue)
            {
                try
                {
                    using (var client = GetClient())
                    {
                        client.ExpireEntryIn(key, ExpireTs.Value);
                    }
                }
                catch (Exception e)
                {
                    string paramStr = string.Format("key: {0}", key);
                    ConsoleHelper.WriteLine(
                        ELogCategory.Error,
                        string.Format("RedisService<{0}>.ResetExpireTime Exception: {1}{2}{3}", GetTypeName(), e.Message, Environment.NewLine, paramStr),
                        true,
                        e: e
                    );
                }
            }
        }

        /// <summary>
        /// Get All Items
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAllItems()
        {
            IList<T> ls = new List<T>();

            try
            {
                var keys = GetAllKeys();
                using (var client = GetReadOnlyClient())
                {
                    var dict = client.GetAll<T>(keys);
                    ls = dict.Select(x => x.Value).ToList();
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.WriteLine(
                    ELogCategory.Error,
                    string.Format("RedisService<{0}>.GetAllItems Exception: {1}", GetTypeName(), e.Message),
                    true,
                    e: e
                );
            }

            return ls;
        }
    }
}