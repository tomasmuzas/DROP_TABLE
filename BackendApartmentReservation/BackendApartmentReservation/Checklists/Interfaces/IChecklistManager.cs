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

        Task AddCarRentForEmployee(string employeeId, string tripId);

        Task UpdateCarRentForEmployee(string employeeId, string tripId, CarReservationRequest info);

        Task<CarReservationInfo> GetCarRentInfo(string employeeId, string tripId);

        Task DeleteCarRent(string employeeId, string tripId);

        Task<ChecklistInformationResponse> GetFullChecklistInformation(string employeeId, string tripId);

        Task AddHotelReservationForEmployee(string employeeId, string tripId);

        Task UpdateHotelReservationForEmployee(string employeeId, string tripId, HotelReservationRequest info);

        Task<HotelReservationInfo> GetHotelReservationInfo(string employeeId, string tripId);

        Task DeleteHotelReservation(string employeeId, string tripId);
    }
}