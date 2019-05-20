namespace BackendApartmentReservation.Confirmations.Interfaces
{
    public interface IConfirmationRepository
    {
        bool HasAcceptedTripParticipation(string employeeId, string tripId);

        bool HasAcceptedTripMerge(string employeeId, string tripId);
    }
}
