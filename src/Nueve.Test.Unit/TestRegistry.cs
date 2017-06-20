using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nueve.Common.Config;
using Nueve.Common.Extensions;
using Nueve.Common.Services;
using Nueve.Common.Telemetry;
using Nueve.ViewModels.Auth;

namespace Nueve.Test
{
    public static class TestRegistry
    {
        /// <summary>
        /// Register common services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterUnitTestServices(
            this IServiceCollection services)
        {
            var configuration = new Mock<IConfigurationRoot>();
            configuration.Setup(x => x.GetSection(It.IsAny<string>()))
                      .Returns(new Mock<IConfigurationSection>().Object);

            var config = configuration.Object.GetConfiguration<AppConfig>();
            var log = new Mock<ILog>().Object;

            services.AddSingleton<IAppConfig>(config);

            services.AddSingleton(log);
            services.AddTransient<ITokenService, TokenService>();

            services.AddTransient<IUserService, UserService>();

            //validators
            services.AddSingleton<IValidator<AuthRequest>, AuthRequestValidator>();

            return services;
        }
    }
}
