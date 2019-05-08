using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
using BackendApartmentReservation.Extensions.Json;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BackendApartmentReservation.Infrastructure.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _environment;

        public GlobalExceptionFilter(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            var logger = new LogFactory().GetCurrentClassLogger();
            var exception = context.Exception;
            logger.Error($"Uncaught exception happened. Error will be responded.\n\nException:{exception.ToJson()}");

            context.ExceptionHandled = true;

            var responseObject = new ErrorResponse
            {
                ErrorCode = ErrorCodes.GenericInternalServerError,
                Exception = _environment.EnvironmentName != "prod" ? exception : null
            };

            var response = new ObjectResult(responseObject);
            response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = response;
        }
    }
}
