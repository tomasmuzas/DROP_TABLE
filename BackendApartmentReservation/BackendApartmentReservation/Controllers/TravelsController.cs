using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApartmentReservation.Entities;
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
        public async Task<IEnumerable<Travel>> Get()
        {
            var travelMock = new Travel()
            {
                Id = 1,
                Cost = 100,
                DepartureDate = DateTime.UtcNow.AddDays(2),
                ReturnDate = DateTime.UtcNow.AddDays(5),
                DestinationOffice = "Office1",
                Apartments = TravelOptionStatus.NotRequired,
                CarRental = TravelOptionStatus.NotRequired,
                TripTickets = TravelOptionStatus.NotRequired
            };

            return await Task.FromResult(new List<Travel> { travelMock });
        }
    }
}
