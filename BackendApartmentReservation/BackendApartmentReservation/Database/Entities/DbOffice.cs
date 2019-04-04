using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbOffice
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }

        public DbApartment OfficeApartment { get; set; }
    }
}
