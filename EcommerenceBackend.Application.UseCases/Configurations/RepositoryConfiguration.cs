using EcommerenceBackend.Application.Domain.Configurations;
using EcommerenceBackend.Application.Domain.Services;
using EcommerenceBackend.Application.UseCases.Commands.RegisterUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
            });

            return services;
        }
    }
}
