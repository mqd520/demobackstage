﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace DemoBackStage.Web.Service._01_Config
{
    public static class MyConfig
    {
        static MyConfig()
        {
            NameValueCollection nvc = ConfigurationManager.AppSettings;

            MD5Key = nvc["MD5Key"] ?? "J7G8E1";
            Administrator = nvc["Administrator"] ?? "";
        }

        /// <summary>
        /// Get MD5Key
        /// </summary>
        public static string MD5Key { get; private set; }

        /// <summary>
        /// Get Administrator
        /// </summary>
        public static string Administrator { get; private set; }
    }
}
