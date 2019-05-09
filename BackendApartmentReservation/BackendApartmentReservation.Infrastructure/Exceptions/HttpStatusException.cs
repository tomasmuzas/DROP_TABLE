using System;

namespace BackendApartmentReservation.Infrastructure.Exceptions
{
    using System.Net;

    public class HttpStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpStatusException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

    }
}
