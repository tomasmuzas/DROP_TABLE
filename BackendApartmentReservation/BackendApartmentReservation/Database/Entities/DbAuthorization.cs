using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbAuthorization
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DbEmployee Employee { get; set; }
    }
}
