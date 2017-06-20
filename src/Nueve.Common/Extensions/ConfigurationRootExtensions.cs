using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Nueve.Common.Extensions
{
    public static class ConfigurationRootExtensions
    {
        public static T GetConfiguration<T>(this IConfigurationRoot configuration) where T : class
        {
            var type = typeof(T);
            var configSection = configuration.GetSection(type.Name);
            var config = (T)Activator.CreateInstance(type);
            new ConfigureFromConfigurationOptions<T>(configSection).Configure(config);

            return config;
        } 
    }
}
