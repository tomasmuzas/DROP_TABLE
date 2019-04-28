namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    using System;
    using System.Collections.Generic;
    using IntermediaryDTOs;

    public class BasicTripInformationResponse
    {
        public string TripId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<BasicPersonalChecklistInfo> ChecklistInfos { get; set; }
    }
}