namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System.ComponentModel.DataAnnotations;

    public class DbHotelReservation
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }
    }
}