using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders
{
    public enum BankCode
    {

        UNKNOWN,
        #region 银行
        /// <summary>
        /// 工商银行
        /// </summary>
        ICBC,
        /// <summary>
        /// 农业银行
        /// </summary>
        ABC,
        /// <summary>
        /// 中国银行
        /// </summary>
        BOCSH,
        /// <summary>
        /// 建设银行
        /// </summary>
        CCB,
        /// <summary>
        /// 招商银行
        /// </summary>
        CMB,
        /// <summary>
        /// 浦东发展银行
        /// </summary>
        SPDB,
        /// <summary>
        /// 广东发展银行
        /// </summary>
        GDB,
        /// <summary>
        /// 交通银行
        /// </summary>
        BOCOM,
        /// <summary>
        /// 邮政储蓄银行
        /// </summary>
        PSBC,
        /// <summary>
        /// 中信银行
        /// </summary>
        CNCB,
        /// <summary>
        /// 民生银行
        /// </summary>
        CMBC,
        /// <summary>
        /// 光大银行
        /// </summary>
        CEB,
        /// <summary>
        /// 华夏银行
        /// </summary>
        HXB,
        /// <summary>
        /// 兴业银行
        /// </summary>
        CIB,
        /// <summary>
        /// 上海银行
        /// </summary>
        BOS,
        /// <summary>
        /// 上海农商银行
        /// </summary>
        PingAn,
        /// <summary>
        /// 平安银行(原深发)
        /// </summary>
        PAB,
        /// <summary>
        /// 北京银行
        /// </summary>
        BCCB,
        /// <summary>
        /// 中国银行(大额)
        /// </summary>
        BOC,    //中行(大额)
        /// <summary>
        /// 银联无卡支付
        /// </summary>
        NOCARD,
        /// <summary>
        /// 银联选择银行
        /// </summary>
        UnionPay,
        /// <summary>
        /// 银联手机版
        /// </summary>
        UnionPayWap,
        UnionPayScan,
        UnionPayDirect,
        #endregion

        /// <summary>
        /// 支付宝
        /// </summary>
        AliPay,
        /// <summary>
        /// 支付宝WAP
        /// </summary>
        AliPayWap,
        /// <summary>
        /// 支付宝扫码
        /// </summary>
        AliPayScan,
        /// <summary>
        /// 支付宝二维码直连
        /// </summary>
        AliPayDirect,

        /// <summary>
        /// 财付通
        /// </summary>
        TenPay,
        /// <summary>
        /// 财付通WAP
        /// </summary>
        TenPayWap,

        /// <summary>
        /// 微信官方扫码
        /// </summary>
        WeChat,

        /// <summary>
        /// 第三/四方微信扫码
        /// </summary>
        WeChatPay,
        /// <summary>
        /// 微信Wap
        /// </summary>
        WeChatWap,
        /// <summary>
        /// 微信二维码直连
        /// </summary>
        WeChatDirect,

        /// <summary>
        /// 微信应用内支付（公众号）
        /// </summary>
        WeChatJSAPI,

        /// <summary>
        /// QQ钱包
        /// </summary>
        QQ,
        /// <summary>
        /// QQ钱包客户端
        /// </summary>
        QQWap,
        /// <summary>
        /// QQ/QQ浏览器
        /// </summary>
        QQJSAPI,
        /// <summary>
        /// QQ二维码直连
        /// </summary>
        QQDirect,

        Baidu,
        JD,
        JDDirect,
        JDJSAPI
    }
}
