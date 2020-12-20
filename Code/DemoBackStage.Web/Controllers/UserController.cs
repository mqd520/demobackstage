using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using Common;
using AutoFacUtils;
using ValidationCodeHelper;
using ValidationCode1;
using DemoBackStage.Def;
using DemoBackStage.Web.IService;

using DemoBackStage.Web.Def;
using DemoBackStage.Web.App_Start;
using DemoBackStage.Web.Common;
using DemoBackStage.Web.Models.User;
using DemoBackStage.Web.Validator;

namespace DemoBackStage.Web.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        #region Property
        private IUserService GetUserService()
        {
            return AutoFacHelper.Get<IUserService>();
        }
        #endregion


        public ActionResult Index()
        {
            var userService = GetUserService();
            if (userService.IsLogin())
            {
                return new RedirectResult("/Home");
            }

            return View();
        }

        public ActionResult Code()
        {
            //var v = new DrawValidationCode();
            //v.FontMinSize = 24;
            //v.FontMaxSize = 30;
            //v.GaussianDeviation = 0;
            //v.BezierCount = 0;
            //v.IsPixel = false;
            //v.IsTwist = false;
            //v.RotationAngle = 0;
            //v.BrightnessValue = 0;
            //v.LineCount = 0;

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    v.CreateImage(ms);
            //    Response.ContentType = "image/gif";
            //    Response.BinaryWrite(ms.GetBuffer());
            //}

            //string code = v.ValidationCode;


            var v = new ValidatedCode();
            string code = v.CreateVerifyCode();

            Response.ClearContent();
            Response.ContentType = "image/jpeg";
            using (MemoryStream ms = new MemoryStream())
            {
                v.CreateImageOnPage(code, ms);
                Response.BinaryWrite(ms.GetBuffer());
            }

            HttpCookie hc = Request.Cookies[Consts.ValicationCode];
            if (hc == null)
            {
                hc = new HttpCookie(Consts.ValicationCode);
                hc.Value = Guid.NewGuid().ToString().Replace("-", "");
            }
            hc.Expires = DateTime.Now.AddSeconds(MyConfig.ValidationCodeTimeout);
            Response.Cookies.Add(hc);

            RedisServiceConfig.CodeRedisService.ResetCode(hc.Value, code);

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Login(UserLoginInfoModel model)
        {
            var userService = GetUserService();
            if (userService.IsLogin())
            {
                return new RedirectResult("/Home");
            }


            bool b = false;
            string msg = "";


            string value = "";
            try
            {
                var v = new UserLoginInfoValidator();
                var result = v.Validate(model);
                if (result.IsValid)
                {
                    EUserLoginResult result1 = EUserLoginResult.Fail;

                    HttpCookie hc = Request.Cookies[Consts.ValicationCode];
                    if (hc != null)
                    {
                        value = hc.Value;
                        var code = RedisServiceConfig.CodeRedisService.GetItemByPrefix(hc.Value);
                        if (!string.IsNullOrEmpty(code))
                        {
                            if (code.Equals(model.Code, StringComparison.OrdinalIgnoreCase))
                            {
                                result1 = userService.Login(model.UserName, model.Pwd);
                            }
                            else
                            {
                                result1 = EUserLoginResult.CodeError;
                            }
                        }
                        else
                        {
                            result1 = EUserLoginResult.CodeInvalid;
                        }
                    }
                    else
                    {
                        result1 = EUserLoginResult.CodeInvalid;
                    }

                    if (result1 == EUserLoginResult.Success)
                    {
                        b = true;
                        msg = "";
                    }
                    else
                    {
                        b = false;
                        msg = EnumTool.GetDescription<EUserLoginResult, int>(result1);
                    }
                }
                else
                {
                    b = false;
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

                b = false;
                msg = "登录失败, 系统异常!";
            }

            if (b)
            {
                HttpCookie hc = new HttpCookie(Consts.ValicationCode);
                hc.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(hc);

                if (!string.IsNullOrEmpty(value))
                {
                    RedisServiceConfig.CodeRedisService.Remove(value);
                }
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