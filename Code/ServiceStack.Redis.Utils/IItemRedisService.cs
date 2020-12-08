using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStack.Redis.Utils
{
    public interface IItemRedisService<T> : IRedisService<T>
    {
        /// <summary>
        /// Get Item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetItem(string key);

        /// <summary>
        /// Get Item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetItemByPrefix(string key);

        /// <summary>
        /// Set Item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        bool SetItem(string key, T item);

        /// <summary>
        /// Set Item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        bool SetItemByPrefix(string key, T item);

        /// <summary>
        /// Get All Items
        /// </summary>
        /// <returns></returns>
        IList<T> GetAllItems();
    }
}
