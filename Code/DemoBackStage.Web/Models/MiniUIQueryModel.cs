using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Models
{
    public class MiniUIQueryModel
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public string sortField { get; set; }

        public string sortOrder { get; set; }
    }
}