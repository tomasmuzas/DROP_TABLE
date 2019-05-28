namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Responses;

    public interface IEmployeeManager
    {
        Task<string> CreateEmployee(DbEmployee dbEmployee);

        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();

        Task<IEnumerable<FullEmployeeInfo>> GetAllEmployeesWithRoles();

        Task<EmployeeInfo> GetEmployeeByEmployeeId(string employeeId);

        Task<FullEmployeeInfo> GetEmployeeWithRoleByEmployeeId(string employeeId);

        Task<IEnumerable<EmployeePlanInfo>> GetEmployeePlans(IEnumerable<string> employeeIds);
    }
}