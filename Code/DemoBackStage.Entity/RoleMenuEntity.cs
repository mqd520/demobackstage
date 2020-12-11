using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace DemoBackStage.Entity
{
    [SugarTable("rolemenu")]
    public class RoleMenuEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Role Id
        /// </summary>
        [SugarColumn(ColumnName = "roleid")]
        public int RoleId { get; set; }

        /// <summary>
        /// Menu Id
        /// </summary>
        [SugarColumn(ColumnName = "menuid")]
        public int MenuId { get; set; }

        /// <summary>
        /// Permissions
        /// </summary>
        [SugarColumn(ColumnName = "permissions")]
        public string Permissions { get; set; }
    }
}
