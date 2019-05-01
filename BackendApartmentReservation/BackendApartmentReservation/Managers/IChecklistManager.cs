using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Managers
{
    using DataContracts.DataTransferObjects.Requests;

    public interface IChecklistManager
    {
        Task<DbEmployeeAmenitiesChecklist> CreateEmptyChecklistForEmployee(string employeeId, string tripId);

        Task AddFlightForEmployee(string employeeId, string tripId);

        Task UpdateFlightForEmployee(string employeeId, string tripId, FlightReservationRequest info);

        Task<FlightReservationInfo> GetFlightInfo(string employeeId, string tripId);

        Task DeleteFlight(string employeeId, string tripId);
    }
}