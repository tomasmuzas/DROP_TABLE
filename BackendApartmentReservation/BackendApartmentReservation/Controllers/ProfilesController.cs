using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
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
        [Route("profiles")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
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

        [HttpGet]
        [Route("profiles/{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            string userIdInString = userId.ToString();

            try
            {
                var result = await _userManager.FindByIdAsync(userIdInString);
                var response = new GetUserResponse()
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    Office = result.Office
                };
                return Ok(response);
            }
            catch (NullReferenceException)
            {
                return BadRequest($"User with ID={userId} was not found.");
            }                
        }
    }
}
