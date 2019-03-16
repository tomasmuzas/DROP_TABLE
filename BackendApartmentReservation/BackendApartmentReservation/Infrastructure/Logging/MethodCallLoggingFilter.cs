using System.Threading.Tasks;
using BackendApartmentReservation.Extensions.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NLog;

namespace BackendApartmentReservation.Infrastructure.Logging
{
    public class MethodCallLoggingFilter : IAsyncActionFilter
    {
        private readonly Logger _logger = new LogFactory().GetCurrentClassLogger();

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
                             $"Parameters: {parameters.ToJson()}\n" +
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
