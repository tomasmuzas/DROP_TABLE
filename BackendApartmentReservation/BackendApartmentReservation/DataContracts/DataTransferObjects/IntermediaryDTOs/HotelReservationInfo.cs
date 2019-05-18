namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    using System;

    public class HotelReservationInfo
    {
        public bool Required { get; set; }

        public string HotelName { get; set; }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }
    }
}