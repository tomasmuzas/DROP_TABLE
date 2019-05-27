namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class ApartmentAddressUpdateRequest
    {
        public int ApartmentId { get; set; }
        public string Address { get; set; }
    }
}
