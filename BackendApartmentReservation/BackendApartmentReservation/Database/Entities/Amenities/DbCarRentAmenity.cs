namespace BackendApartmentReservation.Database.Entities.Amenities
{
    using System.ComponentModel.DataAnnotations;
    using Reservations;

    public class DbCarRentAmenity : AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbCarReservation CarReservation { get; set; }

        public DbFile Documents { get; set; }

        public decimal Cost { get; set; }
    }
}