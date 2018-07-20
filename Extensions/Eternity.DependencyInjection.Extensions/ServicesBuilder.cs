using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Eternity.DependencyInjection.Extensions
{
    public class ServicesBuilder<TKey, TService> : IServicesBuilder<TKey, TService>
    {

        private readonly IServiceCollection _services;
        private readonly Func<Metadata, TKey> _getKey;

        private readonly Dictionary<TKey, Metadata> _registrations
            = new Dictionary<TKey, Metadata>();

        internal ServicesBuilder(IServiceCollection services, Func<Metadata, TKey> getKey)
        {
            _services = services;
            _getKey = getKey;
        }


        /*public IServicesBuilder<TKey, TService> Add<T>(Type implemtnationType, Func<Metadata, TKey> key = null)
        {
            var metadata = new Metadata(implemtnationType);
            _registrations.Add((key ?? _getKey)(metadata), metadata);

            _services.AddTransient(sp =>
            {
                return new Func<TKey, T, TService>((k, p) =>
                {
                    if (_registrations.TryGetValue(k, out var md))
                    {
                        var @params = new object[] { p };
                        return (TService)ActivatorUtilities.CreateInstance(sp, md.ValueType, @params);
                    }
                    return default(TService);

                });
            });
            return this;
        }
        */

        public IServicesBuilder<TKey, TService> Add(Type implemtnationType)
        {
            var metadata = new Metadata(implemtnationType);
            _registrations.Add(_getKey(metadata), metadata);

            return this;
        }


        public IServicesBuilder<TKey, TService> Add(Type[] implemtnationTypes)
        {
            foreach (var implemtnationType in implemtnationTypes)
            {
                Add(implemtnationType);
            }

            return this;
        }

        public void Build(Action<TService, Metadata> actived = null)
        {
            _services.AddSingleton(typeof(IKeyedServicesFactory<TKey, TService>), sp =>
            {
                return new KeyedServicesFactory<TKey, TService>(sp, _registrations, actived);
            });           

        }
    }
}
