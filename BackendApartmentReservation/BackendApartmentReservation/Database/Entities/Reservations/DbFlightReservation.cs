namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System.ComponentModel.DataAnnotations;

    public class DbFlightReservation
    {
        [Key]
        public int Id { get; set; }

        public string FlightNumber { get; set; }
    }
}