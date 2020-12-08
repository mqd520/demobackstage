using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace DemoBackStage.Def
{
    public enum EUserLoginResult
    {
        /// <summary>
        /// Success
        /// </summary>
        [MyEnumAttr(desc: "登录成功")]
        Success = 0,

        /// <summary>
        /// Fail
        /// </summary>
        [MyEnumAttr(desc: "登录失败, 系统异常")]
        Fail = 1,

        /// <summary>
        /// UserName and Pwd Not Match
        /// </summary>
        [MyEnumAttr(desc: "用户名或密码错误")]
        NotMatch = 2,

        /// <summary>
        /// Code Invalid
        /// </summary>
        [MyEnumAttr(desc: "验证码已超时")]
        CodeInvalid = 3,

        /// <summary>
        /// Code Error
        /// </summary>
        [MyEnumAttr(desc: "验证码错误")]
        CodeError = 4
    }
}
