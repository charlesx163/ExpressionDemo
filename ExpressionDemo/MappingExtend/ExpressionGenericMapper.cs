using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionDemo.MappingExtend
{
    /// <summary>
    /// Generic Cache+Expression Map
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class ExpressionGenericMapper<TIn, TOut>
    {
        private static Func<TIn, TOut> _Func = null;
        static ExpressionGenericMapper()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (var item in typeof(TOut).GetProperties())
            {
                MemberExpression prop = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, prop);
                memberBindings.Add(memberBinding);
            }
            foreach (var item in typeof(TOut).GetFields())
            {
                MemberExpression field = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, field);
                memberBindings.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)),memberBindings);
            Expression<Func<TIn, TOut>> exp = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression,
                new ParameterExpression[]
                {
                        parameterExpression
                });

            _Func = exp.Compile();
        }

        public static TOut Map(TIn tin)
        {
            return _Func(tin);
        }
    }
}
