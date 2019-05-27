namespace BackendApartmentReservation.Apartments
{
    using BackendApartmentReservation.Authentication.AuthorizationRequirements.AdminOnly;
    using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
    using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
    using BackendApartmentReservation.Infrastructure.Authorization;
    using BackendApartmentReservation.LivingPlace.Interfaces;
    using System;
    using BackendApartmentReservation.Database.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api")]
    [ApiController]
    public class ApartmentsController : AuthorizedController
    {
        private readonly ILivingPlaceManager _livingPlaceManager;

        public ApartmentsController(ILivingPlaceManager livingPlaceManager)
        {
            _livingPlaceManager = livingPlaceManager;
        }

        [HttpGet]
        [Route("apartments")]
        public async Task<IEnumerable<DbApartment>> GetAllApartments()
        {
            return await _livingPlaceManager.GetAllApartments();
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