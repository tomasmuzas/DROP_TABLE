using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbApartment
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; } // TODO: @tomasmuzas this is most likely a temporary solution
    }
}
