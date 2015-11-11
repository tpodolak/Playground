using System.Linq.Expressions;

namespace CustomLinqProvider
{
    internal class TranslateResult
    {
        internal string CommandText;
        internal LambdaExpression Projector;
    }
}