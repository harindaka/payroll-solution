using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PayrollSolution.Application.Common.Configuration;

public static class ServiceCollectionExtensions
{
    public static TConfig SetupConfigOptions<TConfig>(this IServiceCollection services,
        IConfiguration configuration,
        string sectionPath) where TConfig: class
    {
        var configSection = configuration.GetSection(sectionPath);
        if (!configSection.Exists())
        {
            throw new Exception($"The configuration section {sectionPath} was not found");
        }

        var config = configSection.Get<TConfig>() ?? throw new Exception($"Failed to deserialize configuration section {sectionPath}");
        
        services.AddOptions<TConfig>().Bind(configSection);

        return config;
    }
}