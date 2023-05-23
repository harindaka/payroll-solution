using FluentValidation;
using PayrollSolution.Infrastructure.ApiClients;
using PayrollSolution.Ui.Configuration;
using PayrollSolution.Ui.Data;
using System.Reflection;

namespace PayrollSolution.Ui
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUi(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddApiClientWithConfig<IPayrollApiClient, PayRollApiClientConfig>(PayRollApiClientConfig.Section);

            return services;
        }
    }
}
