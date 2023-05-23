using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PayrollSolution.Application.Configuration;
using PayrollSolution.Infrastructure.Configuration;
using Refit;
using System.Net;

namespace PayrollSolution.Infrastructure.ApiClients
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiClientWithConfig<TApiClient, TApiClientConfig>(
            this IServiceCollection services, string configSection)
            where TApiClientConfig : ApiClientConfig
            where TApiClient : class
        {
            //Add config options with validation
            services.AddConfiguration<TApiClientConfig, ApiClientConfigValidator>(configSection);

            services.AddRefitClient<TApiClient>()
                .ConfigureHttpClient((serviceProvider, httpClient) =>
                {
                    var config = serviceProvider.GetRequiredService<IOptions<TApiClientConfig>>().Value;
                    httpClient.BaseAddress = new Uri(config.BaseAddress);
                })
                .ConfigurePrimaryHttpMessageHandler(services => {
                    var config = services.GetRequiredService<IOptions<TApiClientConfig>>().Value;

                    var handler = new HttpClientHandler();
                    if (config.Proxy.Enabled)
                    {
                        handler.UseProxy = true;
                        handler.Proxy = new WebProxy(config.Proxy.Url, config.Proxy.BypassOnLocal);

                        if (!config.Proxy.SslValidationEnabled)
                        {
                            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true;
                        }
                    }

                    return handler;
                });

            return services;
        }
    }
}
