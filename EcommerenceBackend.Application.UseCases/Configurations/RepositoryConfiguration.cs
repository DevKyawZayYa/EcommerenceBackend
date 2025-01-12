using EcommerenceBackend.Application.Domain.Services;
using EcommerenceBackend.Application.UseCases.Commands.RegisterUser;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Configurations
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            // Register generic repositories
            services.AddTransient<JwtTokenService>();

            // Consolidate MediatR registration
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly);
            });

            return services;
        }
    }
}
