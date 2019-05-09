namespace BackendApartmentReservation.Trips
{
    using System.Threading.Tasks;
    using Checklists.Interfaces;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/trips/{tripId}/employees/{employeeId}")]
    [ApiController]
    public class PersonalTripInformationController : ControllerBase
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

        [HttpDelete]
        [Route("flight")]
        public async Task DeleteFlight(string tripId, string employeeId)
        {
            await _checklistManager.DeleteFlight(employeeId, tripId);
        }
    }
}