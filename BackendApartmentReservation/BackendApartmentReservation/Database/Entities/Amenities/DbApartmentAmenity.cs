namespace BackendApartmentReservation.Database.Entities.Amenities
{
    using System.ComponentModel.DataAnnotations;
    using Reservations;

    public class DbApartmentAmenity : AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbApartmentReservation ApartmentReservation { get; set; }
    }
}