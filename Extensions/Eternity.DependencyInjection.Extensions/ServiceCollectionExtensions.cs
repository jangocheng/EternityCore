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

        public static Type[] GetImplemtnationTypes<T>(this Assembly assembly)
        {
            var serviceType = typeof(T);
            var implemtnationTypes = assembly.ExportedTypes.Where(t => !t.IsAbstract && t.IsClass && serviceType.IsAssignableFrom(t));
            return implemtnationTypes.ToArray();
        }


        public static IServicesBuilder<TKey, TService> AddKeyed<TKey, TService>(this IServiceCollection services, Type[] types, Func<Metadata, TKey> getKey = null)
        {
            var builder = new ServicesBuilder<TKey, TService>(services, getKey);
            builder.Add(types);
            return builder;
        }

        public static IServicesBuilder<string, TService> AddKeyed<TService>(this IServiceCollection services, Type[] types, Func<Metadata, string> getKey = null)
        {
            return AddKeyed<string, TService>(services, types, getKey);
        }


    }
}
