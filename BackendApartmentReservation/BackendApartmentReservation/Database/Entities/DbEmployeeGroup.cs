namespace BackendApartmentReservation.Database.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DbEmployeeGroup
    {
        [Key]
        public int Id { get; set; }

        public DbEmployee DbEmployee { get; set; }

        public DbGroup DbGroup { get; set; }
    }
}