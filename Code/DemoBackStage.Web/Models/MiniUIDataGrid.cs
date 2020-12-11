using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Models
{
    public class MiniUIDataGrid<T>
    {
        public IEnumerable<T> data { get; set; }

        public int total { get; set; }
    }
}