namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    using System;

    public class EmployeePlanInfo
    {
        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }
    }
}