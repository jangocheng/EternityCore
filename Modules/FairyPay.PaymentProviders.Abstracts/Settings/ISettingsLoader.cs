using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders.Settings
{
    public interface ISettingsLoader<in T>
    {
        ISettingsLoader Get(T indexId);
    }
}
