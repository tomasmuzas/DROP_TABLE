namespace BackendApartmentReservation.Checklists.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;

    public interface IChecklistManager
    {
        Task<DbEmployeeAmenitiesChecklist> CreateEmptyChecklistForEmployee(string employeeId, string tripId);

        Task AddFlightForEmployee(string employeeId, string tripId);

        Task UpdateFlightForEmployee(string employeeId, string tripId, FlightReservationRequest info);

        Task<FlightReservationInfo> GetFlightInfo(string employeeId, string tripId);

        Task DeleteFlight(string employeeId, string tripId);

        Task<ChecklistInformationResponse> GetFullChecklistInformation(string employeeId, string tripId);
    }
}