using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TodoAPi.ActionFilters
{
    public class LoggingActionFilter : IActionFilter 
    {
        private readonly ILogger _logger;
        public LoggingActionFilter(ILogger<LoggingActionFilter> logger) {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            var request = context.HttpContext.Request;
            _logger.LogInformation(" {0} {1} ", request.Method, request.Path);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response;
            _logger.LogInformation(" Response Code: {0} ", response.StatusCode);
            
        }
    }
}
