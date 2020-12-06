using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// ConsoleHelper
    /// </summary>
    public class ConsoleHelper
    {
        /// <summary>
        /// Write Line
        /// </summary>
        /// <param name="categeory">categeory</param>
        /// <param name="log">log</param>
        /// <param name="isWriteFile">is Write to File</param>
        /// <param name="showDate">showDate</param>
        /// <param name="showCategory">showCategory</param>
        /// <param name="e">Exception</param>
        public static void WriteLine(ELogCategory categeory, string log, bool isWriteFile = false,
            bool showDate = true, bool showCategory = false, Exception e = null)
        {
            string str = "";
            if (showDate)
            {
                str += string.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (showCategory)
            {
                str += string.Format("[{0}] ", categeory);
            }
            str += log;

            ConsoleColor color = ConsoleColor.Gray;
            switch (categeory)
            {
                case ELogCategory.Debug:
                    color = ConsoleColor.DarkGray;
                    break;
                case ELogCategory.Warn:
                    color = ConsoleColor.DarkYellow;
                    break;
                case ELogCategory.Error:
                    color = ConsoleColor.DarkRed;
                    break;
                case ELogCategory.Fatal:
                    color = ConsoleColor.Red;
                    break;
            }

            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();

            if (isWriteFile)
            {
                CommonLogger.WriteLog(categeory, log, e);
            }
        }
    }
}
