using System.ComponentModel.DataAnnotations;

using BackendApartmentReservation.Database.Entities.Reservations;

namespace BackendApartmentReservation.Database.Entities.Amenities
{
    public class DbFlightAmenity : AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbFlightReservation FlightReservation { get; set; }
    }
}
