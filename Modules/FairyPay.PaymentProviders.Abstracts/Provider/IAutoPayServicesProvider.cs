using System;
using System.Collections.Generic;
using System.Text;
using FairyPay.PaymentProviders.Models;

namespace FairyPay.PaymentProviders.Provider
{
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
