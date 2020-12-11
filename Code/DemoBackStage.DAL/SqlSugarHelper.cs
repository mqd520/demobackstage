using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using SqlSugar;

using Common;

namespace DemoBackStage.DAL
{
    /// <summary>
    /// SqlSugar Helper
    /// </summary>
    public static class SqlSugarHelper
    {
        #region Event
        public static event Action<string, SugarParameter[]> SqlExecutingHandler;
        public static event Action<string, SugarParameter[]> SqlExecutedHandler;
        public static event Action<SqlSugarException> SqlExceptionHandler;
        #endregion


        #region Property
        /// <summary>
        /// Get Enable Sql Executing Log
        /// </summary>
        public static bool EnableSqlExecutingLog { get; set; } = true;

        /// <summary>
        /// Get Enable Sql Executed Log
        /// </summary>
        public static bool EnableSqlExceptionLog { get; set; } = true;
        #endregion


        #region Constructor
        static SqlSugarHelper()
        {

        }
        #endregion


        #region Method
        /// <summary>
        /// Init
        /// </summary>
        public static void Init()
        {
            if (EnableSqlExecutingLog)
            {
                SqlExecutingHandler += SqlSugarHelper_SqlExecutingHandler;
            }
            if (EnableSqlExceptionLog)
            {
                SqlExceptionHandler += SqlSugarHelper_SqlExceptionHandler;
            }
        }

        /// <summary>
        /// Exit
        /// </summary>
        public static void Exit()
        {

        }

        /// <summary>
        /// Get Db
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetDb()
        {
            return GetDb("demobackstage");
        }

        /// <summary>
        /// Get Db
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SqlSugarClient GetDb(string name)
        {
            var css = ConfigurationManager.ConnectionStrings[name];
            if (css == null)
            {
                throw new Exception(string.Format("Not Found ConnectionStrings: {0}", name));
            }

            var cfg = new ConnectionConfig
            {
                ConnectionString = css.ConnectionString,
                DbType = GetDbType(css),
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            };

            var db = new SqlSugarClient(cfg);

            db.Aop.OnLogExecuting = (sql, param) =>
            {
                if (SqlExecutingHandler != null)
                {
                    SqlExecutingHandler.Invoke(sql, param);
                }
            };
            db.Aop.OnError = e =>
            {
                if (SqlExceptionHandler != null)
                {
                    SqlExceptionHandler.Invoke(e);
                }
            };
            db.Aop.OnLogExecuted = (sql, param) =>
            {
                if (SqlExecutedHandler != null)
                {
                    SqlExecutedHandler.Invoke(sql, param);
                }
            };

            return db;
        }

        /// <summary>
        /// Get Db Type
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public static DbType GetDbType(ConnectionStringSettings css)
        {
            if (css.ProviderName.Equals("MySql.Data.MySqlClient", StringComparison.OrdinalIgnoreCase))
            {
                return DbType.MySql;
            }

            else if (css.ProviderName.Equals("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
            {
                return DbType.SqlServer;
            }

            throw new NotImplementedException(string.Format("Invalid ConnectionStrings Provider: {0}", css.ProviderName));
        }
        #endregion


        #region Event Callback
        private static void SqlSugarHelper_SqlExecutingHandler(string arg1, SugarParameter[] arg2)
        {
            string strParam = "";
            if (arg2 != null)
            {
                strParam = arg2.ConcatElement(Environment.NewLine, x => string.Format("{0}: {1}", x.ParameterName, x.Value?.ToString() ?? "null"));
            }

            ConsoleHelper.WriteLine(
                ELogCategory.Sql,
                string.Format("Prepare Execute SQL: {0}{1}{2}", arg1, Environment.NewLine, strParam),
                true
            );
        }

        private static void SqlSugarHelper_SqlExceptionHandler(SqlSugarException obj)
        {
            string strParam = "";
            if (obj.Parametres != null)
            {
                var arr = obj.Parametres as SugarParameter[];
                if (arr != null)
                {
                    strParam = arr.ConcatElement(Environment.NewLine, x => string.Format("{0}: {1}", x.ParameterName, x.Value.ToString()));
                }
            }

            string str = "";

            str += Environment.NewLine + obj.Sql;
            if (!string.IsNullOrEmpty(strParam))
            {
                str += Environment.NewLine + strParam;
            }
            if (!string.IsNullOrEmpty(obj.StackTrace))
            {
                str += Environment.NewLine + obj.StackTrace;
            }

            ConsoleHelper.WriteLine(
                ELogCategory.Sql,
                string.Format("Execute SQL Exception: {0}{1}", obj.Message, str),
                true
            );
        }
        #endregion
    }
}
