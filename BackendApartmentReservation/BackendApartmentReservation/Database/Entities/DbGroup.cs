namespace BackendApartmentReservation.Database.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DbGroup
    {
        [Key]
        public int Id { get; set; }

        public string ExternalGroupId { get; set; }

        public DbEmployee Manager { get; set; }

        public DbOffice StartingOffice { get; set; }
    }
}