using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime;
using System.Text;

namespace ExpressionDemo.MappingExtend
{
    /// <summary>
    /// Expression:只有在第一次的时候 才拼装映射关系，第一次拼装完成后可以缓存起来，第二次就不用在去拼装直接拿来用就可以了
    /// 而反射每次都要去解析
    /// </summary>
    public class ExpressionMapper
    {
        private static Dictionary<string, object> _Dic = new Dictionary<string, object>();

        public static TOut Map<TIn, TOut>(TIn tIn)
        {
            string key = $"funckey_{typeof(TIn).FullName}_{typeof(TOut).FullName}";
            if (!_Dic.ContainsKey(key))
            {
                ParameterExpression parameterExpression = ParameterExpression.Parameter(typeof(TIn), "p");
                List<MemberBinding> memberBindings = new List<MemberBinding>();
                foreach (var item in typeof(TOut).GetProperties())
                {
                    MemberExpression prop =
                        Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, prop);
                    memberBindings.Add(memberBinding);
                }
                foreach (var item in typeof(TOut).GetFields())
                {
                    MemberExpression field =
                        Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, field);
                    memberBindings.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)),memberBindings);
                Expression<Func<TIn, TOut>> exp = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression,
                    new ParameterExpression[]
                    {
                        parameterExpression
                    });

                Func<TIn, TOut> func = exp.Compile();
                _Dic[key] = func;
            }

            return ((Func<TIn, TOut>) _Dic[key]).Invoke(tIn);
        }
    }
}
