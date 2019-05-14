using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Apartments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api")]
    [ApiController]
    public class ApartmentsController : AuthorizedController
    {
        private readonly ILogger<ApartmentsController> _logger;

        public ApartmentsController(ILogger<ApartmentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("apartments")]
        public async Task<IEnumerable<string>> Get()
        {
            _logger.LogInformation("Initial step");
            return await Task.FromResult(new[] { "flat1", "flat2" });
        }
    }
}