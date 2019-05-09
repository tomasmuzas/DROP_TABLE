namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System.ComponentModel.DataAnnotations;

    public class DbCarReservation
    {
        [Key]
        public int Id { get; set; }

        public string CarNumber { get; set; }
    }
}