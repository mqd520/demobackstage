using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using ServiceStack.Redis.Utils._00_Def;

namespace ServiceStack.Redis.Utils._02_Common
{
    public static class HostInfoTool
    {
        public static HostInfo Parse(string host)
        {
            HostInfo info = new HostInfo();

            string host1 = host;
            if (!string.IsNullOrEmpty(host1))
            {
                string pwd = "";
                string ip = "";
                string port = "";

                int index = host1.IndexOf('@');
                if (index > -1)
                {
                    pwd = host1.Substring(0, index);
                    host1 = host1.Substring(index + 1);
                }

                string pattern = "\\:\\d{1,5}$";
                Regex reg = new Regex(pattern);
                Match ma = reg.Match(host1);
                if (ma != null && ma.Success)
                {
                    ip = host1.Substring(0, ma.Index);
                    port = host1.Substring(ma.Index + 1);
                }
                else
                {
                    ip = host1;
                    port = "6379";
                }

                info.Ip = ip;
                info.Port = Convert.ToInt32(port);
                info.Pwd = pwd;
            }

            return info;
        }
    }
}
