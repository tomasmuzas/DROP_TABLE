using System;

namespace BackendApartmentReservation.Infrastructure.Exceptions
{
    public class ErrorCodeException : Exception
    {
        public string ErrorCode { get; set; }

        public ErrorCodeException(string errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
