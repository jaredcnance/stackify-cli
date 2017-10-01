using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace StackifyCli.Extensions
{
    public static class IEnumerableExtensions
    {
        public static List<T> Filter<T>(this List<T> list, Expression<Func<T, string>> selector, string value)
        {
            if (string.IsNullOrWhiteSpace(value) == false)
                return ApplyFilter(list, selector, value);

            return list;
        }

        private static List<T> ApplyFilter<T>(List<T> deployments, Expression<Func<T, string>> selector, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "model");

            var property = selector.Body as MemberExpression;
            var propInfo = property.Member as PropertyInfo;

            var left = Expression.PropertyOrField(parameter, propInfo.Name);
            var right = Expression.Constant(value, typeof(string));
            var body = Expression.Equal(left, right);
            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

            return deployments.Where(predicate.Compile()).ToList();
        }
    }
}