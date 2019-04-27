using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    public class DbCarReservation
    {
        [Key]
        public int Id { get; set; }

        public string CarNumber { get; set; }
    }
}