namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using System;

    public class FlightReservationRequest
    {
        public string Company { get; set; }

        public string FlightNumber { get; set; }

        public DateTimeOffset? FlightTime { get; set; }

        public string AirportAddress { get; set; }

        public decimal Cost { get; set; }
    }
}