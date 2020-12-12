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
        public virtual IRepository<T> GetRepository() { return AutoFacHelper.Get<IRepository<T>>(); }
        #endregion


        #region Query
        #region Query Single
        /// <summary>
        /// Query Single
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T QuerySingleById<Tkey>(Tkey id)
        {
            return GetRepository().QuerySingleById<Tkey>(id);
        }

        /// <summary>
        /// Query Single
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T QuerySingle(Expression<Func<T, bool>> where)
        {
            return GetRepository().QuerySingle(where);
        }

        /// <summary>
        /// Query Single
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T QuerySingle(IEnumerable<Expression<Func<T, bool>>> wheres)
        {
            return GetRepository().QuerySingle(wheres);
        }
        #endregion


        #region Query All
        /// <summary>
        /// Query All
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> QueryAll()
        {
            return GetRepository().QueryAll();
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public virtual IList<T> QueryAll(Expression<Func<T, object>> keySelector, bool asc)
        {
            return GetRepository().QueryAll(keySelector, asc);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public virtual IList<T> QueryAll(Expression<Func<T, bool>> where)
        {
            return GetRepository().QueryAll(where);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public virtual IList<T> QueryAll(IList<Expression<Func<T, bool>>> lsWhere)
        {
            return GetRepository().QueryAll(lsWhere);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public virtual IList<T> QueryAll(IList<Expression<Func<T, bool>>> lsWhere, Expression<Func<T, object>> keySelector, bool asc)
        {
            return GetRepository().QueryAll(lsWhere, keySelector, asc);
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public virtual IList<T> QueryAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> keySelector, bool asc)
        {
            return GetRepository().QueryAll(where, keySelector, asc);
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
        public virtual IList<T> QueryPaging(int page, int size, out int count)
        {
            return GetRepository().QueryPaging(page, size, out count);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, object>> keySelector, bool asc)
        {
            return GetRepository().QueryPaging(page, size, out count, keySelector, asc);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, bool>> where)
        {
            return GetRepository().QueryPaging(page, size, out count, where);
        }

        /// <summary>
        /// Query Paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public virtual IList<T> QueryPaging(int page, int size, out int count, IList<Expression<Func<T, bool>>> lsWhere)
        {
            return GetRepository().QueryPaging(page, size, out count, lsWhere);
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
        public virtual IList<T> QueryPaging(int page, int size, out int count, IList<Expression<Func<T, bool>>> lsWhere, Expression<Func<T, object>> keySelector, bool asc)
        {
            return GetRepository().QueryPaging(page, size, out count, lsWhere, keySelector, asc);
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
        public virtual IList<T> QueryPaging(int page, int size, out int count, Expression<Func<T, bool>> where, Expression<Func<T, object>> keySelector, bool asc)
        {
            return GetRepository().QueryPaging(page, size, out count, where, keySelector, asc);
        }
        #endregion
        #endregion


        #region Add
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add(T entity)
        {
            return GetRepository().Add(entity);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Add1(T entity)
        {
            return GetRepository().Add1(entity);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Add(IEnumerable<T> entities)
        {
            return GetRepository().Add(entities);
        }
        #endregion


        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(T entity)
        {
            return GetRepository().Update(entity);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Update(IEnumerable<T> entities)
        {
            return GetRepository().Update(entities);
        }
        #endregion


        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Delete(T entity)
        {
            return GetRepository().Delete(entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Delete(IEnumerable<T> entities)
        {
            return GetRepository().Delete(entities);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete<TKey>(TKey id)
        {
            return GetRepository().Delete<TKey>(id);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete<TKey>(IEnumerable<TKey> ids)
        {
            return GetRepository().Delete(ids);
        }
        #endregion
    }
}
