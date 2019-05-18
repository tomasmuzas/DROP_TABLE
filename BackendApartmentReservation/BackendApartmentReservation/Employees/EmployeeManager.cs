namespace BackendApartmentReservation.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Responses;
    using Interfaces;

    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<string> CreateEmployee(DbEmployee dbEmployee)
        {
            var employeeId = Guid.NewGuid().ToString();
            dbEmployee.ExternalEmployeeId = employeeId;

            await _employeeRepository.CreateEmployee(dbEmployee);
            return employeeId;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return employees.Select(e => new EmployeeInfo
            {
                Id = e.ExternalEmployeeId,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName
            });
        }

        public async Task<EmployeeInfo> GetEmployeeByEmployeeId(string employeeID)
        {
            var employee = await _employeeRepository.GetEmployeeByEmployeeId(employeeID);
            return new EmployeeInfo
            {
                Id = employee.ExternalEmployeeId,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }

        public async Task<IEnumerable<EmployeePlanInfo>> GetEmployeePlans(IEnumerable<string> employeeIds)
        {
            var employeePlans = await _employeeRepository.GetEmployeePlans(employeeIds);
            return employeePlans.Select(p => new EmployeePlanInfo
            {
                Start = p.StartDate,
                End = p.EndDate
            });
        }
    }
}