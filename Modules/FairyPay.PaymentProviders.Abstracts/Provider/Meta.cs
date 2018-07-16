using System;
using System.Collections.Generic;

namespace FairyPay.PaymentProviders
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Meta : Attribute
    {
        /// <summary>
        /// 接口唯一标识
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 接口描述
        /// </summary>
        public string Description { get; }

        public IDictionary<string, MetaExtend> Extend { get; }

        public Meta(string id, string name, string description = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Extend = new Dictionary<string, MetaExtend>(StringComparer.InvariantCultureIgnoreCase);
        }
    }

    public enum EditorType
    {
        Input,
        TextArea,
        Ratio,
        CheckBox
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MetaExtend : Attribute
    {
        public string Name { get; }
        public string Title { get; }

        public string DefaultValue { get; }
        public EditorType Type { get; }

        public string Tips { get; }

        public MetaExtend(string name, string title, string tips = null, string defaultValue = null, EditorType type = EditorType.Input)
        {
            Name = name;
            Title = title;
            Type = type;
            Tips = tips;
            DefaultValue = defaultValue;
        }
    }
}
