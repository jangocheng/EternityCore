using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders.Mapping
{
    public enum RequestMapField
    {
        Mid,
        Amount,
        ProductName,
        Description,
        OrderId,
        ExtParams,
        ClientNotifyUrl,
        ServerNotifyUrl,
        BankCo
    }

    /// <summary>
    /// 接收参数映射字段
    /// </summary>
    public enum ReceiveMapField
    {
        Result,
        ExtParams,
        Amount,
        /// <summary>
        /// 本系统订单号
        /// </summary>
        OrderId,
        /// <summary>
        /// 支付平台交易号
        /// </summary>
        TradeOrderId,
        /// <summary>
        /// 银行流水单号
        /// </summary>
        BankOrderId
    }
}
