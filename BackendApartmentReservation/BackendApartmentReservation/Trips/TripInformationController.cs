using BackendApartmentReservation.Authentication.AuthorizationRequirements;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
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
        [OrganizerOnly]
        public async Task<BasicTripInformationResponse> GetBasicTripInformation(string tripId)
        {
            return await _tripInformationManager.GetBasicTripInformation(tripId);
        }

        [HttpGet]
        [Route("organized")]
        [OrganizerOnly]
        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllOrganizedTripsInformation()
        {
            var employeeId = GetEmployeeId();
            return await _tripInformationManager.GetAllOrganizedTripsInformation(employeeId);
        }

        [HttpGet]
        [Route("participating")]
        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllParticipatingTripsInformation()
        {
            var employeeId = GetEmployeeId();
            return await _tripInformationManager.GetAllParticipatingTripsInformation(employeeId);
        }
    }
}