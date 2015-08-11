using System;
using System.Linq.Expressions;

namespace XamarinCRM.Helpers
{
    public static class PropertyUtil<TSource>
    {
        public static string GetName<TResult>(Expression<Func<TSource, TResult>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }
    }
}

