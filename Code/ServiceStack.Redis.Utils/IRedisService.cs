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

        int? Db { get; set; }

        TimeSpan? ExpireTs { get; set; }


        /// <summary>
        /// Get All Keys
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllKeys();

        /// <summary>
        /// Reset Expire Time
        /// </summary>
        /// <param name="key"></param>
        void ResetExpireTime(string key);
    }
}
