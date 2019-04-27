using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        [HttpGet]
        [Route("travels")]
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
