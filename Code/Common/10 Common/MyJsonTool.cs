using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Common
{
    /// <summary>
    /// My Json Tool
    /// </summary>
    public static class MyJsonTool
    {
        /// <summary>
        /// Is Json String
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IsJsonStringResult IsJsonString(string json)
        {
            bool b = false;
            string msg = string.Empty;

            try
            {
                JsonConvert.DeserializeObject(json);
                b = true;
            }
            catch (Exception e)
            {
                b = false;
                msg = e.Message;
            }

            return new IsJsonStringResult
            {
                ErrorMsg = msg,
                IsJson = b
            };
        }
    }
}
