using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DemoBackStage.Web.Models;

namespace DemoBackStage.Web.Areas.System.Models
{
    public class UserLoginLogQueryModel : MiniUIQueryModel
    {
        public string UserName { get; set; }

        public string Ip { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}