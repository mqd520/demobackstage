using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServiceStack.Redis.Utils
{
    public class ItemRedisService<T> : RedisService<T>, IItemRedisService<T>
    {
        /// <summary>
        /// Get Item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetItem(string key)
        {
            T t = default(T);

            try
            {
                using (var client = GetReadOnlyClient())
                {
                    t = client.Get<T>(key);
                }
            }
            catch (Exception e)
            {
                string paramStr = string.Format("key: {0}", key);
                ConsoleHelper.WriteLine(
                    ELogCategory.Error,
                    string.Format("ItemRedisService<{0}>.GetItem Exception: {1}{2}{3}", GetTypeName(), e.Message, Environment.NewLine, paramStr),
                    true,
                    e: e
                );
            }

            return t;
        }

        /// <summary>
        /// Set Item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        public bool SetItem(string key, T item)
        {
            bool b = false;

            try
            {
                using (var client = GetClient())
                {
                    if (ExpireTs.HasValue)
                    {
                        b = client.Set<T>(key, item, ExpireTs.Value);
                    }
                    else
                    {
                        b = client.Set<T>(key, item);
                    }
                }
            }
            catch (Exception e)
            {
                string paramStr = string.Format("key: {0}", key);
                ConsoleHelper.WriteLine(
                    ELogCategory.Error,
                    string.Format("ItemRedisService<{0}>.SetItem Exception: {1}{2}{3}", GetTypeName(), e.Message, Environment.NewLine, paramStr),
                    true,
                    e: e
                );
            }

            return b;
        }
    }
}
