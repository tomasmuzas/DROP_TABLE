using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
using BackendApartmentReservation.Managers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Controllers
{
    using System.Collections.Generic;

    [Route("api")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeesController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<IEnumerable<GetUserResponse>> GetAllEmployees()
        {
            return await _employeeManager.GetAllEmployees();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("employees")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            DbEmployee dbEmployee = new DbEmployee();
            dbEmployee.FirstName = model.FirstName;
            dbEmployee.LastName = model.LastName;
            dbEmployee.Email = model.Email;

            var employeeId = await _employeeManager.CreateEmployee(dbEmployee);

            var response = new RegisterResponse
            {
               Id = employeeId
            };
            return Ok(response);
        }
    }
}
