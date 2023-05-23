using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace PayrollSolution.Infrastructure.Configuration
{
    public static class ConfigurationExtensions
    {
        public static OptionsBuilder<TOptions> AddConfiguration<TOptions>(
            this IServiceCollection services, string configurationSection)
            where TOptions : class
        {
            return services.AddOptions<TOptions>().BindConfiguration(configurationSection);
        }

        /// <summary>
        /// Registers a generic config options type with support for fluent validation
        /// </summary>
        public static OptionsBuilder<TOptions> AddConfiguration<TOptions, TValidator>(
            this IServiceCollection services, string configurationSection)
            where TOptions : class
            where TValidator : class, IValidator<TOptions>
        {
            // Add the validator
            services.AddScoped<IValidator<TOptions>, TValidator>();

            return services.AddOptions<TOptions>()
                .BindConfiguration(configurationSection)
                .ValidateUsingFluentValidation()
                .ValidateOnStart();
        }

        /// <summary>
        /// Plugs in fluent validation to be used for configuration validaton
        /// https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-using-flentvalidation/
        /// </summary>
        public static OptionsBuilder<TOptions> ValidateUsingFluentValidation<TOptions>(
            this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
                provider => new FluentValidationOptions<TOptions>(optionsBuilder.Name, provider));
            return optionsBuilder;
        }
    }    
}
