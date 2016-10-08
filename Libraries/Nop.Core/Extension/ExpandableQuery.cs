using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Extension
{
    /// <summary>
    /// An IQueryable wrapper that allows us to visit the query's expression tree just before LINQ to SQL gets to it.
    /// This is based on the excellent work of Tomas Petricek: http://tomasp.net/blog/linq-expand.aspx
    /// </summary>
    public class ExpandableQuery<T> : IQueryable<T>, IOrderedQueryable<T>, IOrderedQueryable
    {
        private readonly ExpandableQueryProvider<T> provider;
        private readonly IQueryable<T> inner;

        internal IQueryable<T> InnerQuery { get { return inner; } }			// Original query, that we're wrapping

        internal ExpandableQuery(IQueryable<T> inner)
        {
            this.inner = inner;
            provider = new ExpandableQueryProvider<T>(this);
        }

        Expression IQueryable.Expression { get { return inner.Expression; } }

        Type IQueryable.ElementType { get { return typeof(T); } }

        IQueryProvider IQueryable.Provider { get { return provider; } }

        public IEnumerator<T> GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        public override string ToString()
        {
            return inner.ToString();
        }
    }

    internal class ExpandableQueryProvider<T> : IQueryProvider
    {
        private readonly ExpandableQuery<T> query;

        internal ExpandableQueryProvider(ExpandableQuery<T> query)
        {
            this.query = query;
        }

        // The following four methods first call ExpressionExpander to visit the expression tree, then call
        // upon the inner query to do the remaining work.

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            return new ExpandableQuery<TElement>(query.InnerQuery.Provider.CreateQuery<TElement>(expression.Expand()));
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            return query.InnerQuery.Provider.CreateQuery(expression.Expand());
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            return query.InnerQuery.Provider.Execute<TResult>(expression.Expand());
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return query.InnerQuery.Provider.Execute(expression.Expand());
        }
    }
}
