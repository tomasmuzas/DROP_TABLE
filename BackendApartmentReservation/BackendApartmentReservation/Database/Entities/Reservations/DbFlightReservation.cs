using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    public class DbFlightReservation
    {
        [Key]
        public int Id { get; set; }

        public string FlightNumber { get; set; }
    }
}
