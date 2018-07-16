using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders
{
    public class ProviderSettings
    {
        public string Mid { get; set; }

        public string Mkey { get; set; }

        public string Gateway { get; set; }


        public IDictionary<string, string> Extend { get; set; }

        public BankCode[] EnabledBanks { get; set; }
    }
}
