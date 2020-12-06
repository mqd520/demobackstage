using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Common;

namespace Common
{
    /// <summary>
    /// NetStreamRead
    /// </summary>
    public class NetStreamRead
    {
        private EEndian endian;
        private MemoryStream ms;


        /// <summary>
        /// NetStreamRead
        /// </summary>
        /// <param name="buf">buf</param>
        /// <param name="offset">offset</param>
        /// <param name="len">len</param>
        /// <param name="endian">endian</param>
        public NetStreamRead(byte[] buf, int offset, int len, EEndian endian = EEndian.BigEndian)
        {
            this.endian = endian;
            ms = new MemoryStream(buf, offset, len);
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
        /// Get Avaliable Read Byte Count
        /// </summary>
        /// <returns></returns>
        public long AvaiableRead
        {
            get
            {
                return ms.AvaliableRead();
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
        /// Read byte
        /// </summary>
        /// <returns>byte</returns>
        public byte ReadByte()
        {
            byte result = 0;

            if (ms.AvaliableRead() > 0)
            {
                result = (byte)ms.ReadByte();
            }

            return result;
        }

        /// <summary>
        /// Read byte[]
        /// </summary>
        /// <param name="len">len</param>
        /// <returns>byte[]</returns>
        public byte[] ReadBytes(int len)
        {
            byte[] buf = null;

            if (ms.AvaliableRead() > 0)
            {
                int len1 = (int)Math.Min(len, ms.AvaliableRead());
                buf = new byte[len1];
                ms.Read(buf, 0, len1);
            }

            return buf;
        }

        /// <summary>
        /// Read Int16
        /// </summary>
        /// <returns>Int16</returns>
        public Int16 ReadInt16()
        {
            Int16 result = 0;

            byte[] buf = ReadBytes(sizeof(Int16));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                result = BitConverter.ToInt16(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read UInt16
        /// </summary>
        /// <returns>UInt16</returns>
        public UInt16 ReadUInt16()
        {
            UInt16 result = 0;

            byte[] buf = ReadBytes(sizeof(UInt16));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                result = BitConverter.ToUInt16(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read Int32
        /// </summary>
        /// <returns>Int32</returns>
        public Int32 ReadInt32()
        {
            Int32 result = 0;

            byte[] buf = ReadBytes(sizeof(Int32));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                result = BitConverter.ToInt32(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read UInt32
        /// </summary>
        /// <returns>UInt32</returns>
        public UInt32 ReadUInt32()
        {
            UInt32 result = 0;

            byte[] buf = ReadBytes(sizeof(UInt32));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                result = BitConverter.ToUInt32(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read Int64
        /// </summary>
        /// <returns>Int64</returns>
        public Int64 ReadInt64()
        {
            Int64 result = 0;

            byte[] buf = ReadBytes(sizeof(Int64));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                result = BitConverter.ToInt64(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read UInt64
        /// </summary>
        /// <returns>UInt64</returns>
        public UInt64 ReadUInt64()
        {
            UInt64 result = 0;

            byte[] buf = ReadBytes(sizeof(UInt64));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }
                result = BitConverter.ToUInt64(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read float
        /// </summary>
        /// <returns>float</returns>
        public float ReadFloat()
        {
            float result = 0;

            byte[] buf = ReadBytes(sizeof(float));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }

                result = BitConverter.ToSingle(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read double
        /// </summary>
        /// <returns>double</returns>
        public double ReadDouble()
        {
            double result = 0;

            byte[] buf = ReadBytes(sizeof(double));
            if (buf != null)
            {
                if (CommonConst.LocalHostEndian != endian)
                {
                    buf.Reverse2(0, buf.Length);
                }

                result = BitConverter.ToDouble(buf, 0);
            }

            return result;
        }

        /// <summary>
        /// Read GB2312 string
        /// </summary>
        /// <param name="len">len</param>
        /// <returns>string</returns>
        public string ReadGB2312(int len)
        {
            string result = "";

            byte[] buf = ReadBytes(len);
            if (buf != null)
            {
                result = Encoding.GetEncoding("GB2312").GetString(buf, 0, buf.Length);
            }

            return result;
        }

        /// <summary>
        /// Read GB2312 string
        /// </summary>
        /// <param name="strPrefixByteLen">string prefix byte len</param>
        /// <param name="hasEndChar">has EndChar</param>
        /// <param name="isPrefixContainEndCharByteLen">is Prefix Contain EndChar Byte Len</param>
        /// <returns>string</returns>
        public string ReadGB2312Prefix(EStringPrefixLen strPrefixByteLen = EStringPrefixLen.Int32,
            bool hasEndChar = true, bool isPrefixContainEndCharByteLen = true)
        {
            string result = "";

            if (!hasEndChar)
            {
                isPrefixContainEndCharByteLen = false;
            }

            int len = 0;
            switch (strPrefixByteLen)
            {
                case EStringPrefixLen.Byte:
                    len = ReadByte();
                    break;
                case EStringPrefixLen.Int16:
                    len = ReadUInt16();
                    break;
                case EStringPrefixLen.Int32:
                    len = (int)ReadUInt32();
                    break;
                case EStringPrefixLen.Int64:
                    len = (int)ReadUInt64();
                    break;
            }

            if (hasEndChar && !isPrefixContainEndCharByteLen)
            {
                len += 1;
            }

            if (ms.AvaliableRead() >= len)
            {
                result = ReadGB2312(len);
            }

            return result;
        }

        /// <summary>
        /// Read UTF-8 string
        /// </summary>
        /// <param name="len">len</param>
        /// <returns>string</returns>
        public string ReadUTF8(int len)
        {
            string result = "";

            byte[] buf = ReadBytes(len);
            if (buf != null)
            {
                result = Encoding.UTF8.GetString(buf, 0, buf.Length);
            }

            return result;
        }

        /// <summary>
        /// Read UTF-8 string
        /// </summary>
        /// <param name="strPrefixByteLen">string prefix byte len</param>
        /// <param name="hasEndChar">has EndChar</param>
        /// <param name="isPrefixContainEndCharByteLen">is Prefix Contain EndChar Byte Len</param>
        /// <returns>string</returns>
        public string ReadUTF8Prefix(EStringPrefixLen strPrefixByteLen = EStringPrefixLen.Int32,
            bool hasEndChar = true, bool isPrefixContainEndCharByteLen = true)
        {
            string result = "";

            if (!hasEndChar)
            {
                isPrefixContainEndCharByteLen = false;
            }

            int len = 0;
            switch (strPrefixByteLen)
            {
                case EStringPrefixLen.Byte:
                    len = ReadByte();
                    break;
                case EStringPrefixLen.Int16:
                    len = ReadUInt16();
                    break;
                case EStringPrefixLen.Int32:
                    len = (int)ReadUInt32();
                    break;
                case EStringPrefixLen.Int64:
                    len = (int)ReadUInt64();
                    break;
            }

            if (hasEndChar && !isPrefixContainEndCharByteLen)
            {
                len += 1;
            }

            if (ms.AvaliableRead() >= len)
            {
                result = ReadUTF8(len);
            }

            return result;
        }
    }
}
