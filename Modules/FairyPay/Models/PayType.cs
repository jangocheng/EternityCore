using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OrchardCore.Data;

namespace FairyPay.Models
{
    public class PayType : IEntity
    {
        /// <summary>
        /// 类型标识，唯一
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        //public string Strategy { get; set; }
    }
}
