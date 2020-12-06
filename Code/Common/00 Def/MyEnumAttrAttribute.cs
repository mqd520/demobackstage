using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// MyEnum Attr Attribute
    /// </summary>
    public class MyEnumAttrAttribute : Attribute
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; private set; } = "";


        public MyEnumAttrAttribute(string desc = "")
        {
            Description = desc;
        }
    }
}
