using System.ComponentModel.DataAnnotations;

using BackendApartmentReservation.Database.Entities.Reservations;

namespace BackendApartmentReservation.Database.Entities.Amenities
{
    public class DbCarRentAmenity : AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbCarReservation CarReservation { get; set; }
    }
}
