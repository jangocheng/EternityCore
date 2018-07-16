using System;
using System.Collections.Generic;
using System.Text;

namespace Eternity.DependencyInjection.Extensions
{
    public interface IServicesBuilder<in TKey, TService>
    {
        IServicesBuilder<TKey, TService> Add<T>(TKey key) where T : class, TService;

        IServicesBuilder<TKey, TService> Add(Type implemtnationType, Func<Metadata, TKey> key);

        void Build(Action<TService, Metadata> actived = null);
    }

    public class Metadata
    {
        internal Metadata(Type type)
        {
            ValueType = type;
        }
        public Type ValueType { get; }

        private IDictionary<string, object> Meta { get; } = new Dictionary<string, object>();

        public object this[string key]
        {
            get => Meta[key];
            set => Meta[key] = value;
        }
    }
}
