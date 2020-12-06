using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using AutoFacUtils;
using DemoBackStage.IRepository;

namespace DemoBackStage.Web.Service
{
    public class Service<T> where T : class, new()
    {
        #region Property
        protected IRepository<T> Repository { get { return AutoFacHelper.Get<IRepository<T>>(); } }
        #endregion


        #region Query
        #region Query Single
        /// <summary>
        /// Query Single
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T QuerySingleById<Tkey>(Tkey id)
        {
            return Repository.QuerySingleById<Tkey>(id);
        }

        /// <summary>
        /// Query Single
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T QuerySingle(Expression<Func<T, bool>> where)
        {
            return Repository.QuerySingle(where);
        }

        /// <summary>
        /// Query Single
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public T QuerySingle(IEnumerable<Expression<Func<T, bool>>> wheres)
        {
            return Repository.QuerySingle(wheres);
        }
        #endregion


        #region Query All
        /// <summary>
        /// Query All
        /// </summary>
        /// <returns></returns>
        public IList<T> QueryAll()
        {
            return Repository.QueryAll();
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IList<T> QueryAll(Expression<Func<T, object>> keySelector, bool asc)
        {
            return Repository.QueryAll(keySelector, asc);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public IList<T> QueryAll(Expression<Func<T, bool>> where)
        {
            return Repository.QueryAll(where);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public IList<T> QueryAll(IList<Expression<Func<T, bool>>> lsWhere)
        {
            return Repository.QueryAll(lsWhere);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IList<T> QueryAll(IList<Expression<Func<T, bool>>> lsWhere, Expression<Func<T, object>> keySelector, bool asc)
        {
            return Repository.QueryAll(lsWhere, keySelector, asc);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IList<T> QueryAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> keySelector, bool asc)
        {
            return Repository.QueryAll(where, keySelector, asc);
        }
        #endregion


        #region Query Paging
        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<T> QueryPaging(int page, int size, out int count)
        {
            return Repository.QueryPaging(page, size, out count);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, object>> keySelector, bool asc)
        {
            return Repository.QueryPaging(page, size, out count, keySelector, asc);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, bool>> where)
        {
            return Repository.QueryPaging(page, size, out count, where);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public IList<T> QueryPaging(int page, int size, out int count, IList<Expression<Func<T, bool>>> lsWhere)
        {
            return Repository.QueryPaging(page, size, out count, lsWhere);
        }

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
        public IList<T> QueryPaging(int page, int size, out int count, IList<Expression<Func<T, bool>>> lsWhere, Expression<Func<T, object>> keySelector, bool asc)
        {
            return Repository.QueryPaging(page, size, out count, lsWhere, keySelector, asc);
        }

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
        public IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, bool>> where, Expression<Func<T, object>> keySelector, bool asc)
        {
            return Repository.QueryPaging(page, size, out count, where, keySelector, asc);
        }
        #endregion
        #endregion


        #region Add
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            return Repository.Add(entity);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add1(T entity)
        {
            return Repository.Add1(entity);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Add(IEnumerable<T> entities)
        {
            return Repository.Add(entities);
        }
        #endregion


        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            return Repository.Update(entity);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Update(IEnumerable<T> entities)
        {
            return Repository.Update(entities);
        }
        #endregion


        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public int Delete(T entity)
        {
            return Repository.Delete(entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public int Delete(IEnumerable<T> entities)
        {
            return Repository.Delete(entities);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete<TKey>(TKey id)
        {
            return Repository.Delete<TKey>(id);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete<TKey>(IEnumerable<TKey> ids)
        {
            return Repository.Delete(ids);
        }
        #endregion
    }
}
