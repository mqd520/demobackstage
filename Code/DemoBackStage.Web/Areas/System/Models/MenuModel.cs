using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Areas.System.Models
{
    public class MenuModel
    {
        public int ParentId { get; set; }

        public int Level { get; set; }

        public bool IsDir { get; set; }

        public int Rank { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Remark { get; set; }
    }
}