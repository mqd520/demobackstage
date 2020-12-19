using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace DemoBackStage.Web.Common
{
    public static class MyConfig
    {
        static MyConfig()
        {
            NameValueCollection nvc = ConfigurationManager.AppSettings;

            int n1 = 0;
            bool b1 = int.TryParse(nvc["ValidationCode_Timeout"], out n1);
            if (b1)
            {
                ValidationCodeTimeout = n1;
            }

            string str2 = nvc["ValidationCode_Prefix"];
            if (!string.IsNullOrEmpty(str2))
            {
                ValidationCodePrefix = str2;
            }

            int n3 = 0;
            bool b3 = int.TryParse(nvc["ValidationCode_Db"], out n3);
            if (b3)
            {
                ValidationCode_Db = n3;
            }

            string str4 = nvc["Administrator"];
            if (!string.IsNullOrEmpty(str4))
            {
                Administrator = str4;
            }

            bool n5 = false;
            bool b5 = bool.TryParse(nvc["EnableWebSecurity"], out n5);
            if (b5)
            {
                EnableWebSecurity = n5;
            }

            string str6 = nvc["PermissionVar"];
            if (!string.IsNullOrEmpty(str6))
            {
                PermissionVar = str6;
            }
        }

        /// <summary>
        /// Get ValidationCode_Timeout
        /// </summary>
        public static int ValidationCodeTimeout { get; private set; } = 180;

        /// <summary>
        /// Get ValidationCode_Prefix
        /// </summary>
        public static string ValidationCodePrefix { get; private set; } = null;

        /// <summary>
        /// Get ValidationCode_Db
        /// </summary>
        public static int? ValidationCode_Db { get; private set; } = null;

        /// <summary>
        /// Get Administrator
        /// </summary>
        public static string Administrator { get; private set; } = "Administrator";

        /// <summary>
        /// Get EnableWebSecurity
        /// </summary>
        public static bool EnableWebSecurity { get; private set; } = true;

        /// <summary>
        /// Get PermissionVar
        /// </summary>
        public static string PermissionVar { get; private set; } = "__$$hMyPermissions";
    }
}