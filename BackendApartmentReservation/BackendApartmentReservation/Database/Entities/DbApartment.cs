namespace BackendApartmentReservation.Database.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DbApartment
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; } // TODO: @tomasmuzas this is most likely a temporary solution
    }
}