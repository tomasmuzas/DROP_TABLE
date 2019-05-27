namespace BackendApartmentReservation.Checklists.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Microsoft.AspNetCore.Http;

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

        Task AddApartmentReservationForAllEmployees(string tripId);

        Task AddApartmentReservationForEmployee(string employeeId, string tripId);

        Task UpdateApartmentReservationForEmployee(string employeeId, string tripId, ApartmentReservationRequest info);

        Task<ApartmentReservationInfo> GetApartmentReservationInfo(string employeeId, string tripId);

        Task DeleteApartmentReservation(string employeeId, string tripId);

        Task<IEnumerable<DbEmployeeAmenitiesChecklist>> GetAllTripChecklists(string tripId);

        Task UpdateChecklistTrip(DbEmployeeAmenitiesChecklist checklist, string newTripId);

        Task AcceptTripParticipationConfirmation(string employeeId, string tripId);

        Task DeclineTripParticipationConfirmation(string employeeId, string tripId);

        Task AcceptTripMergeConfirmation(string employeeId, string tripId);

        Task DeclineTripMergeConfirmation(string employeeId, string tripId);

        Task UpdateFlightTicketForEmployee(string employeeId, string tripId, IFormFile file);

        Task UpdateCarRentDocumentForEmployee(string employeeId, string tripId, IFormFile file);

        Task DeleteLivingPlace(string employeeId, string tripId);

        Task UpdateHotelDocumentsForEmployee(string employeeId, string tripId, IFormFile file);
    }
}