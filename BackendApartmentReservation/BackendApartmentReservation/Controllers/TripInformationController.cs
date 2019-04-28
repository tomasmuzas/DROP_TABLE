namespace BackendApartmentReservation.Controllers
{
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Responses;
    using Managers;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/tripinfo")]
    public class TripInformationController : ControllerBase
    {
        private readonly ITripInformationManager _tripInformationManager;

        public TripInformationController(ITripInformationManager tripInformationManager)
        {
            _tripInformationManager = tripInformationManager;
        }

        [HttpGet]
        [Route("{tripId}/basic")]
        public async Task<BasicTripInformationResponse> GetBasicTripInformation(string tripId)
        {
            return await _tripInformationManager.GetBasicTripInformation(tripId);
        }
    }
}
