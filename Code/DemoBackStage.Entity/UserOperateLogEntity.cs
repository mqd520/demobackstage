using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace DemoBackStage.Entity
{
    [SugarTable("useroperatelog")]
    public class UserOperateLogEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "id", IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        [SugarColumn(ColumnName = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [SugarColumn(ColumnName = "time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [SugarColumn(ColumnName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        [SugarColumn(ColumnName = "ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [SugarColumn(ColumnName = "data")]
        public string Data { get; set; }
    }
}
