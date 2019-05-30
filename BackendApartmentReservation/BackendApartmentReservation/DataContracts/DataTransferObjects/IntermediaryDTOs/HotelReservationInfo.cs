namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    using System;

    public class HotelReservationInfo
    {
        public bool Required { get; set; }

        public string HotelName { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public string DocumentsFileId { get; set; }

        public decimal Price { get; set; }
    }
}