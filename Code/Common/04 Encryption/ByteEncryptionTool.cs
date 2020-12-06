using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Byte Encryption Tool
    /// </summary>
    public class ByteEncryptionTool
    {
        private byte[] _sendByteMap = new byte[256];
        private byte[] _recvByteMap = new byte[256];


        public void ResetSendByteMap(byte[] sendByteMap)
        {
            _sendByteMap = sendByteMap;
        }

        public void ResetRecvByteMap(byte[] recvByteMap)
        {
            _recvByteMap = recvByteMap;
        }


        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="buf">buffer</param>
        /// <param name="offset">offset</param>
        /// <param name="len">len</param>
        /// <returns>int</returns>
        public byte Encrypt(byte[] buf, int offset, int len)
        {
            byte code = 0;

            for (int i = offset; i < offset + len; i++)
            {
                if (i < buf.Length)
                {
                    code += buf[i];
                    buf[i] = _sendByteMap[buf[i]];
                }
            }

            return (byte)(((byte)~code) + 1);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="buf">buffer</param>
        /// <param name="offset">offset</param>
        /// <param name="len">len</param>
        /// <returns>bool</returns>
        public bool Decrypt(byte code, byte[] buf, int offset, int len)
        {
            for (int i = offset; i < offset + len; i++)
            {
                if (i < buf.Length)
                {
                    buf[i] = _recvByteMap[buf[i]];
                    code += buf[i];
                }
            }

            return code == 0;
        }
    }
}