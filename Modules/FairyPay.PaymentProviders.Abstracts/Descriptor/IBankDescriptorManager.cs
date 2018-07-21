using System.Collections.Generic;
using FairyPay.PaymentProviders.Context;

namespace FairyPay.PaymentProviders.Descriptor
{
    public interface IBankDescriptorManager
    {
        IReadOnlyDictionary<BankCode, BankDescriptor> Descriptors { get; }
    }
}
