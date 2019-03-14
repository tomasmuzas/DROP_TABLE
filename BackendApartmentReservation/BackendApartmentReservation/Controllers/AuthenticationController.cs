using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        [Route("authentication")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "auth1", "auth2" };
        }
    }
}
