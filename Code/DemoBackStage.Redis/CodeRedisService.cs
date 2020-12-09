using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Redis.Utils;

using DemoBackStage.Redis._00_Def;

namespace DemoBackStage.Redis
{
    public class CodeRedisService : ItemRedisService<string>, ICodeRedisService
    {
        /// <summary>
        /// Get Code
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetCode(string Id)
        {
            return GetItemByPrefix(Id);
        }

        /// <summary>
        /// Reset Code
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ResetCode(string Id, string code)
        {
            return SetItemByPrefix(Id, code);
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Remove(string Id)
        {
            return RemoveKey(GetKey(Id));
        }
    }
}
