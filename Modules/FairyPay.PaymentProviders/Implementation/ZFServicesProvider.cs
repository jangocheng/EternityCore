using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using FairyPay.PaymentProviders;
using FairyPay.PaymentProviders.Mapping;
using FairyPay.PaymentProviders.Models;
using FairyPay.PaymentProviders.Provider;

namespace FairyPay.PaymentProviders.Implementation
{
    [Meta("zfpay", "众付")]
    [MetaExtend("MSecret", "商户Secret", "由平台分配")]
    public class ZfServicesProvider : PaymentServicesProviderBase
    {
        protected override string GateWay { get; } = "http://pay.hezhongpay.com/";


        protected override ResultType SelectResult(string result)
        {
            return result == "0000" ? ResultType.Success : ResultType.Fail;
        }

        protected override string SignFieldName => "sign";

        public ZfServicesProvider(ProviderSettings settings) : base(settings)
        {
        }

        public override decimal ConvertAmount(decimal amount)
        {
            return amount * 100;
        }

        protected override void AttachReceive(IReceiveMap<ReceiveMapField> receiveMapper)
        {
            receiveMapper.Add(new Dictionary<ReceiveMapField, string>
            {
                {ReceiveMapField.Result, "code"},
                {ReceiveMapField.Amount, "amount"},
                {ReceiveMapField.OrderId, "orderId"},
                //{ReceiveMapField.TradeOrderId,"trade_no" },
                //{ReceiveMapField.BankOrderId,"bank_seq_no" }
            });
        }

        protected override string Sign(NameValueCollection requestParams)
        {
            var unSign = requestParams.JoinNvcToQs(true, false, (k, v) => !string.IsNullOrEmpty(v));
            return SignServices.MD5(unSign + Settings.Extend["MSecret"]);
        }

        protected override bool VerifySign(NameValueCollection receiveParams)
        {
            var unSign = receiveParams.JoinNvcToQs(true, false, (k, v) => !string.IsNullOrEmpty(v) && k != "sign");
            return receiveParams["sign"] == SignServices.MD5(unSign + Settings.Extend["MSecret"]);
        }

        protected override void AttachRequest(PayRequestModel model, IMap<RequestMapField> requestMapper)
        {
            requestMapper.Add(new NameValueCollection
            {
                {"timestamp", Extensions.UnixTimeSpan().ToString()},
                {"orderId", ""},
                {"merchantId", ""},
                {"amount", ""},
                {"type", ""},
                {"notifyUrl", ""},
                {"frontUrl", ""},
                {"merchantKey", Settings.Mkey},
                {"productName", ""}
            })
                .Add(new Dictionary<RequestMapField, string>()
                {
                    {RequestMapField.Mid,"merchantId" },
                    {RequestMapField.Amount,"amount" },
                    {RequestMapField.OrderId,"orderId" },
                    {RequestMapField.ProductName,"productName" },
                    {RequestMapField.ClientNotifyUrl,"frontUrl" },
                    {RequestMapField.ServerNotifyUrl,"notifyUrl" },
                    {RequestMapField.BankCo,"type" }
                });
        }
    }
}
