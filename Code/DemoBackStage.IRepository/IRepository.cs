using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoBackStage.IRepository
{
    public interface IRepository<T> where T : class, new()
    {
        #region Query
        #region Query Single
        /// <summary>
        /// Query Single
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T QuerySingleById<Tkey>(Tkey id);

        /// <summary>
        /// Query Single
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T QuerySingle(Expression<Func<T, bool>> where);

        /// <summary>
        /// Query Single
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        T QuerySingle(IEnumerable<Expression<Func<T, bool>>> wheres);
        #endregion


        #region Query All
        /// <summary>
        /// Query All
        /// </summary>
        /// <returns></returns>
        IList<T> QueryAll();

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        IList<T> QueryAll(Expression<Func<T, object>> keySelector, bool asc);

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        IList<T> QueryAll(Expression<Func<T, bool>> where);

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        IList<T> QueryAll(IList<Expression<Func<T, bool>>> lsWhere);

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        IList<T> QueryAll(IList<Expression<Func<T, bool>>> lsWhere, Expression<Func<T, object>> keySelector, bool asc);

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        IList<T> QueryAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> keySelector, bool asc);
        #endregion


        #region Query Paging
        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<T> QueryPaging(int page, int size, out int count);

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, object>> keySelector, bool asc);

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, bool>> where);

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        IList<T> QueryPaging(int page, int size, out int count, IList<Expression<Func<T, bool>>> lsWhere);

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="lsWhere"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        IList<T> QueryPaging(int page, int size, out int count, IList<Expression<Func<T, bool>>> lsWhere, Expression<Func<T, object>> keySelector, bool asc);

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, bool>> where, Expression<Func<T, object>> keySelector, bool asc);
        #endregion
        #endregion


        #region Add
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(T entity);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add1(T entity);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Add(IEnumerable<T> entities);
        #endregion


        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Update(IEnumerable<T> entities);
        #endregion


        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        int Delete(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        int Delete(IEnumerable<T> entities);

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete<TKey>(TKey id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete<TKey>(IEnumerable<TKey> ids);
        #endregion
    }
}
