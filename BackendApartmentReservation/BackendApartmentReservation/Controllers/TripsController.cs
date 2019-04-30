using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    using Managers;

    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IChecklistManager _checklistManager;

        public TripsController(IChecklistManager checklistManager)
        {
            _checklistManager = checklistManager;
        }

        [HttpPost]
        [Route("{tripId}/employees/{employeeId}/flight")]
        public async Task AddFlightToTrip(string tripId, string employeeId)
        {
            await _checklistManager.AddFlightForEmployee(employeeId, tripId);
        }
    }
}
