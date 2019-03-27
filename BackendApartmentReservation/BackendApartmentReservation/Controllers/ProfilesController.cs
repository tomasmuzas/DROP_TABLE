using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
using BackendApartmentReservation.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly UserManager<DbEmployee> _userManager;
        [HttpGet]
        [Route("profiles")]
        public async Task<IEnumerable<string>> Get()
        {
            return await Task.FromResult(new[] { "userProfile1", "userProfile2" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (RegistrationInputValidator.IsInputValid(model)) return BadRequest("Input is not valid");

            var employee = new DbEmployee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Office = model.Office
            };

            var result = await _userManager.CreateAsync(employee, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors.First().Description);

            var responseId = new RegisterResponse {Id = employee.Id};
            return Ok(responseId);

        }
    }
}
