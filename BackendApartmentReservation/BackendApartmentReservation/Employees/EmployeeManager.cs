namespace BackendApartmentReservation.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Requests;
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

        public async Task<IEnumerable<FullEmployeeInfo>> GetAllEmployeesWithRoles()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return employees.Select(e => new FullEmployeeInfo()
            {
                Id = e.ExternalEmployeeId,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Role = e.Role,
                Office = e.Office.ExternalOfficeId
            });
        }

        public async Task<EmployeeInfo> GetEmployeeByEmployeeId(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByEmployeeId(employeeId);
            return new EmployeeInfo
            {
                Id = employee.ExternalEmployeeId,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }

        public async Task<FullEmployeeInfo> GetEmployeeWithRoleByEmployeeId(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByEmployeeId(employeeId);
            return new FullEmployeeInfo
            {
                Id = employee.ExternalEmployeeId,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Role = employee.Role,
                Office = employee.Office.ExternalOfficeId,
                Version = Encoding.UTF8.GetString(employee.Version)
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

        public async Task ChangeUserInfo(string employeeId, ChangeUserInfoRequest changeUserInfoRequest)
        {
            await _employeeRepository.ChangeUserInfo(employeeId, changeUserInfoRequest);
        }
    }
}