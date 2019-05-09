namespace BackendApartmentReservation
{
    using Autofac.Extensions.DependencyInjection;
    using Infrastructure.Logging;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .ConfigureLogging(builder => builder.ClearProviders())
                .UseKibanaNLog()
                .UseStartup<Startup>();
        }
    }
}