using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace DemoBackStage.Entity
{
    [SugarTable("menu")]
    public class MenuEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Rank
        /// </summary>
        [SugarColumn(ColumnName = "rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        [SugarColumn(ColumnName = "level")]
        public int Level { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        [SugarColumn(ColumnName = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [SugarColumn(ColumnName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// ParentId
        /// </summary>
        [SugarColumn(ColumnName = "parentid")]
        public int ParentId { get; set; }

        /// <summary>
        /// Is Dir
        /// </summary>
        [SugarColumn(ColumnName = "isdir")]
        public int isdir { get; set; }
    }
}
