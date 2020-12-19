using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Def
{
    /// <summary>
    /// Filter Order
    /// </summary>
    public enum EFilterOrder : int
    {
        /// <summary>
        /// Validator FilterAttribute
        /// </summary>
        Validator = 30,

        /// <summary>
        /// Permission
        /// </summary>
        Permission = 10,

        /// <summary>
        /// Login
        /// </summary>
        Login = 2,

        /// <summary>
        /// MySecurity FilterAttribute
        /// </summary>
        Security = 1
    }
}