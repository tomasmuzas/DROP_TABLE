using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbEmployeeGroup
    {
        [Key]
        public int Id { get; set; }

        public DbEmployee DbEmployee { get; set; }

        public DbGroup DbGroup { get; set; }
    }
}
