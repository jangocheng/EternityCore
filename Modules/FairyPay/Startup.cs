using FairyPay.Drivers;
using FairyPay.Recipes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Entities;
using OrchardCore.Environment.Navigation;
using OrchardCore.Environment.Shell;
using OrchardCore.Modules;
using OrchardCore.Recipes.Services;
using OrchardCore.ResourceManagement;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using System;
using Microsoft.AspNetCore.Mvc;
using Eternity.DependencyInjection.Extensions;

namespace FairyPay
{
    public class Startup : StartupBase
    {
        private readonly ShellSettings _shellSettings;

        public override int Order => 99;

        public Startup(ShellSettings shellSettings)
        {
            _shellSettings = shellSettings;
        }
        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaRoute(
               name: "FairyPayHome",
               areaName: "FairyPay",
               template: "FairyPay",
               defaults: new { controller = "Home", action = "Index" }
           );


        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDisplayDriver<ISite>, PaySettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IRecipeStepHandler, FairyPayRecipeHandler>();
            services.AddScoped<IResourceManifestProvider, ResourceManifest>();

        }
    }
}
