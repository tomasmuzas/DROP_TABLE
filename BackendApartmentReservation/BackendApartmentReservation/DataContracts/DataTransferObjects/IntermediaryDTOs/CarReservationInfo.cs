using System;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    public class CarReservationInfo
    {
        public bool IsRequired { get; set; }

        public string CarNumber { get; set; }

        public string RentStartTime { get; set; }

        public string RentEndTime { get; set; }

        public string CarAddress { get; set; }

        public string DocumentsFileId { get; set; }

        public decimal Cost { get; set; }
    }
}