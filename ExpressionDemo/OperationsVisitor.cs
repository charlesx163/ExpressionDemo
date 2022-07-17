using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ExpressionDemo
{
    /// <summary>
    /// ExpressionVisitor:肯定得递归的解析表达式目录树，因为不知道深度的一棵树
    /// 只有一个入口叫Visit
    /// 首先检查是个什么表达式，然后调用对应的protected virtual visit的方法
    /// 得到结果继续去 检查类型--递归调用对应的visit
    /// </summary>
    public class OperationsVisitor : ExpressionVisitor
    {
        public Expression Modify(Expression expression)
        {
            return base.Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add)
            {
                Expression left = base.Visit(node.Left);
                Expression right = base.Visit(node.Right);
                return Expression.Subtract(left, right);
            }

            //if (node.NodeType == ExpressionType.Multiply)
            //{
            //    Expression left = base.Visit(node.Left);
            //    Expression right = base.Visit(node.Right);
                
            //}
            return base.VisitBinary(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }
    }
}
