using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Eternity.DependencyInjection.Extensions;
using FairyPay.PaymentProviders.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.Modules;


namespace FairyPay.PaymentProviders
{
    public class Startup : StartupBase
    {
        private readonly IServiceProvider _serviceProvider;

        public Startup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            /* routes.MapAreaRoute(
                 name: "PaymentHome",
                 areaName: "",
                 template: "Payment",
                 defaults: new { controller = "Home", action = "Index" }
             );
             */

        }

        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddKeyed<string, IPaymentServicesProvider>(Assembly.GetExecutingAssembly(), meta =>
            {
                var providerMeta = meta.ValueType.GetCustomAttribute<Meta>();
                foreach (var extend in meta.ValueType.GetCustomAttributes<MetaExtend>())
                {
                    providerMeta.Extend.Add(extend.Name, extend);
                }
                meta["meta"] = providerMeta;

                return providerMeta.Id;
            }).Build((provider, meta) => { provider.Meta = meta["meta"] as Meta; });


        }
    }
}
