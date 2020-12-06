using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Array Extension Tool
    /// </summary>
    public static class ArrayTool
    {
        /// <summary>
        /// Reverse Element
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="offset">offset</param>
        /// <param name="len">len</param>
        public static void Reverse2(this Array source, int offset, int len)
        {
            if (source.Length > 1)
            {
                int len1 = Math.Max(len, offset + len);
                byte[] buf1 = new byte[len1];
                Array arr1 = Array.CreateInstance(typeof(byte), len1);
                Array.Copy(source, offset, arr1, 0, len1);
                Array.Reverse(arr1);
                Array.Copy(arr1, 0, source, offset, len1);
            }
        }
    }
}
