namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using System;

    public class HotelReservationRequest
    {
        public string HotelName { get; set; }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }
    }
}
