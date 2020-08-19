using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TodoAPi.Operations;

namespace TodoAPi.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestResponseLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestResponseLoggingMiddleware> logger,
            IOperationTransient transientOperation,
            IOperationSingleton singletonOperation
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IOperationScoped scopedOperation)
        {
            var operationId = scopedOperation.OperationId;
            var request = context.Request;
            _logger.LogInformation(" Scoped ID: {0} | Request: {1} {2} ", operationId, request.Method, request.Path);

            // Call the next delegate/middleware in the pipeline
            await _next(context);

            var response = context.Response;
            _logger.LogInformation(" Scoped ID: {0} | Response: {1}",operationId, response.StatusCode);
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
