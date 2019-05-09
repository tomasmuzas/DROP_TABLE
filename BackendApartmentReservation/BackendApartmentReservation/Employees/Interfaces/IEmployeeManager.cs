namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.Responses;

    public interface IEmployeeManager
    {
        Task<string> CreateEmployee(DbEmployee dbEmployee);

        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();

        Task<EmployeeInfo> GetEmployeeByEmployeeId(string employeeID);
    }
}