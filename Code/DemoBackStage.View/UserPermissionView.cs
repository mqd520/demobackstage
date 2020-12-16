using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;

namespace DemoBackStage.View
{
    public class UserPermissionView
    {
        public int UserId { get; set; }

        public string MenuName { get; set; }

        public string MenuUrl { get; set; }

        public string Permissions { get; set; }
    }
}
