using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace DemoBackStage.Web.Common
{
    public class MyJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (string.IsNullOrEmpty(response.ContentType))
            {
                if (!String.IsNullOrEmpty(ContentType))
                {
                    response.ContentType = ContentType;
                }
                else
                {
                    response.ContentType = "application/json";
                }
            }

            if (response.ContentEncoding == null)
            {
                if (ContentEncoding != null)
                {
                    response.ContentEncoding = ContentEncoding;
                }
            }

            if (Data != null)
            {
                response.Write(JsonConvert.SerializeObject(Data, Formatting.None));
            }
        }
    }
}