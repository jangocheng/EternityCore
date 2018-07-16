using System;
using System.Collections.Generic;
using System.Text;

namespace FairyPay.PaymentProviders.Context
{
    public class BankDescriptor
    {
        private string _alias;
        public string Name { get; set; }

        public string Description { get; set; }

        public string Alias
        {
            get { return _alias ?? Name; }
            set { _alias = value; }
        }

        public BankCode BankCode { get; }

        public BankDescriptor(BankCode bankCode)
        {
            BankCode = bankCode;
        }

        public BankDescriptor(string codeText)
        {
            if (Enum.TryParse(codeText, out BankCode bankCode))
            {
                BankCode = bankCode;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(codeText), "无效的参数值");
            }
        }
    }
}
