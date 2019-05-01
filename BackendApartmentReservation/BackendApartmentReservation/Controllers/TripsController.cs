using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Managers;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripManager _tripManager;

        public TripsController(ITripManager tripManager)
        {
            _tripManager = tripManager;
        }

        [HttpGet]
        [Route("trips")]
        public async Task<IEnumerable<DbTrip>> Get()
        {
            var travelMock = new DbTrip()
            {
                Id = 1,
                DepartureDate = DateTime.UtcNow.AddDays(2),
                ReturnDate = DateTime.UtcNow.AddDays(5),
                DestinationOffice = new DbOffice()
            };

            return await Task.FromResult(new List<DbTrip> { travelMock });
        }

        [HttpPost]
        [Route("trips")]
        public async Task<IActionResult> CreateBasicTrip([FromBody] CreateTripRequest tripRequest)
        {
            return Ok(await _tripManager.CreateBasicTrip(tripRequest));
        }
    }
}
