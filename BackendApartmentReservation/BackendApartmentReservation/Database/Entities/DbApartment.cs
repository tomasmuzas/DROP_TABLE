namespace BackendApartmentReservation.Database.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DbApartment
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }

        public List<DbApartmentRoom> Rooms { get; set; }
    }
}