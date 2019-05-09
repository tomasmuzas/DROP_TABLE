namespace BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs
{
    public class LivingPlaceReservationInfo
    {
        public bool Required { get; set; }

        public ApartmentReservationInfo ApartmentReservationInfo { get; set; }

        public HotelReservationInfo HotelReservationInfo { get; set; }
    }
}