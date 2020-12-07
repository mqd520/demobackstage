using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using Common;
using ValidationCodeHelper;

using DemoBackStage.Web.Def;
using DemoBackStage.Web.App_Start;
using DemoBackStage.Web.Common;
using DemoBackStage.Web.Models.User;
using DemoBackStage.Web.Validator;

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

        [HttpPost]
        public ActionResult Login(UserLoginInfoModel model)
        {
            bool b = false;
            string msg = "";

            try
            {
                var v = new UserLoginInfoValidator();
                var result = v.Validate(model);
                if (result.IsValid)
                {
                    //HttpCookie hc = Request.Cookies[Consts.ValicationCode];
                    //if (hc != null)
                    //{
                    //    RedisServiceConfig.CodeRedisService.GetItem("");
                    //}
                    //else
                    //{

                    //}
                    throw new Exception("未知Exception");
                }
                else
                {
                    msg = result.Errors.ConcatElement(" ");
                }
            }
            catch (Exception e)
            {
                string param = model.ToString();
                CommonLogger.WriteLog(
                    ELogCategory.Error,
                    string.Format("UserController.Login Exception: {0}{1}{2}", e.Message, Environment.NewLine, param),
                    e
                );

                msg = "登录失败, 系统异常";
            }

            return new JsonResult
            {
                ContentType = "application/json",
                Data = new { Success = b, Msg = msg },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}