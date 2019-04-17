namespace BackendApartmentReservation.Database.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DbTrip
    {
        [Key]
        public int Id { get; set; }

        public double Cost { get; set; }

        public DbOffice DestinationOffice { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public List<DbGroup> Groups { get; set; }
    }
}