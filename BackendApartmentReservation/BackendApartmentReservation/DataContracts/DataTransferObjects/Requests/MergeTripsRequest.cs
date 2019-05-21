namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class MergeTripsRequest
    {
        public string FirstTripId { get; set; }
        public string SecondTripId { get; set; }
    }
}
