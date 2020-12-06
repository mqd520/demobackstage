using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Rand Tool
    /// </summary>
    public static class RandTool
    {
        /// <summary>
        /// Create Rand
        /// </summary>
        /// <returns>Random</returns>
        public static Random CreateRand()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int seed = BitConverter.ToInt32(buffer, 0);
            Random ran = new Random(seed);

            return ran;
        }

        /// <summary>
        /// Create Rand Value With Max
        /// </summary>
        /// <param name="max">max</param>
        /// <returns>int</returns>
        public static int CreateRandValWithMax(int max)
        {
            Random ran = CreateRand();
            return ran.Next(max);
        }

        /// <summary>
        /// Create Rand Value With Max
        /// </summary>
        /// <param name="min">min</param>
        /// <param name="max">max</param>
        /// <returns>int</returns>
        public static int CreateRandValWithMinMax(int min, int max)
        {
            Random ran = CreateRand();
            return ran.Next(min, max);
        }

        /// <summary>
        /// Rand Number String
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string RandNumberString(int len)
        {
            string str = "";

            for (int i = 0; i < len; i++)
            {
                str += CreateRandValWithMinMax(0, 9);
            }

            return str;
        }
    }
}
