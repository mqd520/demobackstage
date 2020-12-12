using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FluentValidation;

using DemoBackStage.Web.Models;

namespace DemoBackStage.Web.Validator
{
    public class MiniUIQueryValidator<T> : AbstractValidator<T> where T : MiniUIQueryModel
    {
        public MiniUIQueryValidator()
        {
            RuleFor(x => x.pageIndex)
                .Cascade(CascadeMode.Stop)
                .Must(x => x >= 0).WithMessage("pageIndex必须大于等于0");

            RuleFor(x => x.pageSize)
                .Cascade(CascadeMode.Stop)
                .Must(x => x >= 0).WithMessage("pageSize必须大于0");
        }
    }
}