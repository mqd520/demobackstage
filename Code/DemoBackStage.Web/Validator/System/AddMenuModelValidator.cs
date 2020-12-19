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
    public class AddMenuModelValidator : AbstractValidator<AddMenuModel>
    {
        public AddMenuModelValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Must(x => RegExpTool.IsName(x)).WithMessage("Name不可用");

            RuleFor(x => x.Url)
                .Cascade(CascadeMode.Stop)
                .Must(x =>
                {
                    if (!string.IsNullOrEmpty(x))
                    {
                        var pattern = "^[\\w/_]{1,50}$";
                        var reg = new Regex(pattern, RegexOptions.IgnoreCase);
                        return reg.IsMatch(x);
                    }

                    return false;
                }).WithMessage("Url不可用");
        }
    }
}