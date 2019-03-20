using Autofac;

using BackendApartmentReservation.Database;
using BackendApartmentReservation.Infrastructure.Containers;
using BackendApartmentReservation.Infrastructure.Exceptions;
using BackendApartmentReservation.Infrastructure.Logging;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendApartmentReservation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddMvcOptions(options => options.Filters.Add(new MethodCallLoggingFilter()))
                .AddMvcOptions(options => options.Filters.Add(new GlobalExceptionFilter()));

            var connectionString = Configuration.GetConnectionString("DatabaseContext");
            services.AddDbContext<DatabaseContext>(options => 
                options.UseSqlServer(connectionString));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseCors(options =>
                options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "DELETE"));
            app.UseMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<IoCConfig>();
        }
    }
}
