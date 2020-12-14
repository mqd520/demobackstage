using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DemoBackStage.Repository._02_Common
{
    public static class MyCommonTool
    {
        public static Expression<Func<T, object>> GetSortFidld<T>(string property)
        {
            Expression<Func<T, object>> express = null;

            return express;
        }
    }
}
