using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbApartment
    {
        [Key]
        public int Id { get; set; }

        public string Adress { get; set; }
    }
}
