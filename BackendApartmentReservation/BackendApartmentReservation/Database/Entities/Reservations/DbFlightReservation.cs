using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System;

    public class DbFlightReservation
    {
        [Key]
        public int Id { get; set; }

        public string Company { get; set; }

        public string FlightNumber { get; set; }

        public DateTime FlightTime { get; set; }

        public string AirportAddress { get; set; }
    }
}
