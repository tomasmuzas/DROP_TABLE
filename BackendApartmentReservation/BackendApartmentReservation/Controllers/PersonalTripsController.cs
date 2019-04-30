using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    using Managers;

    [Route("api/trips")]
    [ApiController]
    public class PersonalTripsController : ControllerBase
    {
        private readonly IChecklistManager _checklistManager;

        public PersonalTripsController(IChecklistManager checklistManager)
        {
            _checklistManager = checklistManager;
        }

        [HttpPost]
        [Route("{tripId}/employees/{employeeId}/flight")]
        public async Task AddFlight(string tripId, string employeeId)
        {
            await _checklistManager.AddFlightForEmployee(employeeId, tripId);
        }

        [HttpPut]
        [Route("{tripId}/employees/{employeeId}/flight")]
        public async Task UpdateFlight(string tripId, string employeeId, [FromBody] FlightReservationInfo info)
        {
            await _checklistManager.UpdateFlightForEmployee(employeeId, tripId, info);
        }
    }
}
