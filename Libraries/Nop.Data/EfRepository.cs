using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Nop.Core;
using Nop.Core.Collections;
using Nop.Core.Data;
using Nop.Core.Extension;

namespace Nop.Data
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(object id)
        {
            //see some suggested performance optimization (not tested)
            //http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id/11688189#comment34876113_11688189
            return Entities.Find(id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                int numberOfRecords = entities.Count();

                if (numberOfRecords >= 200)
                {
                    int position = 0;
                    while (numberOfRecords - position > 0)
                    {
                        var records = entities.Skip(position)
                            .Take(100)
                            .ToHashSet();

                        foreach (var entity in records)
                            Entities.Add(entity);

                        _context.SaveChanges();

                        position += 100;
                    }
                }
                else
                {
                    foreach (var entity in entities)
                        Entities.Add(entity);

                    _context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Count entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? Entities.Count() : Entities.Count(predicate);
        }

        /// <summary>
        /// Count entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>long</returns>
        public virtual long LongCount(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? Entities.LongCount() : Entities.LongCount(predicate);
        }

        /// <summary>
        /// Find Entities
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="findOptions"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> filterExpression, FindOptions<T> findOptions = null, params Expression<Func<T, dynamic>>[] includes)
        {
            IQueryable<T> queryable = Entities;

            if (!includes.IsNullOrEmpty())
            {
                queryable = includes.Aggregate(queryable, (current, include) => current.Include(include));
            }

            var skip = 0;
            var limit = 0;

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy((IOrderedQueryable<T>)queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }

                if (findOptions.Skip.HasValue && findOptions.Skip.Value > 0)
                {
                    skip = findOptions.Skip.Value;
                }

                if (findOptions.Limit.HasValue && findOptions.Limit.Value > 0)
                {
                    limit = findOptions.Limit.Value;
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            if (skip > 0)
            {
                queryable = queryable.Skip(skip);
            }

            if (limit > 0)
            {
                queryable = queryable.Take(limit);
            }

            return queryable.ToHashSet();
        }

        /// <summary>
        /// Find Entities
        /// </summary>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="projection"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual IEnumerable<TProjection> Find<TProjection>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjection>> projection, FindOptions<T> findOptions = null)
        {
            if (projection == null)
            {
                throw new ArgumentNullException("projection");
            }

            IQueryable<T> queryable = Entities;

            int skip = 0, limit = 0;

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy((IOrderedQueryable<T>)queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }

                if (findOptions.Skip.HasValue && findOptions.Skip.Value > 0)
                {
                    skip = findOptions.Skip.Value;
                }

                if (findOptions.Limit.HasValue && findOptions.Limit.Value > 0)
                {
                    limit = findOptions.Limit.Value;
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            if (skip > 0)
            {
                queryable = queryable.Skip(skip);
            }

            if (limit > 0)
            {
                queryable = queryable.Take(limit);
            }

            return queryable.Select(projection).ToHashSet();
        }

        /// <summary>
        /// Find Entity
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public T FindOne(Expression<Func<T, bool>> filterExpression, FindOptions<T> findOptions = null)
        {
            IQueryable<T> queryable = Entities;

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy((IOrderedQueryable<T>)queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }
            }

            if (filterExpression != null)
            {
                return queryable.FirstOrDefault(filterExpression);
            }

            return queryable.FirstOrDefault();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        #endregion

        #region Non-Public Methods

        private static Expression<Func<TSource, TSourceKey>> GetExpression<TSource, TSourceKey>(LambdaExpression lambdaExpression)
        {
            return (Expression<Func<TSource, TSourceKey>>)lambdaExpression;
        }

        /// <summary>
        /// Orders a queryable according to specified SortDirection
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The queryable</param>
        /// <param name="sortDirection">The sort direction</param>
        /// <param name="isFirst">true to use OrderBy(), false to use ThenBy()</param>
        /// <returns></returns>
        private static IOrderedQueryable<TSource> SetOrderBy<TSource>(
            IOrderedQueryable<TSource> source, KeyValuePair<LambdaExpression, SortDirection> sortDirection, bool isFirst)
        {
            Type type = sortDirection.Key.Body.Type;
            bool isNullable = type.IsNullable();

            if (isNullable)
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (isFirst)
            {
                if (sortDirection.Value == SortDirection.Ascending)
                {
                    #region OrderBy

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return source.OrderBy(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, short?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, long?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, float?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, decimal>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }
                        default:
                            if (type == typeof(Guid))
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, Guid?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, Guid>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion OrderBy
                }
                else
                {
                    #region OrderByDescending

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return source.OrderByDescending(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, short?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, long?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, float?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, decimal>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }

                        default:
                            if (type == typeof(Guid))
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, Guid?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, Guid>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion OrderByDescending
                }
            }
            else
            {
                if (sortDirection.Value == SortDirection.Ascending)
                {
                    #region ThenBy

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return source.ThenBy(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, short?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, long?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, float?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, decimal>(sortDirection.Key));
                            }

                        default:
                            if (type == typeof(Guid))
                            {
                                return isNullable
                                    ? source.ThenBy(GetExpression<TSource, Guid?>(sortDirection.Key))
                                    : source.ThenBy(GetExpression<TSource, Guid>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion ThenBy
                }
                else
                {
                    #region ThenByDescending

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return source.ThenByDescending(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, short?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, long?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, float?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, decimal>(sortDirection.Key));
                            }

                        default:
                            if (type == typeof(Guid))
                            {
                                return isNullable
                                    ? source.ThenByDescending(GetExpression<TSource, Guid?>(sortDirection.Key))
                                    : source.ThenByDescending(GetExpression<TSource, Guid>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion ThenByDescending
                }
            }
        }

        #endregion Non-Public Methods
    }
}