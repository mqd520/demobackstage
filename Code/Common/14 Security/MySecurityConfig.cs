using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;
using System.Configuration;

namespace Common
{
    public static class MySecurityConfig
    {
        static MySecurityConfig()
        {
            var section = ConfigurationManager.GetSection("myWebSecurity");
            var section2 = section as NameValueCollection;
            if (section2 != null)
            {
                string sql = section2["sql"];
                if (!string.IsNullOrEmpty(sql))
                {
                    Sqls = sql.Split(',').Select(x => x.Trim()).ToList();
                }
            }
        }

        /// <summary>
        /// Get Sql list
        /// </summary>
        public static IList<string> Sqls { get; private set; } = new List<string>();
    }
}
