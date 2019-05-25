namespace BackendApartmentReservation.Database.Entities.Amenities
{
    using System.ComponentModel.DataAnnotations;
    using Reservations;

    public class DbFlightAmenity : AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbFlightReservation FlightReservation { get; set; }

        public DbFile Ticket { get; set; }
    }
}