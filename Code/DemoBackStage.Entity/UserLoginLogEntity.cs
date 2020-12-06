using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace DemoBackStage.Entity
{
    /// <summary>
    /// UserLoginLog Entity: userloginlog
    /// </summary>
    [SugarTable("userloginlog")]
    public class UserLoginLogEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        [SugarColumn(ColumnName = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// Agent
        /// </summary>
        [SugarColumn(ColumnName = "agent")]
        public string Agent { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        [SugarColumn(ColumnName = "ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [SugarColumn(ColumnName = "time")]
        public DateTime Time { get; set; }
    }
}
