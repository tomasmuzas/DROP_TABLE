using System;

namespace BackendApartmentReservation.Database.Entities
{
    public class AmenityBase
    {
        public bool Required { get; set; }

        public DateTimeOffset? BookedAt { get; set; }
    }
}
