using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Eternity.DependencyInjection.Extensions
{
    public class ServicesBuilder<TKey, TService> : IServicesBuilder<TKey, TService>
    {

        private readonly IServiceCollection _services;

        private readonly Dictionary<TKey, Metadata> _registrations
            = new Dictionary<TKey, Metadata>();

        internal ServicesBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public IServicesBuilder<TKey, TService> Add(TKey key, Type implemtnationType)
        {
            _registrations.Add(key, new Metadata(implemtnationType));
            _services.AddTransient(implemtnationType);
            return this;
        }

        public IServicesBuilder<TKey, TService> Add<T>(TKey key) where T : class, TService
        {
            return Add(key, typeof(T));
        }

        public IServicesBuilder<TKey, TService> Add(Type implemtnationType, Func<Metadata, TKey> key)
        {

            var meta = new Metadata(implemtnationType);
            var k = key(meta);
            _registrations.Add(k, meta);
            _services.AddTransient(implemtnationType);
            return this;
        }


        public void Build(Action<TService,Metadata> actived = null)
        {
            var registrations = _registrations;
            //Registrations are shared across all instances
            _services.AddSingleton(typeof(IKeyedServicesFactory<TKey, TService>), s =>
            {
                return new KeyedServicesFactory<TKey, TService>(s, registrations, actived);
            });
        }


    }
}
