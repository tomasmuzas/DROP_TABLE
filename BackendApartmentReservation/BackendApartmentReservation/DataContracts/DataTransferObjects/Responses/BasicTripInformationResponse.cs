namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    using System;
    using System.Collections.Generic;
    using IntermediaryDTOs;

    public class BasicTripInformationResponse
    {
        public string TripId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public OfficeInfoResponse Office { get; set; }

        public int AvailableApartments { get; set; }


        public List<BasicPersonalChecklistInfo> ChecklistInfos { get; set; }
    }
}