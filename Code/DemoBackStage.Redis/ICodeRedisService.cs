using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Redis.Utils;

using DemoBackStage.Redis._00_Def;

namespace DemoBackStage.Redis
{
    public interface ICodeRedisService : IRedisService<string>
    {
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        ECode Validate(string code, string Id);

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Save(string code, string Id);
    }
}
