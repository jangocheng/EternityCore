using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Eternity.DependencyInjection.Extensions
{
    public class KeyedServicesFactory<TKey, TService> : IKeyedServicesFactory<TKey, TService>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Action<TService, Metadata> _actived;
        public IReadOnlyDictionary<TKey, Metadata> Meta { get; }
        public KeyedServicesFactory(IServiceProvider serviceProvider, Dictionary<TKey, Metadata> meta,
            Action<TService, Metadata> actived = null)
        {
            _serviceProvider = serviceProvider;
            _actived = actived;
            Meta = meta;
        }

        public TService GetService(TKey key, params object[] p)
        {
            /* var types = new Type[] { typeof(TKey) }.Union(_paramTypes).Union(new[] { typeof(TService) }).ToArray();

             dynamic instanceProxy = _serviceProvider.GetService(Type.GetType("System.Func`" + (p.Length + 2)).MakeGenericType(types));

             var instance = instanceProxy.DynamicInvoke(new object[] { key }.Union(p).ToArray());
             if (instance != null)
             {
                 _actived?.Invoke(instance, Meta[key]);
             }
             return instance;*/

            if (Meta.TryGetValue(key, out var md))
            {
                var instance = (TService)ActivatorUtilities.CreateInstance(_serviceProvider, md.ValueType, p);
                if (instance != null)
                {
                    _actived?.Invoke(instance, md);
                    return instance;
                }

            }

            return default(TService);


        }
    }
}
