using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class HttpWebRequestProxySectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var item in section.ChildNodes.OfType<XmlElement>())
            {
                dict.Add(item.Attributes["key"].InnerText, item.Attributes["value"].InnerText);
            }

            var config = new WebProxyConfig();

            if (dict.Keys.Contains("enable") && !string.IsNullOrEmpty(dict["enable"]))
            {
                bool b = false;
                bool b1 = bool.TryParse(dict["enable"], out b);
                if (b1)
                {
                    config.Enable = b;
                }
            }

            if (dict.Keys.Contains("host") && !string.IsNullOrEmpty(dict["host"]))
            {
                config.Host = dict["host"];
            }

            if (dict.Keys.Contains("port") && !string.IsNullOrEmpty(dict["port"]))
            {
                int n = 0;
                bool b1 = int.TryParse(dict["port"], out n);
                if (b1)
                {
                    config.Port = n;
                }
            }

            return config;
        }
    }
}
