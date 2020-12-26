using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Models
{
    public class ResetPwdModel
    {
        public string OldPwd { get; set; }

        public string NewPwd { get; set; }
    }
}