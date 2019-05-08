namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System.ComponentModel.DataAnnotations;

    public class DbLivingPlaceReservation
    {
        [Key]
        public int Id { get; set; }

        public DbApartmentReservation ApartmentReservation { get; set; }

        public DbHotelReservation HotelReservation { get; set; }
    }
}