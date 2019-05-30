namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    using IntermediaryDTOs;

    public class ChecklistInformationResponse
    {
        public string TripId { get; set; }

        public FlightReservationInfo Flight { get; set; }

        public CarReservationInfo Car { get; set; }

        public LivingPlaceReservationInfo LivingPlace { get; set; }
    }
}