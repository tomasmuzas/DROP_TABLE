using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BackendApartmentReservation.Infrastructure.Exceptions
{
    using Extensions.Json;

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

            IActionResult response;
            if (exception is ErrorCodeException errorCodeException)
            {
                var responseObject = new ErrorResponse
                {
                    ErrorCode = errorCodeException.ErrorCode,
                    Exception = exception
                };
                response = new BadRequestObjectResult(responseObject);
            }
            else
            {
                var responseObject = new ErrorResponse
                {
                    ErrorCode = ErrorCodes.GenericInternalServerError,
                    Exception = _environment.EnvironmentName != "prod" ? exception : null
                };

                response = new ObjectResult(responseObject);
                (response as ObjectResult).StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Result = response;
        }
    }
}
