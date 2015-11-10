using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CustomLinqProvider
{
    public class Query<T> : IOrderedQueryable<T>
    {
        private readonly QueryProvider provider;
        private readonly Expression expression;

        public Expression Expression => this.expression;
        public Type ElementType => typeof(T);
        public IQueryProvider Provider => this.provider;

        public Query(QueryProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            this.provider = provider;
            this.expression = Expression.Constant(this);
        }

        public Query(QueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException(nameof(expression));
            }
            this.provider = provider;
            this.expression = expression;
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)this.provider.Execute(this.expression)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.provider.Execute(this.expression)).GetEnumerator();

        public override string ToString() => this.provider.GetQueryText(this.expression);
    }
}