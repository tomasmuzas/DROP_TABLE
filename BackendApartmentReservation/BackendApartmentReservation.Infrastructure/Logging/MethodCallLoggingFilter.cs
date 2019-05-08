using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NLog;

namespace BackendApartmentReservation.Infrastructure.Logging
{
    using Extensions.Json;

    public class MethodCallLoggingFilter : IAsyncActionFilter
    {
        private readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        // Called on async method execution
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next().ConfigureAwait(false); // Execute method
            
            if (resultContext.Exception == null)
            {
                var result = resultContext.Result as ObjectResult;
                var parameters = context.ActionDescriptor.Parameters;
                var fullActionName = context.ActionDescriptor.DisplayName;
                var httpMethod = context.HttpContext?.Request.Method;

                _logger.Info($"Controller action {fullActionName} has successfully finished execution.\n\n" +
                             $"HTTP method: {httpMethod}\n" +
                             //$"Parameters: {parameters.ToJson()}\n" + TODO  @tomu fix parameter serialization recursion
                             $"Result: {result?.Value?.ToJson() ?? "Void"}\n\n" +
                             $"Status code: {result?.StatusCode?.ToString() ?? "unknown"}");
            }
            else
            {
                var fullActionName = context.ActionDescriptor.DisplayName;

                _logger.Error($"Controller action {fullActionName} ended up with exception: {resultContext.Exception.GetType().Name}");
            }
        }
    }
}
