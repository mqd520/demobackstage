using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Endian enum
    /// </summary>
    public enum EEndian : int
    {
        /// <summary>
        /// Big Endian
        /// </summary>
        BigEndian = 0,

        /// <summary>
        /// Little Endian
        /// </summary>
        LittleEndian = 1
    }

    /// <summary>
    /// String Prefix Byte Len
    /// </summary>
    public enum EStringPrefixLen : int
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Byte
        /// </summary>
        Byte = 1,

        /// <summary>
        /// Int16
        /// </summary>
        Int16 = 2,

        /// <summary>
        /// Int32
        /// </summary>
        Int32 = 4,

        /// <summary>
        /// Int64
        /// </summary>
        Int64 = 8
    }

    /// <summary>
    /// Log Category
    /// </summary>
    public enum ELogCategory : int
    {
        /// <summary>
        /// Debug
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Info
        /// </summary>
        Info = 2,

        /// <summary>
        /// Warn
        /// </summary>
        Warn = 3,

        /// <summary>
        /// Error
        /// </summary>
        Error = 4,

        /// <summary>
        /// Fatal
        /// </summary>
        Fatal = 5,

        /// <summary>
        /// Sql
        /// </summary>
        Sql = 6,

        /// <summary>
        /// Recv
        /// </summary>
        Security = 7,

        /// <summary>
        /// None
        /// </summary>
        None = -99
    }
}
