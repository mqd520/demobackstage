﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Models.User
{
    public class NavInfoModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsDir { get; set; }

        public int Level { get; set; }

        public int Rank { get; set; }
    }
}