using System.Linq.Expressions;

namespace System.Linq;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderBy, bool asc)
    {
        var expression = source.Expression;

        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

        MemberExpression? selector = null;
        string[] arrOrder = orderBy.Split('.');
        foreach (var item in arrOrder)
        {
            selector = Expression.PropertyOrField((Expression?)selector ?? parameter, item);
        }

        var method = asc ? "OrderBy" : "OrderByDescending";
        expression = Expression.Call(typeof(Queryable), method,
            [source.ElementType, selector!.Type],
            expression, Expression.Quote(Expression.Lambda(selector, parameter)));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(expression);
    }
}
