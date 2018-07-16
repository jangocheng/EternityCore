using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders.Provider
{
    public enum ApiResultType
    {
        Success = 0, Fail = 1
    }

    /// <summary>
    /// 代付/查询API结果
    /// </summary>
    public abstract class ApiResult
    {
        public ApiResultType Code;
        public string Message;
        /// <summary>
        /// 查询余额时为余额数，其他情况可能是别的数据
        /// </summary>
        public object Data;
    }

    public class PayOutResult : ApiResult
    {
        /// <summary>
        /// 代付订单号
        /// </summary>
        public string OrderId;
    }

    public class QueryResult : ApiResult
    {

    }
}
