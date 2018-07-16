using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using System;

namespace OrchardCore.Data.EntityFrameworkCore
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
       
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddEFCore(_serviceProvider);
        }
    }
}
