using System;
using System.Collections.Generic;
using System.Text;
using FairyPay.Models;
using FairyPay.PaymentProviders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using OrchardCore.Data;

namespace FairyPay.Entity
{
    public class PayProviderSettingsTypeConfig : EntityTypeConfigurationBase<PayProviderSettings>
    {
        public override void Configure(EntityTypeBuilder<PayProviderSettings> builder)
        {

            builder.HasKey(k => k.Id);
          
            //builder.Property(p => p.Extend)
               // .HasConversion(v => JsonConvert.SerializeObject(v),
                //    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));

           // builder.Property(p => p.EnabledBanks).HasConversion(v => //JsonConvert.SerializeObject(v),
             //   v => JsonConvert.DeserializeObject<BankCode[]>(v));
        }
    }
}
