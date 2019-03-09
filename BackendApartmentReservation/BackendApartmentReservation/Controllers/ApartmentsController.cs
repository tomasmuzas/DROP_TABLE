using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using NLog;

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
            var logger = new LogFactory().GetCurrentClassLogger();
            logger.Info("Hey");
            return new[] { "flat1", "flat2" };
        }
    }
}
