using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.RegisterUser;


namespace EcommerenceBackend.Infrastructure.Configurations
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterUserCommand>();

            return services;
        }
    }
}
