using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Common
{
    public class HttpResultInfo
    {
        public string Result { get; set; }

        public CookieCollection Cookies { get; set; }


        public string StrCookies { get; set; }
    }
}
