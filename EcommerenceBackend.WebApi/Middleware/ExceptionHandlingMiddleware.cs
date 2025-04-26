using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerenceBackend.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError; // 500 default
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var showDetails = env == "Development";

            var result = JsonSerializer.Serialize(new
            {
                Message = "An unexpected error occurred.",
                Details = showDetails ? exception.Message : null
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
