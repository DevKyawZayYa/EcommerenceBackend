using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerenceBackend.Application.UseCases.Mappings
{
    public static class MappingRegistration
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            return services;
        }
    }
}
