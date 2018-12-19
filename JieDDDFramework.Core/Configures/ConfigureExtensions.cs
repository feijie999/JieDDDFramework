using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JieDDDFramework.Core.Configures
{
    public static class ConfigureExtensions
    {
       public static TOptions ConfigureOption<TOptions>(this IServiceCollection services,IConfiguration configuration,Func<TOptions> provider) where TOptions : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            var config = provider();
            configuration.Bind(config);
            services.AddOptions();
            services.Configure<TOptions>(configuration);
            return config;
        }
    }
}
