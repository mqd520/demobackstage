using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

using FluentValidation;

using Common;

using DemoBackStage.Web.Areas.System.Models;

namespace DemoBackStage.Web.Validator.System
{
    public class UserLoginLogQueryModelValidator : MiniUIQueryValidator<UserLoginLogQueryModel>
    {
        public UserLoginLogQueryModelValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .Custom((u, c) =>
                {
                    if (!string.IsNullOrEmpty(u))
                    {
                        string pattern = "^[a-zA-Z]{1}[a-zA-Z0-9_]{5,19}$";
                        var reg = new Regex(pattern);
                        if (!reg.IsMatch(u))
                        {
                            c.AddFailure("用户名格式不正确!");
                        }
                    }
                });

            RuleFor(x => x.Ip)
                .Cascade(CascadeMode.Stop)
                .Custom((ip, c) =>
                {
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (!RegExpTool.IsIp(ip))
                        {
                            c.AddFailure("Ip格式不正确!");
                        }
                    }
                });
        }
    }
}