namespace BackendApartmentReservation.Database.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DbOffice
    {
        [Key]
        public int Id { get; set; }

        public string ExternalOfficeId { get; set; }

        public string Address { get; set; }

        public DbApartment OfficeApartment { get; set; }
    }
}