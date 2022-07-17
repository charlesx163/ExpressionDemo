using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ExpressionDemo.MappingExtend;

namespace ExpressionDemo
{
    public class ExpressionTest
    {
        public static void Show()
        {
            {
                Expression<Func<int, int, int>> exp = (m, n) => m * n + 2;
                var c = exp.Compile().Invoke(1, 2);
                Console.WriteLine(c);
            }
            People p = new People
            {
                Id = 1,
                Name = "Honda",
                Age = 2
            };
            //反射
            {
               // var cp=ReflectionMapper.Mapping<People,PeopleCopy>(p);
            }
            //expression:
            {
               
                var cp = ExpressionMapper.Map<People, PeopleCopy>(p);
            }
            //泛型缓存+Expression
            {
                var cp = ExpressionGenericMapper<People, PeopleCopy>.Map(p);
            }
        }
        
    }
}
