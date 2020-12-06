using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DynamicStream
    {
        protected byte[] _buf;

        protected int _dataEndIndex = -1;

        /// <summary>
        /// Get or Set Size
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Get ReadAvaliable
        /// </summary>
        public int ReadAvaliable
        {
            get
            {
                if (_dataEndIndex != -1)
                {
                    return _dataEndIndex + 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get WriteAvaliable
        /// </summary>
        public int WriteAvaliable
        {
            get
            {
                if (_dataEndIndex != -1)
                {
                    return Size - (_dataEndIndex + 1);
                }
                else
                {
                    return Size;
                }
            }
        }

        public DynamicStream(int size = 1024)
        {
            Size = size;
            _buf = new byte[Size];
        }

        public int Write(byte[] buf, int offset, int len)
        {
            int len1 = Math.Min(WriteAvaliable, len);
            if (len > 0)
            {
                Array.Copy(buf, offset, _buf, _dataEndIndex + 1, len1);
                _dataEndIndex += len1;
            }

            return len1;
        }

        public int Read(byte[] buf, int offset, int len)
        {
            int len1 = Math.Min(ReadAvaliable, len);
            Array.Copy(_buf, 0, buf, offset, len1);

            int i1 = len1;
            if (i1 <= _dataEndIndex)
            {
                int len2 = _dataEndIndex - i1 + 1;
                var buf1 = new byte[len2];
                Array.Copy(_buf, i1, buf1, 0, len2);
                Array.Copy(buf1, 0, _buf, 0, len2);
                _dataEndIndex = len2 - 1;
            }
            else
            {
                _dataEndIndex = 0;
            }

            return len1;
        }

        public int Copy(byte[] buf, int offset, int len)
        {
            int len1 = Math.Min(ReadAvaliable, len);
            Array.Copy(_buf, 0, buf, offset, len1);

            return len1;
        }
    }
}
