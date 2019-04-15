using System;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using DataContracts.DataTransferObjects.Responses;

    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<string> CreateEmployee(DbEmployee dbEmployee)
        {
            var employeeId = Guid.NewGuid().ToString() ;
            dbEmployee.ExternalEmployeeId = employeeId;

            await _employeeRepository.CreateEmployee(dbEmployee);
            return employeeId;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var employees = await  _employeeRepository.GetAllEmployees();
            return employees.Select(e => new EmployeeInfo
            {
                Id = e.ExternalEmployeeId,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName
            });
        }

        public async Task<EmployeeInfo> GetEmployeeById(string employeeID)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeID);
            return new EmployeeInfo
            {
                Id = employee.ExternalEmployeeId,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }
    }
}
