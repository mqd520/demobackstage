using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace DemoBackStage.Entity
{
    /// <summary>
    /// UserInfo Entity: userinfo
    /// </summary>
    [SugarTable("userinfo")]
    public class UserInfoEntity
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
        /// Pwd
        /// </summary>
        [SugarColumn(ColumnName = "pwd")]
        public string Pwd { get; set; }

        /// <summary>
        /// Nick Name
        /// </summary>
        [SugarColumn(ColumnName = "nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// Reg Time
        /// </summary>
        [SugarColumn(ColumnName = "regtime")]
        public DateTime RegTime { get; set; }

        /// <summary>
        /// Reg Ip
        /// </summary>
        [SugarColumn(ColumnName = "regip")]
        public string RegIp { get; set; }
    }
}
