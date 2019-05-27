namespace BackendApartmentReservation.Database.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DbApartmentRoom
    {
        [Key]
        public int Id { get; set; }

        public int RoomNumber { get; set; }
    }
}
