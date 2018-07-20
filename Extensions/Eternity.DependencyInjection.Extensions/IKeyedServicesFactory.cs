using System;
using System.Collections.Generic;
using System.Text;

namespace Eternity.DependencyInjection.Extensions
{
    public interface IKeyedServicesFactory<TKey, out TService>
    {
        /// <summary>
        /// 获取key关联的类型实例
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="p">构造函数参数</param>
        /// <returns></returns>
        TService GetService(TKey key, params object[] p);
        //TService GetService<T>(TKey key, T p);
        IReadOnlyDictionary<TKey, Metadata> Meta { get; }

    }
}
