using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace DemoBackStage.Web.Common
{
    public class RedisSessionLog
    {
        public static TextWriter GetLogWriter()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "Logs\\RedisSession\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string path = string.Format("{0}RedisSessionLog-{1}.txt", dir, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-dd"));
            FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            return new StreamWriter(fs);
        }
    }
}