using System;
using BackendApartmentReservation.Apartments.Interfaces;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.AdminOnly;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Infrastructure.Authorization;
using BackendApartmentReservation.LivingPlace.Interfaces;

namespace BackendApartmentReservation.Apartments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api")]
    [ApiController]
    public class ApartmentsController : AuthorizedController
    {
        private readonly ILogger<ApartmentsController> _logger;
        private readonly ILivingPlaceManager _livingPlaceManager;
        public ApartmentsController(ILogger<ApartmentsController> logger,
            ILivingPlaceManager livingPlaceManager)
        {
            _logger = logger;
            _livingPlaceManager = livingPlaceManager;
        }

        [HttpGet]
        [Route("apartments")]
        public async Task<IEnumerable<string>> Get()
        {
            _logger.LogInformation("Initial step");
            return await Task.FromResult(new[] { "flat1", "flat2" });
        }

        [HttpPost]
        [Route("apartments")]
        [AdminOnly]
        public async Task CreateApartment([FromBody] CreateApartmentRequest apartmentRequest)
        {
            await _livingPlaceManager.CreateApartment(apartmentRequest);

        }

        [HttpGet]
        [Route("trips/{tripId}/apartment/rooms/available")]
        [OrganizerOnly]
        public async Task<int> GetNumberOfAvailableApartmentsRooms(string tripId, [FromQuery] DateTimeOffset dateFrom,
            [FromQuery] DateTimeOffset dateTo)
        {
            return await _livingPlaceManager.GetNumberOfAvailableApartmentRooms(tripId, dateFrom, dateTo);
        }

    }
}