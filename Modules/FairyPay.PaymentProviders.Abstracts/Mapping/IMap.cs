using System.Collections.Generic;
using System.Collections.Specialized;

namespace FairyPay.PaymentProviders.Mapping
{
    public interface IMap<TField> where TField : struct
    {
        IMap<TField> Add(IDictionary<TField, string> kevValues);
        IMap<TField> Add(NameValueCollection collection);
        string this[TField field] { get; set; }
    }

    public interface IRequestMap<TField>:IMap<TField> where TField : struct
    {
        IMap<TField> Add(TField field, string targetName, string value = "");


        string this[string name] { get; set; }
        void Remove(string name);
        void Combine(MapValue<TField, string> value);
        NameValueCollection GetMapResult();
    }

    public interface IReceiveMap<TField> : IMap<TField> where TField : struct
    {

    }
}
