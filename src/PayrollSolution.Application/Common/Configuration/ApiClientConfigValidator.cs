using FluentValidation;

namespace PayrollSolution.Application.Configuration
{
    public class ApiClientConfigValidator : AbstractValidator<ApiClientConfig>
    {
        public ApiClientConfigValidator()
        {
            RuleFor(x => x.BaseAddress).NotEmpty();
            RuleFor(x => x.Proxy).SetValidator(new ApiClientProxyConfigValidator());
        }
    }

    public class ApiClientProxyConfigValidator : AbstractValidator<ApiClientProxyConfig>
    {
        public ApiClientProxyConfigValidator()
        {
            When(x => x.Enabled, () => {
                RuleFor(x => x.Url).NotEmpty();
            });
        }
    }
}
