namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System.ComponentModel.DataAnnotations;

    public class DbHotelReservation
    {
        [Key]
        public int Id { get; set; }

        public string HotelName { get; set; }
    }
}