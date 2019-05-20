using System.Security.Claims;
using System.Text;
using BackendApartmentReservation.Authentication.AuthorizationRequirements;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.AdminOnly;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.EmployeeOnly;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace BackendApartmentReservation
{
    using System.IO;
    using Autofac;
    using Database;
    using Infrastructure.Exceptions;
    using Infrastructure.Logging;
    using IoCContainer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env)
        {
            _environment = env;
            var envName = _environment.EnvironmentName;
            var rootFolder = _environment.ContentRootPath;

            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(rootFolder, "appsettings.json")) // Load default settings
                .AddJsonFile(Path.Combine(rootFolder, $"appsettings.{envName}.json"),
                    optional: true) // Override default settings with env specific settings 
                .AddEnvironmentVariables() // Override appsettings with environment variables
                .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration["JwtTokenSecret"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtTokenIssuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtTokenIssuer"]
                    };
                });

            services.AddSingleton<IAuthorizationHandler, AdminOnlyHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeOnlyHandler>();
            services.AddSingleton<IAuthorizationHandler, OrganizerOnlyHandler>();

            var employeeOnlyPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes("Bearer")
                .RequireAuthenticatedUser()
                .AddRequirements(new EmployeeOnlyRequirement())
                .Build();

            var organizerOnlyPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes("Bearer")
                .RequireAuthenticatedUser()
                .AddRequirements(new OrganizerOnlyRequirement())
                .Build();

            var adminOnlyPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes("Bearer")
                .RequireAuthenticatedUser()
                .AddRequirements(new AdminOnlyRequirement())
                .Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.EmployeeOnly, employeeOnlyPolicy);
                options.AddPolicy(PolicyNames.OrganizerOnly, organizerOnlyPolicy);
                options.AddPolicy(PolicyNames.AdminOnly, adminOnlyPolicy);
            });

            var connectionString = Configuration.GetConnectionString("DatabaseContext");
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddMvc(options =>
                {
                    options.Filters.Add(new AuthorizeFilter(employeeOnlyPolicy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddMvcOptions(options => options.Filters.Add(new MethodCallLoggingFilter()))
                .AddMvcOptions(options => options.Filters.Add(new GlobalExceptionFilter(_environment)))
                .AddMvcOptions(options => options.Filters.Add(new RequestValidationFilter()));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseCors(options =>
                options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "DELETE"));
            app.UseAuthentication();
            app.UseMvc();

            var context = app.ApplicationServices.GetRequiredService<DatabaseContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            context.Seed();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<IoCConfig>();
        }
    }
}