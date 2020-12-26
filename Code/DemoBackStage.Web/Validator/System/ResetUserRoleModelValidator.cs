using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FluentValidation;

using DemoBackStage.Web.Areas.System.Models;

namespace DemoBackStage.Web.Validator.System
{
    public class ResetUserRoleModelValidator : AbstractValidator<ResetUserRoleModel>
    {
        public ResetUserRoleModelValidator()
        {

        }
    }
}