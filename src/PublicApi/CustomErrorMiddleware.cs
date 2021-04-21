using System;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PublicApi
{
    public static class CustomErrorMiddlewareExtension
    {
        public static void UseCustomErrors(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomErrorMiddleware>();
        }
    }

    public class CustomErrorMiddleware
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true
        };

        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IAppLogger<CustomErrorMiddleware> _logger;

        public CustomErrorMiddleware(RequestDelegate next,
            IHostEnvironment environment,
            IAppLogger<CustomErrorMiddleware> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (_environment.IsDevelopment())
                await WriteResponse(httpContext, true);
            else
                await WriteResponse(httpContext, false);

            await _next(httpContext);
        }

        private async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            // Try and retrieve the error from the ExceptionHandler middleware
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            if (ex != null)
            {
                httpContext.Response.ContentType = "application/json";

                var message = includeDetails ? ex.Message : "Something went wrong in our server";
                var details = includeDetails ? ex.ToString() : null;
                var path = (exceptionDetails as ExceptionHandlerFeature)?.Path;

                _logger.LogError(
                    $"{path ?? "/UnrecognizedPath"} | {ex.TargetSite?.ReflectedType?.FullName}{Environment.NewLine}{details}",
                    ex);

                await JsonSerializer.SerializeAsync(httpContext.Response.Body, message, JsonSerializerOptions);
            }
        }
    }
}