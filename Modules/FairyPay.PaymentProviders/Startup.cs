using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using Eternity.DependencyInjection.Extensions;
using FairyPay.PaymentProviders.Context;
using FairyPay.PaymentProviders.Descriptor;
using FairyPay.PaymentProviders.Implementation;
using FairyPay.PaymentProviders.Options;
using FairyPay.PaymentProviders.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;


namespace FairyPay.PaymentProviders
{
    public class Startup : StartupBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IServiceProvider serviceProvider, IHostingEnvironment hostingEnvironment)
        {
            _serviceProvider = serviceProvider;
            _hostingEnvironment = hostingEnvironment;
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
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(_hostingEnvironment.ContentRootPath, "payment.json"), true, true)
                .Build();

            services.Configure<BankDescriptorsOption>(configuration);

            services.AddSingleton<IBankDescriptorManager, DefaultBankDescriptorsManager>();

            services.AddKeyed<IPaymentServicesProvider>(Assembly.GetExecutingAssembly().GetImplemtnationTypes<IPaymentServicesProvider>(), meta =>
            {
                var providerMeta = meta.ValueType.GetCustomAttribute<Meta>();
                providerMeta.Merge(meta.ValueType.GetCustomAttributes<MetaExtend>());
                meta["meta"] = providerMeta;
                return providerMeta.Id;

            }).Build((instance, meta) =>
            {
                (instance as PaymentServicesProviderBase).Meta = meta["meta"] as Meta;
            });

        }


    }
}

