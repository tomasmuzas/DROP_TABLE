using BackendApartmentReservation.Database.Entities;
using System;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class ApartmentReservationRequest
    {
        public DbApartmentRoom Room { get; set; }

        public DateTimeOffset DateFrom { get; set; }

        public DateTimeOffset DateTo { get; set; }
    }
}
