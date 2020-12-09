using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Specialized;

namespace DemoBackStage.Redis._02_Common
{
    public static class MyCommonTool
    {
        public static NameValueCollection Parameters { get; private set; }


        static MyCommonTool()
        {
            SessionStateSection section = ConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
            ProviderSettings setting = section.Providers["MySessionStateStore"];
            Parameters = setting.Parameters;
        }

        public static string GetRedisSessionName()
        {
            return Parameters["applicationName"];
        }

        public static int GetRedisSessionDb()
        {
            string str = Parameters["databaseId"];

            return Convert.ToInt32(str);
        }
    }
}
