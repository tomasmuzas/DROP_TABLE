using System;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    public class CarReservationInfo
    {
        public bool IsRequired { get; set; }

        public string CarNumber { get; set; }

        public DateTime? RentStartTime { get; set; }

        public DateTime? RentEndTime { get; set; }

        public string CarAddress { get; set; }
    }
}