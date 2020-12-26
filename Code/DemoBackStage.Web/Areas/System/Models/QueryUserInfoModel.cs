using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DemoBackStage.Web.Models;

namespace DemoBackStage.Web.Areas.System.Models
{
    public class QueryUserInfoModel : MiniUIQueryModel
    {
        public string UserName { get; set; }
    }
}