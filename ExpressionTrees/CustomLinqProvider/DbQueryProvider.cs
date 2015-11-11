using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomLinqProvider
{
    public class DbQueryProvider : QueryProvider
    {
        DbConnection connection;

        public DbQueryProvider(DbConnection connection)
        {
            this.connection = connection;
        }

        public override string GetQueryText(Expression expression)
        {
            return Translate(expression).CommandText;
        }

        public override object Execute(Expression expression)
        {
            TranslateResult result = this.Translate(expression);

            DbCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = result.CommandText;
            DbDataReader reader = cmd.ExecuteReader();

            Type elementType = TypeSystem.GetElementType(expression.Type);
            if (result.Projector != null)
            {
                Delegate projector = result.Projector.Compile();
                return Activator.CreateInstance(
                    typeof(ProjectionReader<>).MakeGenericType(elementType),
                    BindingFlags.Instance | BindingFlags.NonPublic, null,
                    new object[] { reader, projector },
                    null
                    );
            }
            else
            {
                return Activator.CreateInstance(
                    typeof(ObjectReader<>).MakeGenericType(elementType),
                    BindingFlags.Instance | BindingFlags.NonPublic, null,
                    new object[] { reader },
                    null
                    );
            }
        }

        private TranslateResult Translate(Expression expression)
        {
            expression = Evaluator.PartialEval(expression);
            return new QueryTranslator().Translate(expression);
        }
    }
}