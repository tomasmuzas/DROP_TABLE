namespace BackendApartmentReservation.Database.Entities.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DbHotelReservation
    {
        [Key]
        public int Id { get; set; }

        public string HotelName { get; set; }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }
    }
}