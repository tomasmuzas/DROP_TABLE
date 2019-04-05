using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
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
        [Route("profiles")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            DbEmployee dbEmployee = new DbEmployee();
            dbEmployee.FirstName = model.FirstName;
            dbEmployee.LastName = model.LastName;
            dbEmployee.Email = model.Email;
            dbEmployee.Office = model.Office;

            var employeeId = await _employeeManager.CreateEmployee(dbEmployee);

            var response = new RegisterResponse
            {
               Id = employeeId
            };
            return Ok(response);
        }
    }
}
