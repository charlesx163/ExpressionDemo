using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionDemo
{
    public class ExpressionTreeVisualizer
    {
        public static void Show()
        {
            {
                var para1 = Expression.Parameter(typeof(int), "m");
                var para2 = Expression.Parameter(typeof(int), "n");
                var para3 = Expression.Constant(2);
                BinaryExpression body = Expression.Add(Expression.Multiply(para1, para2), para3);
                Expression<Func<int, int, int>> expression = Expression.Lambda<Func<int, int, int>>(body,
                    new ParameterExpression[]
                    {
                        para1,
                        para2
                    });
                Console.WriteLine(expression.Compile().Invoke(1, 2));
            }

            {
                //Expression<Func<People, bool>> exp = x => x.Id.ToString().Equals("5");
                //拆分上边的表达式
                var para = Expression.Parameter(typeof(People), "x");
                Expression field = Expression.Field(para, typeof(People).GetField("Id"));
                var toString = typeof(People).GetMethod("ToString");
                var toStringCall = Expression.Call(field, toString, new Expression[0]);

                var equals = typeof(People).GetMethod("Equals");
                var constantPara = Expression.Constant(5, typeof(string));
                
                var equalsCall = Expression.Call(toStringCall,equals, new Expression[]
                    {
                        constantPara
                    });
                Expression<Func<People, bool>> expression = Expression.Lambda<Func<People, bool>>(equalsCall,new ParameterExpression[]{para});
            }


        }
    }
}
