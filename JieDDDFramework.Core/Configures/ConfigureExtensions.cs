using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Core.Configures
{
    public static class ConfigureExtensions
    {
       public static TConfig ConfigureOption<TConfig>(this IServiceCollection services,IConfiguration configuration,Func<TConfig> provider) where TConfig : BaseConfig
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            var config = provider();
            configuration.Bind(config);
            services.AddSingleton(config);
            services.AddOptions();
            services.Configure<TConfig>(configuration);
            return config;
        }
    }
}
