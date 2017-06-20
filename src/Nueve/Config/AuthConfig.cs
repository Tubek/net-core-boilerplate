using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Nueve.Common.Config;
using Nueve.Common.Extensions;
using System;
using System.Security.Claims;
using System.Text;

namespace Nueve.Config
{
    /// <summary> 
    /// AuthConfig
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// Secret
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {

            });
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfigurationRoot configuration)
        {
            var config = configuration.GetConfiguration<AppConfig>();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.IdentitySecret));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidIssuer = config.IdentityIssuer,
                ValidateAudience = true,
                ValidAudience = config.IdentityAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });
        }
    }
}
