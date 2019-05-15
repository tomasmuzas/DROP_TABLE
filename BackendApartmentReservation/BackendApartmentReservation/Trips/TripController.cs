namespace BackendApartmentReservation.Trips
{
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Requests;
    using Infrastructure.Authorization;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    public class TripController : AuthorizedController
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
            var employeeId = GetEmployeeId();
            return Ok(await _tripManager.CreateBasicTrip(tripRequest, employeeId));
        }
    }
}