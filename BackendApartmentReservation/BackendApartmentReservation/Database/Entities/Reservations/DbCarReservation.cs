using System;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System.ComponentModel.DataAnnotations;

    public class DbCarReservation
    {
        [Key]
        public int Id { get; set; }

        public string CarNumber { get; set; }

        public DateTime? RentStartTime { get; set; }

        public DateTime? RentEndTime { get; set; }

        public string CarAddress { get; set; }
    }
}