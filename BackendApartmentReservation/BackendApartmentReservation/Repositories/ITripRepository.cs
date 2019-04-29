namespace BackendApartmentReservation.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
    using Database.Entities;

    public interface ITripRepository
    {
        Task<DbTrip> GetTrip (string tripId);

        Task<IEnumerable<DbEmployeeAmenitiesChecklist>> GetTripChecklistsWithEmployees(string tripId);

        Task<DbTrip> CreateTrip(CreateTripRequest trip);
    }
}