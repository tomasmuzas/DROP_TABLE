namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using System;
    using Microsoft.AspNetCore.Builder;

    public class HotelReservationRequest
    {
        public string HotelName { get; set; }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }

        public decimal Cost { get; set; }
    }
}
