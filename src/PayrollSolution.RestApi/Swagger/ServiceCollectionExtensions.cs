using Microsoft.OpenApi.Models;

namespace PayrollSolution.RestApi.Swagger;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupSwagger(this IServiceCollection services, string apiName)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = apiName, Version = "v1"
            });            
        });

        return services;
    }
}