using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.IO;

using FluentValidation.Results;

using Common;

using DemoBackStage.Web.Areas.System.Models;

namespace DemoBackStage.Web.Filter
{
    public class ValidatorFilterAttribute : ActionFilterAttribute
    {
        public Type ValidatorType { get; private set; }

        public Type ModelType { get; private set; }


        public ValidatorFilterAttribute(Type t, Type t2)
        {
            ValidatorType = t;
            ModelType = t2;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ValidatorType != null)
            {
                if (ValidatorType.BaseType.Name.Contains("AbstractValidator") ||
                    ValidatorType.BaseType.BaseType.Name.Contains("AbstractValidator") ||
                    ValidatorType.BaseType.BaseType.BaseType.Name.Contains("AbstractValidator"))
                {
                    try
                    {
                        var param = filterContext.ActionParameters.FirstOrDefault(x => x.Value.GetType().FullName.Equals(ModelType.FullName)).Value;
                        if (param != null)
                        {
                            MethodInfo mi = ValidatorType.GetMethod("Validate", new Type[] { ModelType });
                            if (mi != null)
                            {
                                var instance = Activator.CreateInstance(ValidatorType);
                                object result = mi.Invoke(instance, new object[] { param });
                                if (result != null)
                                {
                                    var result1 = result as ValidationResult;
                                    if (result1 != null)
                                    {
                                        if (!result1.IsValid)
                                        {
                                            string errors = result1.Errors.ConcatElement(" ");
                                            string str = WebTool.GetHttpRequestInfo(filterContext.HttpContext);
                                            CommonLogger.WriteLog(
                                                ELogCategory.Error,
                                                string.Format("Validate Fail: {0}{1}", errors, str)
                                            );

                                            filterContext.HttpContext.Response.ClearContent();
                                            filterContext.HttpContext.Response.ClearHeaders();
                                            filterContext.HttpContext.Response.StatusCode = 403;
                                            filterContext.Result = new ContentResult
                                            {
                                                Content = "403"
                                            };

                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string str = WebTool.GetHttpRequestInfo(filterContext.HttpContext);
                        CommonLogger.WriteLog(
                            ELogCategory.Fatal,
                            string.Format("ValidatorFilterAttribute.OnActionExecuting Exception: {0}{1}", e.Message, str),
                            e
                        );

                        filterContext.HttpContext.Response.ClearContent();
                        filterContext.HttpContext.Response.ClearHeaders();
                        filterContext.HttpContext.Response.StatusCode = 500;
                        filterContext.Result = new ContentResult
                        {
                            Content = "500"
                        };

                        return;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}