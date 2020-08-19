using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TodoAPi.Middlewares {
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
    public class RequestResponseLoggingMiddleware {
        private RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {
            var request = context.Request;
            _logger.LogInformation(" {0} {1} ", request.Method, request.Path);
             
             // Call the next delegate/middleware in the pipeline
            await _next(context);

            var response = context.Response;
            _logger.LogInformation(" Response: {0}", response.StatusCode);


        }
    }
}
