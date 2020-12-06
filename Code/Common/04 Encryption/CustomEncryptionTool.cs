using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CustomEncryptionTool
    {
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string clearText, string key)
        {
            string cipherText = "";

            if (clearText.Length >= 6)
            {
                string md5Str = Md5EncryptionTool.Encrypt(key);
                var indexStart = DateTime.Now.Day;

                string cipherText1 = "";
                for (int i = 0; i < clearText.Length; i++)
                {
                    int index = (indexStart + i) >= 32 ? (indexStart + i - 32) : (indexStart + i);
                    byte ch = (byte)clearText[i];
                    byte ch1 = (byte)md5Str[index];
                    byte newByte = (byte)(ch + ch1);
                    cipherText1 += (char)newByte;
                }

                string prefix = "";
                int ran = RandTool.CreateRandValWithMinMax(6, 10);
                for (int i = 0; i < ran; i++)
                {
                    int ran1 = RandTool.CreateRandValWithMinMax(0, 31);
                    prefix += md5Str[ran1];
                }

                string suffix = ran.ToString();

                cipherText = string.Format("{0}{1}{2}", prefix, cipherText1, suffix);
            }
            else
            {
                cipherText = "";
            }

            return cipherText;
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string key)
        {
            string clearText = "";

            var md5Str = Md5EncryptionTool.Encrypt(key);
            var endChar = cipherText[cipherText.Length - 1];        // 取出密文最后一个字符, 即混淆前缀字符数
            int prefixNum = Convert.ToInt32(endChar.ToString(), 10);
            var cipherText1 = cipherText.Substring(prefixNum, cipherText.Length - prefixNum - 1);
            var indexStart = DateTime.Now.Day;
            var len = cipherText1.Length;

            for (var i = 0; i < len; i++)
            {
                int index = (indexStart + i) >= 32 ? (indexStart + i - 32) : (indexStart + i);
                var ch = cipherText1[i];
                var ch1 = md5Str[index];
                byte newByte = (byte)((byte)ch - (byte)ch1);
                clearText += (char)newByte;
            }

            return clearText;
        }
    }
}
