using BackendApartmentReservation.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BackendApartmentReservation.Authentication
{
    using DataContracts.DataTransferObjects.Requests;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authentication")]
        public async Task<EmployeeAuthenticationInfo> Authenticate([FromBody] AuthenticationRequest request)
        {
            return await _authenticationManager.Authenticate(request);
        }
    }
}