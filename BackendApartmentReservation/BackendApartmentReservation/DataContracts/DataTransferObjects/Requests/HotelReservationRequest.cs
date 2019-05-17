namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using System;

    public class HotelReservationRequest
    {
        public string HotelName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
