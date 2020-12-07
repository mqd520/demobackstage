using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ServiceStack.Redis.Utils;

namespace DemoBackStage.Web.App_Start
{
    public class RedisUtilsConfig
    {
        public static void Init()
        {
            ServiceStackRedisUtils.Init();
        }
    }
}