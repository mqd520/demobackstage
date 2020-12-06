using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Md5EncryptionTool
    /// </summary>
    public static class Md5EncryptionTool
    {
        /// <summary>
        /// Encrypt(32位)
        /// </summary>
        /// <param name="clearText">clear text</param>
        /// <param name="isUpperCase">is upper case or not</param>
        /// <returns></returns>
        public static string Encrypt(string clearText, bool isUpperCase = false)
        {
            string cipherText = "";

            using (MD5 md5 = MD5.Create())
            {
                Byte[] soucebyte = Encoding.Default.GetBytes(clearText);
                Byte[] md5bytes = md5.ComputeHash(soucebyte);
                StringBuilder sb = new StringBuilder();
                foreach (Byte b in md5bytes)
                {
                    sb.Append(b.ToString(isUpperCase ? "X2" : "x2"));
                }
                cipherText = sb.ToString();
            }

            return cipherText;
        }

        /// <summary>
        /// Encrypt(16位)
        /// </summary>
        /// <param name="clearText">clear text</param>
        /// <param name="isUpperCase">is upper case or not</param>
        /// <returns></returns>
        public static string Encrypt16(string clearText, bool isUpperCase = false)
        {
            string cipherText = "";

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(clearText));
                string sBuilder = BitConverter.ToString(data, 4, 8);
                sBuilder = sBuilder.Replace("-", "");

                cipherText = sBuilder.ToString();
                if (!isUpperCase)
                {
                    cipherText = cipherText.ToLower();
                }
            }

            return cipherText;
        }
    }
}
