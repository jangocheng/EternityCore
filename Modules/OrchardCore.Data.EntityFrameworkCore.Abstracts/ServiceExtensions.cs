using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OrchardCore.Data;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Builders.Models;
using OrchardCore.Hosting.ShellBuilders;
using YesSql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {


        public static void AddEFCore(this IServiceCollection services, IServiceProvider serviceProvider)
        {

            var shellSettings = serviceProvider.GetService<ShellSettings>();
            var connectionString = shellSettings.ConnectionString;
            if (shellSettings.DatabaseProvider == "Sqlite")
            {
                var shellOptions = serviceProvider.GetService<IOptions<ShellOptions>>();
                var option = shellOptions.Value;
                var databaseFolder = Path.Combine(option.ShellsApplicationDataPath, option.ShellsContainerName, shellSettings.Name);
                var databaseFile = Path.Combine(databaseFolder, "yessql.db");
                connectionString = $"Data Source ={ databaseFile}; Cache = Shared";
            }

            services.AddEFCore<DBContext>(shellSettings.DatabaseProvider, connectionString);

        }

        public static void AddEFCore<T>(this IServiceCollection services, string databaseProvider, string connectionString) where T : DbContext
        {
            services.AddDbContextPool<T>(options =>
            {
                switch (databaseProvider)
                {
                    case "Sqlite":
                        options.UseSqlite(connectionString);
                        break;
                    case "SqlServer":
                        options.UseSqlServer(connectionString);
                        break;
                    default:
                        throw new ArgumentException("Unknown database provider: " + databaseProvider);
                }
            });
        }

    }
}
