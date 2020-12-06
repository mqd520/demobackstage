using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class WebProxyConfig
    {
        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; } = "127.0.0.1";

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; } = 8888;

        /// <summary>
        /// Enable
        /// </summary>
        public bool Enable { get; set; } = false;
    }
}
