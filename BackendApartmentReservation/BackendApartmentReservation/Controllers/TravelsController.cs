using BackendApartmentReservation.Entities;
using BackendApartmentReservation.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        [HttpGet]
        [Route("travels")]
        public ActionResult<IEnumerable<Travel>> Get()
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

            return new List<Travel> { travelMock };
        }
    }
}
