namespace BackendApartmentReservation.Confirmations.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IConfirmationRepository
    {
        bool HasAcceptedTripParticipation(string employeeId, string tripId);

        bool HasAcceptedTripMerge(string employeeId, string tripId);

        Task CreateConfirmation(DbEmployee employee, DbTrip trip, ConfirmationType type);
    }
}
