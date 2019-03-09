using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        [HttpGet]
        [Route("apartments")]
        public ActionResult<IEnumerable<string>> Get() 
        {
            return new[] { "flat1", "flat2" };
        }
    }
}
