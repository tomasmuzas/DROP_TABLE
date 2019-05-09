using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace BackendApartmentReservation.Infrastructure.Logging
{
    public static class NLogExtensions
    {
        public static IWebHostBuilder UseKibanaNLog(this IWebHostBuilder builder)
        {
            var aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var environmentName = string.IsNullOrWhiteSpace(aspNetCoreEnvironment)
                                      ? "local"
                                      : aspNetCoreEnvironment.ToLower();

            var rootFolder = AppContext.BaseDirectory;

            var fullPath = Path.Combine(rootFolder, $"nlog.{environmentName}.config");

            if (File.Exists(fullPath))
            {
                var logFactory = NLog.LogManager.LoadConfiguration(fullPath);
                NLog.LogManager.Configuration = logFactory.Configuration;
            }

            builder.UseNLog();
            return builder;
        }
    }
}
