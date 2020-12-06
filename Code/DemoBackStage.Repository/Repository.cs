using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

using DemoBackStage.DAL;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    /// <summary>
    /// Repository<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        #region Common
        /// <summary>
        /// Get Db
        /// </summary>
        /// <returns></returns>
        protected SqlSugarClient GetDb()
        {
            return SqlSugarHelper.GetDb();
        }
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
            using (var db = GetDb())
            {
                return db.Queryable<T>().In<Tkey>(id).First();
            }
        }

        /// <summary>
        /// Query Single
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T QuerySingle(Expression<Func<T, bool>> where)
        {
            using (var db = GetDb())
            {
                return db.Queryable<T>().Where(where).First();
            }
        }

        /// <summary>
        /// Query Single
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T QuerySingle(IEnumerable<Expression<Func<T, bool>>> wheres)
        {
            using (var db = GetDb())
            {
                var query = db.Queryable<T>();
                foreach (var item in wheres)
                {
                    query = query.Where(item);
                }

                return query.First();
            }
        }
        #endregion

        #region Query All
        /// <summary>
        /// Query All
        /// </summary>
        /// <returns></returns>
        public IList<T> QueryAll()
        {
            using (var db = GetDb())
            {
                return db.Queryable<T>().Take(10).ToList();
            }
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IList<T> QueryAll(Expression<Func<T, object>> keySelector, bool asc)
        {
            using (var db = GetDb())
            {
                var query = db.Queryable<T>();
                if (asc)
                {
                    query = query.OrderBy(keySelector, OrderByType.Asc);
                }
                else
                {
                    query = query.OrderBy(keySelector, OrderByType.Desc);
                }

                return query.ToList();
            }
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public IList<T> QueryAll(Expression<Func<T, bool>> where)
        {
            using (var db = GetDb())
            {
                var query = db.Queryable<T>();
                query = query.Where(where);

                return query.ToList();
            }
        }

        /// <summary>
        /// Query All
        /// </summary>
        /// <param name="lsWhere"></param>
        /// <returns></returns>
        public IList<T> QueryAll(IList<Expression<Func<T, bool>>> lsWhere)
        {
            using (var db = GetDb())
            {
                var query = db.Queryable<T>();
                foreach (var item in lsWhere)
                {
                    query = query.Where(item);
                }

                return query.ToList();
            }
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
            using (var db = GetDb())
            {
                var query = db.Queryable<T>();
                foreach (var item in lsWhere)
                {
                    query = query.Where(item);
                }
                if (asc)
                {
                    query = query.OrderBy(keySelector, OrderByType.Asc);
                }
                else
                {
                    query = query.OrderBy(keySelector, OrderByType.Desc);
                }

                return query.ToList();
            }
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
            using (var db = GetDb())
            {
                var query = db.Queryable<T>();
                query = query.Where(where);
                if (asc)
                {
                    query = query.OrderBy(keySelector, OrderByType.Asc);
                }
                else
                {
                    query = query.OrderBy(keySelector, OrderByType.Desc);
                }

                return query.ToList();
            }
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
            using (var db = GetDb())
            {
                count = 0;
                var query = db.Queryable<T>();

                count = query.Count();
                if (count > 0)
                {
                    query = query.Skip((page - 1) * size).Take(size);

                    return query.ToList();
                }
                else
                {
                    return new List<T>();
                }
            }
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
            using (var db = GetDb())
            {
                count = 0;
                var query = db.Queryable<T>();

                count = query.Count();
                if (count > 0)
                {
                    if (asc)
                    {
                        query = query.OrderBy(keySelector, OrderByType.Asc);
                    }
                    else
                    {
                        query = query.OrderBy(keySelector, OrderByType.Desc);
                    }

                    query = query.Skip((page - 1) * size).Take(size);

                    return query.ToList();
                }
                else
                {
                    return new List<T>();
                }
            }
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
            using (var db = GetDb())
            {
                count = 0;
                var query = db.Queryable<T>();
                query = query.Where(where);

                count = query.Count();
                if (count > 0)
                {
                    query = query.Skip((page - 1) * size).Take(size);

                    return query.ToList();
                }
                else
                {
                    return new List<T>();
                }
            }
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
            using (var db = GetDb())
            {
                count = 0;
                var query = db.Queryable<T>();
                foreach (var item in lsWhere)
                {
                    query = query.Where(item);
                }

                count = query.Count();
                if (count > 0)
                {
                    query = query.Skip((page - 1) * size).Take(size);

                    return query.ToList();
                }
                else
                {
                    return new List<T>();
                }
            }
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
            using (var db = GetDb())
            {
                count = 0;
                var query = db.Queryable<T>();
                foreach (var item in lsWhere)
                {
                    query = query.Where(item);
                }

                count = query.Count();
                if (count > 0)
                {
                    if (asc)
                    {
                        query = query.OrderBy(keySelector, OrderByType.Asc);
                    }
                    else
                    {
                        query = query.OrderBy(keySelector, OrderByType.Desc);
                    }
                    query = query.Skip((page - 1) * size).Take(size);

                    return query.ToList();
                }
                else
                {
                    return new List<T>();
                }
            }
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
            using (var db = GetDb())
            {
                count = 0;
                var query = db.Queryable<T>();
                query = query.Where(where);

                count = query.Count();
                if (count > 0)
                {
                    if (asc)
                    {
                        query = query.OrderBy(keySelector, OrderByType.Asc);
                    }
                    else
                    {
                        query = query.OrderBy(keySelector, OrderByType.Desc);
                    }
                    query = query.Skip((page - 1) * size).Take(size);

                    return query.ToList();
                }
                else
                {
                    return new List<T>();
                }
            }
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
            using (var db = GetDb())
            {
                return db.Insertable<T>(entity).ExecuteReturnIdentity();
            }
        }

        /// <summary>
        /// Add1
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add1(T entity)
        {
            using (var db = GetDb())
            {
                return db.Insertable<T>(entity).ExecuteReturnEntity();
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Add(IEnumerable<T> entities)
        {
            using (var db = GetDb())
            {
                return db.Insertable<T>(entities.ToArray()).ExecuteCommand();
            }
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
            using (var db = GetDb())
            {
                return db.Updateable<T>(entity).ExecuteCommand();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Update(IEnumerable<T> entities)
        {
            using (var db = GetDb())
            {
                return db.Updateable<T>(entities.ToList()).ExecuteCommand();
            }
        }
        #endregion


        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public int Delete(T entity)
        {
            using (var db = GetDb())
            {
                return db.Deleteable<T>().ExecuteCommand();
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public int Delete(IEnumerable<T> entities)
        {
            using (var db = GetDb())
            {
                return db.Deleteable<T>().Where(entities.ToList()).ExecuteCommand();
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete<TKey>(TKey id)
        {
            using (var db = GetDb())
            {
                return db.Deleteable<T>().In<TKey>(id).ExecuteCommand();
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete<TKey>(IEnumerable<TKey> ids)
        {
            using (var db = GetDb())
            {
                return db.Deleteable<T>().In<TKey>(ids.ToArray()).ExecuteCommand();
            }
        }
        #endregion
    }
}
