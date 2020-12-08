using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DemoBackStage.DAL;

namespace DemoBackStage.Web.App_Start
{
    public class DbConfig
    {
        public static void Init()
        {
            SqlSugarHelper.Init();
        }
    }
}