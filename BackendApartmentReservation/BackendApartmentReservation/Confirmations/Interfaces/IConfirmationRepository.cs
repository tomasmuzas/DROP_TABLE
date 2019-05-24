namespace BackendApartmentReservation.Confirmations.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IConfirmationRepository
    {
        bool HasAcceptedTripParticipation(string employeeId, string tripId);

        bool HasAcceptedTripMerge(string employeeId, string tripId);

        bool HasDeclinedTripParticipation(string employeeId, string tripId);

        bool HasDeclinedTripMerge(string employeeId, string tripId);

        Task CreateConfirmation(DbEmployee employee, DbTrip trip, ConfirmationType type);

        Task AcceptConfirmation(DbConfirmation confirmation);

        Task DeclineConfirmation(DbConfirmation confirmation);

        Task<DbConfirmation> GetWaitingConfirmation(ConfirmationType type, string employeeId, string tripId);
    }
}
