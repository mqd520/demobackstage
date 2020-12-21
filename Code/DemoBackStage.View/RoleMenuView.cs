using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBackStage.View
{
    public class RoleMenuView
    {
        public int RoleId { get; set; }

        public int MenuId { get; set; }

        public string Permissions { get; set; }

        public string MenuName { get; set; }

        public int MenuLevel { get; set; }

        public int MenuRank { get; set; }

        public string MenuUrl { get; set; }

        public int MenuParentId { get; set; }

        public int MenuIsDir { get; set; }

        public string MenuRemark { get; set; }
    }
}
