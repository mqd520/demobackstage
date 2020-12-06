using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// MyUrlTool
    /// </summary>
    public static class MyUrlTool
    {
        public static string Get(string url, string path)
        {
            string result = url;

            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(path))
            {
                Uri uri = new Uri(url);
                string str = string.Format("{0}://{1}", uri.Scheme, uri.Authority);

                if (path.StartsWith("/"))
                {
                    result = str + path;
                }
                else if (path.StartsWith("../"))
                {

                }
                else
                {
                    result = str + "/" + path;
                }
            }

            return result;
        }
    }
}
