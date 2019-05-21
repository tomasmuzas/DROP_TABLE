using BackendApartmentReservation.Apartments.Interfaces;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
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

        [HttpGet]
        [Route("availableApartmentsRooms")]
        [OrganizerOnly]
        public async Task<int> GetNumberOfAvailableApartmentsRooms([FromQuery] string dateFrom,
            [FromQuery] string dateTo)
        {
            return await _livingPlaceManager.GetNumberOfAvailableApartmentRooms(dateFrom, dateTo);
        }

    }
}