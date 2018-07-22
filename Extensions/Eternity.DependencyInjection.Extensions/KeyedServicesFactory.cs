using System;
using System.Collections.Generic;
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
            if (Meta.TryGetValue(key, out var md))
            {
                var instance = (TService) ActivatorUtilities.CreateInstance(_serviceProvider, md.ValueType, p);
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