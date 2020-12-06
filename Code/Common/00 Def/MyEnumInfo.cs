using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// MyEnumInfo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyEnumInfo<T>
    {
        /// <summary>
        /// Value
        /// </summary>
        public T Value { get; set; } = default(T);

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = "";
    }
}
