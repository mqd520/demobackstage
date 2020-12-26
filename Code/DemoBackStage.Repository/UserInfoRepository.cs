using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

using Common;
using DemoBackStage.Entity;
using DemoBackStage.IRepository;

using DemoBackStage.Repository._01_Config;
using DemoBackStage.Repository._02_Common;

namespace DemoBackStage.Repository
{
    /// <summary>
    /// UserInfo Repository
    /// </summary>
    public class UserInfoRepository : Repository<UserInfoEntity>, IUserInfoRepository
    {
        /// <summary>
        /// Get or Set IsAdministrator
        /// </summary>
        public virtual bool IsAdministrator { get; set; } = false;


        /// <summary>
        /// Query By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual UserInfoEntity QueryByUserName(string username)
        {
            return QuerySingle(x => x.UserName == username);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual IList<UserInfoEntity> QueryPaging(int page, int size, out int count,
            string username)
        {
            var ls = new List<Expression<Func<UserInfoEntity, bool>>>();

            if (!string.IsNullOrEmpty(username))
            {
                ls.Add(x => x.UserName.Contains(username));
            }
            if (!IsAdministrator)
            {
                ls.Add(x => x.UserName != MyConfig.Administrator);
            }

            return QueryPaging(page, size, out count, ls);
        }

        /// <summary>
        /// Reset Pwd
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public bool ResetPwd(string username, string oldPwd, string newPwd)
        {
            oldPwd = MyCommonTool.EncryptPwd(oldPwd);
            newPwd = MyCommonTool.EncryptPwd(newPwd);

            using (var db = GetDb())
            {
                string table = db.EntityMaintenance.GetTableName<UserInfoEntity>();
                string property1 = CommonTool.GetPropertyName<UserInfoEntity, string>(x => x.UserName);
                string field1 = db.EntityMaintenance.GetDbColumnName<UserInfoEntity>(property1);
                string property2 = CommonTool.GetPropertyName<UserInfoEntity, string>(x => x.Pwd);
                string field2 = db.EntityMaintenance.GetDbColumnName<UserInfoEntity>(property2);

                string sql = string.Format("update {0} set {1} = @NewPwd where {2} = @UserName and {1} = @OldPwd", table, field2, field1);
                SugarParameter[] paramArr = new SugarParameter[3];
                paramArr[0] = new SugarParameter("@UserName", username, System.Data.DbType.String, System.Data.ParameterDirection.Input, 20);
                paramArr[1] = new SugarParameter("@OldPwd", oldPwd, System.Data.DbType.String, System.Data.ParameterDirection.Input, 20);
                paramArr[2] = new SugarParameter("@NewPwd", newPwd, System.Data.DbType.String, System.Data.ParameterDirection.Input, 20);

                int n = db.Ado.ExecuteCommand(sql, paramArr);

                return n > 0;
            }
        }
    }
}
