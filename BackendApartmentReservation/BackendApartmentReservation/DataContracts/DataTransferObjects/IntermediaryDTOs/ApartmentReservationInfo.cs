using System;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    public class ApartmentReservationInfo
    {
        public bool Required { get; set; }

        public string ApartmentAddress { get; set; }

        public int RoomNumber { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }
    }
}