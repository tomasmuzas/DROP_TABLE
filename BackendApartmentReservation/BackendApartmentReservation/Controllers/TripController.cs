using System.Threading.Tasks;

namespace BackendApartmentReservation.Controllers
{
    using DataContracts.DataTransferObjects.Requests;
    using Managers;
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    public class TripController : ControllerBase
    {
        private readonly ITripManager _tripManager;

        public TripController(ITripManager tripManager)
        {
            _tripManager = tripManager;
        }

        [HttpPost]
        [Route("trips")]
        public async Task<IActionResult> CreateBasicTrip([FromBody] CreateTripRequest tripRequest)
        {
            return Ok(await _tripManager.CreateBasicTrip(tripRequest));
        }
    }
}
