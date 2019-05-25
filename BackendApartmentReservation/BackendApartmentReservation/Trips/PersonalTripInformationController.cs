using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Trips
{
    using System.Threading.Tasks;
    using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
    using Checklists.Interfaces;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/trips/{tripId}/employees/{employeeId}")]
    [ApiController]
    public class PersonalTripInformationController : AuthorizedController
    {
        private readonly IChecklistManager _checklistManager;

        public PersonalTripInformationController(IChecklistManager checklistManager)
        {
            _checklistManager = checklistManager;
        }

        [HttpGet]
        [Route("checklist")]
        public async Task<ChecklistInformationResponse> GetEmployeeChecklistInformation(
            string tripId,
            string employeeId)
        {
            return await _checklistManager.GetFullChecklistInformation(employeeId, tripId);
        }

        [HttpGet]
        [Route("checklist/personal")]
        public async Task<ChecklistInformationResponse> GetEmployeeChecklistInformation(
            string tripId)
        {
            var employeeId = GetEmployeeId();
            return await _checklistManager.GetFullChecklistInformation(employeeId, tripId);
        }

        [HttpGet]
        [Route("flight")]
        public async Task<FlightReservationInfo> GetFlightInfo(string tripId, string employeeId)
        {
            return await _checklistManager.GetFlightInfo(employeeId, tripId);
        }

        [HttpPost]
        [Route("flight")]
        public async Task AddFlight(string tripId, string employeeId)
        {
            await _checklistManager.AddFlightForEmployee(employeeId, tripId);
        }

        [HttpPut]
        [Route("flight")]
        public async Task UpdateFlight(
            string tripId,
            string employeeId,
            [FromBody] FlightReservationRequest info)
        {
            await _checklistManager.UpdateFlightForEmployee(employeeId, tripId, info);
        }

        [HttpPut]
        [Route("flight/ticket")]
        public async Task UpdateFlightTicket(
            string tripId,
            string employeeId,
            [FromForm]FileRequest request)
        {

            await _checklistManager.UpdateFlightTicketForEmployee(employeeId, tripId, request.File);
        }

        [HttpDelete]
        [Route("flight")]
        public async Task DeleteFlight(string tripId, string employeeId)
        {
            await _checklistManager.DeleteFlight(employeeId, tripId);
        }

        [HttpGet]
        [Route("car")]
        public async Task<CarReservationInfo> GetCarRentInfo(string tripId, string employeeId)
        {
            return await _checklistManager.GetCarRentInfo(employeeId, tripId);
        }

        [HttpPost]
        [Route("car")]
        public async Task AddCarRent(string tripId, string employeeId)
        {
            await _checklistManager.AddCarRentForEmployee(employeeId, tripId);
        }

        [HttpPut]
        [Route("car")]
        public async Task UpdateCarRent(
            string tripId,
            string employeeId,
            [FromBody] CarReservationRequest info)
        {
            await _checklistManager.UpdateCarRentForEmployee(employeeId, tripId, info);
        }

        [HttpPut]
        [Route("car/documents")]
        public async Task UpdateCarRentDocuments(
            string tripId,
            string employeeId,
            [FromForm] FileRequest request)
        {
            await _checklistManager.UpdateCarRentDocumentForEmployee(employeeId, tripId, request.File);
        }

        [HttpDelete]
        [Route("car")]
        public async Task DeleteCarRent(string tripId, string employeeId)
        {
            await _checklistManager.DeleteCarRent(employeeId, tripId);
        }

        [HttpGet]
        [Route("apartment")]
        public async Task<ApartmentReservationInfo> GetApartmentReservationInfo(string tripId, string employeeId)
        {
            return await _checklistManager.GetApartmentReservationInfo(employeeId, tripId);
        }

        [HttpPost]
        [Route("apartment")]
        public async Task AddApartmentReservation(string tripId, string employeeId)
        {
            await _checklistManager.AddApartmentReservationForEmployee(employeeId, tripId);
        }

        [HttpPost]
        [Route("~/api/trips/{tripId}/apartment")]
        public async Task AddApartmentReservationForAllEmployees(string tripId)
        {
            await _checklistManager.AddApartmentReservationForAllEmployees(tripId);
        }

        [HttpPut]
        [Route("apartment")]
        public async Task UpdateApartmentReservation(
            string tripId,
            string employeeId,
            [FromBody] ApartmentReservationRequest info)
        {
            await _checklistManager.UpdateApartmentReservationForEmployee(employeeId, tripId, info);
        }

        [HttpDelete]
        [Route("apartment")]
        public async Task DeleteApartmentReservation(string tripId, string employeeId)
        {
            await _checklistManager.DeleteApartmentReservation(employeeId, tripId);
        }

        [HttpGet]
        [Route("hotel")]
        public async Task<HotelReservationInfo> GetHotelReservationInfo(string tripId, string employeeId)
        {
            return await _checklistManager.GetHotelReservationInfo(employeeId, tripId);
        }

        [HttpPost]
        [Route("hotel")]
        public async Task AddHotelReservation(string tripId, string employeeId)
        {
            await _checklistManager.AddHotelReservationForEmployee(employeeId, tripId);
        }

        [HttpPut]
        [Route("hotel")]
        public async Task UpdateHotelReservation(
            string tripId,
            string employeeId,
            [FromBody] HotelReservationRequest info)
        {
            await _checklistManager.UpdateHotelReservationForEmployee(employeeId, tripId, info);
        }

        [HttpDelete]
        [Route("hotel")]
        public async Task DeleteHotelReservation(string tripId, string employeeId)
        {
            await _checklistManager.DeleteHotelReservation(employeeId, tripId);
        }

        [HttpPost]
        [Route("~/api/trips/{tripId}/participation/accept")]
        [OrganizerOnly]
        public async Task AcceptTripParticipationConfirmation(string tripId)
        {
            var employeeId = GetEmployeeId();
            await _checklistManager.AcceptTripParticipationConfirmation(employeeId, tripId);
        }

        [HttpPost]
        [Route("~/api/trips/{tripId}/participation/decline")]
        [OrganizerOnly]
        public async Task DeclineTripParticipationConfirmation(string tripId)
        {
            var employeeId = GetEmployeeId();
            await _checklistManager.DeclineTripParticipationConfirmation(employeeId, tripId);
        }

        [HttpPost]
        [Route("~/api/trips/{tripId}/merge/accept")]
        [OrganizerOnly]
        public async Task AcceptTripMergeConfirmation(string tripId)
        {
            var employeeId = GetEmployeeId();
            await _checklistManager.AcceptTripMergeConfirmation(employeeId, tripId);
        }

        [HttpPost]
        [Route("~/api/trips/{tripId}/merge/decline")]
        [OrganizerOnly]
        public async Task DeclineTripMergeConfirmation(string tripId)
        {
            var employeeId = GetEmployeeId();
            await _checklistManager.DeclineTripMergeConfirmation(employeeId, tripId);
        }
    }
}