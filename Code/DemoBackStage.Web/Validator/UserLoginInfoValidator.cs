using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FluentValidation;

using DemoBackStage.Web.Models.User;

namespace DemoBackStage.Web.Validator
{
    public class UserLoginInfoValidator : AbstractValidator<UserLoginInfoModel>
    {
        public UserLoginInfoValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("用户名不能为空")
                .Matches("^[a-zA-Z]{1}\\w{5,19}$").WithMessage("用户名格式不正确");

            RuleFor(x => x.Pwd)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("密码不能为空")
                .Matches("^\\w{32}$").WithMessage("密码格式不正确");

            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("验证码不能为空")
                .Matches("^\\w{4}$").WithMessage("验证码格式不正确!");
        }
    }
}