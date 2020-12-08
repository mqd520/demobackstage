using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

using DemoBackStage.Web.Service._01_Config;

namespace DemoBackStage.Web.Service._02_Common
{
    public static class MyCommonTool
    {
        public static string Encrypt(string clearText)
        {
            return Md5EncryptionTool.Encrypt(clearText + MyConfig.MD5Key);
        }
    }
}
