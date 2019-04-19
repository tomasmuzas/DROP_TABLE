using System.ComponentModel.DataAnnotations;

using BackendApartmentReservation.Database.Entities.Reservations;

namespace BackendApartmentReservation.Database.Entities.Amenities
{
    public class DbLivingPlaceAmenity : AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbLivingPlaceReservation LivingPlaceReservation { get; set; }
    }
}
