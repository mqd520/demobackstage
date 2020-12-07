using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using ValidationCodeHelper;

using DemoBackStage.Web.Def;
using DemoBackStage.Web.App_Start;
using DemoBackStage.Web.Common;

namespace DemoBackStage.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Code()
        {
            var code = new DrawValidationCode();
            MemoryStream ms = new MemoryStream();
            code.CreateImage(ms);

            HttpCookie hc = Request.Cookies[Consts.ValicationCode];
            if (hc == null)
            {
                hc = new HttpCookie(Consts.ValicationCode);
                hc.Value = Guid.NewGuid().ToString().Replace("-", "");
            }
            hc.Expires = DateTime.Now.AddSeconds(MyConfig.ValidationCodeTimeout);
            Response.Cookies.Add(hc);

            RedisServiceConfig.CodeRedisService.Save(code.ValidationCode, hc.Value);

            Response.ContentType = "image/gif";
            Response.BinaryWrite(ms.GetBuffer());

            return new EmptyResult();
        }
    }
}