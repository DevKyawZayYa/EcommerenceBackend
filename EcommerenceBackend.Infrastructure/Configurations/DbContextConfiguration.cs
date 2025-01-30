using EcommerenceBackend.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerenceBackend.Infrastructure.Configurations
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("NorthwindConnection"),
                    new MySqlServerVersion(new Version(8, 0, 32)),
                    b => b.MigrationsAssembly("EcommerenceBackend.Infrastructure")
                ));

            services.AddDbContext<OrderDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("NorthwindConnection"),
                    new MySqlServerVersion(new Version(8, 0, 32)),
                    b => b.MigrationsAssembly("EcommerenceBackend.Infrastructure")
                ));

            return services;
        }
    }
}
