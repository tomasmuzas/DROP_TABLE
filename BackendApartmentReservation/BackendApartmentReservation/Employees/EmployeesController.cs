using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Authentication.Interfaces;
    using BackendApartmentReservation.DataContracts.DataTransferObjects.IntermediaryDTOs;
    using BackendApartmentReservation.Offices.Interfaces;
    using Database.Entities;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Infrastructure.Exceptions;
    using Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    [ApiController]
    public class EmployeesController : AuthorizedController
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IOfficeManager _officeManager;
        private readonly IAuthenticationManager _authenticationManager;

        public EmployeesController(
            IEmployeeManager employeeManager,
            IOfficeManager officeManager,
            IAuthenticationManager authenticationManager)
        {
            _employeeManager = employeeManager;
            _officeManager = officeManager;
            _authenticationManager = authenticationManager;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
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
            dbEmployee.Office = await _officeManager.GetOfficeById(model.Office);

            await _employeeManager.CreateEmployee(dbEmployee);

            await _authenticationManager.CreateAuthenticationInfo(dbEmployee, model.Password);

            var authenticationInfo = await _authenticationManager.Authenticate(new AuthenticationRequest
            {
                Email = model.Email,
                Password = model.Password
            });

            return Ok(authenticationInfo);
        }

        [HttpGet]
        [Route("employees/{userId}")]
        public async Task<IActionResult> GetEmployeeById(string employeeID)
        {
            var employee = await _employeeManager.GetEmployeeByEmployeeId(employeeID);

            if (employee == null)
            {
                return BadRequest(ErrorCodes.EmployeeNotFound);
            }

            return Ok(employee);
        }

        [HttpPost]
        [Route("employees/plans")]
        public async Task<IEnumerable<EmployeePlanInfo>> GetEmployeePlans(IEnumerable<string> employeeIds)
        {
            return await _employeeManager.GetEmployeePlans(employeeIds);
        }
    }
}