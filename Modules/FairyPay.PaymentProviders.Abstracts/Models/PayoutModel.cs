using System;
using System.Collections.Generic;

namespace FairyPay.PaymentProviders.Models
{
    public class PayoutModel
    {
        public enum PayOutType
        {
            Bank, WeChat, Alipay
        }

        public PayoutModel()
        {
            Properties = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public PayOutType Type { get; set; }
        public decimal Amount { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 开户姓名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 银行支付时为银行卡号，微信/支付宝为账号
        /// </summary>
        public string AccountNo { get; set; }
        /*
        /// <summary>
        /// 余额类型 2/3
        /// </summary>
        public string BalanceType { get; set; }
        /// <summary>
        /// 钱包类型
        /// </summary>
        public string WalletType { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 开户行名称
        /// </summary>
        //public string CnapsName { get; set; }
        /// <summary>
        /// 联行号
        /// </summary>
        //public string CertificateCode { get; set; }
        */
        public string NotifyUrl { get; set; }

        public IDictionary<string, string> Properties { get; }
    }
}
