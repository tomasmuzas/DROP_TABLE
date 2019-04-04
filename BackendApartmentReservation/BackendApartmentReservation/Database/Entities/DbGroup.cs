using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbGroup
    {
        [Key]
        public int Id { get; set; }

        public List<DbEmployee> Employees { get; set; }

        public DbEmployee Manager { get; set; }

        public DbOffice StartingOffice { get; set; }
    }
}
