using System;
using System.Linq;
using System.Linq.Expressions;
using TechFix.Common.Constants;

namespace TechFix.Common.Helper
{
    public  class QueryHelper
    {
        public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string comparison, object value)
        {
            var parameter = Expression.Parameter(typeof(T));
            var left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
            var body = MakeComparison(left, comparison, value);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression MakeComparison(Expression left, string comparison, object value)
        {
            var constant = Expression.Constant(value, left.Type);
            switch (comparison)
            {
                case QueryComparison.Equal:
                    return Expression.MakeBinary(ExpressionType.Equal, left, constant);
                case QueryComparison.NotEqual:
                    return Expression.MakeBinary(ExpressionType.NotEqual, left, constant);
                case QueryComparison.GreaterThan:
                    return Expression.MakeBinary(ExpressionType.GreaterThan, left, constant);
                case QueryComparison.GreaterThanOrEqual:
                    return Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, left, constant);
                case QueryComparison.LessThan:
                    return Expression.MakeBinary(ExpressionType.LessThan, left, constant);
                case QueryComparison.LessThanOrEqual:
                    return Expression.MakeBinary(ExpressionType.LessThanOrEqual, left, constant);
                case QueryComparison.Contains:
                case QueryComparison.StartsWith:
                case QueryComparison.EndsWith:
                    if (value is string)
                    {
                        return Expression.Call(left, comparison, Type.EmptyTypes, constant);
                    }
                    throw new NotSupportedException($"Comparison operator '{comparison}' only supported on string.");
                default:
                    throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
            }
        }
    }
}
