namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    using System;

    public class FlightReservationInfo
    {
        public bool IsRequired { get; set; }

        public string FlightNumber { get; set; }

        public string Company { get; set; }

        public DateTime? FlightTime { get; set; }

        public string AirportAddress { get; set; }
    }
}