using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoFacUtils;
using DemoBackStage.Redis;

using DemoBackStage.Web.Common;

namespace DemoBackStage.Web.App_Start
{
    public class RedisServiceConfig
    {
        #region Field
        private static ICodeRedisService _codeRedisService = null;
        #endregion


        #region Property
        /// <summary>
        /// Code Redis Service
        /// </summary>
        public static ICodeRedisService CodeRedisService
        {
            get
            {
                if (_codeRedisService == null)
                {
                    _codeRedisService = AutoFacHelper.Get<ICodeRedisService>();
                }

                return _codeRedisService;
            }
        }
        #endregion


        public static void Init()
        {
            CodeRedisService.Db = MyConfig.ValidationCode_Db;
            CodeRedisService.ExpireTs = new TimeSpan(0, 0, MyConfig.ValidationCodeTimeout);
            CodeRedisService.Prefix = MyConfig.ValidationCodePrefix;
        }
    }
}