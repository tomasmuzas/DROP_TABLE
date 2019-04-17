namespace BackendApartmentReservation.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        [Route("authentication")]
        public async Task<IEnumerable<string>> Get()
        {
            return await Task.FromResult(new[] { "auth1", "auth2" });
        }
    }
}