using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionDemo
{
    public static class ExpressionExtend
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
            NewExpressionVisitor visitor = new NewExpressionVisitor(parameterExpression);
            var left = visitor.Replace(expr1.Body);
            var right = visitor.Replace(expr2.Body);
            var body = Expression.And(left, right);
            return Expression.Lambda<Func<T,bool>>(body, parameterExpression);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
            NewExpressionVisitor visitor = new NewExpressionVisitor(parameterExpression);
            var left = visitor.Replace(expr1.Body);
            var right = visitor.Replace(expr2.Body);
            var body = Expression.Or(left, right);
            return Expression.Lambda<Func<T, bool>>(body, parameterExpression);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            var candidateExpr = expr.Parameters[0];
            var body = Expression.Not(expr.Body);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }
}
