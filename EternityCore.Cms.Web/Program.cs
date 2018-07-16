using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Logging;

namespace EternityCore.Cms.Web
{
    public class Program
    {
        public static void Main(string[] args)
            => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                //.ConfigureServices(services => services.AddAutofac())
                .UseApplicationInsights()
                .UseNLogWeb()
                .UseKestrel(options => { options.AddServerHeader = false; })

                .UseStartup<Startup>()
                .Build();
    }
}
