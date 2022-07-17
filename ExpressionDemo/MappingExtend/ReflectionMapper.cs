using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionDemo.MappingExtend
{
    public static class ReflectionMapper
    {
        public static TDestination Mapping<TSource, TDestination>(TSource tSource)
        {
            var destinationInstance = Activator.CreateInstance<TDestination>();
            foreach (var destinationProp in destinationInstance.GetType().GetProperties())
            {

                var sourceProp=tSource.GetType().GetProperty(destinationProp.Name);
                destinationProp.SetValue(destinationInstance,sourceProp.GetValue(tSource));
            }
            foreach (var destinationField in destinationInstance.GetType().GetFields())
            {

                var sourceField = tSource.GetType().GetField(destinationField.Name);
                destinationField.SetValue(destinationInstance, sourceField.GetValue(tSource));
            }
            return destinationInstance;
        }
    }
}
