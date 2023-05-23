using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using PayrollSolution.RestApi.Swagger;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

namespace PayrollSolution.RestApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRestApi(this IServiceCollection services, string apiName)
    {
        services.AddHttpLogging(options => options.LoggingFields = HttpLoggingFields.RequestMethod |
                                                                   HttpLoggingFields.RequestPath |
                                                                   HttpLoggingFields.ResponseStatusCode);

        services.AddProblemDetails();        
        services.SetupSwagger(apiName);
        services.AddHttpContextAccessor();
        services.AddHealthChecks();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true));
        });

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
                
        return services;
    }
}