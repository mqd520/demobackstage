using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using ValidationCodeHelper;

using DemoBackStage.Web.App_Start;

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

            HttpCookie hc = new HttpCookie("ValidationCode");
            hc.Value = Guid.NewGuid().ToString().Replace("-", "");
            hc.Expires = DateTime.Now.AddMinutes(3);
            Response.Cookies.Add(hc);

            RedisServiceConfig.CodeRedisService.Save(code.ValidationCode, hc.Value);

            Response.ContentType = "image/gif";
            Response.BinaryWrite(ms.GetBuffer());

            return new EmptyResult();
        }
    }
}