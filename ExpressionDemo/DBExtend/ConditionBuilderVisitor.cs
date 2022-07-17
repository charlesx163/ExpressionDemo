using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionDemo.DBExtend
{
    public class ConditionBuilderVisitor:ExpressionVisitor
    {
        private Stack<string> _StringStack = new Stack<string>();

        public string Condition()
        {
            string condition = string.Concat(this._StringStack.ToArray());
            this._StringStack.Clear();
            return condition;
        }

        /// <summary>
        /// 访问二元运算
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node == null) throw new ArgumentNullException("BinaryExpression");
            this._StringStack.Push(")");
            base.Visit(node.Right);
            this._StringStack.Push(" "+node.NodeType.ToSqlOperator() + "");
            base.Visit(node.Left);
            this._StringStack.Push("(");
            return node;
        }

        /// <summary>
        /// memberExpress: x.Age
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node == null) throw new ArgumentNullException("MemberExpression");
            this._StringStack.Push(" ["+node.Member.Name+"] ");
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node == null) throw new ArgumentNullException("ConstantExpression");
            this._StringStack.Push(" '"+node.Value+"' ");
            return node;
        }

        /// <summary>
        /// methodCall like x.Age.Equal()
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m == null) throw new ArgumentNullException("MethodCallExpression");
            string format;
            switch(m.Method.Name)
            {
                case "StartsWith":
                    format = "({0} LIKE {1}+'%')";
                    break;
                case "Contains":
                    format = "({0} LIKE '%'+{1}+'%')";
                    break;
                case "EndsWith":
                    format = "({0} LIKE '%'+{1})";
                    break;
                default:
                    throw new NotSupportedException($"{m.NodeType} is not support");
            };
            this.Visit(m.Object);
            this.Visit(m.Arguments[0]);
            string right = this._StringStack.Pop();
            string left = this._StringStack.Pop();
            this._StringStack.Push(string.Format(format,left,right));
            return m;
        }
            
    }
}
