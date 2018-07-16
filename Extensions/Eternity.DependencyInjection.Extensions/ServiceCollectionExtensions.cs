using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Eternity.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServicesBuilder<TKey, TService> AddKeyed<TKey, TService>(this IServiceCollection services)
        {
            return new ServicesBuilder<TKey, TService>(services);
        }

        public static IServicesBuilder<TKey, TService> AddKeyed<TKey, TService>(this IServiceCollection services, IEnumerable<Type> implemtnationTypes, Func<Type, TKey> key)
        {

            var builder = new ServicesBuilder<TKey, TService>(services);
            foreach (var type in implemtnationTypes)
            {
                builder.Add(implemtnationType: type, key: key(type));
            }

            return builder;
        }

        public static IServicesBuilder<TKey, TService> AddKeyed<TKey, TService>(this IServiceCollection services, Assembly assembly, Func<Metadata, TKey> key)
        {
            var serviceType = typeof(TService);
            var builder = new ServicesBuilder<TKey, TService>(services);

            var implemtnationTypes = assembly.ExportedTypes.Where(t => !t.IsAbstract && t.IsClass && serviceType.IsAssignableFrom(t));
            foreach (var type in implemtnationTypes)
            {
                builder.Add(type, key);
            }

            return builder;
        }

    }
}
