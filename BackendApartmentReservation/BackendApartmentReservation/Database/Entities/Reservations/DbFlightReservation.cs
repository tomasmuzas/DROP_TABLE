namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DbFlightReservation
    {
        [Key]
        public int Id { get; set; }

        public string Company { get; set; }

        public string FlightNumber { get; set; }

        public DateTimeOffset? FlightTime { get; set; }

        public string AirportAddress { get; set; }
    }
}