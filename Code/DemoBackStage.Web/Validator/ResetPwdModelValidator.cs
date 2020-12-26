using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

using FluentValidation;

using DemoBackStage.Web.Models;

namespace DemoBackStage.Web.Validator
{
    public class ResetPwdModelValidator : AbstractValidator<ResetPwdModel>
    {
        public ResetPwdModelValidator()
        {
            RuleFor(x => x.OldPwd)
                .Cascade(CascadeMode.Stop)
                .Must(x =>
                {
                    string pattern = "^\\w{32}$";
                    var reg = new Regex(pattern);
                    return reg.IsMatch(x);
                }).WithMessage("密码格式不正确!");

            RuleFor(x => x.NewPwd)
                .Cascade(CascadeMode.Stop)
                .Must(x =>
                {
                    string pattern = "^\\w{32}$";
                    var reg = new Regex(pattern);
                    return reg.IsMatch(x);
                }).WithMessage("密码格式不正确!");
        }
    }
}