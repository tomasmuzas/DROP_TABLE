using System.ComponentModel.DataAnnotations;

using BackendApartmentReservation.Database.Entities.Amenities;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbEmployeeAmenitiesChecklist
    {
        [Key]
        public int Id { get; set; }

        public DbEmployee Employee { get; set; }

        public DbTrip Trip { get; set; }

        public DbLivingPlaceAmenity LivingPlace { get; set; }

        public DbCarRentAmenity Car { get; set; }

        public DbFlightAmenity Flight { get; set; }
    }
}
