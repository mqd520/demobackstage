using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoBackStage.Web.Models.User
{
    public class UserLoginInfoModel
    {
        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string Code { get; set; }


        public override string ToString()
        {
            string str = string.Format("UserName: {0}{1}Pwd: {2}{1}Code: {3}", UserName, Pwd, Code);

            return str;
        }
    }
}