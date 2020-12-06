using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{
    /// <summary>
    /// NetStreamWrite
    /// </summary>
    public class NetStreamWrite
    {
        private EEndian endian;
        private MemoryStream ms;


        /// <summary>
        /// NetStreamWrite
        /// </summary>
        /// <param name="len">len</param>
        /// <param name="endian">endian</param>
        public NetStreamWrite(int len = 4096, EEndian endian = EEndian.BigEndian)
        {
            this.endian = endian;
            ms = new MemoryStream(len);
        }

        /// <summary>
        /// Get Endian
        /// </summary>
        public EEndian Endian
        {
            get
            {
                return endian;
            }
        }

        /// <summary>
        /// Get Avaliable Write Byte Count
        /// </summary>
        /// <returns></returns>
        public long AvaiableWrite
        {
            get
            {
                return ms.AvaliableWrite();
            }
        }

        /// <summary>
        /// Get Stream
        /// </summary>
        public MemoryStream Stream
        {
            get
            {
                return ms;
            }
        }

        /// <summary>
        /// Get buffer
        /// </summary>
        /// <returns></returns>
        public byte[] GetBuffer()
        {
            byte[] buf = new byte[ms.Length];

            long tmp = ms.Position;
            ms.Position = 0;
            ms.Read(buf, 0, buf.Length);
            ms.Position = tmp;

            return buf;
        }

        /// <summary>
        /// Write byte
        /// </summary>
        /// <param name="val">val</param>
        /// <returns></returns>
        public bool WriteByte(byte val)
        {
            if (ms.AvaliableWrite() >= sizeof(byte))
            {
                ms.WriteByte(val);

                return true;
            }

            return false;
        }

        public bool WriteBytes(byte[] buf, int offset, int len)
        {
            if (ms.AvaliableWrite() >= len)
            {
                ms.Write(buf, offset, len);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write Int16
        /// </summary>
        /// <param name="val">Int16</param>
        /// <returns>bool</returns>
        public bool WriteInt16(Int16 val)
        {
            if (ms.AvaliableWrite() >= sizeof(Int16))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write UInt16
        /// </summary>
        /// <param name="val">UInt16</param>
        /// <returns>bool</returns>
        public bool WriteUInt16(UInt16 val)
        {
            if (ms.AvaliableWrite() >= sizeof(UInt16))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write Int32
        /// </summary>
        /// <param name="val">Int32</param>
        /// <returns>bool</returns>
        public bool WriteInt32(Int32 val)
        {
            if (ms.AvaliableWrite() >= sizeof(Int32))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write UInt32
        /// </summary>
        /// <param name="val">UInt32</param>
        /// <returns>bool</returns>
        public bool WriteUInt32(UInt32 val)
        {
            if (ms.AvaliableWrite() >= sizeof(UInt32))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write Int64
        /// </summary>
        /// <param name="val">Int64</param>
        /// <returns>bool</returns>
        public bool WriteInt64(Int64 val)
        {
            if (ms.AvaliableWrite() >= sizeof(Int64))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write UInt64
        /// </summary>
        /// <param name="val">UInt64</param>
        /// <returns>bool</returns>
        public bool WriteUInt64(UInt64 val)
        {
            if (ms.AvaliableWrite() >= sizeof(UInt64))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write float
        /// </summary>
        /// <param name="val">float</param>
        /// <returns>bool</returns>
        public bool WriteFloat(float val)
        {
            if (ms.AvaliableWrite() >= sizeof(float))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write double
        /// </summary>
        /// <param name="val">double</param>
        /// <returns>bool</returns>
        public bool WriteDouble(double val)
        {
            if (ms.AvaliableWrite() >= sizeof(double))
            {
                byte[] buf = BitConverter.GetBytes(val);
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                ms.Write(buf, 0, buf.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write gb2312 string
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="strPrefixByteLen">string prefix byte len</param>
        /// <param name="isWriteEndChar">is Write End Char</param>
        /// <param name="isPrefixContainEndCharByteLen">is Prefix Contain EndChar Byte Len</param>
        /// <returns>bool</returns>
        public bool WriteGB2312(string val, EStringPrefixLen strPrefixByteLen = EStringPrefixLen.Int32,
            bool isWriteEndChar = true, bool isPrefixContainEndCharByteLen = true)
        {
            if (!isWriteEndChar)
            {
                isPrefixContainEndCharByteLen = false;
            }

            byte[] buf = new byte[0];
            if (!string.IsNullOrEmpty(val))
            {
                buf = Encoding.GetEncoding("GB2312").GetBytes(val);
            }

            int prefixVal = buf.Length;
            if (isWriteEndChar && isPrefixContainEndCharByteLen)
            {
                prefixVal += 1;
            }
            int len = (int)strPrefixByteLen + buf.Length;
            if (isWriteEndChar)
            {
                len += 1;
            }

            if (ms.AvaliableWrite() >= len)
            {
                switch (strPrefixByteLen)
                {
                    case EStringPrefixLen.Byte:
                        WriteByte((byte)prefixVal);
                        break;
                    case EStringPrefixLen.Int16:
                        WriteUInt16((UInt16)prefixVal);
                        break;
                    case EStringPrefixLen.Int32:
                        WriteUInt32((UInt32)prefixVal);
                        break;
                    case EStringPrefixLen.Int64:
                        WriteUInt64((UInt64)prefixVal);
                        break;
                }

                ms.Write(buf, 0, buf.Length);

                if (isWriteEndChar)
                {
                    WriteByte(0);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Write utf-8 string
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="strPrefixByteLen">string prefix byte len</param>
        /// <param name="isWriteEndChar">is Write End Char</param>
        /// <param name="isPrefixContainEndCharByteLen">is Prefix Contain EndChar Byte Len</param>
        /// <returns>bool</returns>
        public bool WriteUTF8(string val, EStringPrefixLen strPrefixByteLen = EStringPrefixLen.Int32,
            bool isWriteEndChar = true, bool isPrefixContainEndCharByteLen = true)
        {
            if (!isWriteEndChar)
            {
                isPrefixContainEndCharByteLen = false;
            }

            byte[] buf = new byte[0];
            if (!string.IsNullOrEmpty(val))
            {
                buf = Encoding.UTF8.GetBytes(val);
            }

            int prefixVal = buf.Length;
            if (isWriteEndChar && isPrefixContainEndCharByteLen)
            {
                prefixVal += 1;
            }
            int len = (int)strPrefixByteLen + buf.Length;
            if (isWriteEndChar)
            {
                len += 1;
            }

            if (ms.AvaliableWrite() >= len)
            {
                switch (strPrefixByteLen)
                {
                    case EStringPrefixLen.Byte:
                        WriteByte((byte)prefixVal);
                        break;
                    case EStringPrefixLen.Int16:
                        WriteUInt16((UInt16)prefixVal);
                        break;
                    case EStringPrefixLen.Int32:
                        WriteUInt32((UInt32)prefixVal);
                        break;
                    case EStringPrefixLen.Int64:
                        WriteUInt64((UInt64)prefixVal);
                        break;
                }

                ms.Write(buf, 0, buf.Length);

                if (isWriteEndChar)
                {
                    WriteByte(0);
                }

                return true;
            }

            return false;
        }
    }
}
