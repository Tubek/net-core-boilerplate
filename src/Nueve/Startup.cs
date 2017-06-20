using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nueve.Config;
using Nueve.Common;
using FluentValidation.AspNetCore;
using Nueve.Filters;
using Nueve.Common.Telemetry;
using Nueve.Common.Config;
using Nueve.Common.Extensions;
using Nueve.ViewModels.Auth;

namespace Nueve
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Main entry
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterCommonServices(Configuration);

            // register validators
            services.AddSingleton<IValidator<AuthRequest>, AuthRequestValidator>();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILog>();

            services.AddCors();

            AuthConfig.Register(services, Configuration);
            SwaggerConfig.Register(services, Configuration);

            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelStateValidationFilter());
                options.Filters.Add(new GlobalExceptionFilter(logger));
                options.RespectBrowserAcceptHeader = true;
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var config = Configuration.GetConfiguration<AppConfig>();

            if (!env.IsDevelopment())
            {
            }

            SwaggerConfig.Configure(app, env);
            AuthConfig.Configure(app, env, Configuration);

            app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
