using System;
using System.Collections.Generic;
using System.Text;
using FairyPay.PaymentProviders;

namespace FairyPay.PaymentProviders.Provider
{
    public interface IPaymentServicesProviderFactory
    {
        ISettingsLoader Settings { get; }
        IDictionary<string, IDictionary<BankCode, string>> BanksCode { get; }
        IDictionary<string, Meta> GetProvidersDescriptor { get; }

        string GetProviderBankCode(string providerName, BankCode? code);
        IDictionary<string, Meta> GetAutoPayProvidersDescriptor { get; }
        IAutoPayServicesProvider CreatePayOutProvider(string providerName);
        IPaymentServicesProvider CreateProvider(string providerName, ProviderSettings parameters);
        IPaymentServicesProvider CreateProvider(string providerName);
    }
}
