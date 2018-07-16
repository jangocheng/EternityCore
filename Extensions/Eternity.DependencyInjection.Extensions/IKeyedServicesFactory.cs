using System;
using System.Collections.Generic;
using System.Text;

namespace Eternity.DependencyInjection.Extensions
{
    public interface IKeyedServicesFactory<TKey, out TService>
    {
        TService GetService(TKey key, params object[] p);
        IReadOnlyDictionary<TKey, Metadata> Meta { get; }
    }
}
