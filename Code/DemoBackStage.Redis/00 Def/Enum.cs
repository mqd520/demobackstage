using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBackStage.Redis._00_Def
{
    /// <summary>
    /// Code
    /// </summary>
    public enum ECode
    {
        /// <summary>
        /// 验证通过
        /// </summary>
        Success = 1,

        /// <summary>
        /// 验证码已过期
        /// </summary>
        Expire = 2,

        /// <summary>
        /// 验证码错误
        /// </summary>
        Error = 3
    }
}
