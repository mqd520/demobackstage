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
        Validator = 1,

        /// <summary>
        /// MySecurity FilterAttribute
        /// </summary>
        Security = 100
    }
}