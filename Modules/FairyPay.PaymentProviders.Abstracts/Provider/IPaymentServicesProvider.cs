using FairyPay.PaymentProviders.Context;
using FairyPay.PaymentProviders.Models;
using FairyPay.PaymentProviders.Provider;

namespace FairyPay.PaymentProviders
{
    public interface IPaymentServicesProvider : IPaymentRequestProvider, IPaymentReceiveProvider
    {
        /// <summary>
        /// 支付接口元数据
        /// </summary>
        Meta Meta { get; set; }
        /// <summary>
        /// 金额转换
        /// </summary>
        /// <param name="amount">金额单位元</param>
        /// <returns></returns>
        decimal ConvertAmount(decimal amount);

    }

    public interface IPaymentReceiveProvider
    {
        ReceiveContext Receive(bool isServerNotify, CallbackType type);
    }

    public interface IPaymentRequestProvider
    {

        RequestContext Request(PayRequestModel mode);
    }

    public interface IAutoPayServicesProvider
    {
        PayOutResult PayOut(PayoutModel model);
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="type">钱包类型参数</param>
        /// <returns></returns>
        QueryResult QueryBalance(string type = null);

        /// <summary>
        /// 代付结果通知
        /// </summary>
        void PayoutReceive();
    }
}
