using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Common
{
    /// <summary>
    /// Common Const
    /// </summary>
    public static class CommonConst
    {
        /// <summary>
        /// Get localhost endian
        /// </summary>
        public static EEndian LocalHostEndian
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                {
                    return EEndian.LittleEndian;
                }
                else
                {
                    return EEndian.BigEndian;
                }
            }
        }
    }
}
