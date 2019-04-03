using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    public class DbCarReservation
    {
        [Key]
        public int Id { get; set; }

        public string CardPlateNumber { get; set; }
    }
}