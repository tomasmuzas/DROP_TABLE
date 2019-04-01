using BackendApartmentReservation.Extensions.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BackendApartmentReservation.Infrastructure.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var logger = new LogFactory().GetCurrentClassLogger();
            var exception = context.Exception;
            logger.Error($"Uncaught exception happened. Error will be responded.\n\nException:{exception.ToJson()}");

            context.ExceptionHandled = true;

            var responseObject = new
            {
                ErrorCode = ErrorCodes.GenericInternalServerError,
                Exception = "dev" == "dev" ? exception : null
            };

            var response = new ObjectResult(responseObject);
            response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = response;
        }
    }
}
