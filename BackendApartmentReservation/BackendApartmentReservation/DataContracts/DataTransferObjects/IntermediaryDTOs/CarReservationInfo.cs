using System;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    public class CarReservationInfo
    {
        public bool IsRequired { get; set; }

        public string CarNumber { get; set; }

        public DateTimeOffset? RentStartTime { get; set; }

        public DateTimeOffset? RentEndTime { get; set; }

        public string CarAddress { get; set; }

        public string DocumentsFileId { get; set; }
    }
}