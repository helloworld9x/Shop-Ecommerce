using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Nop.Core.Data
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Count entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Count entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long LongCount(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find Entites
        /// </summary>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="projection"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        IEnumerable<TProjection> Find<TProjection>(Expression<Func<T, bool>> filterExpression,Expression<Func<T, TProjection>> projection, FindOptions<T> findOptions);

        /// <summary>
        /// Find Entites
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="findOptions"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> filterExpression, FindOptions<T> findOptions = null,params Expression<Func<T, dynamic>>[] includes);

        /// <summary>
        /// Find Entity
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        T FindOne(Expression<Func<T, bool>> filterExpression, FindOptions<T> findOptions = null);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
