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
        }

        /// <summary>
        /// Get Administrator
        /// </summary>
        public static string Administrator { get; private set; }
    }
}
