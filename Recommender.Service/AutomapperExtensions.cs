using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Recommender.Service
{
    public static class AutomapperExtensions
    {
        public static List<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
            {
                throw new ArgumentNullException();
            }

            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }

        public static TResult MapTo<TResult>(this object self)
        {
            if (self == null)
            {
                throw new ArgumentNullException();
            }

            return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        }

        public static void MapPropertiesFrom<TSource, TDestination>(this object destination, TSource source)
        {
            if (destination == null)
            {
                throw new ArgumentNullException();
            }

            Mapper.Map<TSource, TDestination>(source, (TDestination)destination);
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }

            return expression;
        }
    }
}
