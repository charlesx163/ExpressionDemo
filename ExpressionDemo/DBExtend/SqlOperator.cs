using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionDemo.DBExtend
{
    internal static class SqlOperator
    {
        internal static string ToSqlOperator(this ExpressionType type) =>
            type switch
            {
                ExpressionType.AndAlso => "AND",
                ExpressionType.And => "AND",
                ExpressionType.Or => "OR",
                ExpressionType.OrElse => "OR",
                ExpressionType.Not => "NOT",
                ExpressionType.NotEqual => "<>",
                ExpressionType.GreaterThan => ">",
                ExpressionType.GreaterThanOrEqual => ">=",
                ExpressionType.LessThan => "<",
                ExpressionType.LessThanOrEqual => "<=",
                ExpressionType.Equal => "=",
                _ => throw new Exception("not support this operator"),
            };
    }
}
