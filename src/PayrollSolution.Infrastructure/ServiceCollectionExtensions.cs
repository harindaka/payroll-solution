using Microsoft.Extensions.DependencyInjection;
using PayrollSolution.Application.PayslipGeneration;
using PayrollSolution.Infrastructure.Persistance.InMemory;

namespace PayrollSolution.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //Repositories
        services.AddSingleton<IIncomeTaxRangeSpecificationRepository, IncomeTaxRangeSpecificationRepository>();

        return services;
    }
}