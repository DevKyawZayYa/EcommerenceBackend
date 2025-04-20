using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerenceBackend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<IRedisService, RedisService>();

            return services;
        }
    }
}
