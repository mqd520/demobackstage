using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Redis.Utils;

using DemoBackStage.Redis._00_Def;

namespace DemoBackStage.Redis
{
    public interface ICodeRedisService : IItemRedisService<string>
    {
        /// <summary>
        /// Get Code
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetCode(string Id);

        /// <summary>
        /// Reset Code
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool ResetCode(string Id, string code);
    }
}
