namespace BackendApartmentReservation.Trips.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.Requests;

    public interface ITripRepository
    {
        Task<DbTrip> GetTrip(string tripId);

        Task<IEnumerable<DbEmployeeAmenitiesChecklist>> GetTripChecklistsWithEmployees(string tripId);

        Task<DbTrip> CreateTrip(CreateTripRequest tripRequest, string managerId);

        Task<IEnumerable<DbTrip>> GetAllOrganizedTripsOfEmployee(string employeeId);

        Task<IEnumerable<DbTrip>> GetAllParticipatingTripsOfEmployee(string employeeId);

        Task<IEnumerable<DbTrip>> GetAllTrips();

        Task DeleteTrip(string tripId);
    }
}