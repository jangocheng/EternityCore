using System;
using System.Collections.Generic;

namespace FairyPay.PaymentProviders
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Meta : Attribute
    {
        private readonly Dictionary<string, MetaExtend> _extend;

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

        public IReadOnlyDictionary<string, MetaExtend> Extend => _extend;

        public Meta(string id, string name, string description = null)
        {
            Id = id;
            Name = name;
            Description = description;
            _extend = new Dictionary<string, MetaExtend>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void Merge(MetaExtend extend)
        {
            _extend[extend.Name] = extend;
        }

        public void Merge(IEnumerable<MetaExtend> extends)
        {
            foreach (var extend in extends)
            {
                Merge(extend);
            }
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

        public dynamic DefaultValue { get; }
        public EditorType Type { get; }

        public string Tips { get; }
        /// <summary>
        /// 扩展数据
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="title">标题</param>
        /// <param name="tips">提示</param>
        /// <param name="defaultValue">默认值:非输入类型使用JSON格式字符串</param>
        /// <param name="type">控件类型</param>
        public MetaExtend(string name, string title, string tips = null, string defaultValue = null, EditorType type = EditorType.Input)
        {

            Name = name;
            Title = title;
            Type = type;
            Tips = tips;
            if (type == EditorType.Input || type == EditorType.TextArea)
            {
                DefaultValue = defaultValue;
            }
            else
            {
                DefaultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(defaultValue);
            }
        }
    }
}
