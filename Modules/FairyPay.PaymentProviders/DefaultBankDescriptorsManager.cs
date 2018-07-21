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
        private readonly ILogger<DefaultBankDescriptorsManager> _logger;

        public DefaultBankDescriptorsManager(IOptionsMonitor<BankDescriptorsOption> options, ILogger<DefaultBankDescriptorsManager> logger)
        {
            _logger = logger;
            AsDict(options);
            options.OnChange(o =>
            {
                AsDict(options);
                _logger.LogDebug("asdfdsaf");
            });
        }

        public IReadOnlyDictionary<BankCode, BankDescriptor> Descriptors { get; private set; }


        private void AsDict(IOptionsMonitor<BankDescriptorsOption> options)
        {
            Descriptors = options.CurrentValue.Descriptors.ToDictionary(k => k.BankCode);
        }
    }
}
