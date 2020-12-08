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
        public virtual T GetItem(string key)
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
        /// Get Item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T GetItemByPrefix(string key)
        {
            return GetItem(GetKey(key));
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

        /// <summary>
        /// Set Item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool SetItemByPrefix(string key, T item)
        {
            return SetItem(GetKey(key), item);
        }

        /// <summary>
        /// Get All Items
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetAllItems()
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
                    string.Format("ItemRedisService<{0}>.GetAllItems Exception: {1}", GetTypeName(), e.Message),
                    true,
                    e: e
                );
            }

            return ls;
        }
    }
}
