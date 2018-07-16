using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders.Models
{
    public enum ResultType
    {
        Fail,
        SignError,
        Finished,
        Success,
        MoneyNotMatch

    }

    public class PayReceiveModel
    {
        public ResultType Result { get; internal set; }

        public string ExtParams { get; internal set; }

        public decimal Amount { get; internal set; }

        public string OrderId { get; internal set; }
        public string TradeOrderId { get; internal set; }
        public string BankOrderId { get; internal set; }
    }
}
