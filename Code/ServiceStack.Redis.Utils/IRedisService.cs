using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStack.Redis.Utils
{
    public interface IRedisService<T>
    {
        string Prefix { get; set; }

        string Suffix { get; set; }

        int? Db { get; set; }

        TimeSpan? ExpireTs { get; set; }

        /// <summary>
        /// Get Key
        /// </summary>
        /// <returns></returns>
        string GetKey(string key);

        /// <summary>
        /// Get All Keys
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllKeys();

        /// <summary>
        /// Remove Key
        /// </summary>
        /// <param name="key"></param>
        bool RemoveKey(string key);

        /// <summary>
        /// Remove Keys
        /// </summary>
        /// <param name="keys"></param>
        void RemoveKeys(IEnumerable<string> keys);

        /// <summary>
        /// Reset Expire Time
        /// </summary>
        /// <param name="key"></param>
        void ResetExpireTime(string key);
    }
}
