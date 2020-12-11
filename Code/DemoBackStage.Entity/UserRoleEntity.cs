using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace DemoBackStage.Entity
{
    [SugarTable("userrole")]
    public class UserRoleEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [SugarColumn(ColumnName = "userid")]
        public int UserId { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>
        [SugarColumn(ColumnName = "roleid")]
        public int RoleId { get; set; }
    }
}
