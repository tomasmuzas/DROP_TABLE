using System.Threading.Tasks;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
using BackendApartmentReservation.Managers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeesController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("employees")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var employeeId = await _employeeManager.CreateEmployee(
                             model.FirstName,
                             model.LastName,
                             model.Email,
                             model.Office);

            var response = new RegisterResponse
            {
               Id = employeeId
            };
            return Ok(response);
        }
    }
}
