using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{
    /// <summary>
    /// MemoryStream Tool
    /// </summary>
    public static class MemoryStreamTool
    {
        /// <summary>
        /// Get Avaliable Read Byte Count
        /// </summary>
        /// <param name="source">MemoryStream</param>
        /// <returns>long</returns>
        public static long AvaliableRead(this MemoryStream source)
        {
            return source.Length - source.Position;
        }

        /// <summary>
        /// Get Avaliable Write Byte Count
        /// </summary>
        /// <param name="source">MemoryStream</param>
        /// <returns></returns>
        public static long AvaliableWrite(this MemoryStream source)
        {
            return source.Capacity - source.Length;
        }
    }
}
