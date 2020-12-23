using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Areas.System.Models
{
    public class ResetPermissionModel
    {
        public int RoleId { get; set; }

        public IEnumerable<ResetPermissionItemModel> Items { get; set; }
    }

    public class ResetPermissionItemModel
    {
        public int MenuId { get; set; }

        public string Permissions { get; set; }
    }
}