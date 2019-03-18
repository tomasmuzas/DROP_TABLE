using System.Collections.Generic;
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
        public async Task<IEnumerable<string>> Get()
        {
            return await Task.FromResult(new[] { "userProfile1", "userProfile2" });
        }
    }
}
