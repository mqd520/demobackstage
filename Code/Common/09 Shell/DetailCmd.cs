using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// IDetailCmd
    /// </summary>
    public interface IDetailCmd
    {
        string Cmd { get; }

        void OnDetailCmd(Dictionary<string, string> param);
    }
}