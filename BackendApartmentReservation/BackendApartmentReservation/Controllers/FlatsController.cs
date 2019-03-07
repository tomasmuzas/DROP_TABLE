using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        [HttpGet]
        [Route("/flats")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "flat1", "flat2" };
        }
    }
}
