using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Extentions
{
    public static class OrderByExtentions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName, bool isDescending)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), ExtentionConsts.FIELD);
            Expression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string method = isDescending ? ExtentionConsts.DESCENDING : ExtentionConsts.ORDER_BY;
            MethodInfo orderByMethod = typeof(Queryable).GetMethods().First(x => x.Name == method && x.GetParameters().Length == ExtentionConsts.COUNT_PARAMETERS);
            MethodInfo orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TSource), property.Type);
            object result = orderByGeneric.Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<TSource>)result;
        }
    }
}
