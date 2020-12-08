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
        public virtual string GetTypeName()
        {
            Type t = typeof(T);

            return t.Name;
        }

        /// <summary>
        /// Get Client
        /// </summary>
        /// <returns></returns>
        public virtual IRedisClient GetClient()
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
        public virtual IRedisClient GetReadOnlyClient()
        {
            var client = ServiceStackRedisUtils.GetReadOnlyClient();
            if (Db.HasValue)
            {
                client.Db = Db.Value;
            }

            return client;
        }

        /// <summary>
        /// Get Key
        /// </summary>
        /// <returns></returns>
        public virtual string GetKey(string key)
        {
            if (!string.IsNullOrEmpty(Prefix))
            {
                return string.Format("{0}{1}", Prefix, key);
            }
            else
            {
                return key;
            }
        }

        /// <summary>
        /// Get All Keys
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAllKeys()
        {
            IEnumerable<string> ls = new List<string>();

            string pattern = string.Format("{0}*", Prefix);
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
        /// Remove Key
        /// </summary>
        /// <param name="key"></param>
        public bool RemoveKey(string key)
        {
            try
            {
                using (var client = GetClient())
                {
                    client.Remove(key);
                }

                return true;
            }
            catch (Exception e)
            {
                string param = string.Format("key: {0}", key);
                ConsoleHelper.WriteLine(
                    ELogCategory.Error,
                    string.Format("RedisService<{0}>.RemoveKey Exception: {1}{2}{3}", GetTypeName(), e.Message, Environment.NewLine, param),
                    true,
                    e: e
                );
            }

            return false;
        }

        /// <summary>
        /// Remove Keys
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveKeys(IEnumerable<string> keys)
        {
            try
            {
                using (var client = GetClient())
                {
                    client.RemoveAll(keys);
                }
            }
            catch (Exception e)
            {
                string param = string.Format("keys: {0}", keys.ConcatElement());
                ConsoleHelper.WriteLine(
                    ELogCategory.Error,
                    string.Format("RedisService<{0}>.RemoveKeys Exception: {1}{2}{3}", GetTypeName(), e.Message, Environment.NewLine, param),
                    true,
                    e: e
                );
            }
        }

        /// <summary>
        /// Reset Expire Time
        /// </summary>
        /// <param name="key"></param>
        public virtual void ResetExpireTime(string key)
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
    }
}