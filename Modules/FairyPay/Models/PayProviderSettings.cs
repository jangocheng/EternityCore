using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FairyPay.PaymentProviders;
using OrchardCore.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FairyPay.Models
{
    public class PayProviderSettings : IEntity
    {
        //internal string _extend { get; set; }
        //internal string _enabledBanks { get; set; }

        // private IDictionary<string, string> __extend;
        // private BankCode[] __enabledBanks;

        public int Id { get; set; }
        public string ProviderId { get; set; }

        public string Remark { get; set; }

        public ProviderSettings Settings { get; set; }
        // public new IDictionary<string, string> Extend { get; set; }
        //  public new BankCode[] EnabledBanks { get; set; }

        /* public new IDictionary<string, string> Extend
         {
             get => __extend ?? (__extend = JsonConvert.DeserializeObject<Dictionary<string, string>>(_extend));
             set
             {
                 _extend = JsonConvert.SerializeObject(_extend);
                 __extend = null;
             }
         }
 
         public new BankCode[] EnabledBanks
         {
             get => JsonConvert.DeserializeObject<BankCode[]>(_enabledBanks);
             set
             {
                 _enabledBanks = JsonConvert.SerializeObject(_enabledBanks);
                 __enabledBanks = null;
             }
         }*/
    }
}
