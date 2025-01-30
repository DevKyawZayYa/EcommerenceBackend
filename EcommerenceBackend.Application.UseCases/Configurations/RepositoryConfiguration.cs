using EcommerenceBackend.Application.Domain.Configurations;
using EcommerenceBackend.Application.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.RegisterUser;
using EcommerenceBackend.Application.UseCases.Products.Commands.CreateProduct;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands;

namespace EcommerenceBackend.Application.UseCases.Configurations
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Map JwtSettings from appsettings.json
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // Register JwtTokenService
            services.AddTransient<JwtTokenService>();

            // Register generic repositories
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Consolidate MediatR registration
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly);
            });

            return services;
        }
    }
}
