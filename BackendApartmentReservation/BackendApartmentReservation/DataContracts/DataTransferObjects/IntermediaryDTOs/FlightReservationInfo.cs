namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    using System;

    public class FlightReservationInfo
    {
        public bool IsRequired { get; set; }

        public string FlightNumber { get; set; }

        public string Company { get; set; }

        public string FlightTime { get; set; }

        public string AirportAddress { get; set; }

        public string TicketFileId { get; set; }

        public decimal Cost { get; set; }
    }
}