using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Des EncryptionTool
    /// </summary>
    public static class DesEncryptionTool
    {
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="clearText">clearText</param>
        /// <param name="key">key</param>
        /// <param name="mode">mode</param>
        /// <param name="padding">padding</param>
        /// <param name="iv">iv</param>
        /// <returns>byte[]</returns>
        public static byte[] Encrypt(string clearText, string key,
            CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = "")
        {
            byte[] result;

            if (key.Length < 8)
            {
                int n = 8 - key.Length;
                for (int i = 0; i < n; i++)
                {
                    key += "0";
                }
            }
            key = key.Substring(0, 8);

            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            descsp.Mode = mode;
            descsp.Padding = padding;
            byte[] data = Encoding.UTF8.GetBytes(clearText);
            byte[] bufKey = Encoding.UTF8.GetBytes(key);
            byte[] bufIV = Encoding.UTF8.GetBytes(iv);
            if (mode == CipherMode.ECB)
            {
                bufIV = Encoding.UTF8.GetBytes(key);
            }
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(bufKey, bufIV), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);
            CStream.FlushFinalBlock();
            result = MStream.ToArray();
            CStream.Close();
            descsp.Clear();

            return result;
        }

        /// <summary>
        /// Encrypt(base64)
        /// </summary>
        /// <param name="clearText">clearText</param>
        /// <param name="key">key</param>
        /// <param name="mode">mode</param>
        /// <param name="padding">padding</param>
        /// <param name="iv">iv</param>
        /// <returns>string</returns>
        public static string EncryptToBase64(string clearText, string key,
            CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = "")
        {
            byte[] buf = Encrypt(clearText, key, mode, padding, iv);
            return Convert.ToBase64String(buf);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="buf">buf</param>
        /// <param name="key">key</param>
        /// <param name="mode">mode</param>
        /// <param name="padding">padding</param>
        /// <param name="iv">iv</param>
        /// <returns>string</returns>
        public static string Decrypt(byte[] buf, string key,
    CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = "")
        {
            string result = "";

            if (key.Length < 8)
            {
                int n = 8 - key.Length;
                for (int i = 0; i < n; i++)
                {
                    key += "0";
                }
            }
            key = key.Substring(0, 8);

            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            descsp.Mode = mode;
            descsp.Padding = padding;
            byte[] bufKey = Encoding.UTF8.GetBytes(key);
            byte[] bufIV = Encoding.UTF8.GetBytes(iv);
            if (mode == CipherMode.ECB)
            {
                bufIV = Encoding.UTF8.GetBytes(key);
            }
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(bufKey, bufIV), CryptoStreamMode.Write);
            CStream.Write(buf, 0, buf.Length);
            CStream.FlushFinalBlock();
            result = Encoding.UTF8.GetString(MStream.ToArray());
            CStream.Close();
            descsp.Clear();

            return result;
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText">cipherText</param>
        /// <param name="key">key</param>
        /// <param name="mode">mode</param>
        /// <param name="padding">padding</param>
        /// <param name="iv">iv</param>
        /// <returns>string</returns>
        public static string DecryptFromBase64(string cipherText, string key,
    CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = "")
        {
            byte[] buf = Convert.FromBase64String(cipherText);
            return Decrypt(buf, key, mode, padding, iv);
        }
    }
}
