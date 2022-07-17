using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExpressionDemo.DBExtend;

namespace ExpressionDemo
{
    public class ExpressionVisitorTest
    {
        public static void Show()
        {
            {
                // modify express
                //Expression<Func<int, int, int>> exp = (m, n) => m * n + 2 + 3;
                //modify the (m * n + 2) to (m * n - 2)
                //OperationsVisitor visitor = new OperationsVisitor();
                //var expression = visitor.Modify(exp);
            }
            {
                var source = new List<People>().AsQueryable();
                var result = source.Where(x => x.Age > 5);
                Expression<Func<People, bool>> exp = x => x.Age > 5;
                ConditionBuilderVisitor visitor = new ConditionBuilderVisitor();
                visitor.Visit(exp);
                Console.WriteLine(visitor.Condition());

            }
            {
                Expression<Func<People, bool>> lambda = x =>
                    x.Age > 5 && x.Id > 5 && x.Name.StartsWith("1") && x.Name.EndsWith("1") && x.Name.Contains("1");
                string sql = $"Delete From [{typeof(People).Name}] where [Age]>5 and [ID]>5";
                ConditionBuilderVisitor visitor = new ConditionBuilderVisitor();
                visitor.Visit(lambda);
            }
            //表达式连接
            {
                Expression<Func<People, bool>> lambda1 = x => x.Age > 5;
                Expression<Func<People, bool>> lambda2 = x => x.Id > 5;
                Expression<Func<People, bool>> lambda3 = lambda1.And(lambda2);
            }
        }
    }
}
