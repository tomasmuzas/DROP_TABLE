using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BackendApartmentReservation.Infrastructure.Logging
{
    using Exceptions;

    public class RequestValidationFilter : IAsyncActionFilter
    {
        private readonly Logger<> _logger = NLog.LogManager.GetCurrentClassLogger();

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            { 
                _logger.Error($"Validation failed. Errors: {context.ModelState}");
                throw new HttpStatusException(HttpStatusCode.BadRequest);
            }

            await next.Invoke();
        }
    }
}
