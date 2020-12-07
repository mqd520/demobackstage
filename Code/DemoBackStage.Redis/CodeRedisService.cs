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
        /// Validate
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ECode Validate(string code, string Id)
        {
            string key = string.Format("{0}{1}", Prefix, Id);
            string code1 = GetItem(key);
            if (string.IsNullOrEmpty(code1))
            {
                return ECode.Expire;
            }
            else
            {
                if (code1.Equals(code, StringComparison.OrdinalIgnoreCase))
                {
                    return ECode.Success;
                }
                else
                {
                    return ECode.Error;
                }
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Save(string code, string Id)
        {
            string key = string.Format("{0}{1}", Prefix, Id);

            return SetItem(key, code);
        }
    }
}
