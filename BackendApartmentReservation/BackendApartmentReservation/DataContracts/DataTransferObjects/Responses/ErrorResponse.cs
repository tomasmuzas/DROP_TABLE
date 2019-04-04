using System;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }

        public Exception Exception { get; set; }
    }
}