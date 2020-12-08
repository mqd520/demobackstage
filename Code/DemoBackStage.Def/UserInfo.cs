using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBackStage.Def
{
    public class UserInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Nick Name
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Reg Time
        /// </summary>
        public DateTime RegTime { get; set; }

        /// <summary>
        /// Reg Ip
        /// </summary>
        public string RegIp { get; set; }
    }
}