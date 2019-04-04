using System;
using System.Collections.Generic;

using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
using BackendApartmentReservation.Infrastructure.Exceptions;

using FakeItEasy;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

using Xunit;

namespace BackendApartmentReservation.Tests.Infrastructure
{
    public class GlobalExceptionFilterTests
    {
        [InlineData("dev")]
        [InlineData("local")]
        [InlineData("prod")]
        [Theory]
        public void OnException_DisplaysExceptionCorrectly(string environmentName)
        {
            var fakeEnvironment = A.Fake<IHostingEnvironment>();
            fakeEnvironment.EnvironmentName = environmentName;

            var filter = new GlobalExceptionFilter(fakeEnvironment);

            var exception = new Exception();
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>());
            exceptionContext.Exception = exception;

            filter.OnException(exceptionContext);

            Assert.Equal(exceptionContext.Exception, exception);
            Assert.NotNull(exceptionContext.Result);

            Assert.IsAssignableFrom<ObjectResult>(exceptionContext.Result);
            var result = (ObjectResult)exceptionContext.Result;
            Assert.Equal(result.StatusCode, StatusCodes.Status500InternalServerError);

            Assert.NotNull(result.Value);
            Assert.IsAssignableFrom<ErrorResponse>(result.Value);
            var errorResponse = (ErrorResponse)result.Value;

            if (environmentName == "prod")
            {
                Assert.Null(errorResponse.Exception);
            }
            else
            {
                Assert.Equal(errorResponse.Exception, exception);
            }

            Assert.Equal(errorResponse.ErrorCode, ErrorCodes.GenericInternalServerError);
        }
    }
}
