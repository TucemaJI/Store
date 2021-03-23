using System.Linq;
using System.Linq.Expressions;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Extentions
{
    public static class OrderByExtentions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName, bool isDescending)
        {
            // LAMBDA: x => x.[PropertyName]
            var parameter = Expression.Parameter(typeof(TSource), ExtentionOptions.X);
            Expression property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            // REFLECTION: source.OrderBy(x => x.Property)
            var method = isDescending ? ExtentionOptions.DESCENDING : ExtentionOptions.ORDER_BY;
            var orderByMethod = typeof(Queryable).GetMethods().First(x => x.Name == method && x.GetParameters().Length == ExtentionOptions.COUNT_PARAMETERS);
            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TSource), property.Type);
            var result = orderByGeneric.Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<TSource>)result;
        }
    }
}
