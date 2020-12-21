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
    public class QueryRoleModelValidator : MiniUIQueryValidator<QueryRoleModel>
    {
        public QueryRoleModelValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Custom((n, c) =>
                {
                    if (!string.IsNullOrEmpty(n))
                    {
                        if (!RegExpTool.IsName(n))
                        {
                            c.AddFailure(string.Format("Name不可用: {0}", n));
                        }
                    }
                });
        }
    }
}