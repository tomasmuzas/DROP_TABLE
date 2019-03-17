using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        [HttpGet]
        [Route("apartments")]
        
        public async Task<IEnumerable<string>> Get()
        {
            return await Task.FromResult(new[] { "flat1", "flat2" });
        }
    }
}
