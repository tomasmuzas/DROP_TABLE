namespace BackendApartmentReservation.Trips
{
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Requests;
    using Infrastructure.Authorization;
    using Interfaces;
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize("EmployeeOnly")]
        public async Task<IActionResult> CreateBasicTrip([FromBody] CreateTripRequest tripRequest)
        {
            var employeeId = GetEmployeeId();
            return Ok(await _tripManager.CreateBasicTrip(tripRequest, employeeId));
        }

        [HttpGet]
        [Route("mergeCheck")]
        [Authorize("EmployeeOnly")]
        public async Task<IActionResult> CheckIfMergeTripPossible([FromBody] string firstTripId,
            [FromBody] string secondTripId)
        {
            return Ok(await _tripManager.IsPossibleToMergeTrips(firstTripId, secondTripId));
        }

        [HttpGet]
        [Route("mergeableTrips")]
        [Authorize("EmployeeOnly")]
        public async Task<IActionResult> ReturnAllMergeableTrips([FromBody] string firstTripId)
        {
            return Ok(await _tripManager.GetAllMergeableTrips(firstTripId));
        }
    }
}