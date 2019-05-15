namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System.ComponentModel.DataAnnotations;

    public class DbLivingPlaceReservation
    {
        [Key]
        public int Id { get; set; }

        //TODO add RoomReservation

        public DbHotelReservation HotelReservation { get; set; }
    }
}