using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Detail Cmd Param
    /// </summary>
    public class DetailCmdParam
    {
        /// <summary>
        /// Cmd
        /// </summary>
        public string Cmd { get; set; }

        /// <summary>
        /// Param List
        /// </summary>
        public Dictionary<string, string> Params { get; set; }
    }
}