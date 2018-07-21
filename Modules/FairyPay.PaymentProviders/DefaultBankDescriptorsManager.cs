using System;
using System.Collections.Generic;
using System.Text;
using FairyPay.PaymentProviders.Context;
using FairyPay.PaymentProviders.Descriptor;
using FairyPay.PaymentProviders.Options;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FairyPay.PaymentProviders
{
    public class DefaultBankDescriptorsManager : IBankDescriptorManager
    {
        public DefaultBankDescriptorsManager(IOptionsMonitor<BankDescriptorsOption> options, ILogger<DefaultBankDescriptorsManager> logger)
        {
            AsDict(options);
            options.OnChange(o =>
            {
                AsDict(options);
                if (logger.IsEnabled(LogLevel.Debug))
                    logger.LogDebug("配置自动更新{0}个描述器", Descriptors.Count);
            });
        }

        public IReadOnlyDictionary<BankCode, BankDescriptor> Descriptors { get; private set; }


        private void AsDict(IOptionsMonitor<BankDescriptorsOption> options)
        {
            lock (options)
                Descriptors = options.CurrentValue.Descriptors.ToDictionary(k => k.BankCode);
        }
    }
}
