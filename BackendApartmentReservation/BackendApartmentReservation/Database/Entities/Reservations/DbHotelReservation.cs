using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    public class DbHotelReservation
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }
    }
}
