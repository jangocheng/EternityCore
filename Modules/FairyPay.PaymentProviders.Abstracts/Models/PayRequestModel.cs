using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FairyPay.PaymentProviders.Context;
using FairyPay.PaymentProviders.Mapping;

namespace FairyPay.PaymentProviders.Models
{
    public class PayRequestModel
    {
        /// <summary>
        /// 单位：元，部分接口需要转换
        /// </summary>
        public decimal Amount { get; internal set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string OrderId { get; set; }

        public string ExtParams { get; set; }

        public string ClientNotifyUrl { get; set; }

        public string ServerNotifyUrl { get; set; }

        public string BankCo { get; private set; }

        public string ProviderName { get; private set; }

        //public BankDescriptor BankDescriptor { get; private set; }

        //public bool IsWap { get; private set; }

        //internal bool QRDirect { get; private set; }

        public readonly MapValue<RequestMapField, string> MapValues = new MapValue<RequestMapField, string>();
        public PayRequestModel(decimal amount, string providerName, Action<MapValue<RequestMapField, string>> mapAction)
        {
            Amount = amount;
            ProviderName = providerName;
            mapAction(MapValues);
        }


        public string this[RequestMapField field]
        {
            get => MapValues[field];
            set => MapValues[field] = value;
        }

    }
}
