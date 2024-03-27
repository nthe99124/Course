using System.Linq.Expressions;

namespace CourseProject.API.Common.Ulti
{
    public static class ExtensionUlti
    {
        public static Expression<Func<T, bool>> PedicateAnd<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
