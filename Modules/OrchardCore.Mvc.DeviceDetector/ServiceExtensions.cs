using Microsoft.Extensions.Options;
using OrchardCore.Mvc;
using OrchardCore.Mvc.LocationExpander;
using System;
using OrchardCore.Mvc.DeviceDetector;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds Orchard CMS services to the application. 
        /// </summary>
        public static IServiceCollection AddDeviceDetector(this IServiceCollection services, Action<DeviceOptions> optionsAction)
        {
            services.AddScoped<IViewLocationExpanderProvider, DeviceDectetorViewLocationExpanderProvider>();

            services.Configure(optionsAction);
            return services;
        }

    }
}
