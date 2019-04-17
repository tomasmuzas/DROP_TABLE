namespace BackendApartmentReservation.Database.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DbEmployee
    {
        [Key]
        public int Id { get; set; }

        public string ExternalEmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DbOffice Office { get; set; }
    }
}