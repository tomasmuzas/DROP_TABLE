using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    using DataContracts.DataTransferObjects.Requests;
    using Managers;

    [Route("api/trips")]
    [ApiController]
    public class PersonalTripInformationController : ControllerBase
    {
        private readonly IChecklistManager _checklistManager;

        public PersonalTripInformationController(IChecklistManager checklistManager)
        {
            _checklistManager = checklistManager;
        }

        [HttpGet]
        [Route("{tripId}/employees/{employeeId}/flight")]
        public async Task<FlightReservationInfo> GetFlightInfo(string tripId, string employeeId)
        {
            return await _checklistManager.GetFlightInfo(employeeId, tripId);
        }

        [HttpPost]
        [Route("{tripId}/employees/{employeeId}/flight")]
        public async Task AddFlight(string tripId, string employeeId)
        {
            await _checklistManager.AddFlightForEmployee(employeeId, tripId);
        }

        [HttpPut]
        [Route("{tripId}/employees/{employeeId}/flight")]
        public async Task UpdateFlight(
            string tripId,
            string employeeId,
            [FromBody] FlightReservationRequest info)
        {
            await _checklistManager.UpdateFlightForEmployee(employeeId, tripId, info);
        }

        [HttpDelete]
        [Route("{tripId}/employees/{employeeId}/flight")]
        public async Task DeleteFlight(string tripId, string employeeId)
        {
            await _checklistManager.DeleteFlight(employeeId, tripId);
        }
    }
}
