namespace BackendApartmentReservation.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.Requests;

    public interface ITripRepository
    {
        Task<DbTrip> GetTrip (string tripId);

        Task<IEnumerable<DbEmployeeAmenitiesChecklist>> GetTripChecklistsWithEmployees(string tripId);

        Task<DbTrip> CreateTrip(CreateTripRequest tripRequest);
    }
}