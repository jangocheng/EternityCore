using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Eternity.DependencyInjection.Extensions
{
    public class KeyedServicesFactory<TKey, TService> : IKeyedServicesFactory<TKey, TService>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Action<TService, Metadata> _actived;

        public KeyedServicesFactory(IServiceProvider serviceProvider, Dictionary<TKey, Metadata> meta,
            Action<TService, Metadata> actived = null)
        {
            _serviceProvider = serviceProvider;
            _actived = actived;
            Meta = meta;
        }

        public TService GetService(TKey key, params object[] p)
        {
            if (!Meta.TryGetValue(key, out var meta))
                throw new ArgumentException("No service is registered for given key");
            //var service=(TService)_serviceProvider.GetService(implementationType);

            var service = (TService)Activator.CreateInstance(meta.ValueType, p);

            _actived?.Invoke(service, meta);
            return service;
        }

        public IReadOnlyDictionary<TKey, Metadata> Meta { get; }



    }
}
