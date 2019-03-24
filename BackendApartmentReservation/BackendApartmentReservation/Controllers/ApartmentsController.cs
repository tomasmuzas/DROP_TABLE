using System.Collections.Generic;
using System.Threading.Tasks;

using BackendApartmentReservation.Infrastructure.Logging;

using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private ICustomLogger<ApartmentsController> _logger;

        public ApartmentsController(ICustomLogger<ApartmentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("apartments")]
        
        public async Task<IEnumerable<string>> Get()
        {
            _logger.Info("Initial step");
            return await Task.FromResult(new[] { "flat1", "flat2" });
        }
    }
}
