namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    using Responses;

    public class BasicPersonalChecklistInfo
    {
        public EmployeeInfo Employee { get; set; }

        public bool IsFlightRequired { get; set; }

        public bool IsApartmentRequired { get; set; }

        public bool IsCarRentRequired { get; set; }
    }
}