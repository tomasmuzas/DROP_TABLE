namespace BackendApartmentReservation.Database.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DbFile
    {
        [Key]
        public int Id { get; set; }

        public string ExternalFileId { get; set; }

        public byte[] File { get; set; }
    }
}