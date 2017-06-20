using Microsoft.Extensions.DependencyInjection;
using Nueve.Common.Services;
using Microsoft.Extensions.Configuration;
using Nueve.Common.Config;
using Nueve.Common.Extensions;
using Nueve.Common.Telemetry;

namespace Nueve.Common
{
    /// <summary>
    /// Common installer
    /// </summary>
    public static class CommonInstaller
    {
        /// <summary>
        /// Register common services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommonServices(
            this IServiceCollection services, IConfigurationRoot configuration)
        {
            var config = configuration.GetConfiguration<AppConfig>();

            services.AddSingleton<IAppConfig>(config);
            services.AddSingleton<ILog, Log>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }

}
