using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace DemoBackStage.Web.App_Start
{
    /// <summary>
    /// Newtonsoft Config
    /// </summary>
    public class NewtonsoftConfig
    {
        /// <summary>
        /// Init
        /// </summary>
        public static void Init()
        {
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss"
                };
            };
        }
    }
}