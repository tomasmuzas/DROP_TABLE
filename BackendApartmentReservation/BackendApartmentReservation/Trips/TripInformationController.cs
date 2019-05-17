using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Trips
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Responses;
    using Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/tripinfo")]
    public class TripInformationController : AuthorizedController
    {
        private readonly ITripInformationManager _tripInformationManager;

        public TripInformationController(ITripInformationManager tripInformationManager)
        {
            _tripInformationManager = tripInformationManager;
        }

        [HttpGet]
        [Route("{tripId}/basic")]
        public async Task<BasicTripInformationResponse> GetBasicTripInformation(string tripId)
        {
            return await _tripInformationManager.GetBasicTripInformation(tripId);
        }

        [HttpGet]
        [Authorize("EmployeeOnly")]
        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllOrganizedTripsInformation()
        {
            var employeeId = GetEmployeeId();
            return await _tripInformationManager.GetAllTripsInformation(employeeId);
        }
    }
}