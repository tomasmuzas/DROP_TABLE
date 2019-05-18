using System;
using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.Database.Entities
{
    public class DbRoomReservation
    {
        [Key]
        public int Id { get; set; }

        public DbEmployee Employee { get; set; }

        public DbApartmentRoom Room { get; set; }

        public DateTimeOffset DateFrom { get; set; }

        public DateTimeOffset DateTo { get; set; }
    }
}
