using System;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    public class ApartmentReservationInfo
    {
        public bool Required { get; set; }

        public string ApartmentAddress { get; set; }

        public int RoomNumber { get; set; }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }
    }
}