﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionDemo
{
    internal class NewExpressionVisitor:ExpressionVisitor
    {
        public ParameterExpression _NewParameter { get; private set; }

        public NewExpressionVisitor(ParameterExpression parameter)
        {
            this._NewParameter = parameter;
        }

        public Expression Replace(Expression exp)
        {
            return this.Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this._NewParameter;
        }
    }
}
