using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

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

            RuleFor(x => x.sortField)
                .Cascade(CascadeMode.Stop)
                .Custom((f, c) =>
                {
                    if (!string.IsNullOrEmpty(f))
                    {
                        var pattern = "^[a-zA-z]{1}[0-9a-zA-Z]{0,19}$";
                        var reg = new Regex(pattern, RegexOptions.IgnoreCase);
                        if (!reg.IsMatch(f))
                        {
                            c.AddFailure(string.Format("sortField不可用: {0}", f));
                        }
                    }
                });

            RuleFor(x => x.sortOrder)
                .Cascade(CascadeMode.Stop)
                .Custom((s, c) =>
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (!(s.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                            s.Equals("desc", StringComparison.OrdinalIgnoreCase))
                            )
                        {
                            c.AddFailure(string.Format("sortOrder不可用: {0}", s));
                        }
                    }
                });
        }
    }
}