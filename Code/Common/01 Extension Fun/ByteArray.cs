using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// ByteArrayTool
    /// </summary>
    public static class ByteArrayTool
    {
        public static Int16 ToInt16(this byte[] arr)
        {
            Int16 val = 0;

            if (arr != null && arr.Length >= 2)
            {
                val |= (Int16)arr[0];
                val |= (Int16)((Int16)arr[1] << 8);
            }

            return val;
        }

        public static UInt16 ToUInt16(this byte[] arr)
        {
            UInt16 val = 0;

            if (arr != null && arr.Length >= 2)
            {
                val |= (UInt16)arr[0];
                val |= (UInt16)((UInt16)arr[1] << 8);
            }

            return val;
        }

        public static Int32 ToInt32(this byte[] arr)
        {
            Int32 val = 0;

            if (arr != null && arr.Length >= 4)
            {
                val |= (Int32)arr[0];
                val |= (Int32)((Int32)arr[1] << 8);
                val |= (Int32)((Int32)arr[2] << 16);
                val |= (Int32)((Int32)arr[3] << 24);
            }

            return val;
        }

        public static UInt32 ToUInt32(this byte[] arr)
        {
            UInt32 val = 0;

            if (arr != null && arr.Length >= 4)
            {
                val |= (UInt32)arr[0];
                val |= (UInt32)((UInt32)arr[1] << 8);
                val |= (UInt32)((UInt32)arr[2] << 16);
                val |= (UInt32)((UInt32)arr[3] << 24);
            }

            return val;
        }

        public static Int64 ToInt64(this byte[] arr)
        {
            Int64 val = 0;

            if (arr != null && arr.Length >= 8)
            {
                val |= (Int64)arr[0];
                val |= (Int64)((Int64)arr[1] << 8);
                val |= (Int64)((Int64)arr[2] << 16);
                val |= (Int64)((Int64)arr[3] << 24);
                val |= (Int64)((Int64)arr[4] << 32);
                val |= (Int64)((Int64)arr[5] << 40);
                val |= (Int64)((Int64)arr[6] << 48);
                val |= (Int64)((Int64)arr[7] << 56);
            }

            return val;
        }

        public static UInt64 ToUInt64(this byte[] arr)
        {
            UInt64 val = 0;

            if (arr != null && arr.Length >= 8)
            {
                val |= (UInt64)arr[0];
                val |= (UInt64)((UInt64)arr[1] << 8);
                val |= (UInt64)((UInt64)arr[2] << 16);
                val |= (UInt64)((UInt64)arr[3] << 24);
                val |= (UInt64)((UInt64)arr[4] << 32);
                val |= (UInt64)((UInt64)arr[5] << 40);
                val |= (UInt64)((UInt64)arr[6] << 48);
                val |= (UInt64)((UInt64)arr[7] << 56);
            }

            return val;
        }
    }
}
