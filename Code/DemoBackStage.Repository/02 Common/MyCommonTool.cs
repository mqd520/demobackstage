﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using SqlSugar;

using Common;

using DemoBackStage.Repository._01_Config;

namespace DemoBackStage.Repository._02_Common
{
    public static class MyCommonTool
    {
        public static ISugarQueryable<T> AddOrderBy<T>(ISqlSugarClient db, ISugarQueryable<T> query, string orderby, bool asc)
        {
            string field = db.EntityMaintenance.GetDbColumnName<T>(orderby);
            if (!string.IsNullOrEmpty(field))
            {
                if (asc)
                {
                    query = query.OrderBy(field);
                }
                else
                {
                    query = query.OrderBy(string.Format("{0} desc", field));
                }
            }

            return query;
        }

        public static string EncryptPwd(string pwd)
        {
            return Md5EncryptionTool.Encrypt(pwd + MyConfig.Md5Key);
        }
    }
}
