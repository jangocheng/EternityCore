using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using OrchardCore.Modules;

namespace OrchardCore.Mvc.DeviceDetector
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDeviceDetector(options =>
            {
                options.AddDetector(DeviceType.Mobile, request =>
                {
                    var agent = request.Headers[HeaderNames.UserAgent];
                    return true;
                });
            });
        }
    }
}
