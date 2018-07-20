using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders
{
    public enum CallbackType
    {
        /// <summary>
        /// 本地方式
        /// </summary>
        Local,
        /// <summary>
        /// 远程方式
        /// </summary>
        Remote
    }
    public interface ISettingsLoader
    {
        decimal MinAmount { get; }

        CallbackType CallBack { get; }


    }
}
