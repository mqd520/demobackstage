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

        }
    }
}