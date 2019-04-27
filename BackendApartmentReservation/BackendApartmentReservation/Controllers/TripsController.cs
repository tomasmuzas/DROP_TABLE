using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BackendApartmentReservation.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class TripsController : ControllerBase
    {
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
    }
}
