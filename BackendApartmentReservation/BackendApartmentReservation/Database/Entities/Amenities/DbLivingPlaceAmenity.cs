namespace BackendApartmentReservation.Database.Entities.Amenities
{
    using System.ComponentModel.DataAnnotations;
    using Reservations;

    public class DbLivingPlaceAmenity : AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbHotelReservation Hotel { get; set; }

        public DbApartment Apartment { get; set; }
    }
}