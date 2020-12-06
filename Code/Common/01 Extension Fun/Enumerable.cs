using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Enumerable Extension Tool
    /// </summary>
    public static class EnumerableTool
    {
        /// <summary>
        /// Concat element of Array
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">source</param>
        /// <param name="split">split</param>
        /// <param name="fun">fun</param>
        /// <returns>String</returns>
        public static string ConcatElement<T>(this IEnumerable<T> source, string split = ", ", Func<T, string> fun = null)
        {
            string result = "";

            int n = 1;
            int count = source.Count();
            foreach (var item in source)
            {
                if (fun != null)
                {
                    result += fun.Invoke(item);
                }
                else
                {
                    result += item.ToString();
                }

                if (n < count)
                {
                    result += split;
                }

                n++;
            }

            return result;
        }

        /// <summary>
        /// Reverse Element
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">source</param>
        /// <param name="offset">offset</param>
        /// <param name="len">len</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> Reverse1<T>(this IEnumerable<T> source, int offset, int len)
        {
            if (source.Count() > 1)
            {
                Array arr1 = source.ToArray();
                int len1 = Math.Max(len, offset + len - 1);
                Array arr2 = Array.CreateInstance(typeof(T), len1);
                Array.Copy(arr1, offset, arr2, 0, len1);
                Array.Reverse(arr2);
                Array.Copy(arr2, 0, arr1, offset, len1);

                foreach (var item in arr1)
                {
                    yield return (T)item;
                }
            }
            else
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }
    }
}
