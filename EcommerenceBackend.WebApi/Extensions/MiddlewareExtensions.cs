using Microsoft.AspNetCore.Builder;

namespace EcommerenceBackend.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Middleware.ExceptionHandlingMiddleware>();
        }
    }
}
