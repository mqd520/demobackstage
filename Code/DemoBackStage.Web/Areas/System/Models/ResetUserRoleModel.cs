using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Areas.System.Models
{
    public class ResetUserRoleModel
    {
        public int UserId { get; set; }

        public IEnumerable<int> RoleIds { get; set; }
    }
}