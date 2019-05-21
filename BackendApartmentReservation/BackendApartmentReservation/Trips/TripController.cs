using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;

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
        [OrganizerOnly]
        public async Task<IActionResult> CreateBasicTrip([FromBody] CreateTripRequest tripRequest)
        {
            var employeeId = GetEmployeeId();
            return Ok(await _tripManager.CreateBasicTrip(tripRequest, employeeId));
        }

        [HttpGet]
        [Route("mergeCheck")]
        [OrganizerOnly]
        public async Task<IActionResult> CheckIfMergeTripPossible([FromQuery] string firstId,
            [FromQuery] string secondId)
        {
            return Ok(await _tripManager.IsPossibleToMergeTrips(firstId, secondId));
        }

        [HttpGet]
        [Route("mergeableTrips")]
        [OrganizerOnly]
        public async Task<IActionResult> ReturnAllMergeableTrips([FromQuery] string firstTripId)
        {
            return Ok(await _tripManager.GetAllMergeableTrips(firstTripId));
        }

        [HttpPost]
        [Route("mergeTrips")]
        [OrganizerOnly]
        public async Task<IActionResult> MergeTrips([FromBody] MergeTripsRequest mergeTripsRequest)
        {
            var employeeId = GetEmployeeId();
            return Ok(await _tripManager.MergeTrips(mergeTripsRequest, employeeId));
        }
    }
}