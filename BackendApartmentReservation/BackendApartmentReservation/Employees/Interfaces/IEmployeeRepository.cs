namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Requests;

    public interface IEmployeeRepository
    {
        Task<IEnumerable<DbEmployee>> GetAllEmployees();

        Task<DbEmployee> GetEmployeeById(int employeeID);

        Task<DbEmployee> GetEmployeeByEmployeeId(string employeeID);

        Task CreateEmployee(DbEmployee dbEmployee);

        Task UpdateEmployee(DbEmployee dbEmployee);

        Task DeleteEmployee(DbEmployee dbEmployee);

        Task<IEnumerable<DbEmployeePlan>> GetEmployeePlans(IEnumerable<string> employeeIds);

        Task<DbEmployeePlan> CreateEmployeePlan(DbEmployeePlan dbEmployeePlan);

        Task ChangeUserInfo(string employeeId, ChangeUserInfoRequest changeUserInfoRequest);
    }
}