using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Text;

namespace FairyPay.PaymentProviders.Mapping
{
    public abstract class MapperBase<TField, TMapValue> :/* DynamicObject,*/ IMap<TField> where TField : struct
    {


        protected readonly NameValueCollection MapResult = new NameValueCollection();

        protected readonly IDictionary<TField, TMapValue> Map =
            new Dictionary<TField, TMapValue>();

        /*public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // await New.FooAsync()
            // await New.Foo()

            var binderName = binder.Name;

            if (binderName.EndsWith("Async"))
            {
                binderName = binder.Name.Substring(binder.Name.Length - "Async".Length);
            }

            result = ShapeFactoryExtensions.CreateAsync(this, binderName, Arguments.From(args, binder.CallInfo.ArgumentNames));

            return true;
        }*/
        /// <summary>
        /// 批量增加默认值
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IMap<TField> Add(NameValueCollection collection)
        {
            MapResult.Add(collection);
            return this;
        }

        /// <summary>
        /// 批量增加映射关系
        /// </summary>
        /// <param name="kevValues"></param>
        /// <returns></returns>
        public abstract IMap<TField> Add(IDictionary<TField, string> kevValues);

        public abstract string this[TField field] { get; set; }

        
        public string this[string name]
        {
            get => MapResult[name];
            set => MapResult[name] = value;
        }

        public void Remove(string name)
        {
            MapResult.Remove(name);
        }

        public void Combine(MapValue<TField, string> values)
        {
            foreach (var m in Map)
            {
                this[m.Key] = values[m.Key];
            }
        }

    }
    public class RequestMapper : MapperBase<RequestMapField, RequestMapper.MapValue>, IRequestMap<RequestMapField>
    {
        public class MapValue
        {
            public string TargetName;
            public string Value;

            public MapValue(string targetName, string value = "")
            {
                TargetName = targetName;
                Value = value;
            }
        }
        public override string this[RequestMapField field]
        {
            get => Map[field].Value;
            set => Map[field].Value = value;
        }


        public override IMap<RequestMapField> Add(IDictionary<RequestMapField, string> kevValues)
        {
            foreach (var kv in kevValues)
            {
                Map.Add(kv.Key, new MapValue(kv.Value));
            }
            return this;
        }

        public IMap<RequestMapField> Add(RequestMapField field, string targetName, string value = "")
        {
            Map[field] = new MapValue(targetName, value);
            return this;
        }


        public NameValueCollection GetMapResult()
        {
            foreach (var m in Map)
            {
                this[m.Value.TargetName] = m.Value.Value;
            }

            return MapResult;
        }

    }
    public class ReceiveMapper : MapperBase<ReceiveMapField, string>, IReceiveMap<ReceiveMapField>
    {
        public override IMap<ReceiveMapField> Add(IDictionary<ReceiveMapField, string> kevValues)
        {
            foreach (var kv in kevValues)
            {
                Map.Add(kv.Key, kv.Value);
            }
            return this;
        }

        public override string this[ReceiveMapField field]
        {
            get
            {
                if (Map.TryGetValue(field, out var v))
                {
                    return MapResult[v];
                }
                return null;
            }
            set => throw new NotImplementedException();
        }
    }


    public class MapValue<TK, TV> where TK : struct
    {
        private readonly Dictionary<TK, TV> _mapValues = new Dictionary<TK, TV>();

        public TV this[TK k]
        {
            get => _mapValues[k];
            set => _mapValues[k] = value;
        }

        public IEnumerable<KeyValuePair<TK, TV>> Values
        {
            get
            {
                foreach (var v in _mapValues)
                {
                    yield return v;
                }
            }
        }
    }
}
