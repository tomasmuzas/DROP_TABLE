using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BackendApartmentReservation.Extensions.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BackendApartmentReservation.WebApi
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Response = context.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, context.ModelState);
            }
        }
    }
}
