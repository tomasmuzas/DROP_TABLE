using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        [HttpGet]
        [Route("profiles")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "userProfile1", "userProfile2" };
        }
    }
}
