using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace DemoBackStage.Repository._01_Config
{
    public static class MyConfig
    {
        static MyConfig()
        {
            var nvc = ConfigurationManager.AppSettings;

            Administrator = nvc["Administrator"] ?? "";
            Md5Key = nvc["MD5Key"] ?? "J7G8E1";
        }

        /// <summary>
        /// Get Administrator
        /// </summary>
        public static string Administrator { get; private set; }

        /// <summary>
        /// Get Md5Key
        /// </summary>
        public static string Md5Key { get; private set; }
    }
}
