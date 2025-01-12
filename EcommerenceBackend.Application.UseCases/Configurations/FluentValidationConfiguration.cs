using EcommerenceBackend.Application.UseCases.Commands.RegisterUser;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;


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
